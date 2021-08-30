using IoTHomeAssistant.Domain.Dto;
using IoTHomeAssistant.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IoTHomeAssistant.Domain.Services
{
    public interface IDeviceService
    {
        List<InfoDevice> GetInfoDevices();
        void Toggle(int deviceId, bool toggle);
        void LightControl(int deviceId, bool toggle, int brightness, string color);
        List<Device> GetAllDevices();
    }
}
