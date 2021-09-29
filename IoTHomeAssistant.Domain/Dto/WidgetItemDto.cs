using IoTHomeAssistant.Domain.Enums;

namespace IoTHomeAssistant.Domain.Dto
{
    public class WidgetItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public WidgetItemTypeEnum Type { get; set; }
    }
}
