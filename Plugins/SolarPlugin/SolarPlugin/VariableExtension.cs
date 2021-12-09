using System.Collections;

namespace SolarPlugin
{
    static class VariableExtension
    {
        static public string SEND_STATUS_TOPIC { get; private set; }
        static public string MQTT_ADDR { get; private set; }
        static public string MQTT_USR { get; private set; }
        static public string MQTT_PWD { get; private set; }
        static public string LATITUDE{ get; private set; }
        static public string LONGITUDE { get; private set; }

        static VariableExtension()
        {
            foreach (DictionaryEntry arg in Environment.GetEnvironmentVariables())
            {
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

                if (arg.Key.ToString() == "LATITUDE")
                {
                    LATITUDE = arg.Value?.ToString();
                }

                if (arg.Key.ToString() == "LONGITUDE")
                {
                    LONGITUDE = arg.Value?.ToString();
                }                
            }            

            if (string.IsNullOrEmpty(MQTT_ADDR))
            {
                throw new ArgumentException("MQTT_ADDR Environment variable is required!");
            }

            if (string.IsNullOrEmpty(LATITUDE))
            {
                throw new ArgumentException("LATITUDE Environment variable is required!");
            }

            if (string.IsNullOrEmpty(LONGITUDE))
            {
                throw new ArgumentException("LONGITUDE Environment variable is required!");
            }

            if (string.IsNullOrEmpty(SEND_STATUS_TOPIC))
            {
                throw new ArgumentException("SEND_STATUS_TOPIC Environment variable is required!");
            } 
        }
    }
}