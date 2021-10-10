using Newtonsoft.Json;

namespace Tuya.Response
{
    public class CommandResponse : ApiResponse<bool>
    {
        [JsonProperty("result")]
        public override bool Result { get; set; }
    }
}
