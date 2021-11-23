using IoTHomeAssistant.Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IoTHomeAssistant.Domain.Dto
{
    public class JobTaskDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Це поле обов'язкове для заповнення!")]
        public string Title { get; set; }
        public virtual List<JobTaskConditionDto> Conditions { get; set; }
        public virtual List<JobTaskExecutionDto> Executions { get; set; }
    }
}
