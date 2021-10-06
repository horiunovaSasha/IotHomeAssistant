using IoTHomeAssistant.Domain.Entities;
using IoTHomeAssistant.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IoTHomeAssistant.Domain.Services
{
    public class AreaService : IAreaService
    {
        private readonly IAreaRepository _areaRepository;
        private readonly IWidgetItemRepository _widgetRepository;

        public AreaService(
            IAreaRepository areaRepository,
            IWidgetItemRepository widgetRepository) {
            _areaRepository = areaRepository;
            _widgetRepository = widgetRepository;
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                await _widgetRepository.RemoveAreaWidgets(id);
                await _areaRepository.DeleteAsync(id);

                await _areaRepository.CommitAsync();
            } catch(Exception ex)
            {

            }
        }

        public async Task<List<Area>> GetAreasAsync()
        {
            return await _areaRepository.GetAreasAsync();
        }

        public async Task SaveAsync(Area area)
        {
            if (area.Id == 0)
            {
                await _areaRepository.AddAsync(area);
            } else
            {
                await _areaRepository.UpdateAsync(area);
            }

            await _areaRepository.CommitAsync();
        }
    }
}