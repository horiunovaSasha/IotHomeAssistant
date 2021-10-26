using Microsoft.Extensions.Hosting;
using MiHomeLib;
using MiHomeLib.Devices;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Xiaomi.Temperature
{
    public class MqttBackgroundService : IHostedService, IDisposable
    {
        private const string MQTT_CLIENT_ID = "Xiaomi.Temperature";

        private readonly MqttClient _client;
        private readonly MiHome _miHome;
        private ThSensor _thSensor;

        public MqttBackgroundService()
        {
            _client = new MqttClient(VariableExtension.MQTT_ADDR);
            _miHome = new MiHome(VariableExtension.HUB_ID);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _client.Connect(MQTT_CLIENT_ID + VariableExtension.DEVICE_ID);

            _client.MqttMsgPublishReceived += async (object sender, MqttMsgPublishEventArgs e) =>
            {
                if (e.Topic == VariableExtension.STATUS_TOPIC)
                {
                    await SendStatus();
                }
            };

            _client.Subscribe(new string[] { VariableExtension.STATUS_TOPIC }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });

            _miHome.OnThSensor += (_, thSensor) =>
            {
                if (thSensor.Sid == VariableExtension.DEVICE_ID)
                {
                    if (_thSensor == null)
                    {
                        _thSensor = thSensor;
                    }

                    thSensor.OnTemperatureChange += (_, e) =>
                    {
                        _client.Publish(VariableExtension.SEND_STATUS_TOPIC, Encoding.UTF8.GetBytes(e.Temperature.ToString()));
                    };
                }
            };

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

        private async Task SendStatus(bool untilStop = false)
        {
            if (_thSensor != null)
            {
                _client.Publish(VariableExtension.SEND_STATUS_TOPIC, 
                    Encoding.UTF8.GetBytes(_thSensor.Temperature.ToString()));
            }
        }
    }
}
