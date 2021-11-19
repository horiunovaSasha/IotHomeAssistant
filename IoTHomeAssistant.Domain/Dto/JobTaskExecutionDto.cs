using IoTHomeAssistant.Domain.Dto;
using IoTHomeAssistant.Domain.Enums;

namespace IoTHomeAssistant.Domain.Entities
{
    public class JobTaskExecutionDto : IEntity<int>
    {
        public int Id { get; set; }
        public int JobTaskId { get; set; }
        public JobExecTypeEnum Type { get; set; }
        public int? WaitSeconds { get; set; }
        public int? TriggeredTaskId { get; set; }
        public int? DeviceId { get; set; }
        public int? CommandId { get; set; }
        public string Value { get; set; }
        public int Order { get; set; }

        public DeviceCommandDto DeviceCommand { get; set; }

        public JobTask JobTask { get; set; }
        public JobTask TriggeredTask { get; set; }
    }
}
