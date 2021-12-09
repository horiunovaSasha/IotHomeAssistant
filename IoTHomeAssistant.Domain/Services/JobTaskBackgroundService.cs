using IoTHomeAssistant.Domain.Dto;
using IoTHomeAssistant.Domain.Entities;
using IoTHomeAssistant.Domain.Extensions;
using IoTHomeAssistant.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IoTHomeAssistant.Domain.Services
{
    public class JobTaskBackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        private List<JobTask> _jobTasks;
        private List<DeviceEventDto> _deviceEvents;

        public JobTaskBackgroundService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
            UpdateState();

            var onceTimer = new Timer(OnOnceTimeEvent);
            var everyTimer = new Timer(OnEveryTimeEvent);

            onceTimer.Change(0, 59000);
            everyTimer.Change(0, 59000);
        }

        public void OnEvent(int deviceId, string eventType, string value)
        {
            bool isSuccess = true;
            var now = DateTime.Now;
            var acceptedTasks = new List<JobTask>();
            var deviceEvent = _deviceEvents.FirstOrDefault(e => e.EventType.ToString() == eventType && e.DeviceId == deviceId);

            if (deviceEvent != null) { 
                var tasks = _jobTasks.Where(x =>
                    x.Conditions != null &&
                    x.Conditions.Any(c => c.TriggeredEventId == deviceEvent.EventId));

                foreach (var task in tasks)
                {
                    foreach (var item in task.Conditions)
                    {
                        if (isSuccess && item.Type == Enums.ConditionTypeEnum.ConditionIsMet)
                        {
                            if (deviceEvent.ValueType != null)
                            {
                                switch (deviceEvent.ValueType.Type)
                                {
                                    case Enums.EventValueTypeEnum.String:
                                        isSuccess = item.Compare(value);
                                        break;
                                    case Enums.EventValueTypeEnum.Boolean:
                                    case Enums.EventValueTypeEnum.Collection:
                                        isSuccess = item.Compare(deviceEvent, value);
                                        break;
                                    case Enums.EventValueTypeEnum.Number:
                                        isSuccess = item.CompareFloat(value);
                                        break;
                                }
                            } else
                            {
                                //just event happens
                                isSuccess = true;
                            }
                        }

                        if (isSuccess && item.Type == Enums.ConditionTypeEnum.EveryTime)
                        {
                            isSuccess = CompareEveryTime(item, now);
                        }

                        if (isSuccess && item.Type == Enums.ConditionTypeEnum.Once)
                        {
                            isSuccess = item.Compare(now);
                        }
                    }

                    if (isSuccess)
                        acceptedTasks.Add(task);

                    isSuccess = true;
                }

                foreach (var task in acceptedTasks)
                {
                    Execute(task);
                }
            }
        }

        public void UpdateState()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var jobTaskRepository = scope.ServiceProvider.GetRequiredService<IJobTaskRepository>();
                var deviceService = scope.ServiceProvider.GetRequiredService<IDeviceService>();

                _jobTasks = jobTaskRepository.GetPaggedList(new Dto.Pagging.PageRequest() { PageNumber = 1, PageSize = 1000 }).Result.Items;
                _deviceEvents = deviceService.GetDeviceEventsAsync().Result;
            }
        }

        private void OnOnceTimeEvent(object stateInfo)
        {
            var now = DateTime.Now;
            var tasks = _jobTasks.Where(t =>
                t.Conditions != null &&
                t.Conditions.Any(c => c.Type == Enums.ConditionTypeEnum.Once));
            var acceptedTasks = new List<JobTask>();

            foreach (var task in tasks)
            {
                var items = task.Conditions
                    .Where(c => c.Type == Enums.ConditionTypeEnum.Once)
                    .ToList();

                if (items.Count == 1 && items.First().Compare(now))
                    acceptedTasks.Add(task);
            }

            foreach (var task in acceptedTasks)
            {
                Execute(task);
            }
        }

        private void OnEveryTimeEvent(object stateInfo)
        {
            var now = DateTime.Now;
            var tasks = _jobTasks.Where(t => 
                t.Conditions != null &&
                t.Conditions.Any(c => c.Type == Enums.ConditionTypeEnum.EveryTime));

            var acceptedTasks = new List<JobTask>();

            foreach (var task in tasks)
            {
                var items = task.Conditions
                   .Where(c => c.Type == Enums.ConditionTypeEnum.EveryTime)
                   .ToList();

                if (items.Count == 1 && CompareEveryTime(items.First(), now))
                    acceptedTasks.Add(task);
            }
           
            foreach (var task in acceptedTasks)
            {
                Execute(task);
            }
        }

        public void Execute(int jobTaskId)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<IJobTaskRepository>();
                var task = repo.GetJobTaskAsync(jobTaskId).Result;

                if (task != null)
                    Execute(task);
            }
        }

        private void Execute(JobTask task)
        {
            Task.Run(async () =>
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var commandService = scope.ServiceProvider.GetRequiredService<ICommandService>();
                    foreach (var exec in task.Executions)
                    {
                        if (exec.Type == Enums.JobExecTypeEnum.Command && 
                            exec.DeviceId.HasValue && 
                            exec.DeviceCommand != null && 
                            exec.DeviceCommand.Command != null)
                        {
                            commandService.Exec(exec.DeviceId.Value, exec.DeviceCommand.Command.Key, exec.Value);
                        }

                        if (exec.Type == Enums.JobExecTypeEnum.Wait && exec.WaitSeconds.HasValue)
                        {
                            await Task.Delay(exec.WaitSeconds.Value * 1000);
                        }

                        if (exec.Type == Enums.JobExecTypeEnum.TriggerTask && exec.TriggeredTaskId.HasValue)
                        {
                            var triggerTask = _jobTasks.FirstOrDefault(t => t.Id == exec.TriggeredTaskId.Value);
                            if (triggerTask != null)
                            {
                                Execute(triggerTask);
                            }
                        }
                    }
                }
            });
        }

        private bool CompareEveryTime(JobTaskCondition item, DateTime now)
        {
            if (item.Day.HasValue)
                return ((int)now.DayOfWeek) == item.Day.Value &&
                    now.Hour == item.DateTime.Hour &&
                    now.Minute >= item.DateTime.Minute;
            else
                return now.Hour == item.DateTime.Hour &&
                    now.Minute >= item.DateTime.Minute;
        }
    }
}