using IoTHomeAssistant.Domain.Entities;
using IoTHomeAssistant.Domain.Repositories;
using IoTHomeAssistant.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace IoTHomeAssistant.Infrastructure.Repositories
{
    public class WidgetItemRepository : Repository<WidgetItem, int>, IWidgetItemRepository
    {
        public WidgetItemRepository(IoTDbContext dbContext) :base(dbContext)
        { 
        }

        public async Task RemoveAreaWidgets(int areaId)
        {
            var widgets = await _dbSet
                .AsNoTracking()
                .Where(x => x.AreaId == areaId)
                .ToListAsync();

            foreach(var widget in widgets)
            {
                await DeleteAsync(widget.Id);
            }

            if (widgets.Any())
            {
                await CommitAsync();
            }
        }
    }
}
