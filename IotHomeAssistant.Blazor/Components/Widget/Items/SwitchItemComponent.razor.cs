using IoTHomeAssistant.Domain.Enums;
using IoTHomeAssistant.Domain.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Syncfusion.Blazor.Buttons;
using System.Threading.Tasks;

namespace IotHomeAssistant.Blazor.Components.Widget.Items
{
    public partial class SwitchItemComponent
    {
        private bool _isChecked = false;

        [Inject]
        public ICommandService CommandService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (WidgetItem.Type == WidgetItemTypeEnum.Switcher && WidgetItem.DeviceId > 0)
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
                _isChecked = payload;
                StateHasChanged();
            });

            await hubConnection.StartAsync();
        }

        public void Change(ChangeEventArgs<bool> args)
        {
            CommandService.Exec(WidgetItem.DeviceId, "set_power", _isChecked);
            StateHasChanged();
        }
    }
}