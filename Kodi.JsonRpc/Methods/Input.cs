using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kodi.JsonRpc.Methods
{
    public class Input : MethodLibraryBase
    {

        internal Input(API api) : base(api)
        {
            // todo: notifications
            api.RegisterNotificationHandler("Input.OnInputFinished", typeof(object), HandleOnInputFinished);
            api.RegisterNotificationHandler("Input.OnInputRequested", typeof(object), HandleOnInputRequested);
        }

        private void HandleOnInputFinished(object arg)
        {
            this.OnInputFinished?.Invoke(this, null);
        }

        private void HandleOnInputRequested(object arg)
        {
            this.OnInputRequested?.Invoke(this, null);
        }


        public event EventHandler OnInputFinished;
        public event EventHandler OnInputRequested;

        public Task Back()
        {
            return RunAsync("Input.Back", null);
        }

        public Task ContextMenu()
        {
            return RunAsync("Input.ContextMenu", null);
        }

        public Task Down()
        {
            return RunAsync("Input.Down", null);
        }

        public Task ExecuteAction(string action)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("action", action);

            return RunAsync("Input.ExecuteAction", parameters);
        }

        public Task Home()
        {
            return RunAsync("Input.Home", null);
        }

        public Task Info()
        {
            return RunAsync("Input.Info", null);
        }

        public Task Left()
        {
            return RunAsync("Input.Left", null);
        }

        public Task Right()
        {
            return RunAsync("Input.Right", null);
        }

        public Task Select()
        {
            return RunAsync("Input.Select", null);
        }

        public Task SendText(string text, bool done = true)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("text", text);
            parameters.Add("done", done);

            return RunAsync("Input.SendText", parameters);
        }

        public Task ShowCodec()
        {
            return RunAsync("Input.ShowCodec", null);
        }

        public Task ShowOSD()
        {
            return RunAsync("Input.ShowOSD", null);
        }

        public Task Up()
        {
            return RunAsync("Input.Up", null);
        }

    }
}
