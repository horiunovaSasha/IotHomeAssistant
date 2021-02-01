﻿using IoTHomeAssistant.Domain.Dto;
using IoTHomeAssistant.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace IoTHomeAssistant.Web.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class WidgetController : ControllerBase
    {
        private readonly IWidgetService _widgetService;

        public WidgetController(IWidgetService widgetService)
        {
            _widgetService = widgetService;
        }

        [HttpPost]
        [Route("Info")]
        public EmptyResponse SaveInfoWidget([FromBody]InfoWidget widget)
        {
            return _widgetService.SaveInfoWidget(widget);
        }
    }
}
