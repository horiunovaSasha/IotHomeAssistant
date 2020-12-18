namespace IoTHomeAssistant.Domain.Entities
{
    public class Icon : IEntity<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }
    }
}
