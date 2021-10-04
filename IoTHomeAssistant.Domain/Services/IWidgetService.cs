using IoTHomeAssistant.Domain.Dto;
using IoTHomeAssistant.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IoTHomeAssistant.Domain.Services
{
    public interface IWidgetService
    {
        Task<List<WidgetItem>> GetAllWidgetsAsync();
    }
}
