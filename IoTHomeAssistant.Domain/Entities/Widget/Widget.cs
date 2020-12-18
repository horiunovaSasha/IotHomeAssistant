using System.Collections.Generic;

namespace IoTHomeAssistant.Domain.Entities
{
    public class Widget : IEntity<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public byte Order { get; set; }

        public virtual Area Area { get; set; }
        public virtual ICollection<WidgetItem> Items { get; set; }
    }
}
