using Newtonsoft.Json;
using System.Collections.Generic;

namespace Kodi.JsonRpc.GlobalTypes.Audio.Details
{
    public class Artist : Kodi.JsonRpc.GlobalTypes.Audio.Details.Base
    {
        public string Born { get; set; }
        public string Formed { get; set; }
        public string Died { get; set; }
        public List<string> Style { get; set; }
        public List<string> YearsActive { get; set; }
        public List<string> Mood { get; set; }
        public string MusicBrainzArtistId { get; set; }
        public string Disbanded { get; set; }
        public string Description { get; set; }
        [JsonProperty("artist")]
        public string ArtistName { get; set; }
        public List<string> Instrument { get; set; }
        public int ArtistId { get; set; }
    }
}
