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

        [Range(1, int.MaxValue, ErrorMessage = "Виберіть пристрій зі списку!")]
        public int DeviceId { get; set; }

        //[Range(1, int.MaxValue, ErrorMessage = "Виберіть пристрій зі списку!")]
        public int EventId { get; set; }

        //[Range(1, int.MaxValue, ErrorMessage = "Виберіть тип!")]
        public int IconId { get; set; }

        public Icon Icon { get; set; }

        public int AreaId { get; set; }

        public WidgetItemTypeEnum Type { get; set; }

        public string SymbolAfter { get; set; }

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

            if (widgetItem.EventId.HasValue)
            {
                EventId = widgetItem.EventId.Value;
            }
        }
    }
}
