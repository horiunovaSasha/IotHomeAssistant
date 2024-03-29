﻿using IoTHomeAssistant.Domain.Enums;
using System.Collections.Generic;

namespace IoTHomeAssistant.Domain.Entities
{
    public class Plugin : IEntity<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DeviceTypeEnum DeviceType { get; set; }
        public string DockerImageId { get; set; }
        public string DockerImageSource { get; set; }
        
        public virtual List<PluginConfiguration> Configurations { get; set; }
    }
}
