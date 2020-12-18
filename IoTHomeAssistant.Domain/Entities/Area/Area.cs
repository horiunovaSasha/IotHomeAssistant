using System.Collections;
using System.Collections.Generic;

namespace IoTHomeAssistant.Domain.Entities
{
    public class Area : IEntity<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Device> Devices { get; set; }
        public virtual ICollection<DeviceGroup> DeviceGroups { get; set; }
        public virtual ICollection<Widget> Widgets { get; set; }
    }
}