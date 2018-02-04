using System.Collections.Generic;

namespace Kodi.JsonRpc.GlobalTypes.Video.Details
{
    public class TvShow : Kodi.JsonRpc.GlobalTypes.Video.Details.Item
    {
        public string SortTitle { get; set; }
        public string MPAA { get; set; }
        public string Premiered { get; set; }
        public int Year { get; set; }
        public int Episode { get; set; }
        public int WatchedEpisodes { get; set; }
        public string Votes { get; set; }
        public double Rating { get; set; }
        public int TvShowId { get; set; }
        public List<string> Studio { get; set; }
        public int Season { get; set; }
        public List<string> Genre { get; set; }
        public List<object> Cast { get; set; }
        public string EpisodeGuide { get; set; }
        public List<string> Tag { get; set; }
        public string OriginalTitle { get; set; }
        public string ImdbNumber { get; set; }
    }
}
