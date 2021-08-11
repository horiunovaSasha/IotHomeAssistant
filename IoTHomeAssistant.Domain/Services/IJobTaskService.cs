using IoTHomeAssistant.Domain.Dto.Pagging;
using IoTHomeAssistant.Domain.Entities;
using System.Threading.Tasks;

namespace IoTHomeAssistant.Domain.Services
{
    public interface IJobTaskService
    {
        Task AddJobTask(JobTask plugin);
        Task UpdateJobTask(JobTask plugin);
        Task RemoveJobTask(int id);
        Task<JobTask> GetJobTask(int id);
        Task<PageResponse<JobTask>> GetPaggedList(PageRequest request);
    }
}