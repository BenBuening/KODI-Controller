using Kodi.JsonRpc.GlobalTypes.System.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kodi.JsonRpc.Methods
{
    public class System : MethodLibraryBase
    {

        internal System(API api) : base(api)
        {
            // todo: notifications
            api.RegisterNotificationHandler("System.OnLowBattery", typeof(object), HandleOnLowBattery);
            api.RegisterNotificationHandler("System.OnQuit", typeof(object), HandleOnQuit);
            api.RegisterNotificationHandler("System.OnRestart", typeof(object), HandleOnRestart);
            api.RegisterNotificationHandler("System.OnSleep", typeof(object), HandleOnSleep);
            api.RegisterNotificationHandler("System.OnWake", typeof(object), HandleOnWake);
        }

        private void HandleOnLowBattery(object arg)
        {
            this.OnLowBattery?.Invoke(this, null);
        }

        private void HandleOnQuit(object arg)
        {
            this.OnQuit?.Invoke(this, null);
        }

        private void HandleOnRestart(object arg)
        {
            this.OnRestart?.Invoke(this, null);
        }

        private void HandleOnSleep(object arg)
        {
            this.OnSleep?.Invoke(this, null);
        }

        private void HandleOnWake(object arg)
        {
            this.OnWake?.Invoke(this, null);
        }


        public event EventHandler OnLowBattery;
        public event EventHandler OnQuit;
        public event EventHandler OnRestart;
        public event EventHandler OnSleep;
        public event EventHandler OnWake;

        public Task EjectOpticalDrive()
        {
            return RunAsync("System.EjectOpticalDrive", null);
        }

        // todo:
        public Task<Value> GetProperties(List<string> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString().ToLower()).ToList());

            return RunAsync<Value>("System.GetProperties", parameters);
        }

        public Task Hibernate()
        {
            return RunAsync("System.Hibernate", null);
        }

        public Task Reboot()
        {
            return RunAsync("System.Reboot", null);
        }

        public Task Shutdown()
        {
            return RunAsync("System.Shutdown", null);
        }

        public Task Suspend()
        {
            return RunAsync("System.Suspend", null);
        }

    }
}
