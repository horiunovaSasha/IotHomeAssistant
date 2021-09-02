using Microsoft.AspNetCore.Mvc;

namespace IoTHomeAssistant.Web.Controllers
{
    public class TasksController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
