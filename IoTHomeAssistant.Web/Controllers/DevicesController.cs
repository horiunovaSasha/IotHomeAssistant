using IoTHomeAssistant.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace IoTHomeAssistant.Web.Controllers
{
    public class DevicesController : Controller
    {
        private readonly IDeviceService _deviceService;

        public DevicesController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        public IActionResult Index()
        {
            return View(_deviceService.GetAllDevices());
        }
    }
}
