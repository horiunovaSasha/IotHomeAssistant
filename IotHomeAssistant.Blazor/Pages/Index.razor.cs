﻿using IotHomeAssistant.Blazor.Components;
using IoTHomeAssistant.Domain.Dto;
using IoTHomeAssistant.Domain.Entities;
using IoTHomeAssistant.Domain.Services;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IotHomeAssistant.Blazor.Pages
{
    public partial class Index
    {
        protected EditWidgetComponent widgetModal;
        protected EditAreaComponent areaModal;

        protected List<Area> areas = new List<Area>();
        
        protected WidgetItemDto infoWidgetItem = new WidgetItemDto() { Title = "Температура у спальній" };

        [Inject]
        public IAreaService AreaService { get; set; }


        protected override async Task OnInitializedAsync()
        {
            await RefreshAreas();
        }

        private async Task RefreshAreas()
        {
            areas = await AreaService.GetAreasAsync();
        }
    }
}
