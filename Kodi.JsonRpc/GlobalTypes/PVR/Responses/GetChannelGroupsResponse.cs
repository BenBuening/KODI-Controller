using Kodi.JsonRpc.GlobalTypes.List;
using Kodi.JsonRpc.GlobalTypes.PVR.Details;
using System.Collections.Generic;

namespace Kodi.JsonRpc.GlobalTypes.PVR.Responses
{
    public class GetChannelGroupsResponse
    {
        public LimitsReturned Limits { get; set; }
        public List<ChannelGroup> ChannelGroups { get; set; }
    }
}
