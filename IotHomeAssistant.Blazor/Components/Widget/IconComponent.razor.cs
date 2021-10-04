using IoTHomeAssistant.Domain.Entities;
using IoTHomeAssistant.Domain.Repositories;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IotHomeAssistant.Blazor.Components.Widget
{
    public partial class IconComponent : ComponentBase
    {
        protected List<Icon> icons;
        private bool _isVisible = false;

        [Inject]
        public IIconRepository _iconRepository { get; set; }

        [Parameter] 
        public EventCallback<Icon> OnSelectIcon { get; set; }

        protected override async Task OnInitializedAsync()
        {
            icons = await _iconRepository.GetAllAsync();
        }

        public void Show()
        {
            _isVisible = true;
            StateHasChanged();
        }

        public void Hide()
        {
            _isVisible = false;
            StateHasChanged();
        }        
    }
}
