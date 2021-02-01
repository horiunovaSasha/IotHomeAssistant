using System.Collections.Generic;
using System.Linq;

namespace IoTHomeAssistant.Domain.Dto
{
    public class EmptyResponse
    {
        public bool IsSuccessful { get { return !Errors.Any(); } }
        public List<string> Errors { get; set; }
    }
}
