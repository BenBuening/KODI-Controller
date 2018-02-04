using Newtonsoft.Json;

namespace Kodi.JsonRpc.GlobalTypes.List.Item
{
    public class File : Kodi.JsonRpc.GlobalTypes.List.Item.Base
    {
        public string FileType { get; set; }
        public int Size { get; set; }
        public string MimeType { get; set; }
        [JsonProperty("file")]
        public string FileName { get; set; }
        public string LastModified { get; set; }
    }
}
