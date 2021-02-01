using IoTHomeAssistant.Domain.Dto.Widget;
using System.Collections.Generic;

namespace IoTHomeAssistant.Domain.Dto
{
    public class InfoWidgetItem
    {
        public int DeviceTopicId { get; set; }
        public string Title { get; set; }
        public int Icon { get; set; }
        public WidgetItemFormat Format { get; set; }
        public List<ColorRangeWidgetItem> Colors { get; set; }
    }
}
