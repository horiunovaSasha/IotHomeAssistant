using IoTHomeAssistant.Domain.Dto.Pagging;
using IoTHomeAssistant.Domain.Entities;

namespace IoTHomeAssistant.Domain.Services
{
    public interface IJobTaskService
    {
        void AddJobTask(JobTask plugin);
        void UpdateJobTask(JobTask plugin);
        void RemoveJobTask(int id);
        JobTask GetJobTask(int id);
        PageResponse<JobTask> GetPaggedList(PageRequest request);
    }
}