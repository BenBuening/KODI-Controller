using Newtonsoft.Json;
using System.Collections.Generic;

namespace Kodi.JsonRpc.GlobalTypes.Video.Details
{
    public class Episode : Kodi.JsonRpc.GlobalTypes.Video.Details.File
    {
        public List<Cast> Cast { get; set; }
        public string ProductionCode { get; set; }
        public double Rating { get; set; }
        public string Votes { get; set; }
        [JsonProperty("episode")]
        public int EpisodeNumber { get; set; }
        public string ShowTitle { get; set; }
        public int EpisodeId { get; set; }
        public int TvShowId { get; set; }
        public int Season { get; set; }
        public string FirstAired { get; set; }
        public object UniqueId { get; set; }
        public List<string> Writer { get; set; }
        public string OriginalTitle { get; set; }
    }
}
