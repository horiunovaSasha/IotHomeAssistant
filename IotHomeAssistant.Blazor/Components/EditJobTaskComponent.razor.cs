using IoTHomeAssistant.Domain.Dto;
using IoTHomeAssistant.Domain.Dto.Pagging;
using IoTHomeAssistant.Domain.Entities;
using IoTHomeAssistant.Domain.Services;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.DropDowns;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IotHomeAssistant.Blazor.Components
{
    public partial class EditJobTaskComponent
    {
        private bool _visible = false;
        protected JobTask JobTask { get; set; } = new JobTask() { 
            Conditions = new List<JobTaskCondition>(),
            Executions = new List<JobTaskExecution>()
        };
        protected List<JobTask> jobTasks;
        protected List<DeviceEventDto> deviceEvents = new List<DeviceEventDto>();
        protected DeviceEventDto deviceEvent;
        protected int eventId = 0;

        protected string deviceEventPlaceHolder = "Подія";

        [Inject]
        IJobTaskService JobTaskService { get; set; }

        [Inject]
        IDeviceService DeviceService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            deviceEvents = await DeviceService.GetDeviceEventsAsync();
        }

        public void AddJobTask()
        {
            JobTask = new JobTask()
            {
                Conditions = new List<JobTaskCondition>()
                {
                    new JobTaskCondition()
                },
                Executions = new List<JobTaskExecution>()
                {
                    new JobTaskExecution()
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

        private EventCallback RemoveExecution(JobTaskExecution execution)
        {
            JobTask.Executions.Remove(execution);
            return new EventCallback();
        }

        private EventCallback AddCondition()
        {
            JobTask.Conditions.Add(new JobTaskCondition());
            return new EventCallback();
        }
        
        private EventCallback AddExecution()
        {
            JobTask.Executions.Add(new JobTaskExecution());
            return new EventCallback();
        }

        private void InitJobTasks()
        {
            jobTasks = JobTaskService.GetPaggedList(new PageRequest() { PageNumber = 1, PageSize = 10000 })
              .Result
              .Items.Where(x => x.Id != JobTask.Id)
              .ToList();
        }

        private async Task OnChangeDeviceEvent() {
            deviceEvent = deviceEvents.FirstOrDefault(x => x.EventId == eventId);
            if (deviceEvent != null)
            {
                deviceEventPlaceHolder = deviceEvent.DeviceName;
                StateHasChanged();
            }
        }
    }
}
