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
                var payload = JsonConvert.DeserializeObject<CommandPayload>(message);

                var tuya = new TuyaClient(VariableExtension.CLIENT_KEY, VariableExtension.CLIENT_SECRET);
                await tuya.Authorize();

                if (payload.Command == "set_power")
                {
                    await tuya.SendCommands(
                        VariableExtension.DEVICE_ID,
                        new Commands(
                        new List<Command>() {
                            new Command("switch", payload.Value)
                        }));

                    _client.Publish(VariableExtension.SEND_STATUS_TOPIC, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(
                       new
                       {
                           Event = "power_changed",
                           Value = payload.Value
                       })
                   ));
                }

                if (payload.Command == "set_temperature")
                {
                    await tuya.SendCommands(
                        VariableExtension.DEVICE_ID,
                        new Commands(
                        new List<Command>() {
                            new Command("temp_set", int.Parse(payload.Value.ToString()) *2)
                        }));

                    _client.Publish(VariableExtension.SEND_STATUS_TOPIC, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(
                       new
                       {
                           Event = "target_temperature_changed",
                           Value = payload.Value
                       })
                   ));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task SendStatus()
        {
            //try
            {
                var tuya = new TuyaClient(VariableExtension.CLIENT_KEY, VariableExtension.CLIENT_SECRET);
                await tuya.Authorize();

                var devStatus = await tuya.GetDeviceStatus(VariableExtension.DEVICE_ID);

                _client.Publish(VariableExtension.SEND_STATUS_TOPIC, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(
                    new { 
                        Event = "power_changed",
                        Value = devStatus.First(x => x.Key == "switch").Value
                    })
                ));

                _client.Publish(VariableExtension.SEND_STATUS_TOPIC, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(
                    new
                    {
                        Event = "upper_temperature_changed",
                        Value = int.Parse(devStatus.First(x => x.Key == "upper_temp").Value) / 2
                    })
                ));

                _client.Publish(VariableExtension.SEND_STATUS_TOPIC, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(
                    new
                    {
                        Event = "target_temperature_changed",
                        Value = int.Parse(devStatus.First(x => x.Key == "temp_set").Value) / 2
                    })
                ));
            }
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
        }
    }
}
