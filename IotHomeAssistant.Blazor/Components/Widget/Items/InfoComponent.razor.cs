using IoTHomeAssistant.Domain.Enums;
using IoTHomeAssistant.Domain.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;

namespace IotHomeAssistant.Blazor.Components.Widget.Items
{
    public partial class InfoComponent
    {
        protected string value;

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public ICommandService CommandService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            SubscribeOnEvent();
        }

        public async Task SubscribeOnEvent()
        {
            var hubConnection = new HubConnectionBuilder()
                .WithUrl(NavigationManager.ToAbsoluteUri("/event-publisher"))
                .Build();

            hubConnection.On<string>($"{WidgetItem.EventType}_{WidgetItem.DeviceId}", (payload) =>
            {
                value = payload;
                StateHasChanged();
            });

            await hubConnection.StartAsync();
            await CommandService.GetStatus(WidgetItem.DeviceId);
        }
    }
}
