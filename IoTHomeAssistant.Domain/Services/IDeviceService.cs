using IoTHomeAssistant.Domain.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IoTHomeAssistant.Domain.Services
{
    public interface IDeviceService
    {
        List<InfoDevice> GetInfoDevices();
        void Toggle(int topicId, bool toggle);
        Task YeelightControl(int deviceId, bool toggle, int brightness, string color);
    }
}
