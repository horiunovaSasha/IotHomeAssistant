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

        [Required(ErrorMessage = "Виберіть пристрій зі списку!")]
        public int DeviceId { get; set; }
        
        [Required(ErrorMessage = "Це поле обов'язкове для заповнення!")]
        public int EventId { get; set; }

        public Icon Icon { get; set; }

        public int AreaId { get; set; }

        public WidgetItemTypeEnum Type { get; set; }

        public string SymbolAfter { get; set; }
    }
}
