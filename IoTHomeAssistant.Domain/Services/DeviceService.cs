using IoTHomeAssistant.Domain.Dto;
using IoTHomeAssistant.Domain.Dto.Pagging;
using IoTHomeAssistant.Domain.Enums;
using IoTHomeAssistant.Domain.Options;
using IoTHomeAssistant.Domain.Repositories;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace IoTHomeAssistant.Domain.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly string _mqttBrokerAddress;

        public DeviceService(
            IDeviceRepository deviceRepository,
            IOptions<MqttOption> options)
        {
            _deviceRepository = deviceRepository;
            _mqttBrokerAddress = options.Value.MqttBrokerAddress;
        }

        public async Task SaveDeviceAsync(DeviceEditDto deviceDto)
        {
            var device = new Entities.Device()
            {
                Id = deviceDto.Id,
                Title = deviceDto.Title,
                Type = Enum.Parse<DeviceTypeEnum>(deviceDto.Type),
                PluginDevice = new Entities.PluginDevice()
                {
                    PluginId = deviceDto.Plugin,
                    DeviceId = deviceDto.Id,
                    Configurations = deviceDto.Configurations.Select(x =>
                        new Entities.PluginDeviceConfiguration()
                        {
                            PluginConfigurationId = x.Id,
                            Value = x.Value
                        }
                    ).ToList()
                }
            };

            if (deviceDto.Id == 0)
            {
                await _deviceRepository.AddAsync(device);
                await _deviceRepository.CommitAsync();

                deviceDto.Id = device.Id;

                StartDockerImage(deviceDto);
            }
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
                    deviceEvents.Add(new DeviceEventDto()
                    {
                        DeviceId = deviceId,
                        DeviceName = deviceName,
                        EventId = eventItem.Id,
                        EventTitle = eventItem.Title,
                        EventType = eventItem.Type
                    });
                }
            }

            return deviceEvents;
        }

        public async Task<PageResponse<DeviceDto>> GetPaggedList(PageRequest request)
        {
            return await _deviceRepository.GetPaggedList(request);
        }

        public async Task RemoveDeviceAsync(int id)
        {
            await _deviceRepository.DeleteAsync(id);
            await _deviceRepository.CommitAsync();
        }

        private Task StartDockerImage(DeviceEditDto device)
        {
            var envParams = new List<string>()
            {
                $"--env MQTT_ADDR={_mqttBrokerAddress}",
                $"--env CMD_TOPIC=CMD_{device.Type}_{device.Id}",
                $"--env STATUS_TOPIC=GET_STATUS_{device.Type}_{device.Id}",
                $"--env SEND_STATUS_TOPIC=RECEIVE_EVENTS_{device.Type}_{device.Id}"
            };

            foreach(var conf in device.Configurations)
            {
                envParams.Add($"--env {conf.Key}=\"{conf.Value}\"");
            }

            return Task.Run(() => {
                Cmd("docker", $"run {string.Join(" ", envParams)} --name {device.DockerImageId}_{device.Id} {device.DockerImageId}");
            });
        }

        private Process Cmd(string filename, string command)
        {
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = filename,
                    Arguments = command,
                    UseShellExecute = false,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                }
            };

            process.Start();
            process.WaitForExit();

            var error = process.StandardError.ReadToEnd();
            var output = process.StandardOutput.ReadToEnd();

            return process;
        }
    }
}
