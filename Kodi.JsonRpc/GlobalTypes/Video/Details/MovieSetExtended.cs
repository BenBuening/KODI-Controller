using System.Collections.Generic;

namespace Kodi.JsonRpc.GlobalTypes.Video.Details
{
    public class MovieSetExtended : Kodi.JsonRpc.GlobalTypes.Video.Details.MovieSet
    {
        public Kodi.JsonRpc.GlobalTypes.List.LimitsReturned Limits { get; set; }
        public List<Kodi.JsonRpc.GlobalTypes.Video.Details.Movie> Movies { get; set; }
    }
}
