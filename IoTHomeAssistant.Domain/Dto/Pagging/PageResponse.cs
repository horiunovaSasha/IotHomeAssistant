using System.Collections.Generic;

namespace IoTHomeAssistant.Domain.Dto.Pagging
{
    public class PageResponse<T>
    {
        public int PageNumber { get; set; }
        public int PageCount { get; set; }
        public List<T> Items { get; set; }
    }
}
