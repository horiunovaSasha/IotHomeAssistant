using System;

namespace IoTHomeAssistant.Domain.Entities
{
    public class AreaDevice : IEntity<AreaDeviceId>
    {
        public AreaDeviceId Id { get; set; }
        public virtual Area Area { get; set; }
        public virtual Device Device { get; set; }
    }

    public struct AreaDeviceId : IEquatable<AreaDeviceId>
    {
        int AreaId => 0; 
        int DeviceId => 0;

        public bool Equals(AreaDeviceId other) 
            => other.AreaId == AreaId && other.DeviceId == DeviceId;
    }
}