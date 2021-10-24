using IoTHomeAssistant.Domain.Dto.Pagging;
using IoTHomeAssistant.Domain.Entities;
using IoTHomeAssistant.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using LibGit2Sharp;
using System.IO;
using System.Linq;
using System.Diagnostics;
using IoTHomeAssistant.Domain.Enums;
using IoTHomeAssistant.Domain.Dto;

namespace IoTHomeAssistant.Domain.Services
{
    public class PluginService : IPluginService
    {
        private readonly IPluginRepository _pluginRepository;

        public PluginService(IPluginRepository pluginRepository)
        {
            _pluginRepository = pluginRepository;
        }

        public List<Plugin> GetPlugins()
        {
            return _pluginRepository.GetAllWithDependencies();
        }

        public async Task AddPlugin(Plugin plugin)
        {
            plugin.DockerImageId = BuilDockerImageId(plugin);
            var localPath = GitCloneAndReturnPath(plugin);
            var dockerError = BuildDockerImage(localPath, plugin.DockerImageId);

            if (string.IsNullOrEmpty(dockerError))
            {
                await _pluginRepository.AddAsync(plugin);
                await _pluginRepository.CommitAsync();
            }
            else
            {
                throw new Exception(dockerError);
            }
        }

        public async Task UpdatePlugin(Plugin plugin)
        {
            var dbPlugin = await _pluginRepository.GetPluginAsync(plugin.Id);

            if (dbPlugin != null)
            {
                dbPlugin.Title = plugin.Title;
                dbPlugin.DockerImageSource = plugin.DockerImageSource;
                dbPlugin.DeviceType = plugin.DeviceType;

                foreach (var item in plugin.Configurations)
                {
                    var dbIds = dbPlugin.Configurations.Select(x => x.Id).ToList();
                    var dbItem = dbPlugin.Configurations.FirstOrDefault(x => x.Id == item.Id);

                    if (dbItem != null)
                    {
                        dbItem.Key = item.Key;
                        dbItem.Title = item.Title;
                        dbItem.Description = item.Description;
                        dbItem.Type = item.Type;
                    }
                    else
                    {
                        dbPlugin.Configurations.Add(item);
                    }

                    foreach (var id in dbIds)
                    {
                        if (!plugin.Configurations.Any(x => x.Id == id))
                        {
                            var rmItem = dbPlugin.Configurations.First(x => x.Id == id);
                            dbPlugin.Configurations.Remove(rmItem);
                        }
                    }
                }

                var localPath = GitCloneAndReturnPath(dbPlugin);
                var dockerError = BuildDockerImage(localPath, dbPlugin.DockerImageId);

                if (string.IsNullOrEmpty(dockerError))
                {
                    await _pluginRepository.UpdateAsync(dbPlugin);
                    await _pluginRepository.CommitAsync();
                }
                else
                {
                    throw new Exception(dockerError);
                }
            }
        }

        public async Task RemovePlugin(int id)
        {
            await _pluginRepository.DeleteAsync(id);
            await _pluginRepository.CommitAsync();
        }

        public async Task<Plugin> GetPluginAsync(int id)
        {
            return await _pluginRepository.GetPluginAsync(id);
        }

        public async Task<PageResponse<PluginDto>> GetPagginPlugins(PageRequest request)
        {
            return await _pluginRepository.GetPagedList(request);
        }

        private string BuildDockerImage(string workingDir, string imageId)
        {
            var dockerfile = Path.Combine(workingDir, "Dockerfile");
            var paths = dockerfile.Split("/").ToList();
            paths.Remove(paths.Last());
            workingDir = string.Join("/", paths);

            var process = new Process()
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = "docker",
                    Arguments = $"build -f \"{dockerfile}\" -t {imageId} \"{workingDir}\"",
                    UseShellExecute = false,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                }
            };

            process.Start();
            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                var error = process.StandardError.ReadToEnd();
                if (!string.IsNullOrEmpty(error))
                {
                    return error;
                }

                return process.StandardOutput.ReadToEnd();
            }

            Directory.Delete(workingDir, true);

            return string.Empty;
        }

        private string BuilDockerImageId(Plugin plugin)
         {
            return $"{plugin.DeviceType}-{plugin.Title}-{DateTime.Now.Ticks}"
                .Trim()
                .Replace(" ", string.Empty)
                .ToLower();
        }

        private string GitCloneAndReturnPath(Plugin plugin)
        {
            var remotePaths = plugin.DockerImageSource.Split("@");
            var localPath = Path.Combine(Path.GetTempPath(), plugin.DockerImageId);

            Repository.Clone(remotePaths.First(), localPath);

            if (remotePaths.Length > 1)
            {
                foreach (var path in remotePaths.Last().Split("/"))
                {
                    localPath = Path.Combine(localPath, path);
                }
            }

            return localPath;
        }

        public async Task<List<Plugin>> GetPluginsByTypeAsync(DeviceTypeEnum type)
        {
            return await _pluginRepository.GetPluginsByTypeAsync(type);
        }
    }
}