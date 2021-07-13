namespace IoTHomeAssistant.Domain.Entities
{
    public class PluginDeviceConfiguration : IEntity<int>
    {
        public int Id { get; set; }
        public int PluginDeviceId { get; set; }
        public int PluginConfigurationId {get;set;}
        public string Value { get; set; }

        public virtual PluginDevice PluginDevice { get; set; }
        public virtual PluginConfiguration PluginConfiguration { get; set; }
    }
}