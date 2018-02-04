namespace Kodi.JsonRpc.GlobalTypes.Player.Audio
{
    public class StreamExtended : Kodi.JsonRpc.GlobalTypes.Player.Audio.Stream
    {
        public int Channels { get; set; }
        public string Codec { get; set; }
        public int BitRate { get; set; }
    }
}
