﻿using IoTHomeAssistant.Domain.Dto.Pagging;
using IoTHomeAssistant.Domain.Entities;
using IoTHomeAssistant.Domain.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IoTHomeAssistant.Domain.Services
{
    public class JobTaskService : IJobTaskService
    {
        private readonly IJobTaskRepository _jobTaskRepository;
        private readonly JobTaskBackgroundService _jobTaskBackgroundService;

        public JobTaskService(IJobTaskRepository pluginRepository, JobTaskBackgroundService jobTaskBackgroundService)
        {
            _jobTaskRepository = pluginRepository;
            _jobTaskBackgroundService = jobTaskBackgroundService;
        }

        public async Task AddJobTask(JobTask jobTask)
       {
            try
            {
                int order = 1;

                jobTask.Executions.ForEach(x =>
                {
                    x.Order = order;
                    order++;
                });

                await _jobTaskRepository.AddAsync(jobTask);
                await _jobTaskRepository.CommitAsync();
                _jobTaskBackgroundService.UpdateState();
            } catch(Exception ex)
            {

            }
        }

        public async Task UpdateJobTask(JobTask jobTask)
        {
            var dbTask = await _jobTaskRepository.GetJobTaskAsync(jobTask.Id);

            if (dbTask != null)
            {
                int order = 1;
                dbTask.Title = jobTask.Title;

                foreach (var item in jobTask.Conditions)
                {
                    var dbIds = dbTask.Conditions.Select(x => x.Id).ToList();
                    var dbItem = dbTask.Conditions.FirstOrDefault(x => x.Id == item.Id);

                    if (item.Id != 0)
                    {
                        dbItem.DateTime = item.DateTime;
                        dbItem.Operation = item.Operation;
                        dbItem.SensorId = item.SensorId;
                        dbItem.TriggeredEventId = item.TriggeredEventId;
                        dbItem.TriggeredTaskId = item.TriggeredTaskId;
                        dbItem.Type = item.Type;
                        dbItem.Value = item.Value;
                        dbItem.Day = item.Day;
                    } else
                    {
                        dbTask.Conditions.Add(item);
                    }

                    foreach(var id in dbIds)
                    {
                        if (!jobTask.Conditions.Any(x => x.Id == id))
                        {
                            var rmItem = dbTask.Conditions.First(x => x.Id == id);
                            dbTask.Conditions.Remove(rmItem);
                        }
                    }
                }

                foreach (var item in jobTask.Executions)
                {
                    var dbIds = dbTask.Executions.Select(x => x.Id).ToList();
                    var dbItem = dbTask.Executions.FirstOrDefault(x => x.Id == item.Id);

                    if (item.Id != 0)
                    {
                        dbItem.DeviceCommandId = item.DeviceCommandId;
                        dbItem.DeviceId = item.DeviceId;
                        dbItem.WaitSeconds = item.WaitSeconds;
                        dbItem.TriggeredTaskId = item.TriggeredTaskId;
                        dbItem.Type = item.Type;
                        dbItem.Value = item.Value;
                    }
                    else
                    {
                        dbTask.Executions.Add(item);
                    }

                    foreach (var id in dbIds)
                    {
                        if (!jobTask.Executions.Any(x => x.Id == id))
                        {
                            var rmItem = dbTask.Executions.First(x => x.Id == id);
                            dbTask.Executions.Remove(rmItem);
                        }
                    }
                }

                dbTask.Executions.ForEach(x => {
                    x.Order = order;
                    order++;
                });

                await _jobTaskRepository.UpdateAsync(dbTask);
                await _jobTaskRepository.CommitAsync();
                _jobTaskBackgroundService.UpdateState();
            }
        }

        public async Task RemoveJobTask(int id)
        {
            await _jobTaskRepository.DeleteAsync(id);
            await _jobTaskRepository.CommitAsync();
        }

        public async Task<JobTask> GetJobTask(int id)
        {
            return await _jobTaskRepository.GetJobTaskAsync(id);
        }

        public async Task<PageResponse<JobTask>> GetPaggedList(PageRequest request)
        {
            return await _jobTaskRepository.GetPaggedList(request);
        }
    }
}
