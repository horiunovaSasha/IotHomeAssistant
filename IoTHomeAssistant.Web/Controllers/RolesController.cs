using System.Linq;
using System.Threading.Tasks;
using IoTHomeAssistant.Web.Models;
using IoTHomeAssistant.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IoTHomeAssistant.Web.Controllers
{
      public class RolesController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserService _userService;

        private static string ADMIN_ROLE = "admin";
        private static string USER_ROLE = "user";
        
        public RolesController(UserManager<IdentityUser> userManager, IUserService userService)
        {
            _userManager = userManager;
            _userService = userService;
        }

        public IActionResult Index()
        {
            var viewModel = _userService.GetUsersWithRoles();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] RolesRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if(user != null)
            {
                if (request.IsChecked)
                {
                    await _userManager.AddToRoleAsync(user, ADMIN_ROLE);
                    await _userManager.RemoveFromRoleAsync(user, USER_ROLE);
                }
                else
                {
                    await _userManager.AddToRoleAsync(user, USER_ROLE);
                    await _userManager.RemoveFromRoleAsync(user, ADMIN_ROLE);
                }

                return RedirectToAction("Index");
            }
 
            return NotFound();
        }

        
    }
}