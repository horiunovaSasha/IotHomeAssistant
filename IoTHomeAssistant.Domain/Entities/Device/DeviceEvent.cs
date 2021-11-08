using System.Collections.Generic;

namespace IoTHomeAssistant.Domain.Entities
{
    public class DeviceEvent : IEntity<int>
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public int EventId { get; set; }

        public virtual Device Device { get; set; }
        public virtual Event Event { get; set; }
    }
}
