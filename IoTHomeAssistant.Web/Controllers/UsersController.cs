using System.Threading.Tasks;
using IoTHomeAssistant.Web.Models;
using IoTHomeAssistant.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IoTHomeAssistant.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IUserService _userService;

        public UsersController(UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager,
            IUserService userService )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
        }
        
        [HttpPost]
        public async Task Invite([FromBody] InviteRequest request)
        {
            await _userService.Invite(request?.Email, Url);
        }
        
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null, string userId = null)
        {
            var user = _userManager.FindByIdAsync(userId).Result;
            if (code == null || user == null)
                return NotFound();

            var viewModel = new ResetPasswordViewModel(){Email = user.Email, Code = code};
            return View(viewModel);
        }
 
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return NotFound();
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, null);
                return RedirectPermanent("/");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }
    }
}