using IoTHomeAssistant.Domain.Enums;
using System;

namespace IoTHomeAssistant.Domain.Entities
{
    public class JobTaskCondition : IEntity<int>
    {
        public int Id { get; set; }
        public int JobTaskId { get; set; }
        public ConditionTypeEnum Type { get; set; }
        public DateTime DateTime { get; set; }
        public int? TriggeredEventId { get; set; }
        public int? TriggeredTaskId { get; set; }
        public int? SensorId { get; set; }
        public int? Day { get; set; }
        public ConditionOperationEnum? Operation { get; set; }
        public string Value { get; set; }

        public virtual JobTask JobTask { get; set; }
        public virtual JobTask TriggeredTask { get; set; }
    }
}
