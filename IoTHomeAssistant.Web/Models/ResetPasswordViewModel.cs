using System.ComponentModel.DataAnnotations;

namespace IoTHomeAssistant.Web.Models
{
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
 
        [Required]
        [StringLength(100, ErrorMessage = "Password must contain at least 6 characters ", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
 
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Passwords does not match")]
        public string ConfirmPassword { get; set; }
 
        public string Code { get; set; }
    }
}