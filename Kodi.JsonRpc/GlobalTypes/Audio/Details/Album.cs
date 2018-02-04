using System.Collections.Generic;

namespace Kodi.JsonRpc.GlobalTypes.Audio.Details
{
    public class Album : Kodi.JsonRpc.GlobalTypes.Audio.Details.Media
    {
        public List<string> Theme { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public List<string> Style { get; set; }
        public int AlbumId { get; set; }
        public int PlayCount { get; set; }
        public string AlbumLabel { get; set; }
        public List<string> Mood { get; set; }
    }
}
