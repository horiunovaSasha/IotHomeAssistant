using IoTHomeAssistant.Domain.Enums;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace IotHomeAssistant.Blazor.Components.Widget
{
    public partial class EditSwitchComponent
    {
        [Parameter]
        public WidgetItemTypeEnum WidgetItemType { get; set; }
        protected List<WidgetType> widgetTypes { get; set; }
        protected string CssClass;
        
        protected int type;
        protected int deviceId;
        protected string title;
        protected bool isChecked;

        public EditSwitchComponent()
        {
            widgetTypes = new List<WidgetType>()
            {
                new WidgetType() { Value = 1, Title = "Розетка", ImageUrl = "https://image.flaticon.com/icons/png/512/2865/2865478.png" },
                new WidgetType() { Value = 2, Title = "Лампочка", ImageUrl = "https://image.flaticon.com/icons/png/512/427/427735.png" },
                new WidgetType() { Value = 3, Title = "Торшер", ImageUrl = "https://image.flaticon.com/icons/png/512/2922/2922352.png" }
            };
        }

        private void OnSelectType(int type)
        {
            CssClass = "selected";
            this.type = type;
        }


        public class WidgetType
        {
            public int Value { get; set; }
            public string ImageUrl { get; set; }
            public string Title { get; set; }
        }
    }
}
