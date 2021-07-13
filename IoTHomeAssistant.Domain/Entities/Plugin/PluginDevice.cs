using System.Collections.Generic;

namespace IoTHomeAssistant.Domain.Entities
{
    public class PluginDevice : IEntity<int>
    {
        public int Id { get; set; }
        public int PluginId { get; set; }
        public int DeviceId { get; set; }
        public int ExtDeviceRef { get; set; }
        public virtual List<PluginDeviceConfiguration> Configurations { get; set; }

        public virtual Device Device { get; set; }
        public virtual Plugin Plugin { get; set; }
    }
}
