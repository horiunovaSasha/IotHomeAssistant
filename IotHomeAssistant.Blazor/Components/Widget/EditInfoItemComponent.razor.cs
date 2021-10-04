using IoTHomeAssistant.Domain.Dto;
using IoTHomeAssistant.Domain.Entities;
using IoTHomeAssistant.Domain.Services;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IotHomeAssistant.Blazor.Components.Widget
{
    public partial class EditInfoItemComponent : ComponentBase
    {
        protected List<DeviceEventDto> deviceEvents;

        [Parameter]
        public WidgetItem WidgetItem { get; set; }

        [Inject]
        public IDeviceService DeviceService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            deviceEvents = await DeviceService.GetDeviceEventsAsync(null, true);
        }
    }
}
