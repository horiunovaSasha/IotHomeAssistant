using System.ComponentModel.DataAnnotations;

namespace IoTHomeAssistant.Web.Models
{
    public class InviteRequest
    {
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
    }
}