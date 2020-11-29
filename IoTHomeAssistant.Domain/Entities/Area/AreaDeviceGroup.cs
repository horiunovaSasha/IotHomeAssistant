namespace IoTHomeAssistant.Domain.Entities
{
    public class AreaDeviceGroup
    {
        public int AreaId { get; set; }
        public int DeviceGroupId { get; set; }
        
        public virtual Area Area { get; set; }
        public virtual DeviceGroup DeviceGroup { get; set; }
    }
}