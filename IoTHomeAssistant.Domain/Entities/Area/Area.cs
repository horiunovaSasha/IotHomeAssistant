namespace IoTHomeAssistant.Domain.Entities
{
    public class Area : IEntity<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}