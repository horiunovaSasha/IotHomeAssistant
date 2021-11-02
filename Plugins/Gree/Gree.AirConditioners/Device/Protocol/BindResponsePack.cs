using Newtonsoft.Json;

namespace Gree.AirConditioner.Device.Protocol
{
    internal class BindResponsePack
    {
        [JsonProperty("mac")]
        public string MAC { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }
    }
}
