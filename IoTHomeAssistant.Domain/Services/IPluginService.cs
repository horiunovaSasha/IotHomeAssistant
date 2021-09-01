using IoTHomeAssistant.Domain.Dto.Pagging;
using IoTHomeAssistant.Domain.Entities;
using IoTHomeAssistant.Domain.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IoTHomeAssistant.Domain.Services
{
    public interface IPluginService
    {
        List<Plugin> GetPlugins();
        Task AddPlugin(Plugin plugin);
        Task UpdatePlugin(Plugin plugin);
        Task RemovePlugin(int id);
        Task<Plugin> GetPluginAsync(int id);
        Task<List<Plugin>> GetPluginsByTypeAsync(DeviceTypeEnum type);
        Task<PageResponse<Plugin>> GetPagginPlugins(PageRequest request);
    }
}