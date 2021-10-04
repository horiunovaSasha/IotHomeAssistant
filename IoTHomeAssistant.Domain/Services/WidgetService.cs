using IoTHomeAssistant.Domain.Entities;
using IoTHomeAssistant.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IoTHomeAssistant.Domain.Services
{
    public class WidgetService : IWidgetService
    {
        private readonly IWidgetItemRepository _widgetRepository;

        public WidgetService(IWidgetItemRepository widgetRepository)
        {
            _widgetRepository = widgetRepository;
        }

        public async Task<List<WidgetItem>> GetAllWidgetsAsync()
        {
            return await _widgetRepository.GetAllWidgetsAsync();
        }
    }
}
