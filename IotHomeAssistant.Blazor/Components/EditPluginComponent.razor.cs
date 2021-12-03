using IoTHomeAssistant.Domain.Dto;
using IoTHomeAssistant.Domain.Entities;
using IoTHomeAssistant.Domain.Enums;
using IoTHomeAssistant.Domain.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
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

        [Parameter]
        public EventCallback OnSave { get; set; }

        [Inject]
        protected IPluginService _pluginService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected PluginEditDto Plugin { get; set; } = new PluginEditDto();

        public void AddPlugin()
        {
            Plugin = new PluginEditDto()
            {
                Configurations = new List<DevicePluginConfigurationDto>()
                {
                    new DevicePluginConfigurationDto(){PluginConfigurationId =0}
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
                DockerConfiguration = plugin.DockerImageSource,
                Configurations = plugin.Configurations?
                        .Select(x => new DevicePluginConfigurationDto()
                        {
                            PluginConfigurationId = x.Id,
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

        private async Task SaveAsync()
        {
            var plugin = new Plugin()
            {
                Id = Plugin.Id,
                Title = Plugin.Title,
                DeviceType = Enum.Parse<DeviceTypeEnum>(Plugin.Type),
                DockerImageSource = Plugin.DockerConfiguration,
                Configurations = Plugin.Configurations
                .Select(x => new PluginConfiguration() { 
                    Id = x.PluginConfigurationId,
                    Title = x.Title,
                    Key = x.Value,
                    Type = Enum.Parse<ConfigurationTypeEnum>(x.Type),
                    PluginId = Plugin.Id
                })
                .ToList()
            };

            string notification = "Cтворення Docker образу в процесі і Вас буде повідомлено, як тільки образ створиться";

            if (Plugin.Id == 0)
            {
                _pluginService.AddPlugin(plugin);
            } else
            {
                _pluginService.UpdatePlugin(plugin);
                notification = "Oновлення Docker образу в процесі і Вас буде повідомлено, як тільки образ оновиться";
            }

            await OnSave.InvokeAsync();

            StateHasChanged();
            Hide();

            var hubConnection = new HubConnectionBuilder()
                .WithUrl(NavigationManager.ToAbsoluteUri("/event-publisher"))
                .Build();

            await hubConnection.StartAsync();
            await hubConnection.SendAsync("PublishNotification", NotificationTypeEnum.Info, notification);
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