using System.ComponentModel.DataAnnotations;

namespace IoTHomeAssistant.Domain.Dto
{
    public class AreaDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Це поле обов'язкове для заповнення!")]
        public string Title { get; set; }
    }
}
