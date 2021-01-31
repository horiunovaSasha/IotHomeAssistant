using System.Collections.Generic;

namespace IoTHomeAssistant.Domain.Dto
{
    public class InfoDevice
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<SensorInfo> Sensors { get; set; }
    }
}
