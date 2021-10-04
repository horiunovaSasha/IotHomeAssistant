using IoTHomeAssistant.Domain.Dto;
using IoTHomeAssistant.Domain.Dto.Pagging;
using IoTHomeAssistant.Domain.Entities;
using IoTHomeAssistant.Domain.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IoTHomeAssistant.Domain.Repositories
{
    public interface IDeviceRepository : IRepository<Device, int>
    {
        Task<Device> GetDeviceAsync(int id);
        List<InfoDevice> GetInfoDevices();
        Device GetWithTopics(int id);
        Task<List<Device>> GetDevicesAsync(DeviceTypeEnum? deviceType);
        Task<PageResponse<DeviceDto>> GetPaggedList(PageRequest request);
    }
}
