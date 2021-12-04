using IoTHomeAssistant.Domain.Dto;
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
        protected JobTaskDto JobTask { get; set; } = new JobTaskDto() { 
            Conditions = new List<JobTaskConditionDto>(),
            Executions = new List<JobTaskExecutionDto>()
        };
        protected List<JobTask> jobTasks;
        protected List<DeviceEventDto> deviceEvents = new List<DeviceEventDto>();
        protected List<DeviceCommandDto> deviceCommands = new List<DeviceCommandDto>();

        [Inject]
        IJobTaskService JobTaskService { get; set; }

        [Inject]
        IDeviceService DeviceService { get; set; }

        [Parameter]
        public EventCallback OnSave { get; set; }

        protected override async Task OnInitializedAsync()
        {
            deviceEvents = await DeviceService.GetDeviceEventsAsync();
            deviceCommands = await DeviceService.GetDeviceCommandsAsync();
        }

        public void AddJobTask()
        {
            JobTask = new JobTaskDto()
            {
                Conditions = new List<JobTaskConditionDto>()
                {
                    new JobTaskConditionDto()
                    {
                        DeviceEvent = new DeviceEventDto
                        {
                            DeviceName = "Подія"
                        }
                    }
                },
                Executions = new List<JobTaskExecutionDto>()
                {
                    new JobTaskExecutionDto()
                    {
                        DeviceCommand = new DeviceCommandDto()
                        {
                            DeviceName = "Команда"
                        }
                    }
                }
            };

            InitJobTasks();
            Show();
        }

        public async Task EditJobTaskAsync(int id)
        {
            var task = await JobTaskService.GetJobTask(id);
            JobTask = new JobTaskDto()
            {
                Id = task.Id,
                Title = task.Title,
                Executions = task.Executions
                    .Select(x => JobTaskExecutionDto.Convert(x))
                    .ToList(),
                Conditions = task.Conditions
                    .Select(x => JobTaskConditionDto.Convert(x))
                    .ToList()
            };

            

            foreach(var item in JobTask.Conditions.Where(x => x.TriggeredEventId.HasValue))
            {
                var deviceEvent = deviceEvents.FirstOrDefault(x => x.EventId == item.TriggeredEventId.Value);
                if (deviceEvent != null)
                {
                    item.DeviceEvent = deviceEvent;
                }
            }

            foreach (var item in JobTask.Executions.Where(x => x.DeviceCommandId.HasValue))
            {
                var deviceCommand = deviceCommands.FirstOrDefault(x => x.CommandId == item.DeviceCommandId.Value);
                if (deviceCommand != null)
                {
                    item.DeviceCommand = deviceCommand;
                }
            }

            foreach (var item in JobTask.Conditions.Where(x => !x.TriggeredEventId.HasValue))
            {
                item.DeviceEvent = new DeviceEventDto { DeviceName = "Подія" };
            }

            foreach (var item in JobTask.Executions.Where(x => !x.DeviceCommandId.HasValue))
            {
                item.DeviceCommand = new DeviceCommandDto() { DeviceName = "Команда" };
            }

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
            var jobTask = new JobTask()
            {
                Id = JobTask.Id,
                Title = JobTask.Title,
                Conditions = JobTask.Conditions
                    .Select(x => JobTaskConditionDto.Convert(x)).ToList(),
                Executions = JobTask.Executions
                    .Select(x => JobTaskExecutionDto.Convert(x)).ToList()
            };

            if (jobTask.Id == 0)
            {
                JobTaskService.AddJobTask(jobTask);
            }
            else
            {
                JobTaskService.UpdateJobTask(jobTask);
            }

            OnSave.InvokeAsync();

            StateHasChanged();
            Hide();
        }

        private EventCallback RemoveCondition(JobTaskConditionDto condition)
        {
            JobTask.Conditions.Remove(condition);
            return new EventCallback();
        }

        private EventCallback RemoveExecution(JobTaskExecutionDto execution)
        {
            JobTask.Executions.Remove(execution);
            return new EventCallback();
        }

        private EventCallback AddCondition()
        {
            JobTask.Conditions.Add(new JobTaskConditionDto() {
                DeviceEvent = new DeviceEventDto
                {
                    DeviceName = "Подія"
                }
            });
            return new EventCallback();
        }
        
        private EventCallback AddExecution()
        {
            JobTask.Executions.Add(new JobTaskExecutionDto()
            {
                DeviceCommand = new DeviceCommandDto()
                {
                    DeviceName = "Команда"
                }
            });

            return new EventCallback();
        }

        private void InitJobTasks()
        {
            jobTasks = JobTaskService.GetPaggedList(new PageRequest() { PageNumber = 1, PageSize = 10000 })
              .Result
              .Items.Where(x => x.Id != JobTask.Id)
              .ToList();
        }

        private async Task OnChangeDeviceEvent(JobTaskConditionDto item) {
            if (item.TriggeredEventId.HasValue)
            {
                var deviceEvent = deviceEvents.FirstOrDefault(x => x.EventId == item.TriggeredEventId.Value);
                if (deviceEvent != null)
                {
                    item.DeviceEvent = deviceEvent;
                }
            }
        }

        private async Task OnChangeDeviceCommand(JobTaskExecutionDto item)
        {
            if (item.DeviceCommandId.HasValue)
            {
                var command = deviceCommands.FirstOrDefault(x => x.CommandId == item.DeviceCommandId.Value);
                if (command != null)
                {
                    item.DeviceCommand = command;
                }
            }
        }
    }
}
