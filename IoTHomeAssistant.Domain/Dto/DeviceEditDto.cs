using IoTHomeAssistant.Domain.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IoTHomeAssistant.Domain.Dto
{
    public class DeviceEditDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Це поле обов'язкове для заповнення!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Виберіть тип пристрою!")]
        public string Type { get; set; }
        
        [Range(1, int.MaxValue, ErrorMessage = "Виберіть плагін!")]
        public int Plugin { get; set; }
        public string DockerImageId { get; set; }

        public List<DevicePluginConfigurationDto> Configurations { get; set; }


        public DeviceEditDto()
        {
            Configurations = new List<DevicePluginConfigurationDto>();
        }
    }
}
