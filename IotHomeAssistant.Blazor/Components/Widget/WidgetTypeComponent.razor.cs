using IoTHomeAssistant.Domain.Entities;
using IoTHomeAssistant.Domain.Enums;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace IotHomeAssistant.Blazor.Components.Widget
{
    public partial class WidgetTypeComponent
    {
        [Parameter]
        public WidgetItem Widget { get; set; }
        protected List<WidgetType> widgetTypes { get; set; }
        protected string CssClass { get; set; } = string.Empty;

        public WidgetTypeComponent()
        {
            InitTypes();
        }

        private void OnSelectType(WidgetItemTypeEnum type)
        {
            if (type != WidgetItemTypeEnum.Nothing)
            {
                CssClass = "selected";
                Widget.Type = type;
            } else
            {
                CssClass = string.Empty;
            }
        }

        #region Int Types
        private void InitTypes()
        {
            widgetTypes = new List<WidgetType>()
            {
                new WidgetType() { Value = WidgetItemTypeEnum.Informer, Title = "Інфо", ImageUrl = "https://image.flaticon.com/icons/png/512/1041/1041728.png" },
                new WidgetType() { Value = WidgetItemTypeEnum.Switcher, Title = "Вмикач", ImageUrl = "https://image.flaticon.com/icons/png/512/248/248965.png" },
                new WidgetType() { Value = WidgetItemTypeEnum.Bulb, Title = "Освітлення", ImageUrl = "https://image.flaticon.com/icons/png/512/427/427735.png" },
                new WidgetType() { Value = WidgetItemTypeEnum.AC, Title = "Кондиціонер", ImageUrl = "https://image.flaticon.com/icons/png/512/911/911409.png" },
                new WidgetType() { Value = WidgetItemTypeEnum.Cleaner, Title = "Пилосос", ImageUrl = "https://image.flaticon.com/icons/png/512/1848/1848660.png" },
                new WidgetType() { Value = WidgetItemTypeEnum.Heating, Title = "Опалення", ImageUrl = "https://image.flaticon.com/icons/png/512/741/741236.png" },
                new WidgetType() { Value = WidgetItemTypeEnum.Blinds, Title = "Жалюзі", ImageUrl = "https://image.flaticon.com/icons/png/512/1606/1606312.png" },
                new WidgetType() { Value = WidgetItemTypeEnum.CustomScript, Title = "Задача", ImageUrl = "https://img.icons8.com/officel/64/000000/command-line.png" },
                new WidgetType() { Value = WidgetItemTypeEnum.WeatherForecast, Title = "Прогноз погоди", ImageUrl = "https://img.icons8.com/fluent/64/000000/weather.png" }
            };
        }

        public class WidgetType
        {
            public WidgetItemTypeEnum Value { get; set; }
            public string ImageUrl { get; set; }
            public string Title { get; set; }
        }
        #endregion
    }
}
