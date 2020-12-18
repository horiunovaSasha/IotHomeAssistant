using IoTHomeAssistant.Domain.Entities;
using IoTHomeAssistant.Domain.Repositories;
using IoTHomeAssistant.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace IoTHomeAssistant.Infrastructure.Repositories
{
    public class DeviceMqttTopicRepository : Repository<DeviceMqttTopic, int>, IDeviceMqttTopicRepository
    {
        public DeviceMqttTopicRepository(IoTDbContext dbContext) : base(dbContext)
        {
        }

        public List<DeviceMqttTopic> GetAllWithBrokerInfo()
        {
            return _dbSet
                .Include(x => x.MqttBroker)
                .Include(x => x.Device)
                .ToList();
        }
    }
}
