using IoTHomeAssistant.Domain.Dto;
using IoTHomeAssistant.Domain.Options;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
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
        private readonly JobTaskBackgroundService _jobTaskBackgroundService;
        private readonly MqttClient _client;

        public MqttBackgroundService(
            IServiceScopeFactory serviceScopeFactory, 
            JobTaskBackgroundService jobTaskBackgroundService,
            IOptions<MqttOption> options)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _jobTaskBackgroundService = jobTaskBackgroundService;
            _client = new MqttClient(options.Value.MqttBrokerAddress);
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var deviceService = scope.ServiceProvider.GetRequiredService<IDeviceService>();
                var deviceEvents = await deviceService.GetDeviceEventsAsync();
                var devices = await deviceService.GetDevicesAsync(null);
                var eventPublisher = new HubConnectionBuilder()
                   .WithUrl(new Uri("https://localhost:5001/event-publisher"))
                   .Build();

                _client.Connect(MQTT_CLIENT_ID);
                await eventPublisher.StartAsync();

                foreach (var device in devices)
                {
                    var eventName = $"RECEIVE_EVENTS_{device.Type}_{device.Id}";

                    _client.MqttMsgPublishReceived += (object sender, MqttMsgPublishEventArgs e) =>
                    {
                        if (e.Topic == eventName)
                        {
                            var payload = JsonConvert.DeserializeObject<EventPayload>(Encoding.UTF8.GetString(e.Message));
                            if (eventPublisher.State != HubConnectionState.Connected)
                            {
                                eventPublisher.StartAsync().Wait();
                            }

                            eventPublisher.SendAsync("PublishEvent", $"{payload.Event}_{device.Id}", payload.Value).Wait();
                            _jobTaskBackgroundService.OnEvent(device.Id, payload.Event, payload.Value?.ToString());
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
