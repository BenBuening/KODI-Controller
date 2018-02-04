using Kodi.JsonRpc.GlobalTypes.List;
using Kodi.JsonRpc.GlobalTypes.PVR.Details;
using System.Collections.Generic;

namespace Kodi.JsonRpc.GlobalTypes.PVR.Responses
{
    public class GetChannelsResponse
    {
        public LimitsReturned Limits { get; set; }
        public List<Channel> Channels { get; set; }
    }
}
