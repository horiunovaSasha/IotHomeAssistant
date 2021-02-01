using IoTHomeAssistant.Domain.Dto;
using IoTHomeAssistant.Domain.Entities;
using System.Collections.Generic;

namespace IoTHomeAssistant.Domain.Services
{
    public interface IWidgetService
    {
        List<Widget> GetAllWidgets();
        EmptyResponse SaveInfoWidget(InfoWidget widget);
    }
}
