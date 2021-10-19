namespace Tuya.Blinds
{
    public class Command
    {
        public CommandType Type { get; set; }
    }

    public enum CommandType
    {
        Stop = 0,
        Open = 1,
        Close = 2
    }
}
