namespace IoTHomeAssistant.Web.Models
{
    public class UserViewModel
    {
        public Microsoft.AspNetCore.Identity.IdentityUser User { get; set; }
        public bool IsAdmin { get; set; }
    }
}