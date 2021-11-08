namespace IoTHomeAssistant.Domain.Entities
{
    public class Command : IEntity<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Key { get; set; }
    }
}