using IoTHomeAssistant.Domain.Enums;

namespace IoTHomeAssistant.Domain.Entities.Command
{
    public class Command<T>
    {
        public CommandTypeEnum Type { get; set; }
        public T Value { get; set; }
    }
}