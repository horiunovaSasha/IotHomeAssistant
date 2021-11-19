using IoTHomeAssistant.Domain.Entities;
using IoTHomeAssistant.Domain.Enums;
using System;

namespace IoTHomeAssistant.Domain.Dto
{
    public class JobTaskConditionDto
    {
        public int Id { get; set; }
        public int JobTaskId { get; set; }
        public ConditionTypeEnum Type { get; set; }
        public DateTime DateTime { get; set; }
        public int? TriggeredEventId { get; set; }
        public int? TriggeredTaskId { get; set; }
        public int? SensorId { get; set; }
        public ConditionOperationEnum? Operation { get; set; }
        public float? Value { get; set; }
        public DeviceEventDto DeviceEvent { get; set; }

        public virtual JobTask JobTask { get; set; }
        public virtual JobTask TriggeredTask { get; set; }

        public static JobTaskConditionDto Convert(JobTaskCondition entity)
        {
            return new JobTaskConditionDto()
            {
                Id = entity.Id,
                JobTaskId = entity.JobTaskId,
                Type = entity.Type,
                DateTime = entity.DateTime,
                TriggeredEventId = entity.TriggeredEventId,
                TriggeredTaskId = entity.TriggeredTaskId,
                SensorId = entity.SensorId,
                Operation = entity.Operation,
                Value = entity.Value,
                JobTask = entity.JobTask,
                TriggeredTask = entity.TriggeredTask
            };
        }
    }
}
