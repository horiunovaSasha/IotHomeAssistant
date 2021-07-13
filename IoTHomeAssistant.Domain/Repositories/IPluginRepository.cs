using IoTHomeAssistant.Domain.Entities;
using System.Collections.Generic;

namespace IoTHomeAssistant.Domain.Repositories
{
    public interface IPluginRepository : IRepository<Plugin, int>
    {
        List<Plugin> GetAllWithDependencies();
    }
}
