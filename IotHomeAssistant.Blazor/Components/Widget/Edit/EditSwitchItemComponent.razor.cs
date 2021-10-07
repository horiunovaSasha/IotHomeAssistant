using IoTHomeAssistant.Domain.Dto;
using IoTHomeAssistant.Domain.Entities;
using IoTHomeAssistant.Domain.Enums;
using IoTHomeAssistant.Domain.Repositories;
using IoTHomeAssistant.Domain.Services;
using IotHomeAssistant.Blazor.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IotHomeAssistant.Blazor.Components.Widget.Edit
{
    public partial class EditSwitchItemComponent
    {
        [Parameter]
        public WidgetItemDto WidgetItem { get; set; }

        [Parameter]
        public EditForm EditForm { get; set; }

        [Parameter]
        public EditContext EditContext { get; set; }

        [Inject]
        public IDeviceService DeviceService { get; set; }

        [Inject]
        public IIconRepository IconRepository { get; set; }

        protected string CssClass;
        protected List<Icon> icons = new List<Icon>();
        protected List<Device> devices = new List<Device>();

        protected bool isChecked;

        protected override async Task OnInitializedAsync()
        {
            icons = await IconRepository.GetAllByKeysAsync(new List<string>() { "rosette", "bulb", "floor_lamp" });
            devices = await DeviceService.GetDevicesAsync(DeviceTypeEnum.Switch);
            CssClass = WidgetItem != null && WidgetItem.Icon != null ? "selected" : string.Empty;
        }

        private void OnSelectType(Icon icon)
        {
            CssClass = "selected";
            WidgetItem.Icon = icon;
            WidgetItem.IconId = icon.Id;

            EditForm.ClearValidationMessages();
            EditContext.Validate();
            EditContext.NotifyValidationStateChanged();
        }
    }
}
