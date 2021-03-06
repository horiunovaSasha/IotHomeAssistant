using System.Collections.Generic;

namespace IoTHomeAssistant.Domain.Entities
{
    public class DeviceGroup : IEntity<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual ICollection<Device> Devices { get; set; }
    }
}