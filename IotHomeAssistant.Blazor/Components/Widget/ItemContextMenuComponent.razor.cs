using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.SplitButtons;

namespace IotHomeAssistant.Blazor.Components.Widget
{
    public partial class ItemContextMenuComponent
    {
        [Parameter]
        public EventCallback OnEdit { get; set; }

        private void MenuItemSelected(MenuEventArgs args)
        {
            if (args.Item.Id == "edit")
            {
                OnEdit.InvokeAsync();
            }
        }
    }
}
