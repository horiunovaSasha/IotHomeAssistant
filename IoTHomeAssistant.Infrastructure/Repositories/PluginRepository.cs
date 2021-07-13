using IoTHomeAssistant.Domain.Entities;
using IoTHomeAssistant.Domain.Repositories;
using IoTHomeAssistant.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace IoTHomeAssistant.Infrastructure.Repositories
{
    public class PluginRepository : Repository<Plugin, int>, IPluginRepository
    {
        public PluginRepository(IoTDbContext dbContext) : base(dbContext)
        {
        }

        public List<Plugin> GetAllWithDependencies()
        {
            return _dbSet
                .Include(x => x.Configurations)
                .ToList();
        }
    }
}
