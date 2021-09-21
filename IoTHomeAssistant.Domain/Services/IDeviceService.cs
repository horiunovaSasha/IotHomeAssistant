using IoTHomeAssistant.Domain.Dto;
using IoTHomeAssistant.Domain.Dto.Pagging;
using IoTHomeAssistant.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IoTHomeAssistant.Domain.Services
{
    public interface IDeviceService
    {
        Task<Device> GetDeviceAsync(int id);
        Task AddDeviceAsync(Device device);
        Task UpdateDeviceAsync(Device device);
        Task RemoveDeviceAsync(int id);
        List<InfoDevice> GetInfoDevices();
        void Toggle(int deviceId, bool toggle);
        void LightControl(int deviceId, bool toggle, int brightness, string color);
        Task<PageResponse<DeviceDto>> GetPaggedList(PageRequest request);
    }
}
