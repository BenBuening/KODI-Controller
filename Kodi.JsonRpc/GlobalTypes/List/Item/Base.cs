using Kodi.JsonRpc.GlobalTypes.Media;
using Kodi.JsonRpc.GlobalTypes.Video;
using Kodi.JsonRpc.GlobalTypes.Video.Details;
using System;
using System.Collections.Generic;

namespace Kodi.JsonRpc.GlobalTypes.List.Item
{
    public class Base
    {
        public string Album { get; set; }
        public List<string> AlbumArtist { get; set; }
        public List<int> AlbumArtistId { get; set; }
        public int? AlbumId { get; set; }
        public string AlbumLabel { get; set; }
        public ArtWork Art { get; set; }
        public List<string> Artist { get; set; }
        public List<int> ArtistId { get; set; }
        public List<Cast> Cast { get; set; }
        public string Comment { get; set; }
        public List<string> Country { get; set; }
        public DateTime? DateAdded { get; set; }
        public string Description { get; set; }
        public List<string> Director { get; set; }
        public int? Disc { get; set; }
        public string DisplayArtist { get; set; }
        public int Duration { get; set; }
        public int Episode { get; set; }
        public string EpisodeGuide { get; set; }
        public string FanArt { get; set; }
        public string File { get; set; }
        public string FirstAired { get; set; }
        public List<string> Genre { get; set; }
        public List<int> GenreId { get; set; }
        public int Id { get; set; }
        public string ImdbNumber { get; set; }
        public string Label { get; set; }
        public DateTime? LastPlayed { get; set; }
        public string Lyrics { get; set; }
        public List<string> Mood { get; set; }
        public string MPAA { get; set; }
        public string MusicBrainzAlbumArtistiId { get; set; }
        public string MusicBrainzAlbumId { get; set; }
        public string MusicBrainzArtistId { get; set; }
        public string MusicBrianzTrackId { get; set; }
        public string OriginalTitle { get; set; }
        public int? PlayCount { get; set; }
        public string Plot { get; set; }
        public string PlotOutline { get; set; }
        public string Premiered { get; set; }
        public string ProductionCode { get; set; }
        public double? Rating { get; set; }
        public Resume Resume { get; set; }
        public int? RunTime { get; set; }
        public int? Season { get; set; }
        public string Set { get; set; }
        public int? SetId { get; set; }
        public List<string> ShowLink { get; set; }
        public string ShowTitle { get; set; }
        public string SortTitle { get; set; }
        public Streams StreamDetails { get; set; }
        public List<string> Studio { get; set; }
        public List<string> Style { get; set; }
        public List<string> Tag { get; set; }
        public string TagLine { get; set; }
        public List<string> Theme { get; set; }
        public string Thumbnail { get; set; }
        public string Title { get; set; }
        public int? Top250 { get; set; }
        public int? Track { get; set; }
        public string Trailer { get; set; }
        public int? TvShowId { get; set; }
        public string Type { get; set; }
        public Dictionary<string, string> UniqueId { get; set; }
        public string Votes { get; set; }
        public int? WatchedEpisodes { get; set; }
        public List<string> Writer { get; set; }
        public int? Year { get; set; }
    }
}
