using Newtonsoft.Json;

namespace Gree.AirConditioner.Device.Protocol
{
    internal class BindRequestPack
    {
        [JsonProperty("t")]
        public string Type { get => "bind"; private set { } }

        [JsonProperty("uid")]
        public int UID { get => 0; private set { } }

        [JsonProperty("mac")]
        public string MAC { get; set; }
    }
}
