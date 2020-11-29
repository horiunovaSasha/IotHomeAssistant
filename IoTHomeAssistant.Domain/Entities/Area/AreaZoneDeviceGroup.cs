namespace IoTHomeAssistant.Domain.Entities
{
    public class AreaZoneGroupDevice
    {
        public int AreaZoneId { get; set; }
        public int DeviceGroupId { get; set; }
        
        public virtual AreaZone AreaZone { get; set; }
        public virtual DeviceGroup DeviceGroup { get; set; }
    }
}