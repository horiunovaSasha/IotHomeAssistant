using Microsoft.AspNetCore.Identity;

namespace IoTHomeAssistant.Web.Models
{
    public class UserViewModel
    {
        public string User { get; set; }
        public bool IsAdmin { get; set; }
    }
}