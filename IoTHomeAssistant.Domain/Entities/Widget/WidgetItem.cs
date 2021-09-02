using IoTHomeAssistant.Domain.Enums;
using System.Collections.Generic;

namespace IoTHomeAssistant.Domain.Entities
{
    public class WidgetItem : IEntity<int>
    {
        public int Id { get; set; }
        public WidgetItemTypeEnum Type { get; set; }
        public string Title { get; set; }

        public virtual Widget Widget { get; set; }
        public virtual Icon Icon { get; set; }
        public virtual Color IconColor { get; set; }
        public virtual ICollection<WidgetItemColorRange> ColorRange { get; set; }
        public virtual Device Device { get; set; }
    }
}
