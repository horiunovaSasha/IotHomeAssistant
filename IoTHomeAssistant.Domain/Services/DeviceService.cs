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
        private readonly IDeviceTypeEventRepository _deviceEventRepository;
        private readonly IDeviceTypeCommandRepository _deviceCommandRepository;
        private readonly string _mqttBrokerAddress;

        public DeviceService(
            IDeviceRepository deviceRepository,
            IDeviceTypeEventRepository deviceEventRepository,
            IDeviceTypeCommandRepository deviceCommandRepository,
            IOptions<MqttOption> options)
        {
            _deviceRepository = deviceRepository;
            _deviceEventRepository = deviceEventRepository;
            _deviceCommandRepository = deviceCommandRepository;
            _mqttBrokerAddress = options.Value.MqttBrokerAddress;
        }

        public async Task SaveDeviceAsync(DeviceEditDto deviceDto)
        {
            var type = Enum.Parse<DeviceTypeEnum>(deviceDto.Type);
            var events = await _deviceEventRepository.GetItemsAsync(type);
            var commands = await _deviceCommandRepository.GetItemsAsync(type);

            var device = new Entities.Device()
            {
                Id = deviceDto.Id,
                Title = deviceDto.Title,
                Type = type,
                PluginDevice = new Entities.PluginDevice()
                {
                    Id = deviceDto.PluginDeviceId,
                    PluginId = deviceDto.Plugin,
                    DeviceId = deviceDto.Id,
                    Configurations = deviceDto.Configurations.Select(x =>
                        new Entities.PluginDeviceConfiguration()
                        {
                            Id = x.Id,
                            PluginDeviceId = deviceDto.PluginDeviceId,
                            PluginConfigurationId = x.PluginConfigurationId,
                            Value = x.Value
                        }
                    ).ToList()
                }
                
            };

            if (deviceDto.Id == 0)
            {
                device.DeviceEvents = events.Select(x =>
                    new Entities.DeviceEvent()
                    {
                        DeviceId = deviceDto.Id,
                        EventId = x.EventId
                    })
                    .ToList();

                device.DeviceCommands = commands.Select(x =>
                    new Entities.DeviceCommand()
                    {
                        DeviceId = deviceDto.Id,
                        CommandId = x.CommandId
                    })
                    .ToList();

                await _deviceRepository.AddAsync(device);
                await _deviceRepository.CommitAsync();

                deviceDto.Id = device.Id;

                StartDockerImage(deviceDto);
            }
            else {
                try
                {
                    await _deviceRepository.UpdateAsync(device);
                    await _deviceRepository.CommitAsync();
                } catch(Exception ex)
                {

                }

                UpdateDockerImage(deviceDto);
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

        public async Task<List<DeviceEventDto>> GetDeviceEventsAsync()
        {
            var deviceEvents = new List<DeviceEventDto>();
            var devices = (await GetDevicesAsync())
                .Where(x =>
                    x.DeviceEvents != null &&
                    x.DeviceEvents.Any());

            foreach (var device in devices)
            {
                var deviceId = device.Id;
                var deviceName = device.Title;

                foreach (var eventItem in device.DeviceEvents)
                {
                    deviceEvents.Add(new DeviceEventDto()
                    {
                        DeviceId = deviceId,
                        DeviceName = deviceName,
                        EventId = eventItem.Id,
                        EventTitle = eventItem.Event.Title,
                        EventType = eventItem.Event.Type,
                        ValueType = eventItem.Event.ValueType
                    });
                }
            }

            return deviceEvents;
        }

        public async Task<List<DeviceCommandDto>> GetDeviceCommandsAsync()
        {
            var deviceCommands = new List<DeviceCommandDto>();
            var devices = (await GetDevicesAsync())
                .Where(x =>
                    x.DeviceCommands != null &&
                    x.DeviceCommands.Any());

            foreach (var device in devices)
            {
                var deviceId = device.Id;
                var deviceName = device.Title;

                foreach (var item in device.DeviceCommands)
                {
                    deviceCommands.Add(new DeviceCommandDto()
                    {
                        DeviceId = deviceId,
                        DeviceName = deviceName,
                        CommandId = item.Id,
                        CommandTitle = item.Command.Title,
                        ValueType = item.Command.ValueType
                    });
                }
            }

            return deviceCommands;
        }

        public async Task<PageResponse<DeviceDto>> GetPaggedList(PageRequest request)
        {
            return await _deviceRepository.GetPaggedList(request);
        }

        public async Task RemoveDeviceAsync(DeviceEditDto device)
        {
            await _deviceRepository.DeleteAsync(device.Id);
            await _deviceRepository.CommitAsync();
            RemoveDockerImage(device);
        }

        private Task RemoveDockerImage(DeviceEditDto device)
        {
            return Task.Run(() => {
                Cmd("docker", $"rm --force {device.DockerImageId}_{device.Id}");
            });
        }
        private Task UpdateDockerImage(DeviceEditDto device)
        {
            return Task.Run(() => {
                RemoveDockerImage(device);
                StartDockerImage(device);
            });
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

            foreach (var conf in device.Configurations)
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
