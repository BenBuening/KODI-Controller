using System.Collections.Generic;

namespace Kodi.JsonRpc.GlobalTypes.PVR.Details
{
    public class ChannelGroupExtended : Kodi.JsonRpc.GlobalTypes.PVR.Details.ChannelGroup
    {
        public Kodi.JsonRpc.GlobalTypes.List.LimitsReturned Limits { get; set; }
        public List<Kodi.JsonRpc.GlobalTypes.PVR.Details.Channel> Channels { get; set; }
    }
}
