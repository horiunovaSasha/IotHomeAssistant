using IoTHomeAssistant.Domain.Enums;

namespace IoTHomeAssistant.Domain.Entities
{
    public class PluginConfiguration : IEntity<int>
    {
        public int Id { get; set; }
        public int PluginId {get;set;}
        public string Title { get; set; }
        public string Key { get; set; }
        public string Description { get; set; }
        public ConfigurationTypeEnum Type { get; set; }

        public virtual Plugin Plugin { get; set; }
    }
}