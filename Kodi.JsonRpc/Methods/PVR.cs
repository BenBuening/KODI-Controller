using Kodi.JsonRpc.GlobalTypes.List;
using Kodi.JsonRpc.GlobalTypes.PVR.Details;
using Kodi.JsonRpc.GlobalTypes.PVR.Property;
using Kodi.JsonRpc.GlobalTypes.PVR.Responses;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kodi.JsonRpc.Methods
{
    public class PVR : MethodLibraryBase
    {

        internal PVR(API api) : base(api) { }


        // todo:
        public Task<Channel> GetChannelDetails(int channelId, List<string> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("channelid", channelId);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString().ToLower()).ToList());

            return RunAsync<Channel>("PVR.GetChannelDetails", parameters);
        }

        // todo:
        public Task<ChannelGroupExtended> GetChannelGroupDetails(object channelGroupId, ChannelGroupDetailParams channels)
        {
            Dictionary<string, object> channelsParams = new Dictionary<string, object>();
            channelsParams.ConditionalAdd("limits", channels.Limits);
            channelsParams.ConditionalAdd("properties", channels.Properties?.Select(x => x.ToString().ToLower()).ToList());

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("channelgroupid", channelGroupId);
            parameters.ConditionalAdd("channels", channelsParams);

            return RunAsync<ChannelGroupExtended>("PVR.GetChannelGroupDetails", parameters);
        }

        // todo:
        public Task<GetChannelGroupsResponse> GetChannelGroups(string channelType, Limits limits)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("channeltype", channelType);
            parameters.ConditionalAdd("limits", limits);

            return RunAsync<GetChannelGroupsResponse>("PVR.GetChannelGroups", parameters);
        }

        // todo:
        public Task<GetChannelsResponse> GetChannels(object channelGroupId, List<string> properties, Limits limits)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("channelgroupid", channelGroupId);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString().ToLower()).ToList());
            parameters.ConditionalAdd("limits", limits);

            return RunAsync<GetChannelsResponse>("PVR.GetChannels", parameters);
        }

        // todo:
        public Task<Value> GetProperties(List<string> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString().ToLower()).ToList());

            return RunAsync<Value>("PVR.GetProperties", parameters);
        }

        // todo:
        public Task Record(string record = "toggle", string channel = "current")
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("record", record);
            parameters.ConditionalAdd("channel", channel);

            return RunAsync("PVR.Record", parameters);
        }

        public Task Scan()
        {
            return RunAsync("PVR.Scan", null);
        }



        // todo:
        public class ChannelGroupDetailParams
        {
            public Limits Limits { get; set; }
            public List<string> Properties { get; set; }
        }
    }
}
