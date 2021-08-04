using IoTHomeAssistant.Domain.Dto.Pagging;
using IoTHomeAssistant.Domain.Entities;

namespace IoTHomeAssistant.Domain.Repositories
{
    public interface IJobTaskRepository : IRepository<JobTask, int>
    {
        PageResponse<JobTask> GetPaggedList(PageRequest request);
    }
}
