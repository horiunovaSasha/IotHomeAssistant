using IoTHomeAssistant.Domain.Enums;
using IoTHomeAssistant.Domain.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Syncfusion.Blazor.Buttons;
using System;
using System.Threading.Tasks;

namespace IotHomeAssistant.Blazor.Components.Widget.Items
{
    public partial class ClimateComponent
    {
        protected bool isChecked;
        protected int temperature;
        protected AirModeEnum mode = AirModeEnum.auto;
        protected AirSpeedEnum speed = AirSpeedEnum.auto;

        [Inject]
        public ICommandService CommandService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (WidgetItem.Type == WidgetItemTypeEnum.AC && WidgetItem.DeviceId > 0)
            {
                await SubscribeOnEvent();
                await CommandService.GetStatus(WidgetItem.DeviceId);
            }
        }

        public async Task SubscribeOnEvent()
        {
            var hubConnection = new HubConnectionBuilder()
                .WithUrl(NavigationManager.ToAbsoluteUri("/event-publisher"))
                .Build();

            hubConnection.On<bool>($"{EventTypeEnum.power_changed}_{WidgetItem.DeviceId}", (payload) =>
            {
                isChecked = payload;
                StateHasChanged();
            });

            hubConnection.On<int>($"{EventTypeEnum.temperature_changed}_{WidgetItem.DeviceId}", (payload) =>
            {
                temperature = payload;
                StateHasChanged();
            });

            hubConnection.On<string>($"{EventTypeEnum.air_mode_changed}_{WidgetItem.DeviceId}", (payload) =>
            {
                AirModeEnum currentMode;
                if (Enum.TryParse(payload, out currentMode)) {
                    mode = currentMode;
                }
            });

            hubConnection.On<string>($"{EventTypeEnum.air_speed_changed}_{WidgetItem.DeviceId}", (payload) =>
            {
                AirSpeedEnum currentSpeed;
                if (Enum.TryParse(payload, out currentSpeed)) {
                    speed = currentSpeed;
                }
            });

            await hubConnection.StartAsync();
        }

        public void Change(ChangeEventArgs<bool> args)
        {
            CommandService.Exec(WidgetItem.DeviceId, "set_power", isChecked);
            StateHasChanged();
        }

        public void Up()
        {
            if (isChecked)
            {
                if (temperature < 30)
                {
                    temperature++;
                }

                CommandService.Exec(WidgetItem.DeviceId, "set_temperature", temperature);
                StateHasChanged();
            }
        }

        public void Down()
        {
            if (isChecked)
            {
                if (temperature > 16)
                {
                    temperature--;
                }

                CommandService.Exec(WidgetItem.DeviceId, "set_temperature", temperature);
                StateHasChanged();
            }
        }

        public void ChangeSpeed(ChangeEventArgs args)
        {
            int value;
            if (int.TryParse(args.Value.ToString(), out value))
            {
                speed = (AirSpeedEnum)value;
                CommandService.Exec(WidgetItem.DeviceId, "set_air_speed", speed.ToString());
            }
        }

        public void SetMode(AirModeEnum setMode)
        {
            if (isChecked)
            {
                mode = setMode;
                CommandService.Exec(WidgetItem.DeviceId, "set_air_mode", mode.ToString());
            }
        }
    }
}
