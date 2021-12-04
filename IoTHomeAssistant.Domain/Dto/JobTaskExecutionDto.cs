using IoTHomeAssistant.Domain.Dto;
using IoTHomeAssistant.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace IoTHomeAssistant.Domain.Entities
{
    public class JobTaskExecutionDto : IEntity<int>
    {
        public int Id { get; set; }
        public int JobTaskId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Це поле обов'язкове!")]
        public JobExecTypeEnum Type { get; set; }
        public int? WaitSeconds { get; set; }
        public int? TriggeredTaskId { get; set; }
        public int? DeviceId { get; set; }
        public int? DeviceCommandId { get; set; }

        [Required(ErrorMessage = "Це поле обов'язкове для заповнення!")]
        public string Value { get; set; }
        public int Order { get; set; }

        public DeviceCommandDto DeviceCommand { get; set; }

        public JobTask JobTask { get; set; }
        public JobTask TriggeredTask { get; set; }

        public static JobTaskExecutionDto Convert(JobTaskExecution entity)
        {
            return new JobTaskExecutionDto()
            {
                Id = entity.Id,
                JobTaskId = entity.JobTaskId,
                Type = entity.Type,
                WaitSeconds = entity.WaitSeconds,
                DeviceId = entity.DeviceId,
                TriggeredTaskId = entity.TriggeredTaskId,
                DeviceCommandId = entity.DeviceCommandId,
                Order = entity.Order,
                Value = entity.Value,
                JobTask = entity.JobTask,
                TriggeredTask = entity.TriggeredTask
            };
        }

        public static JobTaskExecution Convert(JobTaskExecutionDto dto)
        {
            var item = new JobTaskExecution()
            {
                Id = dto.Id,
                JobTaskId = dto.JobTaskId,
                Type = dto.Type,
                WaitSeconds = dto.WaitSeconds,
                DeviceId = dto.DeviceId,
                TriggeredTaskId = dto.TriggeredTaskId,
                DeviceCommandId = dto.DeviceCommandId,
                Order = dto.Order,
                Value = dto.Value,
                JobTask = dto.JobTask,
                TriggeredTask = dto.TriggeredTask
            };

            if (dto.DeviceCommand != null)
            {
                item.DeviceId = dto.DeviceCommand.DeviceId;
            }

            return item;
        }
    }
}
