using System.Collections.Generic;

namespace Kodi.JsonRpc.GlobalTypes.Video.Details
{
    public class MusicVideo : Kodi.JsonRpc.GlobalTypes.Video.Details.File
    {
        public List<string> Genre { get; set; }
        public List<string> Artist { get; set; }
        public int MusicVideoId { get; set; }
        public List<string> Tag { get; set; }
        public string Album { get; set; }
        public int Track { get; set; }
        public List<string> Studio { get; set; }
        public int Year { get; set; }
    }
}
