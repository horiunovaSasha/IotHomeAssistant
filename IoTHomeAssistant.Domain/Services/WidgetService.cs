using IoTHomeAssistant.Domain.Dto;
using IoTHomeAssistant.Domain.Entities;
using IoTHomeAssistant.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IoTHomeAssistant.Domain.Services
{
    public class WidgetService : IWidgetService
    {
        private readonly IWidgetItemRepository _widgetRepository;

        public WidgetService(IWidgetItemRepository widgetRepository)
        {
            _widgetRepository = widgetRepository;
        }

        public async Task RemoveAsync(int id)
        {
            await _widgetRepository.DeleteAsync(id);
            await _widgetRepository.CommitAsync();
        }

        public async Task SaveAsync(WidgetItemDto widgetItem)
        {
            var widget = new WidgetItem()
            {
                Id = widgetItem.Id,
                Title = widgetItem.Title,
                Type = widgetItem.Type,
                SymbolAfter = widgetItem.SymbolAfter,
                AreaId = widgetItem.AreaId
            };

            if (widgetItem.Icon != null)
            {
                widget.IconId = widgetItem.Icon.Id;
            }

            if (widgetItem.DeviceId > 0)
            {
                widget.DeviceId = widgetItem.DeviceId;
            }

            if (widgetItem.EventId > 0)
            {
                widget.EventId = widgetItem.EventId;
            }

            if (widgetItem.JobTaskId > 0)
            {
                widget.JobTaskId = widgetItem.JobTaskId;
            }

            if (widgetItem.Latitude > 0)
            {
                widget.Latitude = widgetItem.Latitude;
            }

            if (widgetItem.Longitude > 0)
            {
                widget.Longitude = widgetItem.Longitude;
            }

            try
            {
                if (widget.Id == 0)
                {
                    await _widgetRepository.AddAsync(widget);
                }
                else
                {
                    await _widgetRepository.UpdateAsync(widget);
                }

                await _widgetRepository.CommitAsync();
            } catch(Exception ex)
            {

            }
        }
    }
}
