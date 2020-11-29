namespace IoTHomeAssistant.Domain.Entities
{
    public class Group : IEntity<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}