using Kodi.JsonRpc.GlobalTypes.Library.Details;
using Kodi.JsonRpc.GlobalTypes.List;
using System.Collections.Generic;

namespace Kodi.JsonRpc.GlobalTypes.Audio.Responses
{
    public class GetGenresResponse
    {
        public List<Genre> Genres { get; set; }
        public LimitsReturned Limits { get; set; }
    }
}
