using Innovative.Geometry;
using Innovative.SolarCalculator;
using Microsoft.Extensions.Hosting;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using MQTTnet.Client.Subscribing;
using Newtonsoft.Json;
using System.Text;

namespace SolarPlugin
{
    public class MqttBackgroundService : IHostedService, IDisposable
    {
        private const string MQTT_CLIENT_ID = "SolarPlugin";
        private const string SUNRISE_OCCURRED = "sunrise_occurred";
        private const string SUNSET_OCCURRED = "sunset_occurred";

        private IMqttClient _mqttClient;

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var options = new MqttClientOptionsBuilder()
                .WithClientId(MQTT_CLIENT_ID + Guid.NewGuid().ToString())
                .WithTcpServer(VariableExtension.MQTT_ADDR)
                .WithCredentials(VariableExtension.MQTT_USR, VariableExtension.MQTT_PWD)
                .Build();

            _mqttClient = new MqttFactory().CreateMqttClient();

            if (decimal.TryParse(VariableExtension.LATITUDE, out var lat) && 
                decimal.TryParse(VariableExtension.LONGITUDE, out var lng)) {
                await PushNotification(lat, lng, options, cancellationToken);
            }
        }

        private async Task PushNotification(decimal lat, decimal lng, IMqttClientOptions options, CancellationToken cancellationToken)
        {
            var now = DateTime.Now;
            var solarTimes = new SolarTimes(now, new Angle(lat), new Angle(lng));
            var sunset = TimeZoneInfo.ConvertTimeFromUtc(solarTimes.Sunset.ToUniversalTime(), TimeZoneInfo.Local);
            var sunrise = TimeZoneInfo.ConvertTimeFromUtc(solarTimes.Sunrise.ToUniversalTime(), TimeZoneInfo.Local);

            if (now <= sunrise)
            {
                await Task.Delay(TimeSpan.FromSeconds((sunrise - now).TotalSeconds));
                await Notify(SUNRISE_OCCURRED, options, cancellationToken);
            }
            else if (now <= sunset){
                await Task.Delay(TimeSpan.FromSeconds((sunset - now).TotalSeconds));
                await Notify(SUNSET_OCCURRED, options, cancellationToken);
            } else
            {
                await Task.Delay(TimeSpan.FromHours(1));
            }

            await PushNotification(lat, lng, options, cancellationToken);
        }

        private async Task Notify(string eventName, IMqttClientOptions options, CancellationToken cancellationToken)
        {
            await _mqttClient.ConnectAsync(options, cancellationToken);

            var eventMsg = Encoding.UTF8.GetBytes(
                JsonConvert.SerializeObject(new {
                    Event = eventName,
                    Value = eventName == SUNRISE_OCCURRED ? "Настав схід сонця" : "Настав захід сонця"
                }));

            var message = new MqttApplicationMessageBuilder()
               .WithTopic(VariableExtension.SEND_STATUS_TOPIC)
               .WithPayload(eventMsg)
               .WithExactlyOnceQoS()
               .WithRetainFlag()
               .Build();

            await _mqttClient.PublishAsync(message, CancellationToken.None);
            await _mqttClient.DisconnectAsync(CancellationToken.None);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _mqttClient.DisconnectAsync(cancellationToken);
        }

        public void Dispose()
        {
            _mqttClient.DisconnectAsync(CancellationToken.None).Wait();
        }
    }
}