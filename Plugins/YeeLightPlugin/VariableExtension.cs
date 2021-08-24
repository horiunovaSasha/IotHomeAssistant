
using System;
using System.Collections;

namespace YeeLightPlugin
{
    static class VariableExtension
    {
        static public string CMD_TOPIC { get; private set; }
        static public string STATUS_TOPIC { get; private set; }
        static public string MQTT_ADDR { get; private set; }
        static public string MQTT_USR { get; private set; }
        static public string MQTT_PWD { get; private set; }
        static public string IP_ADDR { get; private set; }

        static VariableExtension()
        {
            foreach (DictionaryEntry arg in Environment.GetEnvironmentVariables())
            {
                if (arg.Key.ToString() == "CMD_TOPIC")
                {
                    CMD_TOPIC = arg.Value?.ToString();
                }
                
                if (arg.Key.ToString() == "STATUS_TOPIC")
                {
                    STATUS_TOPIC = arg.Value?.ToString();
                }

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

                if (arg.Key.ToString() == "IP_ADDR")
                {
                    IP_ADDR = arg.Value?.ToString();
                }
            }

            if (string.IsNullOrEmpty(MQTT_ADDR))
            {
                throw new ArgumentException("MQTT_ADDR Environment variable is required!");
            }

            if (string.IsNullOrEmpty(CMD_TOPIC))
            {
                throw new ArgumentException("CMD_TOPIC Environment variable is required!");
            }
            
            if (string.IsNullOrEmpty(STATUS_TOPIC))
            {
                throw new ArgumentException("STATUS_TOPIC Environment variable is required!");
            }

            if (string.IsNullOrEmpty(IP_ADDR))
            {
                throw new ArgumentException("IP_ADDR Environment variable is required!");
            }
        }
    }
}
