using IoTHomeAssistant.Domain.Enums;

namespace IoTHomeAssistant.Domain.Entities
{
    public class WidgetItem : IEntity<int>
    {
        public int Id { get; set; }
        public WidgetItemTypeEnum Type { get; set; }
        public string Title { get; set; }
        public byte Order { get; set; }

        public virtual Icon Icon { get; set; }
        public virtual Device Device { get; set; }
        public virtual Area Area { get; set; }
    }
}
