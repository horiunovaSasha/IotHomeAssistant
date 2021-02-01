namespace IoTHomeAssistant.Domain.Dto
{
    public class ColorRangeWidgetItem
    {
        public int ColorId { get; set; }
        public float? From { get; set; }
        public float? To { get; set; }
        public bool IsDefault { get; set; }
    }
}
