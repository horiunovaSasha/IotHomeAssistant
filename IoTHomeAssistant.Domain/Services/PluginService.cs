using IoTHomeAssistant.Domain.Dto.Pagging;
using IoTHomeAssistant.Domain.Entities;
using IoTHomeAssistant.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public void AddPlugin(Plugin plugin)
        {
            _pluginRepository.Add(plugin);
            _pluginRepository.Commit();
        }

        public void UpdatePlugin(Plugin plugin)
        {
            _pluginRepository.Update(plugin);
            _pluginRepository.Commit();
        }

        public void RemovePlugin(int id)
        {
            _pluginRepository.Delete(id);
            _pluginRepository.Commit();
        }

        public async Task<Plugin> GetPluginAsync(int id)
        {
            return await _pluginRepository.GetPluginAsync(id);
        }

        public async Task<PageResponse<Plugin>> GetPagginPlugins(PageRequest request)
        {
            return await _pluginRepository.GetPaggedList(request);
        }
    }
}
