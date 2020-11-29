using IoTHomeAssistant.Domain.Enums;

namespace IoTHomeAssistant.Domain.Entities
{
    public class Device : IEntity<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int GroupId { get; set; }
        public DeviceTypeEnum Type { get; set; }
        
        public virtual DeviceVendor Vendor { get; set; }
        public virtual Group Group { get; set; }
    }
}