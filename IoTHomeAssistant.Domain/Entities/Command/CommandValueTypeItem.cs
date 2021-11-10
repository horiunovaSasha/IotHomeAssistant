namespace IoTHomeAssistant.Domain.Entities
{
    public class CommandValueTypeItem : IEntity<int>
    {
        public int Id { get; set; }
        public int CommandValueTypeId { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }

        public virtual CommandValueType CommandValueType { get; set; }
    }
}