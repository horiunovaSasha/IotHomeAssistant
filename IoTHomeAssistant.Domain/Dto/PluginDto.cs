using IoTHomeAssistant.Domain.Entities;

namespace IoTHomeAssistant.Domain.Dto
{
    public class PluginDto
    {
        public PluginDto(Plugin plugin)
        {
            Id = plugin.Id;
            Title = plugin.Title;
            DeviceType = plugin.DeviceType.ToString();

        }
        
        public int Id { get; set; }
        public string Title { get; set; }
        public string DeviceType { get; set; }
    }
}