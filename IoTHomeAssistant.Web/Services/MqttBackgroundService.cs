using IoTHomeAssistant.Domain.Entities;
using IoTHomeAssistant.Domain.Enums;
using IoTHomeAssistant.Domain.Repositories;
using IoTHomeAssistant.Web.Hubs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MQTTnet;
using MQTTnet.Client.Options;
using MQTTnet.Extensions.ManagedClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IoTHomeAssistant.Web.Services
{
    public class MqttBackgroundService : IHostedService, IDisposable
    {
        private const string MQTT_CLIENT_ID = "IoTHomeAssistant";
        private const int AUTO_RECONNECT_DELAY = 5;
        private const int MQTT_SERVER_PORT = 1883;

        private readonly IServiceScopeFactory _serviceScopeFactory;

        public MqttBackgroundService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            NotificationHub notificationHub = null;
            var deviceTopics = new List<DeviceMqttTopic>();

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var topicRepository = scope.ServiceProvider.GetRequiredService<IDeviceMqttTopicRepository>();
                notificationHub = scope.ServiceProvider.GetRequiredService<NotificationHub>();
                deviceTopics = topicRepository.GetAllWithBrokerInfo();
            }

            foreach (var deviceTopic in deviceTopics.Where(x => x.TopicType == MqttTopicTypeEnum.Subscribe))
            {
                var options = new ManagedMqttClientOptionsBuilder()
                    .WithAutoReconnectDelay(TimeSpan.FromSeconds(AUTO_RECONNECT_DELAY))
                    .WithClientOptions(
                        new MqttClientOptionsBuilder()
                        .WithClientId(MQTT_CLIENT_ID + deviceTopic.Id)
                        .WithTcpServer(deviceTopic.MqttBroker.Address, MQTT_SERVER_PORT)
                        .Build())
                    .Build();

                var subscriber = new MqttFactory().CreateManagedMqttClient();

                subscriber.UseApplicationMessageReceivedHandler(message =>
                    notificationHub.NotifyDevice(
                        deviceTopic.Id,
                        message.ApplicationMessage.ConvertPayloadToString()));

                await subscriber.SubscribeAsync(new MqttTopicFilterBuilder()
                    .WithTopic(deviceTopic.Topic)
                    .Build());

                await subscriber.StartAsync(options);
            }

            await Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {
        }
    }
}
