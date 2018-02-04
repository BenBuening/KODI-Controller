using Kodi.JsonRpc.GlobalTypes.List;
using Kodi.JsonRpc.GlobalTypes.List.Item;
using System.Collections.Generic;

namespace Kodi.JsonRpc.GlobalTypes.Playlist.Responses
{
    public class GetItemsResponse
    {
        public LimitsReturned Limits { get; set; }
        public List<All> Items { get; set; }
    }
}
