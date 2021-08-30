using IoTHomeAssistant.Domain.Dto;
using IoTHomeAssistant.Domain.Options;
using IoTHomeAssistant.Domain.Repositories;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace IoTHomeAssistant.Domain.Services
{
    public class DeviceService : IDeviceService
    {
        private const string MQTT_CLIENT_ID = "IoTHomeAssistant";

        private readonly IDeviceRepository _deviceRepository;
        private readonly IDeviceMqttTopicRepository _deviceMqttTopicRepository;
        private readonly string _mqttBrokerAddress;

        public DeviceService(
            IDeviceRepository deviceRepository, 
            IDeviceMqttTopicRepository deviceMqttTopicRepository,
            IOptions<MqttOption> options)
        {
            _deviceRepository = deviceRepository;
            _deviceMqttTopicRepository = deviceMqttTopicRepository;
            _mqttBrokerAddress = options.Value.MqttBrokerAddress;
        }

        public List<InfoDevice> GetInfoDevices()
        {
            return _deviceRepository.GetInfoDevices();
        }

        public void LightControl(int deviceId, bool toggle, int brightness, string color)
        {
            var device = _deviceRepository.Get(deviceId);
            if (device != null)
            {
                var client = new MqttClient(_mqttBrokerAddress);
                client.Connect(MQTT_CLIENT_ID + deviceId);

                var payload = JsonSerializer.Serialize(new {toggle, brightness, color});

                client.Publish($"light-cmd-{deviceId}", Encoding.UTF8.GetBytes(payload), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
            }
        }

        public void Toggle(int deviceId, bool toggle)
        {
            var device = _deviceRepository.Get(deviceId);
            if (device != null)
            {
                var client = new MqttClient(_mqttBrokerAddress);
                client.Connect(MQTT_CLIENT_ID + deviceId);

                var payload = JsonSerializer.Serialize(new { toggle });

                client.Publish($"toggle-cmd-{deviceId}", Encoding.UTF8.GetBytes(payload), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
            }
        }

        public List<Entities.Device> GetAllDevices()
        {
           return _deviceRepository.Get().ToList();
        }
    }
}
