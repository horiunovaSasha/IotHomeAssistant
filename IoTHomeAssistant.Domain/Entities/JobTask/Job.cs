using System;

namespace IoTHomeAssistant.Domain.Entities
{
    public class Job : IEntity<long>
    {
        public long Id { get; set; }
        public int JobTaskId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? FinishTime { get; set; }
        public bool Succeed { get; set; }
        public string ErrorMessage { get; set; }

        public virtual JobTask JobTask { get; set; }
    }
}
