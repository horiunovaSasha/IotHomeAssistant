using IoTHomeAssistant.Domain.Entities;
using IoTHomeAssistant.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IoTHomeAssistant.Domain.Services
{
    public class AreaService : IAreaService
    {
        private readonly IAreaRepository _areaRepository;

        public AreaService(IAreaRepository areaRepository) {
            _areaRepository = areaRepository;
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