using Newtonsoft.Json;

namespace Kodi.JsonRpc.GlobalTypes.List
{
    public class Limits
    {
        [JsonProperty("end")]
        public int End { get; set; }
        [JsonProperty("start")]
        public int Start { get; set; }
    }
}
