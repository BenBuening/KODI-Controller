using System.Collections.Generic;

namespace Kodi.JsonRpc.GlobalTypes.Video.Details
{
    public class Movie : Kodi.JsonRpc.GlobalTypes.Video.Details.File
    {
        public string PlotOutline { get; set; }
        public string SortTitle { get; set; }
        public int MovieId { get; set; }
        public List<Cast> Cast { get; set; }
        public string Votes { get; set; }
        public List<string> ShowLink { get; set; }
        public int? Top250 { get; set; }
        public string Trailer { get; set; }
        public int? Year { get; set; }
        public List<string> Country { get; set; }
        public List<string> Studio { get; set; }
        public string Set { get; set; }
        public List<string> Genre { get; set; }
        public string MPAA { get; set; }
        public int? SetId { get; set; }
        public double? Rating { get; set; }
        public List<string> Tag { get; set; }
        public string TagLine { get; set; }
        public List<string> Writer { get; set; }
        public string OriginalTitle { get; set; }
        public string ImdbNumber { get; set; }
    }
}
