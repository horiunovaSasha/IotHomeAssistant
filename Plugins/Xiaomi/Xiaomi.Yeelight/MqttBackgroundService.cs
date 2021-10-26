using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using YeelightAPI.Models;

namespace Xiaomi.Yeelight
{
    public class MqttBackgroundService : IHostedService, IDisposable
    {
        private const string MQTT_CLIENT_ID = "Xiaomi.Yeelight";

        private readonly MqttClient _client;

        public MqttBackgroundService()
        {
            _client = new MqttClient(VariableExtension.MQTT_ADDR);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _client.Connect(MQTT_CLIENT_ID + VariableExtension.IP_ADDRESS);

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

                if (payload.Command == "set_power")
                {
                    var toggle = false;
                    if (bool.TryParse(payload.Value, out toggle)) {
                        var light = new YeelightAPI.Device(VariableExtension.IP_ADDRESS, 55443);
                        
                        if (await light.Connect())
                        {
                            await light.SetPower(toggle);
                            light.Disconnect();
                        }
                    }
                }

                if (payload.Command == "set_brightness")
                {
                    int brightness = 0;
                    if (int.TryParse(payload.Value, out brightness))
                    {
                        var light = new YeelightAPI.Device(VariableExtension.IP_ADDRESS, 55443);

                        if (await light.Connect())
                        {
                            await light.SetBrightness(brightness);
                            light.Disconnect();
                        }
                    }
                }

                if (payload.Command == "set_color")
                {
                    if (!string.IsNullOrWhiteSpace(payload.Value))
                    {
                        var light = new YeelightAPI.Device(VariableExtension.IP_ADDRESS, 55443);

                        if (await light.Connect())
                        {
                            var color = ColorTranslator.FromHtml(payload.Value);
                            if (!color.IsEmpty)
                            {
                                await light.SetRGBColor(color.R, color.G, color.B);
                            }

                            light.Disconnect();
                        }
                    }
                }
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
                var light = new YeelightAPI.Device(VariableExtension.IP_ADDRESS, 55443);

                if (await light.Connect())
                {
                    var status = new
                    {
                        Toggle = await light.GetProp(PROPERTIES.power),
                        Color = await light.GetProp(PROPERTIES.rgb),
                        Brightness = await light.GetProp(PROPERTIES.bright)
                    };

                    var message = JsonConvert.SerializeObject(status);

                    _client.Publish(VariableExtension.SEND_STATUS_TOPIC, Encoding.UTF8.GetBytes(message));
                    light.Disconnect();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}