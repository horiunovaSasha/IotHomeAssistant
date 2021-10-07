using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.SplitButtons;

namespace IotHomeAssistant.Blazor.Components.Widget
{
    public partial class ItemContextMenuComponent
    {
        [Parameter]
        public EventCallback OnEdit { get; set; }
        
        [Parameter]
        public EventCallback OnDelete{ get; set; }

        [Parameter]
        public bool IsPreview { get; set; }

        private void MenuItemSelected(MenuEventArgs args)
        {
            if (args.Item.Id == "edit")
            {
                OnEdit.InvokeAsync();
            }
            
            if (args.Item.Id == "remove")
            {
                OnDelete.InvokeAsync();
            }
        }
    }
}
