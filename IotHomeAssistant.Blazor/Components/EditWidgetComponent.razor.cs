using IotHomeAssistant.Blazor.Components.Widget;
using IoTHomeAssistant.Domain.Dto;
using IoTHomeAssistant.Domain.Entities;

namespace IotHomeAssistant.Blazor.Components
{
    public partial class EditWidgetComponent
    {
        private bool _visible = false;
        private bool _canGoNext = false;

        protected WidgetItemDto widget = new WidgetItemDto();
        protected WidgetTypeComponent widgetTypeComponent;

        public void AddWidget()
        {
            widget = new WidgetItemDto();

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
