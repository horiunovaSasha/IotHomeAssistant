using IoTHomeAssistant.Domain.Dto;
using IoTHomeAssistant.Domain.Repositories;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace IoTHomeAssistant.Domain.Services
{
    public class DeviceService : IDeviceService
    {
        private const string MQTT_BROKER_ADDRESS = "192.168.1.226";
        private const string MQTT_CLIENT_ID = "IoTHomeAssistant";

        private readonly IDeviceRepository _deviceRepository;
        private readonly IDeviceMqttTopicRepository _deviceMqttTopicRepository;

        public DeviceService(
            IDeviceRepository deviceRepository, 
            IDeviceMqttTopicRepository deviceMqttTopicRepository)
        {
            _deviceRepository = deviceRepository;
            _deviceMqttTopicRepository = deviceMqttTopicRepository;
        }

        public List<InfoDevice> GetInfoDevices()
        {
            return _deviceRepository.GetInfoDevices();
        }

        public async Task YeelightControl(int deviceId, bool toggle, int brightness, string color)
        {
            var device = _deviceRepository.Get(deviceId);
            if (device != null && !string.IsNullOrWhiteSpace(device.IPAddress))
            {
                var light = new YeelightAPI.Device(device.IPAddress, 55443);
                if (await light.Connect())
                {
                    if (toggle)
                    {
                        await light.SetPower(true);
                        await light.SetBrightness(brightness);

                        if (!string.IsNullOrWhiteSpace(color))
                        {
                            var rgb = ColorTranslator.FromHtml(color);
                            await light.SetRGBColor(rgb.R, rgb.G, rgb.B);
                        }
                    }
                    else
                    {
                        await light.SetPower(false);
                    }

                    light.Disconnect();
                }
            }
        }

        public void Toggle(int topicId, bool toggle)
        {
            var topic = _deviceMqttTopicRepository.GetWitDevice(topicId);
            if (topic != null && topic.Device != null)
            {
                var device = _deviceRepository.GetWithTopics(topic.Device.Id);
                if (device != null && device.Topics.Any())
                {
                    var publichTopic = device.Topics.FirstOrDefault(x => x.TopicType == Enums.MqttTopicTypeEnum.Publish);

                    if (publichTopic != null)
                    {
                        var client = new MqttClient(MQTT_BROKER_ADDRESS);
                        client.Connect(MQTT_CLIENT_ID + device.Id);

                        client.Publish(publichTopic.Topic, Encoding.UTF8.GetBytes(toggle.ToString()), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
                    }
                }
            }
        }
    }
}
