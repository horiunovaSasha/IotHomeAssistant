using IoTHomeAssistant.Domain.Dto;
using Microsoft.AspNetCore.Components;

namespace IotHomeAssistant.Blazor.Data
{
    public class BaseItemComponent : ComponentBase
    {
        [Parameter]
        public WidgetItemDto WidgetItem { get; set; }

        [Parameter]
        public bool IsPreview { get; set; }

        [Parameter]
        public EventCallback OnEditWidget { get; set; }

        [Parameter]
        public EventCallback OnDeleteWidget { get; set; }
    }
}
