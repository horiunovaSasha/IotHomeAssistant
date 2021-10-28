using IoTHomeAssistant.Domain.Enums;
using IoTHomeAssistant.Domain.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Syncfusion.Blazor.Buttons;
using System.Threading.Tasks;

namespace IotHomeAssistant.Blazor.Components.Widget.Items
{
    public partial class LightComponent
    {
        protected bool isChecked;
        protected int brightness = 0;
        protected string color = string.Empty;

        [Inject]
        public ICommandService CommandService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (WidgetItem.Type == WidgetItemTypeEnum.Bulb && WidgetItem.DeviceId > 0)
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

            hubConnection.On<object>($"{EventTypeEnum.power_changed}_{WidgetItem.DeviceId}", (payload) =>
            {
                bool.TryParse(payload.ToString(), out isChecked);
                StateHasChanged();
            });

            hubConnection.On<object>($"{EventTypeEnum.brightness_changed}_{WidgetItem.DeviceId}", (payload) =>
            {
                int.TryParse(payload.ToString(), out brightness);
                StateHasChanged();
            });
            
            hubConnection.On<object>($"{EventTypeEnum.color_changed}_{WidgetItem.DeviceId}", (payload) =>
            {
                color = payload.ToString();
                StateHasChanged();
            });

            await hubConnection.StartAsync();
        }

        public void Change(ChangeEventArgs<bool> args)
        {
            CommandService.Exec(WidgetItem.DeviceId, "set_power", isChecked);
            StateHasChanged();
        }

        public void ChangeBrightness(ChangeEventArgs args)
        {
            if (int.TryParse(args.Value.ToString(), out brightness))
            {
                CommandService.Exec(WidgetItem.DeviceId, "set_brightness", brightness);
                StateHasChanged();
            }
        }

        public void ColorSelect(Syncfusion.Blazor.Inputs.ColorPickerEventArgs args)
        {
            color = args.CurrentValue.Hex;
            CommandService.Exec(WidgetItem.DeviceId, "set_color", color);
            StateHasChanged();
        }
    }
}