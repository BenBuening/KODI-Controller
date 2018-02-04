using System.Collections.Generic;

namespace Kodi.JsonRpc.GlobalTypes.Audio.Details
{
    public class Media : Kodi.JsonRpc.GlobalTypes.Audio.Details.Base
    {
        public string DisplayArtist { get; set; }
        public List<string> Artist { get; set; }
        public List<int> GenreId { get; set; }
        public string MusicBrainzAlbumArtistId { get; set; }
        public int Year { get; set; }
        public int Rating { get; set; }
        public List<int> ArtistId { get; set; }
        public string Title { get; set; }
        public string MusicBrainzAlbumId { get; set; }
    }
}
