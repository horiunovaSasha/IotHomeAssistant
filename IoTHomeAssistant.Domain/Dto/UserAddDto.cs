using System.ComponentModel.DataAnnotations;

namespace IoTHomeAssistant.Domain.Dto
{
    public class UserAddDto
    {
        [Required(ErrorMessage = "Це поле обов'язкове для заповнення!")]
        public string Email { get; set; }
    }
}
