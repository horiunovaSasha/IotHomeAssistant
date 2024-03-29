﻿
using IoTHomeAssistant.Domain.Entities;

namespace IoTHomeAssistant.Domain.Dto
{
    public class DeviceDto
    {
        public DeviceDto(Device device)
        {
            Id = device.Id;
            Title = device.Title;
            Type = device.Type.ToString();
            Plugin = device.PluginDevice?.Plugin?.Title;
            DockerImageId = device.PluginDevice?.Plugin?.DockerImageId;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Plugin { get; set; }
        public string DockerImageId { get; set; }
    }
}
