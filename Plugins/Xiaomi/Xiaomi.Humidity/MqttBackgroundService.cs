using Microsoft.Extensions.Hosting;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Xiaomi.Humidity
{
    public class MqttBackgroundService : IHostedService, IDisposable
    {
        private const string MQTT_CLIENT_ID = "Xiaomi.Humidity";
        private const string XIAOMI_GET_HUMIDITY = "xiaomi_get_humidity";
        private const string XIAOMI_HUMIDITY_CHANGED = "xiaomi_humidity_changed";

        private readonly MqttClient _client;

        public MqttBackgroundService()
        {
            _client = new MqttClient(VariableExtension.MQTT_ADDR);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _client.Connect(MQTT_CLIENT_ID + VariableExtension.DEVICE_ID);

            _client.MqttMsgPublishReceived += (object sender, MqttMsgPublishEventArgs e) =>
            {
                if (e.Topic == VariableExtension.STATUS_TOPIC)
                {
                    _client.Publish(XIAOMI_GET_HUMIDITY, Encoding.UTF8.GetBytes(VariableExtension.DEVICE_ID));
                }
                
                if (e.Topic == $"{XIAOMI_HUMIDITY_CHANGED}_{VariableExtension.DEVICE_ID}")
                {
                    _client.Publish(VariableExtension.SEND_STATUS_TOPIC, e.Message);
                }
            };

            _client.Subscribe(new string[] { VariableExtension.STATUS_TOPIC }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            _client.Subscribe(new string[] { $"{XIAOMI_HUMIDITY_CHANGED}_{VariableExtension.DEVICE_ID}" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _client.Disconnect();
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _client.Disconnect();
        }
    }
}