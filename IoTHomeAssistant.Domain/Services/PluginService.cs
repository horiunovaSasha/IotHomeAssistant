using IoTHomeAssistant.Domain.Entities;
using IoTHomeAssistant.Domain.Repositories;
using System.Collections.Generic;

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
    }
}
