using IoTHomeAssistant.Domain.Entities;
using IoTHomeAssistant.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace IoTHomeAssistant.Domain.Dto
{
    public class JobTaskConditionDto
    {
        public int Id { get; set; }
        public int JobTaskId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Це поле обов'язкове!")]
        public ConditionTypeEnum Type { get; set; }
        public DateTime DateTime { get; set; }
        public int? TriggeredEventId { get; set; }
        public int? TriggeredTaskId { get; set; }
        public int? SensorId { get; set; }
        public int? Day { get; set; }
        public ConditionOperationEnum? Operation { get; set; }
        public string Value { get; set; }
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
                TriggeredTask = entity.TriggeredTask,
                Day = entity.Day
            };
        }

        public static JobTaskCondition Convert(JobTaskConditionDto dto)
        {
            return new JobTaskCondition()
            {
                Id = dto.Id,
                JobTaskId = dto.JobTaskId,
                Type = dto.Type,
                DateTime = dto.DateTime,
                TriggeredEventId = dto.TriggeredEventId,
                TriggeredTaskId = dto.TriggeredTaskId,
                SensorId = dto.SensorId,
                Operation = dto.Operation,
                Value = dto.Value,
                JobTask = dto.JobTask,
                TriggeredTask = dto.TriggeredTask,
                Day = dto.Day
            };
        }
    }
}
