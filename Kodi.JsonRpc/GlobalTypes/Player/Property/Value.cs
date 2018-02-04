using System.Collections.Generic;

namespace Kodi.JsonRpc.GlobalTypes.Player.Property
{
    public class Value
    {
        public bool CanRepeat { get; set; }
        public bool CanMove { get; set; }
        public bool CanShuffle { get; set; }
        public int Speed { get; set; }
        public double Percentage { get; set; }
        public int PlaylistId { get; set; } // -1
        public List<Kodi.JsonRpc.GlobalTypes.Player.Audio.Stream> AudioStreams { get; set; }
        public int Position { get; set; }
        public string Repeat { get; set; } // "off"
        public Kodi.JsonRpc.GlobalTypes.Player.Subtitle CurrentSubTitle { get; set; }
        public bool CanRotate { get; set; }
        public bool CanZoom { get; set; }
        public bool CanChangeSpeed { get; set; }
        public string Type { get; set; } // "video"
        public bool PartyMode { get; set; }
        public List<Kodi.JsonRpc.GlobalTypes.Player.Subtitle> SubTitles { get; set; }
        public bool CanSeek { get; set; }
        public Kodi.JsonRpc.GlobalTypes.Global.Time Time { get; set; }
        public Kodi.JsonRpc.GlobalTypes.Global.Time TotalTime { get; set; }
        public bool Shuffled { get; set; }
        public Kodi.JsonRpc.GlobalTypes.Player.Audio.StreamExtended CurrentAudioStream { get; set; }
        public bool Live { get; set; }
        public bool SubtitleEnabled { get; set; }
    }
}
