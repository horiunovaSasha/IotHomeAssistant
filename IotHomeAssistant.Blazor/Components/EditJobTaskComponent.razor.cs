using IoTHomeAssistant.Domain.Dto.Pagging;
using IoTHomeAssistant.Domain.Entities;
using IoTHomeAssistant.Domain.Services;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IotHomeAssistant.Blazor.Components
{
    public partial class EditJobTaskComponent
    {
        private bool _visible = false;
        protected JobTask JobTask { get; set; } = new JobTask() { Conditions = new List<JobTaskCondition>()};
        protected List<JobTask> jobTasks;

        [Inject]
        IJobTaskService JobTaskService { get; set; }

        [Inject]
        IDeviceService DeviceService { get; set; }

        public void AddJobTask()
        {
            JobTask = new JobTask()
            {
                Conditions = new List<JobTaskCondition>()
                {
                    new JobTaskCondition()
                }
            };

            InitJobTasks();
            Show();
        }

        public async Task EditJobTaskAsync(int id)
        {
            JobTask = await JobTaskService.GetJobTask(id);
            InitJobTasks();

            Show();
        }

        private void Show()
        {
            _visible = true;
            StateHasChanged();
        }

        private void Hide()
        {
            _visible = false;
            StateHasChanged();
        }

        private void Save()
        {
            StateHasChanged();
            Hide();
        }

        private EventCallback RemoveCondition(JobTaskCondition condition)
        {
            JobTask.Conditions.Remove(condition);
            return new EventCallback();
        }
        private EventCallback AddCondition()
        {
            JobTask.Conditions.Add(new JobTaskCondition());
            return new EventCallback();
        }

        private void InitJobTasks()
        {
            jobTasks = JobTaskService.GetPaggedList(new PageRequest() { PageNumber = 1, PageSize = 10000 })
              .Result
              .Items.Where(x => x.Id != JobTask.Id)
              .ToList();
        }
    }
}
