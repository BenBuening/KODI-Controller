using System.Collections.Generic;

namespace Kodi.JsonRpc.GlobalTypes.Video
{
    public class Streams
    {
        public List<VideoStream> Video { get; set; }
        public List<AudioStream> Audio { get; set; }
        public List<SubtitleStream> Subtitle { get; set; }
    }
}
