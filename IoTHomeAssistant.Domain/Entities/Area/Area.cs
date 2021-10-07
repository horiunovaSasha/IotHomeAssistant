using System.Collections.Generic;

namespace IoTHomeAssistant.Domain.Entities
{
    public class Area : IEntity<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<WidgetItem> Widgets { get; set; }
    }
}