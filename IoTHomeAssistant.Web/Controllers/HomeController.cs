using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using IoTHomeAssistant.Web.Models;
using IoTHomeAssistant.Domain.Services;
using System.Net.Http;
using Microsoft.AspNetCore.Identity;

namespace IoTHomeAssistant.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWidgetService _widgetService;
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<IdentityUser> _signInManager;

        public HomeController(ILogger<HomeController> logger, IWidgetService widgetService, SignInManager<IdentityUser> signInManager)
        {
            _logger = logger;
            _widgetService = widgetService;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            var widgets = _widgetService.GetAllWidgets();
            return View(widgets);
        }

        public async Task<IActionResult> Weather(string place_id, string city)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("https://forecast7.com/api/getUrl/" + place_id);
            ViewBag.City = city;

            if (response.IsSuccessStatusCode) {
                ViewBag.PlaceId = await response.Content.ReadAsStringAsync();
                ViewBag.FoundWeather = (await httpClient.GetAsync($"https://forecast7.com/uk/{ViewBag.PlaceId}/")).IsSuccessStatusCode;
            }

            return View();
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
        
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectPermanent("/identity/Account/Login/");
        }
    }
}