using IoTHomeAssistant.Domain.Enums;
using Microsoft.AspNetCore.Components;

namespace IotHomeAssistant.Blazor.Components.Widget
{
    public partial class EditInfoComponent
    {
        [Parameter]
        public WidgetItemTypeEnum WidgetItemType { get; set; }

    }
}
