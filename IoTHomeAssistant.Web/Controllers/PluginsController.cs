using IoTHomeAssistant.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace IoTHomeAssistant.Web.Controllers
{
    public class PluginsController : Controller
    {
        private readonly IPluginService _service;

        public PluginsController(IPluginService service) {
            _service = service;
        }

        public IActionResult Index()
        {
            return View(_service.GetPlugins());
        }
    }
}
