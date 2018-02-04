using System;

namespace Kodi.JsonRpc.GlobalTypes.Video.Details
{
    public class Item : Kodi.JsonRpc.GlobalTypes.Video.Details.Media
    {
        public DateTime? DateAdded { get; set; }
        public string File { get; set; }
        public DateTime? LastPlayed { get; set; }
        public string Plot { get; set; }
    }
}
