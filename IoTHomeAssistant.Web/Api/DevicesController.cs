using IoTHomeAssistant.Domain.Dto;
using IoTHomeAssistant.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace IoTHomeAssistant.Web.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly IDeviceService _deviceService;

        public DevicesController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        [HttpGet]
        [Route("Info")]
        public List<InfoDevice> GetInfoDevices()
        {
            return _deviceService.GetInfoDevices();
        }
    }
}
