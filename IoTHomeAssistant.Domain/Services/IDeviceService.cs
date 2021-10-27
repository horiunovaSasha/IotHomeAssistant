using IoTHomeAssistant.Domain.Dto;
using IoTHomeAssistant.Domain.Dto.Pagging;
using IoTHomeAssistant.Domain.Entities;
using IoTHomeAssistant.Domain.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IoTHomeAssistant.Domain.Services
{
    public interface IDeviceService
    {
        Task<Device> GetDeviceAsync(int id);
        Task SaveDeviceAsync(DeviceEditDto device);
        
        Task RemoveDeviceAsync(int id);
        Task<PageResponse<DeviceDto>> GetPaggedList(PageRequest request);
        Task<List<Device>> GetDevicesAsync(DeviceTypeEnum? deviceType);
        Task<List<DeviceEventDto>> GetDeviceEventsAsync(bool? hasValue);
    }
}
