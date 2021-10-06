using IoTHomeAssistant.Domain.Entities;
using System.Threading.Tasks;

namespace IoTHomeAssistant.Domain.Repositories
{
    public interface IWidgetItemRepository: IRepository<WidgetItem, int>
    {
        Task RemoveAreaWidgets(int areaId);
    }
}
