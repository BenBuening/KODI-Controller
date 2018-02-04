using Kodi.JsonRpc.GlobalTypes.Audio.Responses;
using Kodi.JsonRpc.GlobalTypes.List;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kodi.JsonRpc.Methods
{
    public class AudioLibrary : MethodLibraryBase
    {

        internal AudioLibrary(API api) : base(api)
        {
            // todo: notifications
            api.RegisterNotificationHandler("AudioLibrary.OnCleanFinished", typeof(object), HandleOnCleanFinished);
            api.RegisterNotificationHandler("AudioLibrary.OnCleanStarted", typeof(object), HandleOnCleanStarted);
            api.RegisterNotificationHandler("AudioLibrary.OnRemove", typeof(object), HandleOnRemove);
            api.RegisterNotificationHandler("AudioLibrary.OnScanFinished", typeof(object), HandleOnScanFinished);
            api.RegisterNotificationHandler("AudioLibrary.OnScanStarted", typeof(object), HandleOnScanStarted);
            api.RegisterNotificationHandler("AudioLibrary.OnUpdate", typeof(object), HandleOnUpdate);
        }

        private void HandleOnCleanFinished(object arg)
        {
            this.OnCleanFinished?.Invoke(this, null);
        }

        private void HandleOnCleanStarted(object arg)
        {
            this.OnCleanStarted?.Invoke(this, null);
        }

        private void HandleOnRemove(object arg)
        {
            this.OnRemove?.Invoke(this, null);
        }

        private void HandleOnScanFinished(object arg)
        {
            this.OnScanFinished?.Invoke(this, null);
        }

        private void HandleOnScanStarted(object arg)
        {
            this.OnScanStarted?.Invoke(this, null);
        }

        private void HandleOnUpdate(object arg)
        {
            this.OnUpdate?.Invoke(this, null);
        }



        public event EventHandler OnCleanFinished;
        public event EventHandler OnCleanStarted;
        public event EventHandler OnRemove;
        public event EventHandler OnScanFinished;
        public event EventHandler OnScanStarted;
        public event EventHandler OnUpdate;


        public Task Clean()
        {
            return RunAsync("AudioLibrary.Clean", null);
        }

        // todo:
        public Task Export(object options)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("options", options);

            return RunAsync("AudioLibrary.Export", parameters);
        }

        // todo:
        public Task<GetAlbumDetailsResponse> GetAlbumDetails(int albumId, List<string> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("albumid", albumId);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString().ToLower()).ToList());

            return RunAsync<GetAlbumDetailsResponse>("AudioLibrary.GetAlbumDetails", parameters);
        }

        // todo:
        public Task<GetAlbumsResponse> GetAlbums(Limits limits, Sort sort, List<string> properties, object filter)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("limits", limits);
            parameters.ConditionalAdd("sort", sort);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString().ToLower()).ToList());
            parameters.ConditionalAdd("filter", filter);

            return RunAsync<GetAlbumsResponse>("AudioLibrary.GetAlbums", parameters);
        }

        // todo:
        public Task<GetArtistDetailsResponse> GetArtistDetails(int artistId, List<string> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("artistid", artistId);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString().ToLower()).ToList());

            return RunAsync<GetArtistDetailsResponse>("AudioLibrary.GetArtistDetails", parameters);
        }

        // todo:
        public Task<GetArtistsResponse> GetArtists(Limits limits, Sort sort, List<string> properties, object filter, bool? albumArtistsOnly = null)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("limits", limits);
            parameters.ConditionalAdd("sort", sort);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString().ToLower()).ToList());
            parameters.ConditionalAdd("filter", filter);
            parameters.ConditionalAdd("albumartistsonly", albumArtistsOnly);

            return RunAsync<GetArtistsResponse>("AudioLibrary.GetArtists", parameters);
        }

        // todo:
        public Task<GetGenresResponse> GetGenres(Limits limits, Sort sort, List<string> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("limits", limits);
            parameters.ConditionalAdd("sort", sort);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString().ToLower()).ToList());

            return RunAsync<GetGenresResponse>("AudioLibrary.GetGenres", parameters);
        }

        // todo:
        public Task<GetRecentlyAddedAlbumsResponse> GetRecentlyAddedAlbums(Limits limits, Sort sort, List<string> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("limits", limits);
            parameters.ConditionalAdd("sort", sort);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString().ToLower()).ToList());

            return RunAsync<GetRecentlyAddedAlbumsResponse>("AudioLibrary.GetRecentlyAddedAlbums", parameters);
        }

        // todo:
        public Task<GetRecentlyAddedSongsResponse> GetRecentlyAddedSongs(Limits limits, Sort sort, List<string> properties, int? albumLimit)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("limits", limits);
            parameters.ConditionalAdd("sort", sort);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString().ToLower()).ToList());
            parameters.ConditionalAdd("albumlimit", albumLimit);

            return RunAsync<GetRecentlyAddedSongsResponse>("AudioLibrary.GetRecentlyAddedSongs", parameters);
        }

        // todo:
        public Task<GetRecentlyPlayedAlbumsResponse> GetRecentlyPlayedAlbums(Limits limits, Sort sort, List<string> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("limits", limits);
            parameters.ConditionalAdd("sort", sort);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString().ToLower()).ToList());

            return RunAsync<GetRecentlyPlayedAlbumsResponse>("AudioLibrary.GetRecentlyPlayedAlbums", parameters);
        }

        // todo:
        public Task<GetRecentlyPlayedSongsResponse> GetRecentlyPlayedSongs(Limits limits, Sort sort, List<string> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("limits", limits);
            parameters.ConditionalAdd("sort", sort);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString().ToLower()).ToList());

            return RunAsync<GetRecentlyPlayedSongsResponse>("AudioLibrary.GetRecentlyPlayedSongs", parameters);
        }

        // todo:
        public Task<GetSongDetailsResponse> GetSongDetails(int songId, List<string> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("songid", songId);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString().ToLower()).ToList());

            return RunAsync<GetSongDetailsResponse>("AudioLibrary.GetSongDetails", parameters);
        }

        public Task<GetSongsResponse> GetSongs(Limits limits, Sort sort, List<string> properties, object filter)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("limits", limits);
            parameters.ConditionalAdd("sort", sort);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString().ToLower()).ToList());
            parameters.ConditionalAdd("filter", filter);

            return RunAsync<GetSongsResponse>("AudioLibrary.GetSongs", parameters);
        }

        public Task Scan(string directory)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("directory", directory);

            return RunAsync("AudioLibrary.Scan", parameters);
        }

        public Task SetAlbumDetails(AlbumDetails albumDetails)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("albumdetails", albumDetails);

            return RunAsync("AudioLibrary.SetAlbumDetails", parameters);
        }

        public Task SetArtistDetails(ArtistDetails artistDetails)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("artistdetails", artistDetails);

            return RunAsync("AudioLibrary.SetArtistDetails", parameters);
        }

        public Task SetSongDetails(SongDetails songDetails)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("songdetails", songDetails);

            return RunAsync("AudioLibrary.SetSongDetails", parameters);
        }



        public class AlbumDetails
        {
            public int AlbumId { get; set; }
            public string Title { get; set; }
            public List<string> Artist { get; set; }
            public string Description { get; set; }
            public List<string> Genre { get; set; }
            public List<string> Theme { get; set; }
            public List<string> Mood { get; set; }
            public List<string> Style { get; set; }
            public string Type { get; set; }
            public string AlbumLabel { get; set; }
            public int Rating { get; set; }
            public int Year { get; set; }
        }

        public class ArtistDetails
        {
            public int ArtistId { get; set; }
            public string Artist { get; set; }
            public List<string> Instrument { get; set; }
            public List<string> Style { get; set; }
            public List<string> Mood { get; set; }
            public string Born { get; set; }
            public string Formed { get; set; }
            public string Description { get; set; }
            public List<string> Genre { get; set; }
            public string Died { get; set; }
            public string Disbanded { get; set; }
            public List<string> YearsActive { get; set; }
        }

        public class SongDetails
        {
            public int SongId { get; set; }
            public string Title { get; set; }
            public List<string> Artist { get; set; }
            public List<string> AlbumArtist { get; set; }
            public List<string> Genre { get; set; }
            public int Year { get; set; }
            public int Rating { get; set; }
            public string Album { get; set; }
            public int Track { get; set; }
            public int Disc { get; set; }
            public int Duration { get; set; }
            public string Comment { get; set; }
            public string MusicBrainzTrackId { get; set; }
            public string MusicBrainzArtistId { get; set; }
            public string MusicBrainzAlbumId { get; set; }
            public string MusicBrainzAlbumArtistId { get; set; }
        }

    }
}
