using IoTHomeAssistant.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IoTHomeAssistant.Domain.Services
{
    public interface IAreaService
    {
        Task<List<Area>> GetAreasAsync();
        Task SaveAsync(Area area);
        Task RemoveAsync(int id);
    }
}