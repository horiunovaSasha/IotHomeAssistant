using IoTHomeAssistant.Domain.Enums;

namespace IoTHomeAssistant.Domain.Entities 
{ 
    public class DeviceTypeEvent : IEntity<int>
    {
        public int Id { get; set; }
        public DeviceTypeEnum Type { get; set; }
        public int EventId { get; set; }

        public virtual Event Event { get; set; }
    }
}