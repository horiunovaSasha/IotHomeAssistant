using IoTHomeAssistant.Domain.Enums;
using IoTHomeAssistant.Domain.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;

namespace IotHomeAssistant.Blazor.Components.Widget.Items
{
    public partial class BlindsComponent
    {
        [Inject]
        public ICommandService CommandService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected bool isChecked;
        protected int top = 0;

        protected override async Task OnInitializedAsync()
        {
            if (WidgetItem.Type == WidgetItemTypeEnum.Blinds)
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

            hubConnection.On<string>($"Event_{WidgetItem.DeviceId}_{WidgetItem.EventId}", (payload) =>
            {
                int value = 0;
                if (int.TryParse(payload, out value))
                {
                    top = - ((int)((100 - value) * 1.5) + 13);
                    StateHasChanged();
                }
            });

            await hubConnection.StartAsync();
        }

        private void Open()
        {
            CommandService.Exec(WidgetItem.DeviceId, "open");
            isChecked = true;
        }

        private void Close()
        {
            CommandService.Exec(WidgetItem.DeviceId, "close");
            isChecked = false;
        }

        private void Stop()
        {
            CommandService.Exec(WidgetItem.DeviceId, "stop");
        }
    }
}