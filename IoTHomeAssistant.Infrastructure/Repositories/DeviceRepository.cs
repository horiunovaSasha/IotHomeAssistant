using IoTHomeAssistant.Domain.Dto;
using IoTHomeAssistant.Domain.Entities;
using IoTHomeAssistant.Domain.Enums;
using IoTHomeAssistant.Domain.Repositories;
using IoTHomeAssistant.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace IoTHomeAssistant.Infrastructure.Repositories
{
    public class DeviceRepository : Repository<Device, int>, IDeviceRepository
    {
        public DeviceRepository(IoTDbContext dbContext) :base(dbContext)
        { 
        }

        public List<InfoDevice> GetInfoDevices() {
            var infoDeviceTypes = new List<DeviceTypeEnum>() {
                 DeviceTypeEnum.DoorWindowSensor,
                 DeviceTypeEnum.MotionDetector,
                 DeviceTypeEnum.TemperatureSensor,
                 DeviceTypeEnum.WeatherStation
            };

            return _dbSet
                .Where(x => infoDeviceTypes.Contains(x.Type))
                .Select(x => new InfoDevice()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Sensors = x.Topics
                        .Select(t => new SensorInfo() { Id = t.Id, Title = t.Title } )
                        .ToList()
                })
                .ToList();
        }

        public Device GetWithTopics(int id)
        {
            return _dbSet
               .Include(x => x.Topics)
               .FirstOrDefault(x => x.Id == id);
        }
    }
}
