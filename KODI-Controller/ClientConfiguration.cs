using System.Collections.Generic;

namespace KODI_Controller
{
    internal class ClientConfiguration
    {
        public List<KodiAddress> Kodis { get; set; }
        public KodiAddress LastConnected { get; set; }
        public string ImageCachePath { get; set; }
    }
    
}
