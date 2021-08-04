using IoTHomeAssistant.Domain.Dto.Pagging;
using IoTHomeAssistant.Domain.Entities;
using IoTHomeAssistant.Domain.Repositories;
using IoTHomeAssistant.Infrastructure.EntityConfigurations;
using System.Collections.Generic;
using System.Linq;

namespace IoTHomeAssistant.Infrastructure.Repositories
{
    public class JobTaskRepository : Repository<JobTask, int>, IJobTaskRepository
    {
        public JobTaskRepository(IoTDbContext dbContext) : base(dbContext)
        {
        }

        public PageResponse<JobTask> GetPaggedList(PageRequest request)
        {
            var count = _dbSet.Count();

            if (count > 0)
            {
                int skipRows = (request.PageNumber - 1) * request.PageSize;

                return new PageResponse<JobTask>()
                {
                    Items = _dbSet
                        .Skip(skipRows)
                        .Take(request.PageSize)
                        .ToList(),
                    PageCount = count / request.PageSize,
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
    }
}