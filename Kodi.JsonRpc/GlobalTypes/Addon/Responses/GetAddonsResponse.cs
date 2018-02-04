using Kodi.JsonRpc.GlobalTypes.List;
using System.Collections.Generic;

namespace Kodi.JsonRpc.GlobalTypes.Addon.Responses
{
    public class GetAddonsResponse
    {
        public LimitsReturned Limits { get; set; }
        public List<Details> Addons { get; set; }
    }
}
