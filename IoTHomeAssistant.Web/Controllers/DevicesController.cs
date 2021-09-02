using Microsoft.AspNetCore.Mvc;

namespace IoTHomeAssistant.Web.Controllers
{
    public class DevicesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
