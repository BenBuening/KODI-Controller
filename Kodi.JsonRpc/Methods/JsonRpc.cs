using Kodi.JsonRpc.GlobalTypes.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kodi.JsonRpc.Methods
{
    public class JsonRpc : MethodLibraryBase
    {

        internal JsonRpc(API api) : base(api) { }


        public Task<Configuration> GetConfiguration()
        {
            return RunAsync<Configuration>("JSONRPC.GetConfiguration", null);
        }

        // todo:
        public Task<object> Introspect(IntrospectFilter filter, bool filterByTransport = true, bool getMetaData = false, bool getDescription = true)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("filter", filter);
            parameters.Add("filterbytransport", filterByTransport);
            parameters.Add("getmetadata", getMetaData);
            parameters.Add("getdescription", getDescription);

            return RunAsync<object>("JSONRPC.Introspect", parameters);
        }

        // todo:
        public Task<object> NotifyAll(string sender, string message, object data = null)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("sender", sender);
            parameters.Add("message", message);
            parameters.Add("data", data);

            return RunAsync<object>("JSONRPC.NotifyAll", parameters);
        }

        public Task<Permissions> Permission()
        {
            return RunAsync<Permissions>("JSONRPC.Permission", null);
        }

        public Task Ping()
        {
            return RunAsync("JSONRPC.Ping", null);
        }

        public Task<Configuration> SetConfiguration(ConfigurationNotifications notifications)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("notifications", notifications);

            return RunAsync<Configuration>("JSONRPC.SetConfiguration", parameters);
        }

        public Task<Version> GetVersion()
        {
            return RunAsync<Version>("JSONRPC.Version", null);
        }



        public class IntrospectFilter
        {
            public bool GetReferences { get; set; }
            public string Id { get; set; }
            public string Type { get; set; }
        }

        public class Permissions
        {
            public bool ReadData { get; set; }
            public bool WriteFile { get; set; }
            public bool ControlPvr { get; set; }
            public bool ControlSystem { get; set; }
            public bool RemoveData { get; set; }
            public bool ControlPlayback { get; set; }
            public bool Navigate { get; set; }
            public bool ControlPower { get; set; }
            public bool ExecuteAddon { get; set; }
            public bool ManageAddon { get; set; }
            public bool ControlGui { get; set; }
            public bool ControlNotify { get; set; }
            public bool UpdateData { get; set; }
        }

        public class ConfigurationNotifications
        {
            public bool? Gui { get; set; }
            public bool? Other { get; set; }
            public bool? Input { get; set; }
            public bool? VideoLibrary { get; set; }
            public bool? AudioLibrary { get; set; }
            public bool? Playlist { get; set; }
            public bool? System { get; set; }
            public bool? Player { get; set; }
            public bool? Application { get; set; }
        }

        public class Version
        {
            public int Minor { get; set; }
            public int Patch { get; set; }
            public int Major { get; set; }
        }

    }
}
