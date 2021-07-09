using IoTHomeAssistant.Domain.Enums;
using System.Collections.Generic;

namespace IoTHomeAssistant.Domain.Entities
{
    public class Device : IEntity<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string IPAddress { get; set; }
        public DeviceTypeEnum Type { get; set; }
        
        public virtual Area Area { get; set; }        
        public virtual DeviceVendor Vendor { get; set; }
        public virtual ICollection<DeviceGroup> Groups { get; set; }
        public virtual ICollection<DeviceMqttTopic> Topics { get; set; }
    }
}