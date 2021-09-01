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

        public async Task<Plugin> GetPluginAsync(int id)
        {
            return await _dbSet
                .Include(x => x.Configurations)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PageResponse<Plugin>> GetPaggedList(PageRequest request)
        {
            var count = _dbSet.Count();

            if (count > 0)
            {
                int skipRows = (request.PageNumber - 1) * request.PageSize;

                return new PageResponse<Plugin>()
                {
                    Items = await _dbSet
                        .Skip(skipRows)
                        .Take(request.PageSize)
                        .ToListAsync(),
                    PageCount = (int)Math.Ceiling(count / (decimal)request.PageSize),
                    PageNumber = request.PageNumber
                };
            }

            return new PageResponse<Plugin>()
            {
                Items = new List<Plugin>(),
                PageCount = 0,
                PageNumber = 1
            };
        }

        public async Task<List<Plugin>> GetPluginsByTypeAsync(DeviceTypeEnum type)
        {
            return await _dbSet
                .Where(x => x.DeviceType == type)
                .Include(x => x.Configurations)
                .ToListAsync();
        }
    }
}
