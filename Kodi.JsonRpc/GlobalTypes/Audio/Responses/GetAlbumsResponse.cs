using Kodi.JsonRpc.GlobalTypes.Audio.Details;
using Kodi.JsonRpc.GlobalTypes.List;
using System.Collections.Generic;

namespace Kodi.JsonRpc.GlobalTypes.Audio.Responses
{
    public class GetAlbumsResponse
    {
        public List<Album> albums { get; set; }
        public LimitsReturned Limits { get; set; }

    }
}
