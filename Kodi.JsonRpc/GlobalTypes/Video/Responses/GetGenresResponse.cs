using Kodi.JsonRpc.GlobalTypes.Item.Details;
using Kodi.JsonRpc.GlobalTypes.List;
using System.Collections.Generic;

namespace Kodi.JsonRpc.GlobalTypes.Video.Responses
{
    public class GetGenresResponse
    {
        public LimitsReturned Limits { get; set; }
        public List<Genre> Genres { get; set; }
    }
}
