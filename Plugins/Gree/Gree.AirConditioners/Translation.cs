using System.Collections.Generic;

namespace Gree.AirConditioner
{
    public static class Translation
    {
        public const string SET_POWER = "set_power";
        public const string SET_TEMPERATURE = "set_temperature";
        public const string SET_AIR_MODE = "set_air_mode";
        public const string SET_AIR_SPEED = "set_air_speed";

        public const string GET_POWER = "Pow";
        public const string GET_TEMPERATURE = "SetTem";
        public const string GET_AIR_MODE = "Mod";
        public const string GET_AIR_SPEED = "WdSpd";

        public static readonly new Dictionary<string, string> AirToMqttCmd;
        public static readonly new Dictionary<string, string> MqttToAirCmd;
        public static readonly new Dictionary<int, string> AirToMqttMode;
        public static readonly new Dictionary<string, int> MqttToAirMode;
        public static readonly new Dictionary<int, string> AirToMqttSpeed;
        public static readonly new Dictionary<string, int> MqttToAirSpeed;

        static Translation()
        {
            AirToMqttCmd = new Dictionary<string, string>()
            {
                { GET_POWER, "power_changed" },
                { GET_TEMPERATURE, "temperature_changed" },
                { GET_AIR_MODE, "air_mode_changed" },
                { GET_AIR_SPEED, "air_speed_changed" },
            };

            MqttToAirCmd = new Dictionary<string, string>()
            {
                { SET_POWER, GET_POWER },
                { SET_TEMPERATURE, GET_TEMPERATURE },
                { SET_AIR_MODE, GET_AIR_MODE },
                { SET_AIR_SPEED, GET_AIR_SPEED },
            };

            AirToMqttMode = new Dictionary<int, string>() {
                {0, "auto"},
                {1, "cool"},
                {2, "dry"},
                {3, "fan"},
                {4, "heating"}
            };

            MqttToAirMode = new Dictionary<string, int>() {
                {"auto", 0},
                {"cool", 1},
                {"dry", 2},
                {"fan", 3},
                {"heating", 4}
            };

            AirToMqttSpeed = new Dictionary<int, string>() {
                {0, "auto"},
                {1, "min"},
                {2, "low"},
                {3, "mid"},
                {4, "high"},
                {5, "max" }
            };

            MqttToAirSpeed = new Dictionary<string, int>() {
                {"auto", 0},
                {"min", 1},
                {"low", 2},
                {"mid",3 },
                {"high", 4},
                {"max", 5 }
            };
        }
    }
}
