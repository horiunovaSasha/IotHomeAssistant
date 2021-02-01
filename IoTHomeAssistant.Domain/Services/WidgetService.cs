using IoTHomeAssistant.Domain.Dto;
using IoTHomeAssistant.Domain.Entities;
using IoTHomeAssistant.Domain.Repositories;
using System.Collections.Generic;

namespace IoTHomeAssistant.Domain.Services
{
    public class WidgetService : IWidgetService
    {
        private readonly IWidgetRepository _widgetRepository;

        public WidgetService(IWidgetRepository widgetRepository)
        {
            _widgetRepository = widgetRepository;
        }

        public List<Widget> GetAllWidgets()
        {
            return _widgetRepository.GetFullWidgets();
        }

        public EmptyResponse SaveInfoWidget(InfoWidget widget)
        {
            throw new System.NotImplementedException();
        }
    }
}
