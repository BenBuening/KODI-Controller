using Kodi.JsonRpc.GlobalTypes.List;
using Kodi.JsonRpc.GlobalTypes.Video.Details;
using System.Collections.Generic;

namespace Kodi.JsonRpc.GlobalTypes.Video.Responses
{
    public class GetSeasonsResponse
    {
        public LimitsReturned Limits { get; set; }
        public List<Season> Seasons { get; set; }
    }
}
