using IoTHomeAssistant.Domain.Entities;
using IoTHomeAssistant.Domain.Repositories;
using IoTHomeAssistant.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IoTHomeAssistant.Infrastructure.Repositories
{
    public class WidgetItemRepository : Repository<WidgetItem, int>, IWidgetItemRepository
    {
        public WidgetItemRepository(IoTDbContext dbContext) :base(dbContext)
        { 
        }

        public async Task<List<WidgetItem>> GetAllWidgetsAsync()
        {
            return await _dbSet
                .Include(x => x.Icon)
                .Include(x => x.Device)
                .ToListAsync();
        }
    }
}
