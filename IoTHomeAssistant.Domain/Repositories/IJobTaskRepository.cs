using IoTHomeAssistant.Domain.Dto.Pagging;
using IoTHomeAssistant.Domain.Entities;
using System.Threading.Tasks;

namespace IoTHomeAssistant.Domain.Repositories
{
    public interface IJobTaskRepository : IRepository<JobTask, int>
    {
        Task<PageResponse<JobTask>> GetPaggedList(PageRequest request);
        Task<JobTask> GetJobTaskAsync(int id);
    }
}
