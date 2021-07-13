namespace IoTHomeAssistant.Domain.Entities
{
    public class PluginDeviceConfiguration : IEntity<int>
    {
        public int Id { get; set; }
        public int PluginDeviceId {get;set;}
        public string Title { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }

        public virtual PluginDevice PluginDevice { get; set; }
    }
}