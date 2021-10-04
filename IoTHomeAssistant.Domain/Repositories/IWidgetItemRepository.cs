using IoTHomeAssistant.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IoTHomeAssistant.Domain.Repositories
{
    public interface IWidgetItemRepository: IRepository<WidgetItem, int>
    {
        Task<List<WidgetItem>> GetAllWidgetsAsync();
    }
}
