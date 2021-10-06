using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace IotHomeAssistant.Blazor.Components
{
    public partial class DeleteConfirmComponent
    {
        private bool _isVisible = false;

        [Parameter]
        public string Title { get; set; } = string.Empty;

        [Parameter]
        public EventCallback OnConfirm { get; set; }

        public void Show(string title)
        {
            Title = title;
            Show();
        }
        
        public void Show()
        {
            _isVisible = true;
            StateHasChanged();
        }

        private async Task ConfirmClickAsync() {
            await OnConfirm.InvokeAsync();
            _isVisible = false;
            StateHasChanged();
        }
    }
}
