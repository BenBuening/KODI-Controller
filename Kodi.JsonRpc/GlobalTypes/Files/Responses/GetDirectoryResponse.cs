using Kodi.JsonRpc.GlobalTypes.List;
using Kodi.JsonRpc.GlobalTypes.List.Item;
using System.Collections.Generic;

namespace Kodi.JsonRpc.GlobalTypes.Files.Responses
{
    public class GetDirectoryResponse
    {
        public LimitsReturned Limits { get; set; }
        public List<File> File { get; set; }
    }
}
