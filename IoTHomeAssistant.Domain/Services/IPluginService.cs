using IoTHomeAssistant.Domain.Dto.Pagging;
using IoTHomeAssistant.Domain.Entities;
using System.Collections.Generic;

namespace IoTHomeAssistant.Domain.Services
{
    public interface IPluginService
    {
        List<Plugin> GetPlugins();
        void AddPlugin(Plugin plugin);
        void UpdatePlugin(Plugin plugin);
        void RemovePlugin(int id);
        Plugin GetPlugin(int id);
        PageResponse<Plugin> GetPagginPlugins(PageRequest request);
    }
}