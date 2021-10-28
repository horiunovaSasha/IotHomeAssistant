using System;
using System.Collections;

namespace Xiaomi.Yeelight
{
    static class VariableExtension
    {
        static public string CMD_TOPIC { get; private set; }
        static public string STATUS_TOPIC { get; private set; }
        static public string SEND_STATUS_TOPIC { get; private set; }
        static public string MQTT_ADDR { get; private set; }
        static public string MQTT_USR { get; private set; }
        static public string MQTT_PWD { get; private set; }
        static public string IP_ADDRESS { get; private set; }

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

                if (arg.Key.ToString() == "SEND_STATUS_TOPIC")
                {
                    SEND_STATUS_TOPIC = arg.Value?.ToString();
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

                if (arg.Key.ToString() == "IP_ADDRESS")
                {
                    IP_ADDRESS = arg.Value?.ToString();
                }

            }

            SEND_STATUS_TOPIC = "RECEIVE_EVENTS_Light_16";

            IP_ADDRESS = "192.168.1.139";

            MQTT_ADDR = "192.168.1.226";

            CMD_TOPIC = "CMD_Light_16";

            STATUS_TOPIC = "GET_STATUS_Light_16";


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

            if (string.IsNullOrEmpty(SEND_STATUS_TOPIC))
            {
                throw new ArgumentException("SEND_STATUS_TOPIC Environment variable is required!");
            }

            if (string.IsNullOrEmpty(IP_ADDRESS))
            {
                throw new ArgumentException("IP_ADDRESS Environment variable is required!");
            }
        }
    }
}
