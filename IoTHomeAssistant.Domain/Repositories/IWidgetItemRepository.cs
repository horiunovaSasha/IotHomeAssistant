using IoTHomeAssistant.Domain.Entities;
using System.Collections.Generic;

namespace IoTHomeAssistant.Domain.Repositories
{
    public interface IWidgetItemRepository : IRepository<WidgetItem, int>
    {
        WidgetItem GetWithDeviceTopic(int id);
    }
}
