using IotHomeAssistant.Blazor.Extensions;
using IoTHomeAssistant.Domain.Dto;
using IoTHomeAssistant.Domain.Entities;
using IoTHomeAssistant.Domain.Repositories;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Syncfusion.Blazor.DropDowns;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IotHomeAssistant.Blazor.Components.Widget.Edit
{
    public partial class EditTaskItemComponent : ComponentBase
    {
        protected List<JobTask> tasks;

        [Parameter]
        public WidgetItemDto WidgetItem { get; set; }

        [Parameter]
        public EditForm EditForm { get; set; }

        [Parameter]
        public EditContext EditContext { get; set; }

        [Inject]
        public IJobTaskRepository JobTaskRepository { get; set; }

        protected IconComponent iconComponent;

        protected override async Task OnInitializedAsync()
        {
            tasks = await JobTaskRepository.GetJobTasksAsync();
        }

        private void SelectIcon()
        {
            iconComponent.Show();
            StateHasChanged();
        }

        public void OnSelectIcon(Icon icon)
        {
            WidgetItem.Icon = icon;
            iconComponent.Hide();
            StateHasChanged();
        }

        private async Task OnSelectEvent(ChangeEventArgs<int, JobTask> args)
        {
            if (args.Value > 0)
            {
                EditForm.ClearValidationMessages();
                EditContext.Validate();
                EditContext.NotifyValidationStateChanged();
            }
        }
    }
}
