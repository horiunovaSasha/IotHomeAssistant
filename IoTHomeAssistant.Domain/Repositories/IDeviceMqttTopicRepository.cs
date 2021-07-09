using IoTHomeAssistant.Domain.Entities;
using System.Collections.Generic;

namespace IoTHomeAssistant.Domain.Repositories
{
    public interface IDeviceMqttTopicRepository : IRepository<DeviceMqttTopic, int>
    {
        List<DeviceMqttTopic> GetAllWithBrokerInfo();
        DeviceMqttTopic GetWitDevice(int id);
    }
}
