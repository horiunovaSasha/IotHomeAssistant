using IoTHomeAssistant.Domain.Entities;
using IoTHomeAssistant.Domain.Enums;
using IoTHomeAssistant.Domain.Repositories;
using IoTHomeAssistant.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoTHomeAssistant.Infrastructure.Repositories
{
    public class DeviceTypeEventRepository : Repository<DeviceTypeEvent, int>, IDeviceTypeEventRepository
    {
        public DeviceTypeEventRepository(IoTDbContext dbContext) :base(dbContext)
        { 
        }

        public async Task<List<DeviceTypeEvent>> GetItemsAsync(DeviceTypeEnum deviceType)
        {
            return await _dbSet
                .Where(x => x.Type == deviceType)
                .ToListAsync();
        }
    }
}
