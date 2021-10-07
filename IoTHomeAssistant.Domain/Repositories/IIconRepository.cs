using IoTHomeAssistant.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IoTHomeAssistant.Domain.Repositories
{
    public interface IIconRepository : IRepository<Icon, int>
    {
        Task<List<Icon>> GetAllAsync();
        Task<List<Icon>> GetAllByKeysAsync(List<string> keys);
    }
}
