using Kodi.JsonRpc.GlobalTypes.Addon.Responses;
using Kodi.JsonRpc.GlobalTypes.List;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kodi.JsonRpc.Methods
{
    public class Addons : MethodLibraryBase
    {

        internal Addons(API api) : base(api) { }

        // todo:
        public Task ExecuteAddon(string addonId, object args, bool? wait)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("addonid", addonId);
            parameters.ConditionalAdd("args", args);
            parameters.ConditionalAdd("wait", wait);

            return RunAsync("Addons.ExecuteAddon", parameters);
        }

        // todo:
        public Task<GetAddonDetailsResponse> GetAddonDetails(string addonId, List<string> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("addonid", addonId);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString().ToLower()).ToList());

            return RunAsync<GetAddonDetailsResponse>("Addons.GetAddonDetails", parameters);
        }

        // todo:
        public Task<GetAddonsResponse> GetAddons(Limits limits, List<string> properties, string type = "unknown", string content = "unknown", object enabled = null)
        {
            //if (enabled == null) enabled = "all";

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("limits", limits);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString().ToLower()).ToList());
            parameters.ConditionalAdd("type", type);
            parameters.ConditionalAdd("content", content);
            parameters.ConditionalAdd("enabled", enabled);

            return RunAsync<GetAddonsResponse>("Addons.GetAddons", parameters);
        }

        // todo:
        public Task SetAddonEnabled(string addonId, object enabled)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("addonid", addonId);
            parameters.ConditionalAdd("enabled", enabled);

            return RunAsync("Addons.ExecuteAddon", parameters);
        }

    }
}
