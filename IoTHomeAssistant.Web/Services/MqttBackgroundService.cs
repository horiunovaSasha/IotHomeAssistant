using IoTHomeAssistant.Domain.Entities;
using IoTHomeAssistant.Domain.Enums;
using IoTHomeAssistant.Domain.Options;
using IoTHomeAssistant.Domain.Repositories;
using IoTHomeAssistant.Web.Hubs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace IoTHomeAssistant.Web.Services
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
            NotificationHub notificationHub = null;

            //using (var scope = _serviceScopeFactory.CreateScope())
            //{
            //    var topicRepository = scope.ServiceProvider.GetRequiredService<IDeviceMqttTopicRepository>();
            //    notificationHub = scope.ServiceProvider.GetRequiredService<NotificationHub>();
            //}

            //_client.Connect(MQTT_CLIENT_ID);

            //foreach (var deviceTopic in deviceTopics.Where(x => x.TopicType == MqttTopicTypeEnum.Subscribe))
            //{
            //    _client.MqttMsgPublishReceived += (object sender, MqttMsgPublishEventArgs e) =>
            //    {
            //        if (e.Topic == deviceTopic.Topic)
            //        {
            //            var message = Encoding.UTF8.GetString(e.Message);
            //            notificationHub.NotifyDevice(deviceTopic.Id, message).Wait();
            //        }
            //    };

            //    _client.Subscribe(new string[] { deviceTopic.Topic }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            //}

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
