using IoTHomeAssistant.Domain.Entities;
using IoTHomeAssistant.Domain.Repositories;
using IoTHomeAssistant.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoTHomeAssistant.Infrastructure.Repositories
{
    public class IconRepository : Repository<Icon, int>, IIconRepository
    {
        public IconRepository(IoTDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Icon>> GetAllAsync() {
            return await _dbSet
                .ToListAsync();
        }

        public async Task<List<Icon>> GetAllByKeysAsync(List<string> keys)
        {
            return await _dbSet
                .Where(x => keys.Contains(x.Key))
                .ToListAsync();
        }
    }
}
