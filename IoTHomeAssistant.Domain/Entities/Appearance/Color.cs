namespace IoTHomeAssistant.Domain.Entities
{
    public class Color : IEntity<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }
    }
}
