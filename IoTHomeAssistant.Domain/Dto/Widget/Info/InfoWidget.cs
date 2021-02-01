using System.Collections.Generic;

namespace IoTHomeAssistant.Domain.Dto
{
    public class InfoWidget
    {
        public int AreaId { get; set; }
        public string Title { get; set; }
        public int Type { get; set; }
        public List<InfoWidgetItem> Items { get; set; }
    }
}
