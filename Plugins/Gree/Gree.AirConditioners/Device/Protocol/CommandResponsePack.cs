using Newtonsoft.Json;
using System.Collections.Generic;

namespace Gree.AirConditioner.Device.Protocol
{
    internal class CommandResponsePack
    {
        [JsonProperty("opt")]
        public List<string> Columns { get; set; }

        [JsonProperty("p")]
        public List<int> Values { get; set; }
    }
}
