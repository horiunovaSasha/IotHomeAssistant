using System.Collections.Generic;

namespace IoTHomeAssistant.Domain.Entities
{
    public class DeviceCommand : IEntity<int>
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public int CommandId { get; set; }

        public virtual Device Device { get; set; }
        public virtual Command Command { get; set; }
    }
}
