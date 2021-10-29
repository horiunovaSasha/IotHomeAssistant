using IoTHomeAssistant.Domain.Entities;
using IoTHomeAssistant.Domain.Repositories;
using IoTHomeAssistant.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IoTHomeAssistant.Infrastructure.Repositories
{
    public class AreaRepository : Repository<Area, int>, IAreaRepository
    {
        public AreaRepository(IoTDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Area>> GetAreasAsync()
        {
            return await _dbSet
                .AsNoTracking()
                .Include("Widgets.Icon")
                .Include("Widgets.Event")
                .ToListAsync();
        }
    }
}
