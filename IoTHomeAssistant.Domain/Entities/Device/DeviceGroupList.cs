namespace IoTHomeAssistant.Domain.Entities
{
    public class DeviceGroupList
    {
        public int DeviceGroupId { get; set; }
        public int DeviceId { get; set; }
        
        public virtual Group Group { get; set; }
        public virtual Device Device { get; set; }
    }
}