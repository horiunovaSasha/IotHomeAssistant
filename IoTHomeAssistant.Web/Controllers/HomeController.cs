using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using IoTHomeAssistant.Web.Models;
using IoTHomeAssistant.Domain.Services;

namespace IoTHomeAssistant.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWidgetService _widgetService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IWidgetService widgetService)
        {
            _logger = logger;
            _widgetService = widgetService;
        }

        public IActionResult Index()
        {
            var widgets = _widgetService.GetAllWidgets();
            return View(widgets);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}