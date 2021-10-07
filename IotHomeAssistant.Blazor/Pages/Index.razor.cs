using IotHomeAssistant.Blazor.Components;
using IoTHomeAssistant.Domain.Dto;
using IoTHomeAssistant.Domain.Entities;
using IoTHomeAssistant.Domain.Services;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.SplitButtons;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IotHomeAssistant.Blazor.Pages
{
    public partial class Index
    {
        protected EditWidgetComponent widgetModal;
        protected EditAreaComponent areaModal;
        protected DeleteConfirmComponent deleteAreaConfirm;
        protected DeleteConfirmComponent deleteWidgetConfirm;

        protected List<Area> areas = new List<Area>();

        private int _areaIdToRemove;
        private int _widgetIdToRemove;

        [Inject]
        public IAreaService AreaService { get; set; }
        
        [Inject]
        public IWidgetService WidgetService { get; set; }


        protected override async Task OnInitializedAsync()
        {
            await RefreshAreas();
        }

        private async Task RefreshAreas()
        {
            areas = await AreaService.GetAreasAsync();
        }

        private void OnDeleteWidget(int id)
        {
            deleteWidgetConfirm.Show();
            _widgetIdToRemove = id;
        }

        private async Task DeleteAreaAsync()
        {
            await AreaService.RemoveAsync(_areaIdToRemove);
            await RefreshAreas();
        }
        
        private async Task DeleteWidgetAsync()
        {
            await WidgetService.RemoveAsync(_widgetIdToRemove);
            await RefreshAreas();
        }

        private void MenuItemSelected(MenuEventArgs args, Area area)
        {
            if (args.Item.Id == "edit")
            {
                areaModal.UpdatewArea(area);
            }

            if (args.Item.Id == "add")
            {
                widgetModal.AddWidget(area.Id);
            }

            if (args.Item.Id == "remove")
            {
                deleteAreaConfirm.Show();
                _areaIdToRemove = area.Id;
            }
        }
    }
}
