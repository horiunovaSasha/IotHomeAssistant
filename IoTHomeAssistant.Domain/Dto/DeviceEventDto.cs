﻿using IoTHomeAssistant.Domain.Entities;
using IoTHomeAssistant.Domain.Enums;

namespace IoTHomeAssistant.Domain.Dto
{
    public class DeviceEventDto
    {
        public int DeviceId { get; set; }
        public string DeviceName { get; set; }
        public int EventId { get; set; }
        public string EventTitle { get; set; }
        public EventTypeEnum EventType { get; set; }
        public EventValueType ValueType { get; set; }
        public bool EventHasValue { get; set; }
    }
}
