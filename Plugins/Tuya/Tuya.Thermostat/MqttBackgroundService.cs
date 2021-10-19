using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tuya.Request;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Tuya.Thermostat
{
    public class MqttBackgroundService : IHostedService, IDisposable
    {
        private const string MQTT_CLIENT_ID = "Tuya.Thermostat";

        private readonly MqttClient _client;

        public MqttBackgroundService()
        {
            _client = new MqttClient(VariableExtension.MQTT_ADDR);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _client.Connect(MQTT_CLIENT_ID + VariableExtension.DEVICE_ID);

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

                var tuya = new TuyaClient(VariableExtension.CLIENT_KEY, VariableExtension.CLIENT_SECRET);
                await tuya.Authorize();

                await tuya.SendCommands(
                    VariableExtension.DEVICE_ID,
                    new Commands(
                    new List<Request.Command>() {
                        new Request.Command("switch", command.Toggle),
                        new Request.Command("temp_set", command.Temperature *2)
                    }));
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
                var tuya = new TuyaClient(VariableExtension.CLIENT_KEY, VariableExtension.CLIENT_SECRET);
                await tuya.Authorize();

                var devStatus = await tuya.GetDeviceStatus(VariableExtension.DEVICE_ID);

                var status = new Status()
                {
                    Toggle = bool.Parse(devStatus.First(x => x.Key == "switch").Value),
                    CurrentTemperature = int.Parse(devStatus.First(x => x.Key == "upper_temp").Value) / 2,
                    TargetTemperature = int.Parse(devStatus.First(x => x.Key == "temp_set").Value) / 2
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
