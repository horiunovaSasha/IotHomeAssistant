using Newtonsoft.Json;

namespace Tuya.Response
{
    public class DeviceFunctions : IResult
    {
        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("functions")]
        public Specs Functions { get; set; }
    }
}
