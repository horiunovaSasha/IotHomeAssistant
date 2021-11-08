using IoTHomeAssistant.Domain.Enums;
using System.Collections.Generic;

namespace IoTHomeAssistant.Domain.Entities
{
    public class Device : IEntity<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DeviceTypeEnum Type { get; set; }

        public virtual PluginDevice PluginDevice { get; set; }
        public virtual List<DeviceEvent> DeviceEvents { get; set; }
        public virtual List<DeviceCommand> DeviceCommands { get; set; }
    }
}