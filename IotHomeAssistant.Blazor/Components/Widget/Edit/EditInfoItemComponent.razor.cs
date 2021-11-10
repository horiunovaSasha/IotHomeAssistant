using IotHomeAssistant.Blazor.Components.Widget.Items;
using IotHomeAssistant.Blazor.Extensions;
using IoTHomeAssistant.Domain.Dto;
using IoTHomeAssistant.Domain.Entities;
using IoTHomeAssistant.Domain.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Syncfusion.Blazor.DropDowns;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IotHomeAssistant.Blazor.Components.Widget.Edit
{
    public partial class EditInfoItemComponent : ComponentBase
    {
        protected List<DeviceEventDto> deviceEvents;

        [Parameter]
        public WidgetItemDto WidgetItem { get; set; }

        [Parameter]
        public EditForm EditForm { get; set; }

        [Parameter]
        public EditContext EditContext { get; set; }

        [Inject]
        public IDeviceService DeviceService { get; set; }

        protected IconComponent iconComponent;
        protected InfoComponent previewComponent;

        protected override async Task OnInitializedAsync()
        {
            deviceEvents = (await DeviceService.GetDeviceEventsAsync());
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
                EditForm.ClearValidationMessages();
                EditContext.Validate();
                EditContext.NotifyValidationStateChanged();

                var deviceEvent = deviceEvents.First(x => x.EventId == args.Value);
                WidgetItem.EventId = args.Value;
                WidgetItem.EventType = deviceEvent.EventType;
                WidgetItem.DeviceId = deviceEvent.DeviceId;

                await previewComponent.SubscribeOnEvent();
            }
        }

        private void Save()
        {

        }
    }
}
