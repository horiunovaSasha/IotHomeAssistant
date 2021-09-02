namespace IoTHomeAssistant.Domain.Entities.Event
{
    public class Event : IEntity<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Key { get; set; }
    }
}