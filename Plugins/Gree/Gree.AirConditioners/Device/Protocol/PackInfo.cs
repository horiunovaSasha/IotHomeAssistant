using Newtonsoft.Json;

namespace Gree.AirConditioner.Device.Protocol
{
    internal class PackInfo
    {
        [JsonProperty("t")]
        public string Type { get; set; }

        [JsonProperty("cid")]
        public string ClientId { get; set; }
    }
}
