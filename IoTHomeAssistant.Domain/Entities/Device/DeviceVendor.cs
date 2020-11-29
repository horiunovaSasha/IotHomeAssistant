namespace IoTHomeAssistant.Domain.Entities
{
    public class DeviceVendor : IEntity<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}