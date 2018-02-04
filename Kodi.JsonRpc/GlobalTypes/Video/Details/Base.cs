namespace Kodi.JsonRpc.GlobalTypes.Video.Details
{
    public class Base : Kodi.JsonRpc.GlobalTypes.Media.Details.Base
    {
        public Kodi.JsonRpc.GlobalTypes.Media.ArtWork Art { get; set; }
        public int? PlayCount { get; set; }
    }
}
