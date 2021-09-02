using IoTHomeAssistant.Domain.Dto.Pagging;
using IoTHomeAssistant.Domain.Entities;
using IoTHomeAssistant.Domain.Enums;
using IoTHomeAssistant.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using IoTHomeAssistant.Web.Models;

namespace IoTHomeAssistant.Web.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PluginsController : ControllerBase
    {
        private readonly IPluginService _pluginService;

        public PluginsController(IPluginService pluginService)
        {
            _pluginService = pluginService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<Plugin> Get(int id)
        {
            return await _pluginService.GetPluginAsync(id);
        }

        [HttpGet]
        public async Task<PageResponse<Plugin>> Items(int pageNumber = 1, int pageSize = 10)
        {
            return await _pluginService.GetPagginPlugins(
                new PageRequest() { 
                    PageNumber = pageNumber, 
                    PageSize = pageSize 
                });
        }

        [HttpGet]
        [Route("type/{type}")]
        public async Task<List<Plugin>> GetByTypes(DeviceTypeEnum type)
        {
            return await _pluginService.GetPluginsByTypeAsync(type);
        }

        [HttpPost]
        public async Task Add(Plugin request)
        {
            await _pluginService.AddPlugin(request);
        }

        [HttpPut]
        public async Task Update(Plugin request)
        {
            await _pluginService.UpdatePlugin(request);
        }

        [HttpDelete]
        public async Task Delete([FromBody]DeletePluginRequest request)
        {
            await _pluginService.RemovePlugin(request.Id);
        }
    }
}