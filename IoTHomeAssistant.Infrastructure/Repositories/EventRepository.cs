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
using IoTHomeAssistant.Domain.Dto;

namespace IoTHomeAssistant.Infrastructure.Repositories
{
    public class EventRepository : Repository<Event, int>, IEventRepository
    {
        public EventRepository(IoTDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Event>> GetEventsAsync(bool? hasValue)
        {
            return await _dbSet
                .Where(x => !hasValue.HasValue || x.HasValue == hasValue.Value)
                .ToListAsync();
        }
    }
}
