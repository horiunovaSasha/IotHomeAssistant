using IoTHomeAssistant.Domain.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IoTHomeAssistant.Domain.Dto
{
    public class PluginEditDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Це поле обов'язкове для заповнення!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Виберіть тип пристрою!")]
        public string Type { get; set; }
        
        public List<DevicePluginConfigurationDto> Configurations { get; set; }

        [Required(ErrorMessage = "Це поле обов'язкове для заповнення!")]
        public string DockerConfiguration { get; set; }
        
        public PluginEditDto()
        {
            Configurations = new List<DevicePluginConfigurationDto>();
        }
    }
}
