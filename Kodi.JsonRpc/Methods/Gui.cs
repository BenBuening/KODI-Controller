using Kodi.JsonRpc.GlobalTypes.Gui.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kodi.JsonRpc.Methods
{
    public class Gui : MethodLibraryBase
    {

        #region Child Object Definitions

        public class ScreensaverDeactivatedEventArgs : EventArgs
        {
            public bool ShuttingDown { get; private set; }
            public ScreensaverDeactivatedEventArgs(bool? shuttingDown) { this.ShuttingDown = shuttingDown.GetValueOrDefault(); }
        }

        #endregion  



        internal Gui(API api) : base(api)
        {
            // todo: notifications
            api.RegisterNotificationHandler("GUI.OnScreensaverDeactivated", typeof(object), HandleOnScreensaverDeactivated);
            api.RegisterNotificationHandler("GUI.OnScreensaverActivated", typeof(object), HandleOnScreensaverActivated);
        }

        private void HandleOnScreensaverDeactivated(object arg)
        {
            this.OnScreensaverDeactivated?.Invoke(this, null);
        }

        private void HandleOnScreensaverActivated(object arg)
        {
            this.OnScreensaverActivated?.Invoke(this, null);
        }


        public event EventHandler OnScreensaverDeactivated;
        public event EventHandler OnScreensaverActivated;

        // todo:
        public Task ActivateWindow(string window, List<object> parameters)
        {
            Dictionary<string, object> args = new Dictionary<string, object>();
            args.Add("window", window);
            args.ConditionalAdd("parameters", parameters);

            return RunAsync("GUI.ActivateWindow", args);
        }

        // todo:
        public Task<Value> GetProperties(List<string> properties)
        {
            Dictionary<string, object> args = new Dictionary<string, object>();
            args.Add("properties", properties?.Select(x => x.ToString().ToLower()).ToList());

            return RunAsync<Value>("GUI.GetProperties", args);
        }

        // todo:
        public Task<bool> SetFullscreen(object fullscreen)
        {
            Dictionary<string, object> args = new Dictionary<string, object>();
            args.Add("fullscreen", fullscreen);

            return RunAsync<bool>("GUI.SetFullscreen", args);
        }

        public Task ShowNotification(string title, string message, string image = null, int? displayTime = null)
        {
            Dictionary<string, object> args = new Dictionary<string, object>();
            args.Add("title", title);
            args.Add("message", message);
            args.ConditionalAdd("image", image);
            args.ConditionalAdd("displayTime", displayTime);

            return RunAsync("GUI.ShowNotification", args);
        }

    }
}
