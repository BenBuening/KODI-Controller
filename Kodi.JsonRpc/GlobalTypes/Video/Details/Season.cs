using Newtonsoft.Json;

namespace Kodi.JsonRpc.GlobalTypes.Video.Details
{
    public class Season : Kodi.JsonRpc.GlobalTypes.Video.Details.Base
    {
        public string ShowTitle { get; set; }
        public int WatchedEpisodes { get; set; }
        public int TvShowId { get; set; }
        public int Episode { get; set; }
        [JsonProperty("season")]
        public int SeasonNumber { get; set; }
    }
}
