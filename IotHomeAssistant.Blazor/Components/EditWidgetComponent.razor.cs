using IotHomeAssistant.Blazor.Components.Widget;
using IotHomeAssistant.Blazor.Extensions;
using IoTHomeAssistant.Domain.Dto;
using IoTHomeAssistant.Domain.Enums;
using IoTHomeAssistant.Domain.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;

namespace IotHomeAssistant.Blazor.Components
{
    public partial class EditWidgetComponent
    {
        private bool _visible = false;
        private bool _canGoNext = false;

        protected WidgetItemDto widget = new WidgetItemDto();
        protected WidgetTypeComponent widgetTypeComponent;

        protected EditForm editForm;

        [Inject]
        public IWidgetService WidgetService { get; set; }

        [Parameter]
        public EventCallback OnSave { get; set; }

        public void AddWidget(int areaId)
        {
            widget = new WidgetItemDto()
            {
                AreaId = areaId
            };

            _visible = true;
            _canGoNext = false;
            StateHasChanged();
        }
        
        public void UpdateWidget(WidgetItemDto widgetItem)
        {
            widget = widgetItem;

            _visible = true;
            _canGoNext = true;
            StateHasChanged();
        }

        private async Task SaveAsync()
        {
            await WidgetService.SaveAsync(widget);

            StateHasChanged();

            await OnSave.InvokeAsync();
            Hide();
        }

        private async Task NextAsync(EditContext editContext)
        {
            if (_canGoNext) 
            {
                if (!editContext.Validate())
                    editContext.NotifyValidationStateChanged();
                else
                    await SaveAsync();
            }
            else
            {
                if (widget.Type != WidgetItemTypeEnum.Nothing)
                {
                    editForm.ClearValidationMessages();

                     _canGoNext = true;
                }
            }
        }

        public void Hide()
        {
            _visible = false;
            StateHasChanged();
        }
    }
}
