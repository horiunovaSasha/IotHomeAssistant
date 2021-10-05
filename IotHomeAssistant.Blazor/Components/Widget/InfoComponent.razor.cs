using IoTHomeAssistant.Domain.Dto;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Syncfusion.Blazor.SplitButtons;
using System.Threading.Tasks;

namespace IotHomeAssistant.Blazor.Components.Widget
{
    public partial class InfoComponent : ComponentBase
    {
        protected string value;

        [Inject]
        public NavigationManager NavigationManager { get; set; }              

        [Parameter]
        public WidgetItemDto WidgetItem { get; set; } = new WidgetItemDto();

        [Parameter]
        public EditWidgetComponent EditPopup { get; set; }

        [Parameter]
        public bool IsPreview { get; set; }

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

        private void MenuItemSelected(MenuEventArgs args)
        {
            if (args.Item.Id == "edit")
            {
                EditPopup.UpdateWidget(WidgetItem);
            }
        }
    }
}
