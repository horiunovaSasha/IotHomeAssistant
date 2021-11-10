using IoTHomeAssistant.Domain.Entities;
using IoTHomeAssistant.Domain.Enums;

namespace IoTHomeAssistant.Domain.Dto
{
    public class DeviceCommandDto
    {
        public int DeviceId { get; set; }
        public string DeviceName { get; set; }
        public int CommandId { get; set; }
        public string CommandTitle { get; set; }
        public CommandTypeEnum EventType { get; set; }
        public CommandValueType ValueType { get; set; }
    }
}
