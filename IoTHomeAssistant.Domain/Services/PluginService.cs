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
using Microsoft.AspNetCore.SignalR.Client;
using IoTHomeAssistant.Domain.Options;

namespace IoTHomeAssistant.Domain.Services
{
    public class PluginService : IPluginService
    {
        private readonly IPluginRepository _pluginRepository;
        private readonly HostOptions _hostOptions;

        public PluginService(IPluginRepository pluginRepository, HostOptions hostOptions)
        {
            _pluginRepository = pluginRepository;
            _hostOptions = hostOptions;
        }

        public List<Plugin> GetPlugins()
        {
            return _pluginRepository.GetAllWithDependencies();
        }

        public async Task AddPlugin(Plugin plugin)
        {
            plugin.DockerImageId = BuilDockerImageId(plugin);

            Task.Run(async() =>
            {
                try
                {
                    var localPath = GitCloneAndReturnPath(plugin);
                    BuildDockerImageAsync(localPath, plugin.DockerImageId, plugin.Title);
                }
                catch
                {
                    var eventPublisher = new HubConnectionBuilder()
                        .WithUrl(new Uri($"{_hostOptions.Host}/event-publisher"))
                        .Build();

                    await eventPublisher.StartAsync();
                    eventPublisher.SendAsync("PublishNotification", NotificationTypeEnum.Danger, $"Виникла помилка під час створення Docker образу для '{plugin.Title}'!");
                }
            });

            await _pluginRepository.AddAsync(plugin);
            await _pluginRepository.CommitAsync();
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

                Task.Run(async () => {
                    try
                    {
                        var localPath = GitCloneAndReturnPath(dbPlugin);
                        UpdateDockerImage(localPath, dbPlugin.DockerImageId, plugin.Title);
                    } catch
                    {
                        var eventPublisher = new HubConnectionBuilder()
                            .WithUrl(new Uri($"{_hostOptions.Host}/event-publisher"))
                            .Build();

                        await eventPublisher.StartAsync();
                        eventPublisher.SendAsync("PublishNotification", NotificationTypeEnum.Danger, $"Виникла помилка під час створення Docker образу для '{plugin.Title}'!");
                    }
                });
             
                await _pluginRepository.UpdateAsync(dbPlugin);
                await _pluginRepository.CommitAsync();
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

        private async Task BuildDockerImageAsync(string workingDir, string imageId, string pluginName)
        {
            var dockerfile = Path.Combine(workingDir, "Dockerfile");
            workingDir = Path.GetDirectoryName(workingDir);

            var eventPublisher = new HubConnectionBuilder()
                .WithUrl(new Uri($"{_hostOptions.Host}/event-publisher"))
                .Build();

            var cmd = Cmd("docker", $"build -f \"{dockerfile}\" -t {imageId} \"{workingDir}\"");

            if (cmd.ExitCode != 0)
            {
                //var error = cmd.StandardError.ReadToEnd();
                //if (!string.IsNullOrEmpty(error))
                //{
                //}

                await eventPublisher.StartAsync();
                await eventPublisher.SendAsync("PublishNotification", NotificationTypeEnum.Danger, $"Виникла помилка під час створення Docker образу для '{pluginName}'!");
            }
            else
            {
                await eventPublisher.StartAsync();
                await eventPublisher.SendAsync("PublishNotification", NotificationTypeEnum.Success, $"Docker образ для '{pluginName}' успішно створено!");
            }

            while(!workingDir.EndsWith(imageId))
            {
                workingDir = Path.GetDirectoryName(workingDir);
            }

            DeleteDirectory(workingDir);
        }

        private void UpdateDockerImage(string workingDir, string imageId, string pluginName)
        {
            var cmd = Cmd("docker", $"ps -aqf \"ancestor={imageId}\"");

            if (cmd.ExitCode == 0)
            {
                var containerId = cmd.StandardOutput.ReadToEnd();
                Cmd("docker", $"rm -f {containerId}");
            }

            cmd = Cmd("docker", $"images -q {imageId}");

            if (cmd.ExitCode == 0)
            {
                var imgId = cmd.StandardOutput.ReadToEnd();
                Cmd("docker", $"rmi -f {imgId}");
            }

            BuildDockerImageAsync(workingDir, imageId, pluginName);
        }

        private Process Cmd(string filename, string command)
        {
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = filename,
                    Arguments = command,
                    UseShellExecute = false,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                }
            };

            process.Start();
            process.WaitForExit();

            return process;
        }

        private string BuilDockerImageId(Plugin plugin)
         {
            return $"{plugin.DeviceType}-{plugin.Title}-{DateTime.Now.Ticks}"
                .Trim()
                .Replace(" ", string.Empty)
                .Replace("&", string.Empty)
                .ToLower();
        }

        private string GitCloneAndReturnPath(Plugin plugin)
        {
            var remotePaths = plugin.DockerImageSource.Split("@");
            var localPath = Path.Combine(Path.GetTempPath(), plugin.DockerImageId);
            
            if (Directory.Exists(localPath)) {
                DeleteDirectory(localPath);
            }

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

        public static void DeleteDirectory(string target_dir)
        {
            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(target_dir, false);
        }
    }
}