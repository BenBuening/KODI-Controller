using Newtonsoft.Json;

namespace Kodi.JsonRpc.GlobalTypes.Player
{
    public class Speed
    {
        [JsonProperty("speed")]
        public int SpeedValue { get; set; }
    }
}
