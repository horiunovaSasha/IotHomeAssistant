using IoTHomeAssistant.Domain.Dto;
using Microsoft.AspNetCore.Components;

namespace IotHomeAssistant.Blazor.Components.Widget.Items
{
    public partial class SwitchItemComponent
    {
        [Parameter]
        public WidgetItemDto WidgetItem { get; set; }

        [Parameter]
        public bool IsPreview { get; set; }

        [Parameter]
        public EventCallback OnEditWidget { get; set; }

        [Parameter]
        public EventCallback OnDeleteWidget { get; set; }

        private bool _isChecked = false;
    }
}
