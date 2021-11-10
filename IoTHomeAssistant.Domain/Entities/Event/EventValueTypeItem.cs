namespace IoTHomeAssistant.Domain.Entities
{
    public class EventValueTypeItem : IEntity<int>
    {
        public int Id { get; set; }
        public int EventValueTypeId { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }

        public virtual EventValueType EventValueType { get; set; }
    }
}