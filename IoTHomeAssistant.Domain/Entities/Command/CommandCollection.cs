using IoTHomeAssistant.Domain.Enums;
using System.Collections.Generic;

namespace IoTHomeAssistant.Domain.Entities.Command
{
    public class CommandCollection : IEntity<int>
    {
        public int Id { get; set; }
        public DeviceTypeEnum DeviceType { get; set; }
        public int? PluginId { get; set; }

        public virtual ICollection<Command> Commands { get; set; }
        public virtual Plugin Plugin { get; set; }
    }
}
