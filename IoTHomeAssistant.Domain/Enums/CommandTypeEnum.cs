using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTHomeAssistant.Domain.Enums
{
    public enum CommandTypeEnum
    {
        no_command = 0,
        set_power = 1,
        set_temperature = 2,
        set_brightness = 3,
        set_color = 4,
        set_air_mode = 5,
        set_air_speed = 6,
        open = 7,
        close = 8,
        stop = 9
    }
}
