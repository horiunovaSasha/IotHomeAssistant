using IoTHomeAssistant.Domain.Entities;
using IoTHomeAssistant.Domain.Repositories;
using IoTHomeAssistant.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace IoTHomeAssistant.Infrastructure.Repositories
{
    public class WidgetItemRepository : Repository<WidgetItem, int>, IWidgetItemRepository 
    {
        public WidgetItemRepository(IoTDbContext dbContext) :base(dbContext)
        { 
        }
        
        public WidgetItem GetWithDeviceTopic(int id) {
            return _dbSet
                .Include(x => x.DeviceTopic)
                .Include("DeviceTopic.Device")
                .FirstOrDefault(x=>x.Id == id);
        }
    }
}
