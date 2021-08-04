using System.Collections.Generic;

namespace IoTHomeAssistant.Domain.Entities
{
    public class JobTask : IEntity<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<JobTaskCondition> Conditions { get; set; }
        public List<JobTaskExecution> Executions { get; set; }
    }
}
