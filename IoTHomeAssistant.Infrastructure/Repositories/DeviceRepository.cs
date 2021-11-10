using IoTHomeAssistant.Domain.Dto;
using IoTHomeAssistant.Domain.Dto.Pagging;
using IoTHomeAssistant.Domain.Entities;
using IoTHomeAssistant.Domain.Enums;
using IoTHomeAssistant.Domain.Repositories;
using IoTHomeAssistant.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoTHomeAssistant.Infrastructure.Repositories
{
    public class DeviceRepository : Repository<Device, int>, IDeviceRepository
    {
        public DeviceRepository(IoTDbContext dbContext) :base(dbContext)
        { 
        }

        public async Task<Device> GetDeviceAsync(int id)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(x => x.PluginDevice.Plugin)
                .Include("PluginDevice.Configurations.PluginConfiguration")
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public List<InfoDevice> GetInfoDevices() {
            var infoDeviceTypes = new List<DeviceTypeEnum>() {
                 DeviceTypeEnum.DoorWindowSensor,
                 DeviceTypeEnum.MotionDetector,
                 DeviceTypeEnum.TemperatureSensor
            };

            return _dbSet
                .Where(x => infoDeviceTypes.Contains(x.Type))
                .Select(x => new InfoDevice()
                {
                    Id = x.Id,
                    Title = x.Title
                })
                .ToList();
        }

        public async Task<List<Device>> GetDevicesAsync(DeviceTypeEnum? deviceType)
        {
            try
            {
                return await _dbSet
                    .AsNoTracking()
                    .Where(x => !deviceType.HasValue || x.Type == deviceType.Value)
                    .Include("DeviceEvents.Event.ValueType.Items")
                    .Include("DeviceCommands.Command.ValueType.Items")
                    .ToListAsync();
            } catch( Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PageResponse<DeviceDto>> GetPaggedList(PageRequest request)
        {
            var count = _dbSet.Count();

            if (count > 0)
            {
                int skipRows = (request.PageNumber - 1) * request.PageSize;

                return new PageResponse<DeviceDto>()
                {
                    Items = (await _dbSet
                        .Include(x => x.PluginDevice.Plugin)
                        .Skip(skipRows)
                        .Take(request.PageSize)
                        .ToListAsync()).Select(x => new DeviceDto(x)).ToList(),
                    PageCount = (int)Math.Ceiling(count / (decimal)request.PageSize),
                    PageNumber = request.PageNumber
                };
            }

            return new PageResponse<DeviceDto>()
            {
                Items = new List<DeviceDto>(),
                PageCount = 0,
                PageNumber = 1
            };
        }

        public Device GetWithTopics(int id)
        {
            return _dbSet
               .FirstOrDefault(x => x.Id == id);
        }
    }
}
