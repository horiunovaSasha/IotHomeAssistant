using IoTHomeAssistant.Domain.Dto.Pagging;
using IoTHomeAssistant.Domain.Entities;
using IoTHomeAssistant.Domain.Repositories;

namespace IoTHomeAssistant.Domain.Services
{
    public class JobTaskService : IJobTaskService
    {
        private readonly IJobTaskRepository _jobTaskRepository;

        public JobTaskService(IJobTaskRepository pluginRepository)
        {
            _jobTaskRepository = pluginRepository;
        }

        public void AddJobTask(JobTask jobTask)
        {
            _jobTaskRepository.Add(jobTask);
            _jobTaskRepository.Commit();
        }

        public void UpdateJobTask(JobTask jobTask)
        {
            _jobTaskRepository.Update(jobTask);
            _jobTaskRepository.Commit();
        }

        public void RemoveJobTask(int id)
        {
            _jobTaskRepository.Delete(id);
            _jobTaskRepository.Commit();
        }

        public JobTask GetJobTask(int id)
        {
            return _jobTaskRepository.Get(id);
        }

        public PageResponse<JobTask> GetPaggedList(PageRequest request)
        {
            return _jobTaskRepository.GetPaggedList(request);
        }
    }
}
