using IoTHomeAssistant.Domain.Services;
using Microsoft.AspNetCore.Components;

namespace IotHomeAssistant.Blazor.Components.Widget.Items
{
    public partial class CleanerComponent
    {
        [Inject]
        public ICommandService CommandService { get; set; }

        protected void Start()
        {
            CommandService.Exec(WidgetItem.DeviceId, "start_cleaning");
        }

        protected void Pause()
        {
            CommandService.Exec(WidgetItem.DeviceId, "pause_cleaning");
        }

        protected void GoToCharge()
        {
            CommandService.Exec(WidgetItem.DeviceId, "go_to_charge");
        }
    }
}