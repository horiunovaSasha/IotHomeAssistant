using IotHomeAssistant.Blazor.Extensions;
using IoTHomeAssistant.Domain.Dto;
using IoTHomeAssistant.Domain.Entities;
using IoTHomeAssistant.Domain.Enums;
using IoTHomeAssistant.Domain.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Syncfusion.Blazor.DropDowns;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IotHomeAssistant.Blazor.Components.Widget.Edit
{
    public partial class EditItemComponent
    {
        [Parameter]
        public WidgetItemDto WidgetItem { get; set; }

        [Parameter]
        public EditForm EditForm { get; set; }

        [Parameter]
        public EditContext EditContext { get; set; }

        [Inject]
        public IDeviceService DeviceService { get; set; }

        protected List<Device> devices = new List<Device>();

        protected override async Task OnInitializedAsync()
        {
            devices = await DeviceService.GetDevicesAsync(GetDeviceTypeEnum());
        }

        private DeviceTypeEnum? GetDeviceTypeEnum()
        {
            if (WidgetItem != null)
            {
                switch (WidgetItem.Type) {
                    case WidgetItemTypeEnum.AC:
                        return DeviceTypeEnum.AC;
                    case WidgetItemTypeEnum.Blinds:
                        return DeviceTypeEnum.Blinds;
                    case WidgetItemTypeEnum.Bulb:
                        return DeviceTypeEnum.Light;
                    case WidgetItemTypeEnum.Cleaner:
                        return DeviceTypeEnum.Cleaner;
                    case WidgetItemTypeEnum.Heating:
                        return DeviceTypeEnum.Thermostat;
                }
            }

            return null;
        }

        private async Task OnSelectEvent(ChangeEventArgs<int, Device> args)
        {
            if (args.Value > 0)
            {
                EditForm.ClearValidationMessages();
                EditContext.Validate();
                EditContext.NotifyValidationStateChanged();
            }
        }

    }
}
