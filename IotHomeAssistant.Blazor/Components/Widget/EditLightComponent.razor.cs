using IoTHomeAssistant.Domain.Enums;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace IotHomeAssistant.Blazor.Components.Widget
{
    public partial class EditLightComponent
    {
        [Parameter]
        public WidgetItemTypeEnum WidgetItemType { get; set; }

        protected bool isChecked;
        protected int value = 65;


    }
}
