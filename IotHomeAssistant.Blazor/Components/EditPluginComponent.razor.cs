using IoTHomeAssistant.Domain.Dto;
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
    public partial class EditPluginComponent
    {
        private bool _visible = false;
        private bool _typeEnabled = false;
        private bool _pluginEnabled = false;

        private SfDropDownList<string, string> _typeInput;
        private SfDropDownList<int, Plugin> _pluginInput;

        private string[] _types = Enum.GetNames(typeof(DeviceTypeEnum));
        private string[] _configurationTypes = Enum.GetNames(typeof(ConfigurationTypeEnum));
        private List<Plugin> _plugins = new List<Plugin>();

        [Inject]
        protected IPluginService _pluginService { get; set; }
        protected PluginEditDto Plugin { get; set; } = new PluginEditDto();

        public void AddPlugin()
        {
            Plugin = new PluginEditDto()
            {
                Configurations = new List<DevicePluginConfigurationDto>()
                {
                    new DevicePluginConfigurationDto(){Id =0}
                }
            };
            _typeEnabled = true;
            _pluginEnabled = false;

            Show();
        }

        public async Task EditPluginAsync(int id)
        {
            var plugin = await _pluginService.GetPluginAsync(id);
            Plugin = new PluginEditDto()
            {
                Id = plugin.Id,
                Title = plugin.Title,
                Type = plugin.DeviceType.ToString(),
                Configurations = plugin.Configurations?
                        .Select(x => new DevicePluginConfigurationDto()
                        {
                            Id = x.Id,
                            Title = x.Title,
                            Description = x.Description,
                            Value = x.Key,
                            Type = x.Type.ToString()
                        })
                        .ToList()
            };

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

        private async Task OnChangePlugin()
        {
            if (_pluginInput.Value > 0)
            {
                var plugin = _plugins.FirstOrDefault(x => x.Id == _pluginInput.Value);
                if (plugin != null && plugin.Configurations != null)
                {
                    Plugin.Configurations.Clear();

                    foreach(var item in plugin.Configurations)
                    {
                        Plugin.Configurations.Add(new DevicePluginConfigurationDto()
                        {
                            Id = item.Id,
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
            StateHasChanged();
            Hide();
        }

        private void Clone()
        {
            Plugin.Configurations.Add(new DevicePluginConfigurationDto());
        }
        
        private EventCallback RemoveConfiguration(DevicePluginConfigurationDto configurationDto)
        {
            Plugin.Configurations.Remove(configurationDto);
            return new EventCallback();
        }
    }
}