using Kodi.JsonRpc.GlobalTypes.List;
using Kodi.JsonRpc.GlobalTypes.Video.Details;
using System.Collections.Generic;

namespace Kodi.JsonRpc.GlobalTypes.Video.Responses
{
    public class GetMovieSetsResponse
    {
        public LimitsReturned Limits { get; set; }
        public List<MovieSet> Sets { get; set; }
    }
}
