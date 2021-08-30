using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IoTHomeAssistant.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace IoTHomeAssistant.Web.Services
{
    public class UserService: IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly IEmailService _emailService;
        
        private const string AdminRole = "admin";

        public UserService(UserManager<IdentityUser> userManager,  IActionContextAccessor actionContextAccessor, IEmailService emailService)
        {
            _userManager = userManager;
            _actionContextAccessor = actionContextAccessor;
            _emailService = emailService;
        }
        
        public List<UserViewModel> GetUsersWithRoles()
        {
            var result = new List<UserViewModel>();

            foreach (var identityUser in _userManager.Users)
            {
                var isAdmin = _userManager.GetRolesAsync(identityUser).Result?.FirstOrDefault() == AdminRole;
                result.Add(new UserViewModel()
                {
                    IsAdmin = isAdmin,
                    User = identityUser
                });
            }

            return result;
        }

        public async Task Invite(string email, IUrlHelper url)
        {
            if (!string.IsNullOrWhiteSpace(email))
            {
                var existingUser =  _userManager.FindByEmailAsync(email).Result;
                if (existingUser != null)
                    await _userManager.DeleteAsync(existingUser);
                
                var user = new IdentityUser() {UserName = email, Email =  email};
                var identityResult = await _userManager.CreateAsync(user);
                if (identityResult.Succeeded)
                {
                    var dbUser = await _userManager.FindByEmailAsync(user.Email);
                    var code = await _userManager.GeneratePasswordResetTokenAsync(dbUser);
                    var callbackUrl = url.Action("ResetPassword", "Users", new { userId = dbUser.Id, code = code }, _actionContextAccessor.ActionContext.HttpContext.Request.Scheme);
                    await _emailService.SendEmailAsync(dbUser.Email, "Welcome to Iot home assistant", callbackUrl);
                }
            }
        }

    }
}