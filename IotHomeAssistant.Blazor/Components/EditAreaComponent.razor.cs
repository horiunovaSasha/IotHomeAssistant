using IoTHomeAssistant.Domain.Dto;
using IoTHomeAssistant.Domain.Entities;
using IoTHomeAssistant.Domain.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace IotHomeAssistant.Blazor.Components
{
    public partial class EditAreaComponent
    {
        private bool _visible = false;

        protected AreaDto area = new AreaDto();

        [Inject]
        public IAreaService AreaService { get; set; }

        [Parameter]
        public EventCallback OnSave { get; set; }

        public void AddArea()
        {
            area = new AreaDto();
            _visible = true;

            StateHasChanged();
        }

        private async Task SaveAsync()
        {
            await AreaService.SaveAsync(new Area()
            {
                Id = area.Id,
                Title = area.Title
            });

            StateHasChanged();
            await OnSave.InvokeAsync();

            Hide();
        }

        private void Hide()
        {
            _visible = false;
            StateHasChanged();
        }
    }
}
