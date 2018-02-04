using Newtonsoft.Json;

namespace Kodi.JsonRpc.GlobalTypes.PVR.Details
{
    public class Channel : Kodi.JsonRpc.GlobalTypes.Item.Details.Base
    {
        public string ChannelType { get; set; } // "tv"
        public string Thumbnail { get; set; }
        [JsonProperty("channel")]
        public string ChannelName { get; set; }
        public bool Hidden { get; set; }
        public int ChannelId { get; set; }
        public bool Locked { get; set; }
        public string LastPlayed { get; set; }
    }
}
