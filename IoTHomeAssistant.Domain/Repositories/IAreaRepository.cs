using IoTHomeAssistant.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IoTHomeAssistant.Domain.Repositories
{
    public interface IAreaRepository: IRepository<Area, int>
    {
        Task<List<Area>> GetAreasAsync();
    }
}
