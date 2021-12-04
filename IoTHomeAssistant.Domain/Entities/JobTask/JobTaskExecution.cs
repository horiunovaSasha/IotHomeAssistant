using IoTHomeAssistant.Domain.Enums;
using System;

namespace IoTHomeAssistant.Domain.Entities
{
    public class JobTaskExecution : IEntity<int>
    {
        public int Id { get; set; }
        public int JobTaskId { get; set; }
        public JobExecTypeEnum Type { get; set; }
        public int? WaitSeconds { get; set; }
        public int? TriggeredTaskId { get; set; }
        public int? DeviceId { get; set; }
        public int? DeviceCommandId { get; set; }
        public string Value { get; set; }
        public int Order { get; set; }

        public virtual JobTask JobTask { get; set; }
        public virtual JobTask TriggeredTask { get; set; }
        public virtual DeviceCommand DeviceCommand { get; set; }
    }
}
