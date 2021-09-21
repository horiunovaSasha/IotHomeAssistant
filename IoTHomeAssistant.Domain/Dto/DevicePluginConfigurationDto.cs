using System.ComponentModel.DataAnnotations;

namespace IoTHomeAssistant.Domain.Dto
{
    public class DevicePluginConfigurationDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }

        [Required(ErrorMessage = "Це поле обовязкове для заповнення!")]
        public string Value { get; set; }
    }
}
