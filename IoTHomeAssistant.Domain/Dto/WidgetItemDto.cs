using IoTHomeAssistant.Domain.Entities;
using IoTHomeAssistant.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace IoTHomeAssistant.Domain.Dto
{
    public class WidgetItemDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Це поле обов'язкове для заповнення!")]
        public string Title { get; set; }

        public int DeviceId { get; set; }

        public int EventId { get; set; }
        public EventTypeEnum EventType { get; set; }

        public int IconId { get; set; }        

        public Icon Icon { get; set; }

        public int AreaId { get; set; }

        public WidgetItemTypeEnum Type { get; set; }

        public string SymbolAfter { get; set; }

        public int JobTaskId { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public WidgetItemDto()
        {
        }

        public WidgetItemDto(WidgetItem widgetItem)
        {
            Id = widgetItem.Id;
            Title = widgetItem.Title;            
            Icon = widgetItem.Icon;
            AreaId = widgetItem.Area.Id;
            Type = widgetItem.Type;
            SymbolAfter = widgetItem.SymbolAfter;

            if (widgetItem.DeviceId.HasValue)
            {
                DeviceId = widgetItem.DeviceId.Value;
            }

            if (widgetItem.Event != null)
            {
                EventId = widgetItem.Event.Id;
                EventType = widgetItem.Event.Type;
            }

            if (widgetItem.JobTaskId.HasValue)
            {
                JobTaskId = widgetItem.JobTaskId.Value;
            }

            if (widgetItem.Latitude.HasValue)
            {
                Latitude = widgetItem.Latitude.Value;
            }

            if (widgetItem.Longitude.HasValue)
            {
                Longitude = widgetItem.Longitude.Value;
            }
        }
    }
}
