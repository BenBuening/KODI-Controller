using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KODI_Controller
{
    public partial class MainWindow
    {

        private void SetupApiSystemHandlers()
        {
            this.API.MessageReceived += API_MessageReceived;
            this.API.Connected += API_Connected;
            this.API.ShuttingDown += API_ShuttingDown;
        }

        private void API_ShuttingDown(object sender, EventArgs e)
        {
            this.Dispatcher.Invoke(Close);
        }

        private void API_MessageReceived(object sender, string e)
        {
            this.Dispatcher.Invoke(new Action<string>(x => this._messages.Add(x)), new object[] { e });
        }

        private async void API_Connected(object sender, EventArgs e)
        {
            Task a = LoadPlaylist();
            Task b = LoadCurrentlyPlaying();
            await Task.WhenAll(a, b);
            this.ViewModel.IsAppReady = true;
        }

    }
}
