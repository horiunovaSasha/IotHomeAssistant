﻿using IoTHomeAssistant.Domain.Dto.Pagging;
using IoTHomeAssistant.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IoTHomeAssistant.Domain.Repositories
{
    public interface IPluginRepository : IRepository<Plugin, int>
    {
        List<Plugin> GetAllWithDependencies();
        Task<PageResponse<Plugin>> GetPaggedList(PageRequest request);
    }
}
