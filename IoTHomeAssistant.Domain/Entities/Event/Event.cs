namespace IoTHomeAssistant.Domain.Entities
{
    public class Event : IEntity<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Key { get; set; }
        public bool HasValue { get; set; }
    }
}