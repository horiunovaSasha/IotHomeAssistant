using IoTHomeAssistant.Domain.Enums;
using System.Collections.Generic;

namespace IoTHomeAssistant.Domain.Entities
{
    public class EventValueType : IEntity<int>
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public EventValueTypeEnum Type { get; set; }
        public int? Min { get; set; }
        public int? Max { get; set; }

        public virtual Event Event { get; set; }
        public virtual List<EventValueTypeItem> Items { get; set; }
    }
}
