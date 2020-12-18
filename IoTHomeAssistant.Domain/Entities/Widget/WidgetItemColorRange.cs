namespace IoTHomeAssistant.Domain.Entities
{
    public class WidgetItemColorRange : IEntity<int>
    {
        public int Id { get; set; }
        public float ValueFrom { get; set; }
        public float ValueTo { get; set; }

        public virtual Color Color { get; set; }
        public virtual WidgetItem WidgetItem { get; set; }
    }
}
