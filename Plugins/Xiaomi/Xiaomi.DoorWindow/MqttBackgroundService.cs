using Microsoft.Extensions.Hosting;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Xiaomi.DoorWindow
{
    public class MqttBackgroundService : IHostedService, IDisposable
    {
        private const string MQTT_CLIENT_ID = "Xiaomi.DoorWindow";
        private const string XIAOMI_GET_DOORWINDOW = "xiaomi_get_doorwindow";
        private const string XIAOMI_DOORWINDOW_CLOSED = "xiaomi_doorwindow_closed";
        private const string XIAOMI_DOORWINDOW_OPENED = "xiaomi_doorwindow_opened";

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
                    _client.Publish(XIAOMI_GET_DOORWINDOW, Encoding.UTF8.GetBytes(VariableExtension.DEVICE_ID));
                }

                if (e.Topic == $"{XIAOMI_DOORWINDOW_CLOSED}_{VariableExtension.DEVICE_ID}")
                {
                    _client.Publish(VariableExtension.ON_CLOSED_TOPIC, e.Message);
                }
                if (e.Topic == $"{XIAOMI_DOORWINDOW_OPENED}_{VariableExtension.DEVICE_ID}")
                {
                    _client.Publish(VariableExtension.ON_OPENED_TOPIC, e.Message);
                }
            };

            _client.Subscribe(new string[] { VariableExtension.STATUS_TOPIC }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            _client.Subscribe(new string[] { $"{XIAOMI_DOORWINDOW_CLOSED}_{VariableExtension.DEVICE_ID}" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            _client.Subscribe(new string[] { $"{XIAOMI_DOORWINDOW_OPENED}_{VariableExtension.DEVICE_ID}" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });

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
