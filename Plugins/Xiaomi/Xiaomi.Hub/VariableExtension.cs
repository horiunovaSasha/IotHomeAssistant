using System;
using System.Collections;

namespace Xiaomi.Hub
{
    static class VariableExtension
    {
        static public string MQTT_ADDR { get; private set; }
        static public string MQTT_USR { get; private set; }
        static public string MQTT_PWD { get; private set; }
        static public string HUB_ID { get; private set; }

        static VariableExtension()
        {
            foreach (DictionaryEntry arg in Environment.GetEnvironmentVariables())
            {
                if (arg.Key.ToString() == "MQTT_ADDR")
                {
                    MQTT_ADDR = arg.Value?.ToString();
                }

                if (arg.Key.ToString() == "MQTT_USR")
                {
                    MQTT_USR = arg.Value?.ToString();
                }

                if (arg.Key.ToString() == "MQTT_PWD")
                {
                    MQTT_PWD = arg.Value?.ToString();
                }

                if (arg.Key.ToString() == "HUB_ID")
                {
                    HUB_ID = arg.Value?.ToString();
                }
            }

            if (string.IsNullOrEmpty(MQTT_ADDR))
            {
                throw new ArgumentException("MQTT_ADDR Environment variable is required!");
            }

            if (string.IsNullOrEmpty(HUB_ID))
            {
                throw new ArgumentException("HUB_ID Environment variable is required!");
            }
        }
    }
}
