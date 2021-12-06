using IoTHomeAssistant.Domain.Services;
using Microsoft.AspNetCore.Components;

namespace IotHomeAssistant.Blazor.Components.Widget.Items
{
    public partial class TaskComponent
    {
        [Inject]
        public JobTaskBackgroundService JobTaskService { get; set; }

        protected void Run() 
        {
            if (WidgetItem.JobTaskId > 0)
            {
                JobTaskService.Execute(WidgetItem.JobTaskId);
            }
        }
    }
}