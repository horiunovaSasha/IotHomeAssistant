using IoTHomeAssistant.Domain.Entities;
using IoTHomeAssistant.Domain.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IoTHomeAssistant.Domain.Repositories
{
    public interface IDeviceTypeCommandRepository : IRepository<DeviceTypeCommand, int>
    {
        Task<List<DeviceTypeCommand>> GetItemsAsync(DeviceTypeEnum deviceType);
    }
}
