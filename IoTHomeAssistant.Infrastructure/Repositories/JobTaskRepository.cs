using IoTHomeAssistant.Domain.Dto.Pagging;
using IoTHomeAssistant.Domain.Entities;
using IoTHomeAssistant.Domain.Repositories;
using IoTHomeAssistant.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoTHomeAssistant.Infrastructure.Repositories
{
    public class JobTaskRepository : Repository<JobTask, int>, IJobTaskRepository
    {
        public JobTaskRepository(IoTDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<PageResponse<JobTask>> GetPaggedList(PageRequest request)
        {
            var count = _dbSet.Count();

            if (count > 0)
            {
                int skipRows = (request.PageNumber - 1) * request.PageSize;

                return new PageResponse<JobTask>()
                {
                    Items = await _dbSet
                        .Include("Executions.DeviceCommand.Command")
                        .Include(x => x.Conditions)
                        .Skip(skipRows)
                        .Take(request.PageSize)
                        .ToListAsync(),
                    PageCount = (int)Math.Ceiling(count / (decimal)request.PageSize),
                    PageNumber = request.PageNumber
                };
            }

            return new PageResponse<JobTask>()
            {
                Items = new List<JobTask>(),
                PageCount = 0,
                PageNumber = 1
            };
        }

        public async Task<JobTask> GetJobTaskAsync(int id)
        {
            return await _dbSet
                .Include(x => x.Conditions)
                .Include(x => x.Executions)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<JobTask>> GetJobTasksAsync()
        {
            return await _dbSet.ToListAsync();
        }
    }
}