namespace IoTHomeAssistant.Domain.Enums
{
    public enum EventTypeEnum
    {
        no_event = 0,
        status_changed = 1,
        power_changed = 2,
        target_temperature_changed = 3,
        brightness_changed = 4,
        color_changed = 5,
        humidity_changed = 6,
        temperature_changed = 7,
        waterleak_detected = 8,
        waterleak_stopped = 9,
        motion_detected = 10,
        motion_stopped = 11
    }
}
