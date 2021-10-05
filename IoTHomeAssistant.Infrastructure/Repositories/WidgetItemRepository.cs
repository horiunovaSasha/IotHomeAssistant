using IoTHomeAssistant.Domain.Entities;
using IoTHomeAssistant.Domain.Repositories;
using IoTHomeAssistant.Infrastructure.EntityConfigurations;

namespace IoTHomeAssistant.Infrastructure.Repositories
{
    public class WidgetItemRepository : Repository<WidgetItem, int>, IWidgetItemRepository
    {
        public WidgetItemRepository(IoTDbContext dbContext) :base(dbContext)
        { 
        }
    }
}
