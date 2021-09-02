using IoTHomeAssistant.Domain.Dto;
using IoTHomeAssistant.Domain.Dto.Pagging;
using IoTHomeAssistant.Domain.Entities;
using IoTHomeAssistant.Domain.Services;
using IoTHomeAssistant.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        [Route("{id}")]
        public async Task<Device> Get(int id)
        {
            return await _deviceService.GetDeviceAsync(id);
        }

        [HttpGet]
        [Route("Info")]
        public List<InfoDevice> GetInfoDevices()
        {
            return _deviceService.GetInfoDevices();
        }

        [HttpPost]
        [Route("Toggle")]
        public void Toggle(ToggleRequest request)
        {
            _deviceService.Toggle(request.Id, request.Toggle);
        }
        
        [HttpPost]
        [Route("Light")]
        public void Light(LightRequest request)
        {
            _deviceService.LightControl(request.Id, request.Toggle, request.Brightness, request.Color);
        }

        [HttpGet]
        public async Task<PageResponse<DeviceDto>> Items(int pageNumber = 1, int pageSize = 10)
        {
            return await _deviceService.GetPaggedList(
                new PageRequest()
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize
                });
        }
    }
}
