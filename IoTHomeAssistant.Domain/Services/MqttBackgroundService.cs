using IoTHomeAssistant.Domain.Options;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace IoTHomeAssistant.Domain.Services
{
    public class MqttBackgroundService : IHostedService, IDisposable
    {
        private const string MQTT_CLIENT_ID = "IoTHomeAssistant";

        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly MqttClient _client;

        public MqttBackgroundService(IServiceScopeFactory serviceScopeFactory, IOptions<MqttOption> options)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _client = new MqttClient(options.Value.MqttBrokerAddress);
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var deviceService = scope.ServiceProvider.GetRequiredService<IDeviceService>();
                var deviceEvents = await deviceService.GetDeviceEventsAsync(null);
                var eventPublisher = new HubConnectionBuilder()
                   .WithUrl(new Uri("https://localhost:5001/event-publisher"))
                   .Build();

                _client.Connect(MQTT_CLIENT_ID);
                await eventPublisher.StartAsync();

                foreach (var deviceEvent in deviceEvents)
                {
                    var eventName = $"Event_{deviceEvent.DeviceId}_{deviceEvent.EventId}";

                    _client.MqttMsgPublishReceived += (object sender, MqttMsgPublishEventArgs e) =>
                    {
                        if (e.Topic == eventName)
                        {
                            var payload = e.Message != null && e.Message.Length > 0 ? Encoding.UTF8.GetString(e.Message): string.Empty;
                            eventPublisher.SendAsync("PublishEvent", eventName, payload).Wait();
                        }
                    };

                    _client.Subscribe(new string[] { eventName }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
                }
            }

            await Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _client.Disconnect();
        }
    }
}
