using System.ComponentModel.DataAnnotations;

namespace IoTHomeAssistant.Domain.Dto
{
    public class UserAddDto
    {
        [Required(ErrorMessage = "Це поле обов'язкове для заповнення!")]
        [EmailAddress(ErrorMessage = "Введіть правильну адресу")]
        public string Email { get; set; }
    }
}
