using IoTHomeAssistant.Domain.Entities;
using IoTHomeAssistant.Domain.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IoTHomeAssistant.Domain.Repositories
{
    public interface IDeviceTypeEventRepository : IRepository<DeviceTypeEvent, int>
    {
        Task<List<DeviceTypeEvent>> GetItemsAsync(DeviceTypeEnum deviceType);
    }
}
