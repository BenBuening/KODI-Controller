using Kodi.JsonRpc.GlobalTypes.Application.Notifications;
using Kodi.JsonRpc.GlobalTypes.Application.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kodi.JsonRpc.Methods
{
    public class Application : MethodLibraryBase
    {

        internal Application(API api) : base(api)
        {
            api.RegisterNotificationHandler("Application.OnVolumeChanged", typeof(OnVolumeChangedEventData), HandleOnPause);
        }

        private void HandleOnPause(object arg)
        {
            this.OnVolumeChanged?.Invoke(this, ((OnVolumeChangedEventParams)arg).Data);
        }


        public event EventHandler<OnVolumeChangedEventData> OnVolumeChanged;

        public Task<Value> GetProperties(List<string> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString().ToLower()).ToList());

            return RunAsync<Value>("Application.GetProperties", parameters);
        }

        public Task Quit()
        {
            return RunAsync("Application.Quit", null);
        }

        public Task<bool> SetMute(object mute)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("mute", mute);

            return RunAsync<bool>("Application.SetMute", parameters);
        }

        public Task<int> SetVolume(object volume)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("volume", volume);

            return RunAsync<int>("Application.SetVolume", parameters);
        }

    }
}
