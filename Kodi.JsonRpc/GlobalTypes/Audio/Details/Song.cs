using System.Collections.Generic;

namespace Kodi.JsonRpc.GlobalTypes.Audio.Details
{
    public class Song : Kodi.JsonRpc.GlobalTypes.Audio.Details.Media
    {
        public string Lyrics { get; set; }
        public int SongId { get; set; }
        public List<int> AlbumArtistId { get; set; }
        public int Disc { get; set; }
        public string Comment { get; set; }
        public int PlayCount { get; set; }
        public string Album { get; set; }
        public string File { get; set; }
        public string LastPlayed { get; set; }
        public int AlbumId { get; set; }
        public string MusicBrainzArtistId { get; set; }
        public List<string> AlbumArtist { get; set; }
        public int Duration { get; set; }
        public string MusicBrainzTrackId { get; set; }
        public int Track { get; set; }
    }
}
