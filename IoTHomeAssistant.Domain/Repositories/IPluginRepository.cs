﻿using IoTHomeAssistant.Domain.Dto.Pagging;
using IoTHomeAssistant.Domain.Entities;
using IoTHomeAssistant.Domain.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IoTHomeAssistant.Domain.Repositories
{
    public interface IPluginRepository : IRepository<Plugin, int>
    {
        Task<Plugin> GetPluginAsync(int id);
        Task<List<Plugin>> GetPluginsByTypeAsync(DeviceTypeEnum type);
        List<Plugin> GetAllWithDependencies();
        Task<PageResponse<Plugin>> GetPaggedList(PageRequest request);
    }
}
