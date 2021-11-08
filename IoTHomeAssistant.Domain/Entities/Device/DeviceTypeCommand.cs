using IoTHomeAssistant.Domain.Enums;

namespace IoTHomeAssistant.Domain.Entities 
{ 
    public class DeviceTypeCommand : IEntity<int>
    {
        public int Id { get; set; }
        public DeviceTypeEnum Type { get; set; }
        public int CommandId { get; set; }

        public virtual Command Command { get; set; }
    }
}