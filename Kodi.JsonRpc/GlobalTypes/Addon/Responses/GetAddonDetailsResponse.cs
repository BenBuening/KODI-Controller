using Kodi.JsonRpc.GlobalTypes.List;

namespace Kodi.JsonRpc.GlobalTypes.Addon.Responses
{
    public class GetAddonDetailsResponse
    {
        public LimitsReturned Limits { get; set; }
        public Details Addon { get; set; }
    }
}
