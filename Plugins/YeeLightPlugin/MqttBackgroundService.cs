using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace YeeLightPlugin
{
    public class MqttBackgroundService : IHostedService, IDisposable
    {
        private const string MQTT_CLIENT_ID = "YeeLightPlugin";

        private readonly MqttClient _client;

        public MqttBackgroundService()
        {
            _client = new MqttClient(VariableExtension.MQTT_ADDR);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _client.Connect(MQTT_CLIENT_ID + VariableExtension.IP_ADDR);

            _client.MqttMsgPublishReceived += async (object sender, MqttMsgPublishEventArgs e) =>
            {
                if (e.Topic == VariableExtension.CMD_TOPIC)
                {
                    await ExecCommand(e.Message);
                }

                if (e.Topic == VariableExtension.STATUS_TOPIC)
                {
                    await SendStatus();
                }
            };

            _client.Subscribe(new string[] { VariableExtension.CMD_TOPIC }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            _client.Subscribe(new string[] { VariableExtension.STATUS_TOPIC }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });

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

        private async Task ExecCommand(byte[] msg)
        {
            try
            {
                var message = Encoding.UTF8.GetString(msg);
                var command = JsonConvert.DeserializeObject<Command>(message);

                await YeeLightDevice.Exec(command);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task SendStatus()
        {
            try
            {
                var deviceInfo = await YeeLightDevice.GetStatus();
                var status = new {
                    Available = deviceInfo != null,
                    Info = deviceInfo
                };

                var json = JsonConvert.SerializeObject(status);

                _client.Publish(VariableExtension.STATUS_TOPIC + "-echo", Encoding.UTF8.GetBytes(json));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
