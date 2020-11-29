namespace IoTHomeAssistant.Domain.Entities
{
    public class AreaZoneDevice
    {
        public int AreaZoneId { get; set; }
        public int DeviceId { get; set; }
        
        public virtual AreaZone AreaZone { get; set; }
        public virtual Device Device { get; set; }
    }
}