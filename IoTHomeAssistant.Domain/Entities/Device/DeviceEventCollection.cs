namespace IoTHomeAssistant.Domain.Entities
{
    public class DeviceEventCollection : IEntity<int>
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public int EventCollectionId { get; set; }

        public virtual Device Device { get; set; }
        public virtual EventCollection EventCollection { get; set; }
    }
}
