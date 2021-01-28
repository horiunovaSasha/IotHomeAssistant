using IoTHomeAssistant.Domain.Entities;
using IoTHomeAssistant.Domain.Repositories;
using IoTHomeAssistant.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace IoTHomeAssistant.Infrastructure.Repositories
{
    public class WidgetRepository : Repository<Widget, int>, IWidgetRepository 
    {
        public WidgetRepository(IoTDbContext dbContext) :base(dbContext)
        { 
        }

        public List<Widget> GetFullWidgets() {
            return _dbSet
                .Include(x => x.Area)
                .Include(x => x.Items)
                .Include("Items.Icon")
                .Include("Items.IconColor")
                .Include("Items.DeviceTopic")
                .ToList();
        }
    }
}
