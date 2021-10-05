﻿using IoTHomeAssistant.Domain.Enums;

namespace IoTHomeAssistant.Domain.Entities
{
    public class WidgetItem : IEntity<int>
    {
        public int Id { get; set; }
        public WidgetItemTypeEnum Type { get; set; }
        public string Title { get; set; }
        public byte Order { get; set; }
        public string SymbolAfter { get; set; }

        public int AreaId { get; set; }
        public int? DeviceId { get; set; }
        public int? EventId { get; set; }
        public int? IconId { get; set; }

        public virtual Icon Icon { get; set; }
        public virtual Device Device { get; set; }
        public virtual Area Area { get; set; }
        public virtual Event Event { get; set; }
    }
}
