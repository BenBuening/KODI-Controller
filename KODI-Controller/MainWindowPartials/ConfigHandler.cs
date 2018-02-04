using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KODI_Controller
{
    public partial class MainWindow
    {

        private void SetUpConfigHandlers()
        {
            // todo: config
            this.Config = new ClientConfiguration();
            this.Config.Kodis = new List<KodiAddress>(2) { new KodiAddress() { IpAddress = "192.168.1.148", Port = 9090 }, new KodiAddress() { IpAddress = "192.168.1.149", Port = 9090 } };
            this.Config.LastConnected = Config.Kodis[0];
            this.Config.ImageCachePath = @"c:\_temp\images\";
        }

    }
}
