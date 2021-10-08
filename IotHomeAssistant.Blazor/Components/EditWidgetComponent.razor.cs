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
                editForm.ClearValidationMessages();
                var messageStore = new ValidationMessageStore(editContext);

                if (widget.Type != WidgetItemTypeEnum.CustomScript && 
                    widget.Type != WidgetItemTypeEnum.WeatherForecast && 
                    widget.DeviceId == 0)
                {
                    messageStore.Add(editContext.Field("DeviceId"), "Виберіть пристрій зі списку!");
                    editContext.NotifyValidationStateChanged();
                }

                if (widget.Type == WidgetItemTypeEnum.Informer && widget.EventId == 0) {
                    messageStore.Add(editContext.Field("EventId"), "Виберіть подію пристрою зі списку!");
                    editContext.NotifyValidationStateChanged();
                }

                if (widget.Type == WidgetItemTypeEnum.Switcher && widget.IconId == 0)
                {
                    messageStore.Add(editContext.Field("IconId"), "Виберіть тип перемикача!");                        
                    editContext.NotifyValidationStateChanged();
                }

                if (widget.Type == WidgetItemTypeEnum.CustomScript && widget.JobTaskId == 0)
                {
                    messageStore.Add(editContext.Field("JobTaskId"), "Виберіть задачу зі списку!");
                    editContext.NotifyValidationStateChanged();
                }

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
