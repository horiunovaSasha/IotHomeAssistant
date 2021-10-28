using IoTHomeAssistant.Domain.Enums;
using IoTHomeAssistant.Domain.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Syncfusion.Blazor.Buttons;
using System.Threading.Tasks;

namespace IotHomeAssistant.Blazor.Components.Widget.Items
{
    public partial class ThermostatComponent
    {
        protected bool isChecked;
        protected int temperature = 0;

        [Inject]
        public ICommandService CommandService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (WidgetItem.Type == WidgetItemTypeEnum.Heating && WidgetItem.DeviceId > 0)
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

            hubConnection.On<string>($"{EventTypeEnum.power_changed}_{WidgetItem.DeviceId}", (payload) =>
            {
                bool.TryParse(payload, out isChecked);
                StateHasChanged();
            });

            hubConnection.On<object>($"{EventTypeEnum.target_temperature_changed}_{WidgetItem.DeviceId}", (payload) =>
            {
                int.TryParse(payload.ToString(), out temperature);
                StateHasChanged();
            });

            await hubConnection.StartAsync();
        }

        public void Up()
        {
            CommandService.Exec(WidgetItem.DeviceId, "set_temperature", temperature + 1);
            StateHasChanged();
        }

        public void Down()
        {
            CommandService.Exec(WidgetItem.DeviceId, "set_temperature", temperature - 1);
            StateHasChanged();
        }

        public void Change(ChangeEventArgs<bool> args)
        {
            CommandService.Exec(WidgetItem.DeviceId, "set_power", isChecked);
            StateHasChanged();
        }
    }   
}
