using Microsoft.Extensions.Hosting;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using MQTTnet.Client.Subscribing;
using Newtonsoft.Json;
using System.Text;

namespace Zigbee2Mqtt.DoubleSwitch
{
    public class MqttBackgroundService : IHostedService, IDisposable
    {
        private const string MQTT_CLIENT_ID = "Zigbee2Mqtt.Switch";
        private const string SET_POWER = "set_power";
        private const string SET_POWER_TOGGLE = "set_power_toggle";
        private const string POWER_CHANGED = "power_changed";

        private IMqttClient _mqttClient;
        private bool _completelyDisconnect = false;

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

                await _mqttClient.SubscribeAsync(new MqttClientSubscribeOptionsBuilder()
                    .WithTopicFilter($"zigbee2mqtt/{VariableExtension.DEVICE_ID}").Build());
            });

            _mqttClient.UseApplicationMessageReceivedHandler(async e =>
            {
                if (e.ApplicationMessage.Topic == VariableExtension.CMD_TOPIC)
                {
                    var message = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                    var payload = JsonConvert.DeserializeObject<CommandPayload>(message);

                    if (payload.Command == SET_POWER)
                    {
                        var toggle = Encoding.UTF8.GetBytes("{\"state_" + VariableExtension.SIDE + "\": \"" + (payload.Value ? "ON" : "OFF") + "\"}");
                        var cmd = new MqttApplicationMessageBuilder()
                         .WithTopic($"zigbee2mqtt/{VariableExtension.DEVICE_ID}/set")
                         .WithPayload(toggle)
                         .Build();

                        await _mqttClient.PublishAsync(cmd, CancellationToken.None);
                    }
                    
                    if (payload.Command == SET_POWER_TOGGLE)
                    {
                        var toggle = Encoding.UTF8.GetBytes("{\"state_" + VariableExtension.SIDE + "\": \"TOGGLE\"}");
                        var cmd = new MqttApplicationMessageBuilder()
                         .WithTopic($"zigbee2mqtt/{VariableExtension.DEVICE_ID}/set")
                         .WithPayload(toggle)
                         .Build();

                        await _mqttClient.PublishAsync(cmd, CancellationToken.None);
                    }
                }

                if (e.ApplicationMessage.Topic == VariableExtension.STATUS_TOPIC)
                {
                    var message = Encoding.UTF8.GetBytes("{\"state_" + VariableExtension.SIDE + "\": \"\"}");
                    var cmd = new MqttApplicationMessageBuilder()
                     .WithTopic($"zigbee2mqtt/{VariableExtension.DEVICE_ID}/get")
                     .WithPayload(message)
                     .Build();

                    await _mqttClient.PublishAsync(cmd, CancellationToken.None);
                }

                if (e.ApplicationMessage.Topic == $"zigbee2mqtt/{VariableExtension.DEVICE_ID}")
                {
                    var payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                    var status = JsonConvert.DeserializeObject<StatusPayload>(payload);
                    var state = VariableExtension.SIDE == "left" ? status?.State_Left : status?.State_Right;

                    var toggle = state == "ON";
                    var eventMsg = Encoding.UTF8.GetBytes(
                        JsonConvert.SerializeObject(new
                        {
                            Event = POWER_CHANGED,
                            Value = toggle
                        }));

                    var message = new MqttApplicationMessageBuilder()
                       .WithTopic(VariableExtension.SEND_STATUS_TOPIC)
                       .WithPayload(eventMsg)
                       .WithExactlyOnceQoS()
                       .WithRetainFlag()
                       .Build();

                    await _mqttClient.PublishAsync(message, CancellationToken.None);
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
                        Console.WriteLine($"Mqtt reconnecting failed...\n\r {ex.Message}");
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
    }
}
