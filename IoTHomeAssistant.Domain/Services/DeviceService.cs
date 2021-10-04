using IoTHomeAssistant.Domain.Dto;
using IoTHomeAssistant.Domain.Dto.Pagging;
using IoTHomeAssistant.Domain.Enums;
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

        public Task AddDeviceAsync(Entities.Device device)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Entities.Device> GetDeviceAsync(int id)
        {
            return await _deviceRepository.GetDeviceAsync(id);
        }

        public async Task<List<Entities.Device>> GetDevicesAsync(DeviceTypeEnum? deviceType = null)
        {
            return await _deviceRepository.GetDevicesAsync(deviceType);
        }

        public async Task<List<DeviceEventDto>> GetDeviceEventsAsync(bool? hasValue)
        {
            var deviceEvents = new List<DeviceEventDto>();
            var devices = (await GetDevicesAsync())
                .Where(x => 
                    x.DeviceEvents != null &&
                    x.DeviceEvents.EventCollection != null &&
                    x.DeviceEvents.EventCollection.Events != null &&
                    x.DeviceEvents.EventCollection.Events.Any(e => !hasValue.HasValue || e.HasValue == hasValue.Value));

            foreach (var device in devices)
            {
                var deviceId = device.Id;
                var deviceName = device.Title;

                foreach (var eventItem in device.DeviceEvents.EventCollection.Events.Where(e => !hasValue.HasValue || e.HasValue == hasValue.Value))
                {
                    deviceEvents.Add(new DeviceEventDto() {
                        DeviceId = deviceId,
                        DeviceName = deviceName, 
                        EventId = eventItem.Id, 
                        EventTitle = eventItem.Title 
                    });
                }
            }

            return deviceEvents;
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

        public async Task RemoveDeviceAsync(int id)
        {
            await _deviceRepository.DeleteAsync(id);
            await _deviceRepository.CommitAsync();
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

        public Task UpdateDeviceAsync(Entities.Device device)
        {
            throw new System.NotImplementedException();
        }
    }
}
