using Gree.AirConditioner.Device;
using Gree.AirConditioner.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using MQTTnet.Client.Subscribing;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gree.AirConditioner
{
    public class MqttBackgroundService : IHostedService, IDisposable
    {
        private const string MQTT_CLIENT_ID = "Gree.AirConditioner";

        private IMqttClient _mqttClient;
        private bool _completelyDisconnect = false;

        private readonly Controller _controller;
        private readonly ILogger _logger;        

        public MqttBackgroundService()
        {
            _controller = new Controller(
                VariableExtension.DEVICE_ID,
                VariableExtension.IP_ADDR,
                VariableExtension.SECRET_KEY);

            _controller.DeviceStatusChanged += DeviceStatusChanged;
            _logger = Logger.CreateDefaultLogger();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var options = new MqttClientOptionsBuilder()
                .WithClientId(MQTT_CLIENT_ID + VariableExtension.DEVICE_ID)
                .WithTcpServer(VariableExtension.MQTT_ADDR)
                .WithCredentials(VariableExtension.MQTT_USR, VariableExtension.MQTT_PWD)
                .Build();

            _mqttClient = new MqttFactory().CreateMqttClient();

            _mqttClient.UseConnectedHandler(async e =>
            {
                await _mqttClient.SubscribeAsync(new MqttClientSubscribeOptionsBuilder()
                    .WithTopicFilter(VariableExtension.STATUS_TOPIC).Build());
                
                await _mqttClient.SubscribeAsync(new MqttClientSubscribeOptionsBuilder()
                    .WithTopicFilter(VariableExtension.CMD_TOPIC).Build());
            });

            _mqttClient.UseApplicationMessageReceivedHandler(async e =>
            {
                if (e.ApplicationMessage.Topic == VariableExtension.CMD_TOPIC)
                {
                    var message = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                    var payload = JsonConvert.DeserializeObject<CommandPayload>(message);

                    if (payload.Command == Translation.SET_POWER)
                    {
                        bool toggle;
                        if (bool.TryParse(payload.Value, out toggle))
                        {
                            await _controller.SetDeviceParameter(
                                Translation.MqttToAirCmd[payload.Command],
                                toggle ? 1 : 0);
                        }
                    }

                    if (payload.Command == Translation.SET_TEMPERATURE)
                    {
                        int temperature;
                        if (int.TryParse(payload.Value, out temperature))
                        {
                            await _controller.SetDeviceParameter(
                                Translation.MqttToAirCmd[payload.Command],
                                temperature);
                        }
                    }

                    if (payload.Command == Translation.SET_AIR_MODE)
                    {
                        if (Translation.MqttToAirMode.ContainsKey(payload.Value))
                        {
                            await _controller.SetDeviceParameter(
                                Translation.MqttToAirCmd[payload.Command],
                                Translation.MqttToAirMode[payload.Value]);
                        }
                    }

                    if (payload.Command == Translation.SET_AIR_SPEED)
                    {
                        if (Translation.MqttToAirSpeed.ContainsKey(payload.Value))
                        {
                            await _controller.SetDeviceParameter(
                                Translation.MqttToAirCmd[payload.Command],
                                Translation.MqttToAirSpeed[payload.Value]);
                        }
                    }
                }

                if (e.ApplicationMessage.Topic == VariableExtension.STATUS_TOPIC)
                {
                    await _controller.UpdateDeviceStatus();
                }
            });

            _mqttClient.UseDisconnectedHandler(async e =>
            {
                if (!_completelyDisconnect)
                {
                    await Task.Delay(TimeSpan.FromSeconds(1));

                    try
                    {
                        await _mqttClient.ConnectAsync(options, cancellationToken);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError("Mqtt reconnecting failed...", ex);
                    }
                }
            });


            await _mqttClient.ConnectAsync(options, cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _completelyDisconnect = true;
            await _mqttClient.DisconnectAsync(cancellationToken);
        }

        public void Dispose()
        {
            _completelyDisconnect = true;
            _mqttClient.DisconnectAsync(CancellationToken.None).Wait();
        }

        private void DeviceStatusChanged(object sender, DeviceStatusChangedEventArgs e)
        {
            foreach(var prop in _controller.Parameters)
            {
                if (Translation.AirToMqttCmd.ContainsKey(prop.Key))
                {
                    var value = prop.Value;
                    byte[] payload = new byte[] { };
                    var eventName = Translation.AirToMqttCmd[prop.Key];

                    if (prop.Key == Translation.GET_POWER)
                    {
                        payload = Encoding.UTF8.GetBytes(
                            JsonConvert.SerializeObject(new {
                                Event = eventName,
                                Value = value == 1
                            }));
                    }

                    if (prop.Key == Translation.GET_TEMPERATURE)
                    {
                        payload = Encoding.UTF8.GetBytes(
                            JsonConvert.SerializeObject(new
                            {
                                Event = eventName,
                                Value = value
                            }));
                    }

                    if (prop.Key == Translation.GET_AIR_MODE)
                    {
                        payload = Encoding.UTF8.GetBytes(
                            JsonConvert.SerializeObject(new
                            {
                                Event = eventName,
                                Value = Translation.AirToMqttMode.ContainsKey(value) 
                                    ? Translation.AirToMqttMode[value] 
                                    : Translation.AirToMqttMode[0]
                            }));
                    }

                    if (prop.Key == Translation.GET_AIR_SPEED)
                    {
                        payload = Encoding.UTF8.GetBytes(
                            JsonConvert.SerializeObject(new
                            {
                                Event = eventName,
                                Value = Translation.AirToMqttSpeed.ContainsKey(value)
                                    ? Translation.AirToMqttSpeed[value]
                                    : Translation.AirToMqttSpeed[0]
                            }));
                    }

                    var message = new MqttApplicationMessageBuilder()
                        .WithTopic(VariableExtension.SEND_STATUS_TOPIC)
                        .WithPayload(payload)
                        .WithExactlyOnceQoS()
                        .WithRetainFlag()
                        .Build();

                    _mqttClient.PublishAsync(message, CancellationToken.None);
                }
            }
        }
    }
}