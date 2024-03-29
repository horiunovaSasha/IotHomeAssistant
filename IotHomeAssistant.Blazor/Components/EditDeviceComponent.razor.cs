﻿using IoTHomeAssistant.Domain.Dto;
using IoTHomeAssistant.Domain.Entities;
using IoTHomeAssistant.Domain.Enums;
using IoTHomeAssistant.Domain.Services;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.DropDowns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IotHomeAssistant.Blazor.Components
{
    public partial class EditDeviceComponent
    {
        private bool _visible = false;
        private bool _typeEnabled = false;
        private bool _pluginEnabled = false;

        private SfDropDownList<string, string> _typeInput;
        private SfDropDownList<int, Plugin> _pluginInput;

        private string[] _types = Enum.GetNames(typeof(DeviceTypeEnum));
        private List<Plugin> _plugins = new List<Plugin>();

        [Inject]
        protected IDeviceService _deviceService { get; set; }
        [Inject]
        protected IPluginService _pluginService { get; set; }

        [Parameter]
        public EventCallback OnSave { get; set; }

        protected DeviceEditDto Device { get; set; } = new DeviceEditDto();

        public void AddDevice()
        {
            Device = new DeviceEditDto();
            _typeEnabled = true;
            _pluginEnabled = false;

            Show();
        }

        public async Task EditDeviceAsync(int id)
        {
            var device = await _deviceService.GetDeviceAsync(id);
            Device = new DeviceEditDto()
            {
                Id = device.Id,
                Title = device.Title,
                Type = device.Type.ToString(),
                Plugin = device.PluginDevice?.PluginId ?? 0,
                DockerImageId = device.PluginDevice?.Plugin?.DockerImageId ?? string.Empty,
                PluginDeviceId = device.PluginDevice?.Id ?? 0,
                Configurations = device.PluginDevice?.Configurations
                        .Select(x => new DevicePluginConfigurationDto()
                        {
                            Id = x.Id,
                            PluginConfigurationId = x.PluginConfigurationId,
                            Title = x.PluginConfiguration?.Title,
                            Description = x.PluginConfiguration?.Description,
                            Key = x.PluginConfiguration?.Key,
                            Value = x.Value,
                            Type = "Text"
                        })
                        .ToList()
            };

            _plugins = await _pluginService.GetPluginsByTypeAsync(device.Type);

            _typeEnabled = false;
            _pluginEnabled = false;

            Show();
        }

        private void Show() {
            _visible = true;
            StateHasChanged();
        }

        private void Hide()
        {
            _visible = false;
            StateHasChanged();
        }

        private async Task OnChangeType()
        {
            if (!string.IsNullOrEmpty(_typeInput.Value))
            {
                _plugins = await _pluginService.GetPluginsByTypeAsync(Enum.Parse<DeviceTypeEnum>(_typeInput.Value));
                Device.Configurations.Clear();
            }

            _pluginEnabled = _plugins.Any();
        }

        private async Task OnChangePlugin()
        {
            if (_pluginInput.Value > 0)
            {
                var plugin = _plugins.FirstOrDefault(x => x.Id == _pluginInput.Value);
                if (plugin != null && plugin.Configurations != null)
                {
                    Device.Configurations.Clear();
                    Device.DockerImageId = plugin.DockerImageId;

                    foreach(var item in plugin.Configurations)
                    {
                        Device.Configurations.Add(new DevicePluginConfigurationDto()
                        {
                            PluginConfigurationId = item.Id,
                            Key = item.Key,
                            Title = item.Title,
                            Description = item.Description,
                            Type = item.Type.ToString()
                        });
                    }
                    
                }
            }
        }

        private void Save()
        {
            _deviceService.SaveDeviceAsync(Device);

            OnSave.InvokeAsync();
            StateHasChanged();
            Hide();
        }
    }
}