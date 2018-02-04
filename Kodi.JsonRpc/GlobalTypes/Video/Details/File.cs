using System.Collections.Generic;

namespace Kodi.JsonRpc.GlobalTypes.Video.Details
{
    public class File : Kodi.JsonRpc.GlobalTypes.Video.Details.Item
    {
        public Video.Streams StreamDetails { get; set; }
        public List<string> Director { get; set; }
        public Video.Resume Resume { get; set; }
        public int? RunTime { get; set; }
    }
}
