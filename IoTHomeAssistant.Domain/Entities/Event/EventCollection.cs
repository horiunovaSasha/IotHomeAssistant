using IoTHomeAssistant.Domain.Enums;
using System.Collections.Generic;

namespace IoTHomeAssistant.Domain.Entities
{
    public class EventCollection : IEntity<int>
    {
        public int Id { get; set; }
        public DeviceTypeEnum DeviceType { get; set; }
        public int? PluginId { get; set; }

        public virtual ICollection<Event> Events { get; set; }
        public virtual Plugin Plugin { get; set; }
    }
}
