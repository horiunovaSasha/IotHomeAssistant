using Microsoft.AspNetCore.Components;

namespace IotHomeAssistant.Blazor.Components.Widget
{
    public partial class SwitchComponent
    {
        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public string ImageUrl { get; set; }

        [Parameter]
        public bool IsChecked { get; set; }
    }
}