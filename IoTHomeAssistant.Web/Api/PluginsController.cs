﻿using IoTHomeAssistant.Domain.Dto.Pagging;
using IoTHomeAssistant.Domain.Entities;
using IoTHomeAssistant.Domain.Services;
using Microsoft.AspNetCore.Mvc;

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
        public Plugin Get(int id)
        {
            return _pluginService.GetPlugin(id);
        }

        [HttpGet]
        public PageResponse<Plugin> Items(int pageNumber = 1, int pageSize = 10)
        {
            return _pluginService.GetPagginPlugins(
                new PageRequest() { 
                    PageNumber = pageNumber, 
                    PageSize = pageSize 
                });
        }

        [HttpPost]
        public void Add(Plugin request)
        {
            _pluginService.AddPlugin(request);
        }

        [HttpPut]
        public void Update(Plugin request)
        {
            _pluginService.UpdatePlugin(request);
        }

        [HttpDelete]
        public void Delete(int id)
        {
            _pluginService.RemovePlugin(id);
        }
    }
}