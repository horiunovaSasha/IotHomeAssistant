using IoTHomeAssistant.Domain.Dto;
using IoTHomeAssistant.Domain.Dto.Pagging;
using IoTHomeAssistant.Domain.Options;
using IoTHomeAssistant.Domain.Repositories;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace IoTHomeAssistant.Domain.Services
{
    public class DeviceService : IDeviceService
    {
        private const string MQTT_CLIENT_ID = "IoTHomeAssistant";

        private readonly IDeviceRepository _deviceRepository;
        private readonly string _mqttBrokerAddress;

        public DeviceService(
            IDeviceRepository deviceRepository, 
            IOptions<MqttOption> options)
        {
            _deviceRepository = deviceRepository;
            _mqttBrokerAddress = options.Value.MqttBrokerAddress;
        }

        public async Task<Entities.Device> GetDeviceAsync(int id)
        {
            return await _deviceRepository.GetDeviceAsync(id);
        }

        public List<InfoDevice> GetInfoDevices()
        {
            return _deviceRepository.GetInfoDevices();
        }

        public async Task<PageResponse<DeviceDto>> GetPaggedList(PageRequest request)
        {
            return await _deviceRepository.GetPaggedList(request);
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
    }
}
