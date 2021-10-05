using IoTHomeAssistant.Domain.Dto;
using IoTHomeAssistant.Domain.Entities;
using IoTHomeAssistant.Domain.Services;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.DropDowns;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IotHomeAssistant.Blazor.Components.Widget
{
    public partial class EditInfoItemComponent : ComponentBase
    {
        protected List<DeviceEventDto> deviceEvents;

        [Parameter]
        public WidgetItemDto WidgetItem { get; set; }

        [Inject]
        public IDeviceService DeviceService { get; set; }        

        protected IconComponent iconComponent;
        protected InfoComponent previewComponent;

        protected override async Task OnInitializedAsync()
        {
            deviceEvents = await DeviceService.GetDeviceEventsAsync(true);
        }

        private void SelectIcon()
        {
            iconComponent.Show();
            StateHasChanged();
        }

        public void OnSelectIcon(Icon icon)
        {
            WidgetItem.Icon = icon;
            iconComponent.Hide();
            StateHasChanged();
        }

        private async Task OnSelectEvent(ChangeEventArgs<int, DeviceEventDto> args)
        {
            if (args.Value > 0)
            {
                WidgetItem.DeviceId = deviceEvents.First(x => x.EventId == args.Value).DeviceId;
                await previewComponent.SubscribeOnEvent();
            }
        }

        private void Save()
        {

        }
    }
}
