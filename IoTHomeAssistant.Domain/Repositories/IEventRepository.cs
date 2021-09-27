using IoTHomeAssistant.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IoTHomeAssistant.Domain.Repositories
{
    public interface IEventRepository : IRepository<Event, int>
    {
        Task<List<Event>> GetEventsAsync(bool? hasValue);
    }
}
