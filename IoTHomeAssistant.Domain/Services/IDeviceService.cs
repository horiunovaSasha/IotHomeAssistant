using IoTHomeAssistant.Domain.Dto;
using System.Collections.Generic;

namespace IoTHomeAssistant.Domain.Services
{
    public interface IDeviceService
    {
        List<InfoDevice> GetInfoDevices();
    }
}
