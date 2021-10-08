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

        protected override async Task OnInitializedAsync()
        {
            if (!IsPreview && WidgetItem.Id > 0)
            {
                SubscribeOnEvent();
            }
        }

        public async Task SubscribeOnEvent()
        {
            var hubConnection = new HubConnectionBuilder()
                .WithUrl(NavigationManager.ToAbsoluteUri("/event-publisher"))
                .Build();

            hubConnection.On<string>($"Event_{WidgetItem.DeviceId}_{WidgetItem.EventId}", (payload) =>
            {
                value = payload;
                StateHasChanged();
            });

            await hubConnection.StartAsync();
        }
    }
}
