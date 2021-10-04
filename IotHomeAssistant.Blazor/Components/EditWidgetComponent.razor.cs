using IotHomeAssistant.Blazor.Components.Widget;
using IoTHomeAssistant.Domain.Entities;

namespace IotHomeAssistant.Blazor.Components
{
    public partial class EditWidgetComponent
    {
        private bool _visible = false;
        private bool _canGoNext = false;

        protected WidgetItem widget = new WidgetItem();
        protected WidgetTypeComponent widgetTypeComponent;

        public void AddWidget()
        {
            widget = new WidgetItem()
            {
                Device = new Device()
            };

            _visible = true;
            StateHasChanged();
        }

        private void Save()
        {
            StateHasChanged();
            Hide();
        }

        private void Next()
        {
            _canGoNext = true;
        }

        public void Hide()
        {
            _visible = false;
            StateHasChanged();
        }
    }
}
