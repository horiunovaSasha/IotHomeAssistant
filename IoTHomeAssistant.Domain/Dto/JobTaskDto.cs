using IoTHomeAssistant.Domain.Entities;
using System.Linq;

namespace IoTHomeAssistant.Domain.Dto
{
    public class JobTaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Conditions { get; set; }
        public string Executions { get; set; }

        public JobTaskDto(JobTask jobTask)
        {
            Id = jobTask.Id;
            Title = jobTask.Title;
            Conditions = string.Join(", ", jobTask.Conditions?.Select(x => x.Type));
            Executions = string.Join(", ", jobTask.Executions?.Select(x => x.Type));
        }
    }
}
