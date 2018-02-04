using System.Collections.Generic;

namespace Kodi.JsonRpc.GlobalTypes.Addon
{
    public class Details : Kodi.JsonRpc.GlobalTypes.Item.Details.Base
    {
        public string AddonId { get; set; }
        public string Disclaimer { get; set; }
        public string FanArt { get; set; }
        public object Broken { get; set; }
        public string Author { get; set; }
        public bool Enabled { get; set; }
        public List<object> ExtraInfo { get; set; }
        public string Thumbnail { get; set; }
        public string Path { get; set; }
        public List<object> Dependencies { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string Summary { get; set; }
        public int Rating { get; set; }
    }
}
