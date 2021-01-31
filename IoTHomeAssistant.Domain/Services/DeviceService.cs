using IoTHomeAssistant.Domain.Dto;
using IoTHomeAssistant.Domain.Repositories;
using System.Collections.Generic;

namespace IoTHomeAssistant.Domain.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _deviceRepository;

        public DeviceService(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        public List<InfoDevice> GetInfoDevices()
        {
            return _deviceRepository.GetInfoDevices();
        }
    }
}
