using IoTHomeAssistant.Domain.Entities.Command;
using IoTHomeAssistant.Domain.Enums;

namespace IoTHomeAssistant.Domain.Entities
{
    public class Device : IEntity<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DeviceTypeEnum Type { get; set; }

        public virtual PluginDevice PluginDevice { get; set; }
        public virtual CommandCollection CommandCollection { get; set; }
        public virtual EventCollection EventCollection { get; set; }
    }
}