namespace Tuya.Blinds
{
    public class Status
    {
        public int Percent { get; set; }
        public Stage Stage { get; set; }
    }

    public enum Stage
    {
        Stopped = 0,
        Opening = 1,
        Closing = 2,
        Opened = 3,
        Closed = 4
    }
}
