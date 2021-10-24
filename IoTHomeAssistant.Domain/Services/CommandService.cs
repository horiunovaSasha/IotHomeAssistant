using IoTHomeAssistant.Domain.Options;
using IoTHomeAssistant.Domain.Repositories;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;

namespace IoTHomeAssistant.Domain.Services
{
    public class CommandService : ICommandService
    {
        private const string MQTT_CLIENT_ID = "IoTHomeAssistant";
        
        private readonly MqttOption _options;
        private readonly IDeviceRepository _deviceRepository;

        public CommandService(IDeviceRepository deviceRepository, IOptions<MqttOption> options) {
            
            _deviceRepository = deviceRepository;
            _options = options.Value;
        }

        public async Task Exec(int deviceId, string command, object value = null)
        {
            var payload = Encoding.UTF8.GetBytes(
                JsonConvert.SerializeObject(new { command, value }));

            await Publish(deviceId, "control", payload);
        }

        public async Task GetStatus(int deviceId)
        {
            await Publish(deviceId, "status", new byte[] { });
        }

        private async Task Publish(int deviceId, string topic, byte[] payload)
        {
            var device = await _deviceRepository.GetAsync(deviceId);

            if (device != null)
            {
                var client = new MqttClient(_options.MqttBrokerAddress);
                client.Connect(MQTT_CLIENT_ID + deviceId);

                client.Publish($"{device.Type.ToString().ToLower()}_{topic}_{deviceId}", payload);
            }
        }
    }
}
