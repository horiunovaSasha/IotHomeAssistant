using IoTHomeAssistant.Domain.Dto;
using IoTHomeAssistant.Domain.Entities;
using System.Collections.Generic;

namespace IoTHomeAssistant.Domain.Repositories
{
    public interface IDeviceRepository : IRepository<Device, int>
    {
        List<InfoDevice> GetInfoDevices();
    }
}
