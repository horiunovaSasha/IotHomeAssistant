using IoTHomeAssistant.Domain.Enums;
using System.Collections.Generic;

namespace IoTHomeAssistant.Domain.Entities
{
    public class CommandValueType : IEntity<int>
    {
        public int Id { get; set; }
        public int CommandId { get; set; }
        public CommandValueTypeEnum Type { get; set; }
        public int? Min { get; set; }
        public int? Max { get; set; }

        public virtual Command Command { get; set; }
        public virtual List<CommandValueTypeItem> Items { get; set; }
    }
}
