using IoTHomeAssistant.Domain.Dto;
using System.Threading.Tasks;

namespace IoTHomeAssistant.Domain.Services
{
    public interface IWidgetService
    {
        Task SaveAsync(WidgetItemDto widgetItem);
    }
}
