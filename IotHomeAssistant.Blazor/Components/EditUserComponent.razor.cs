using IoTHomeAssistant.Domain.Dto;
using Microsoft.AspNetCore.Components;
using IoTHomeAssistant.Blazor.Services;
using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace IotHomeAssistant.Blazor.Components
{
    public partial class EditUserComponent
    {
        private bool _visible = false;

        [Inject]
        protected IUserService _userService { get; set; }

        [Inject]
        protected UserManager<IdentityUser> _userManager { get; set; }

        protected UserAddDto User { get; set; } = new UserAddDto();

        public void AddPlugin()
        {
            Show();
        }

        private void Show()
        {
            _visible = true;
            StateHasChanged();
        }

        private void Hide()
        {
            _visible = false;
            StateHasChanged();
        }

        private void Save()
        {
            _userService.Invite(User.Email, null);
            StateHasChanged();
            Hide();
        }

    }
}