using Newtonsoft.Json;

namespace Tuya.Response
{
    public class DeviceSpecs : DeviceFunctions, IResult
    {
        [JsonProperty("status")]
        public Specs Status { get; set; }
    }
}
