using System.Collections.Generic;
using System.Linq;

namespace IoTHomeAssistant.Domain.Dto
{
    public class GenericResponse<T>
    {
        public bool IsSuccessful { get { return !Errors.Any(); } }
        public List<string> Errors { get; set; }
        public T Result { get; set; }
    }
}
