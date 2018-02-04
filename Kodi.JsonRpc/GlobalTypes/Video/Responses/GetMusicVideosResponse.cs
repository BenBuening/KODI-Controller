using Kodi.JsonRpc.GlobalTypes.List;
using Kodi.JsonRpc.GlobalTypes.Video.Details;
using System.Collections.Generic;

namespace Kodi.JsonRpc.GlobalTypes.Video.Responses
{
    public class GetMusicVideosResponse
    {
        public LimitsReturned Limits { get; set; }
        public List<MusicVideo> MusicVideos { get; set; }
    }
}
