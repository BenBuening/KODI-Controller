using Kodi.JsonRpc.GlobalTypes.Global;
using Kodi.JsonRpc.GlobalTypes.List;
using Kodi.JsonRpc.GlobalTypes.Playlist;
using Kodi.JsonRpc.GlobalTypes.Playlist.Notifications;
using Kodi.JsonRpc.GlobalTypes.Playlist.Property;
using Kodi.JsonRpc.GlobalTypes.Playlist.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kodi.JsonRpc.Methods
{
    public class Playlist : MethodLibraryBase
    {

        internal Playlist(API api)
            : base(api)
        {
            api.RegisterNotificationHandler("Playlist.OnAdd", typeof(OnAddEventData), HandleOnAdd);
            api.RegisterNotificationHandler("Playlist.OnClear", typeof(OnClearEventData), HandleOnClear);
            api.RegisterNotificationHandler("Playlist.OnRemove", typeof(OnRemoveEventData), HandleOnRemove);
        }

        private void HandleOnAdd(object arg)
        {
            this.OnAdd?.Invoke(this, (OnAddEventData)arg);
        }

        private void HandleOnClear(object arg)
        {
            this.OnClear?.Invoke(this, (OnClearEventData)arg);
        }

        private void HandleOnRemove(object arg)
        {
            this.OnRemove?.Invoke(this, (OnRemoveEventData)arg);
        }


        public IReadOnlyList<AllProperty> CommonSummaryProperties { get; private set; }
            = new List<AllProperty>()
            {
                AllProperty.episode,
                AllProperty.lastplayed,
                AllProperty.runtime,
                AllProperty.season,
                AllProperty.showtitle,
                AllProperty.title,
            };

        public IReadOnlyList<AllProperty> CommonProperties { get; private set; }
            = new List<AllProperty>()
            {
                AllProperty.album,
                AllProperty.albumartist,
                AllProperty.albumartistid,
                AllProperty.albumid,
                AllProperty.albumlabel,
                AllProperty.art,
                AllProperty.artist,
                AllProperty.artistid,
                AllProperty.cast,
                AllProperty.channel,
                AllProperty.channelnumber,
                AllProperty.channeltype,
                AllProperty.comment,
                AllProperty.country,
                AllProperty.dateadded,
                AllProperty.description,
                AllProperty.director,
                AllProperty.disc,
                AllProperty.displayartist,
                AllProperty.duration,
                AllProperty.endtime,
                AllProperty.episode,
                AllProperty.episodeguide,
                AllProperty.fanart,
                AllProperty.file,
                AllProperty.firstaired,
                AllProperty.genre,
                AllProperty.genreid,
                AllProperty.hidden,
                AllProperty.imdbnumber,
                AllProperty.lastplayed,
                AllProperty.locked,
                AllProperty.lyrics,
                AllProperty.mood,
                AllProperty.mpaa,
                AllProperty.musicbrainzalbumartistid,
                AllProperty.musicbrainzalbumid,
                AllProperty.musicbrainzartistid,
                AllProperty.musicbrainztrackid,
                AllProperty.originaltitle,
                AllProperty.playcount,
                AllProperty.plot,
                AllProperty.plotoutline,
                AllProperty.premiered,
                AllProperty.productioncode,
                AllProperty.rating,
                AllProperty.resume,
                AllProperty.runtime,
                AllProperty.season,
                AllProperty.set,
                AllProperty.setid,
                AllProperty.showlink,
                AllProperty.showtitle,
                AllProperty.sorttitle,
                AllProperty.starttime,
                AllProperty.streamdetails,
                AllProperty.studio,
                AllProperty.style,
                AllProperty.tag,
                AllProperty.tagline,
                AllProperty.theme,
                AllProperty.thumbnail,
                AllProperty.title,
                AllProperty.top250,
                AllProperty.track,
                AllProperty.trailer,
                AllProperty.tvshowid,
                AllProperty.uniqueid,
                AllProperty.votes,
                AllProperty.watchedepisodes,
                AllProperty.writer,
                AllProperty.year
            };




        public event EventHandler<OnAddEventData> OnAdd;
        public event EventHandler<OnClearEventData> OnClear;
        public event EventHandler<OnRemoveEventData> OnRemove;



        public Task AddAlbum(int playlistId, int albumId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("playlistid", playlistId);
            parameters.Add("item", new Dictionary<string, object>(1) { ["albumId"] = albumId });

            return RunAsync("Playlist.Add", parameters);
        }

        public Task AddArtist(int playlistId, int artistId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("playlistid", playlistId);
            parameters.Add("item", new Dictionary<string, object>(1) {["artistid"] = artistId });

            return RunAsync("Playlist.Add", parameters);
        }

        public Task AddEpisode(int playlistId, int episodeId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("playlistid", playlistId);
            parameters.Add("item", new { episodeId } /*Dictionary<string, object>(1) {["episodeid"] = episodeId }*/);

            return RunAsync("Playlist.Add", parameters);
        }

        public Task AddFile(int playlistId, string file)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("playlistid", playlistId);
            parameters.Add("item", new Dictionary<string, object>(1) {["file"] = file });

            return RunAsync("Playlist.Add", parameters);
        }

        public Task AddGenre(int playlistId, int genreId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("playlistid", playlistId);
            parameters.Add("item", new Dictionary<string, object>(1) {["genreid"] = genreId });

            return RunAsync("Playlist.Add", parameters);
        }

        public Task AddMovie(int playlistId, int movieId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("playlistid", playlistId);
            parameters.Add("item", new Dictionary<string, object>(1) {["movieid"] = movieId });

            return RunAsync("Playlist.Add", parameters);
        }

        public Task AddMusicVideo(int playlistId, int musicVideoId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("playlistid", playlistId);
            parameters.Add("item", new Dictionary<string, object>(1) {["musicvideoid"] = musicVideoId });

            return RunAsync("Playlist.Add", parameters);
        }

        public Task Add(int playlistId, string directory, MediaType media, bool recursive)
        {
            Dictionary<string, object> item = new Dictionary<string, object>(3);
            item.Add("directory", directory);
            item.Add("media", media.ToString());
            item.Add("recursive", recursive);

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("playlistid", playlistId);
            parameters.Add("item", item);

            return RunAsync("Playlist.Add", parameters);
        }

        public Task AddSong(int playlistId, int songId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("playlistid", playlistId);
            parameters.Add("item", new Dictionary<string, object>(1) {["songid"] = songId });

            return RunAsync("Playlist.Add", parameters);
        }

        public Task Clear(int playlistId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>(1);
            parameters.Add("playlistid", playlistId);

            return RunAsync("Playlist.Clear", parameters);
        }





        public Task<GetItemsResponse> GetItems(int playlistId, Limits limits, Sort sort, IEnumerable<AllProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("playlistid", playlistId);
            parameters.ConditionalAdd("limits", limits);
            parameters.ConditionalAdd("sort", sort);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<GetItemsResponse>("Playlist.GetItems", parameters);
        }

        public Task<GetItemsResponse> GetItems(int playlistId, Limits limits, Sort sort)
        {
            return GetItems(playlistId, limits, sort, this.CommonSummaryProperties);
        }

        public Task<GetItemsResponse> GetItems(int playlistId, IEnumerable<AllProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("playlistid", playlistId);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<GetItemsResponse>("Playlist.GetItems", parameters);
        }

        public Task<GetItemsResponse> GetItems(int playlistId)
        {
            return GetItems(playlistId, this.CommonSummaryProperties);
        }

        public Task<GetItemsResponse> GetItems(int playlistId, Limits limits, IEnumerable<AllProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("playlistid", playlistId);
            parameters.ConditionalAdd("limits", limits);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<GetItemsResponse>("Playlist.GetItems", parameters);
        }

        public Task<GetItemsResponse> GetItems(int playlistId, Limits limits)
        {
            return GetItems(playlistId, limits, this.CommonSummaryProperties);
        }

        public Task<GetItemsResponse> GetItems(int playlistId, Sort sort, IEnumerable<AllProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("playlistid", playlistId);
            parameters.ConditionalAdd("sort", sort);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<GetItemsResponse>("Playlist.GetItems", parameters);
        }

        public Task<GetItemsResponse> GetItems(int playlistId, Sort sort)
        {
            return GetItems(playlistId, sort, this.CommonSummaryProperties);
        }









        public Task<List<GlobalTypes.Playlist.Responses.Playlist>> GetPlaylists()
        {
            return RunAsync<List<GlobalTypes.Playlist.Responses.Playlist>>("Playlist.GetPlaylists", null);
        }

        public Task<Value> GetProperties(int playlistId, List<ValueProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("playlistid", playlistId);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString().ToLower()).ToList());

            return RunAsync<Value>("Playlist.GetProperties", parameters);
        }

        public Task InsertAlbum(int playlistId, int position, int albumId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("playlistid", playlistId);
            parameters.Add("position", position);
            parameters.Add("item", new Dictionary<string, object>(1) {["albumid"] = albumId });

            return RunAsync("Playlist.Insert", parameters);
        }

        public Task InsertArtist(int playlistId, int position, int artistId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("playlistid", playlistId);
            parameters.Add("position", position);
            parameters.Add("item", new Dictionary<string, object>(1) {["artistid"] = artistId });

            return RunAsync("Playlist.Insert", parameters);
        }

        public Task InsertEpisode(int playlistId, int position, int episodeId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("playlistid", playlistId);
            parameters.Add("position", position);
            parameters.Add("item", new Dictionary<string, object>(1) {["episodeid"] = episodeId });

            return RunAsync("Playlist.Insert", parameters);
        }

        public Task InsertFile(int playlistId, int position, string file)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("playlistid", playlistId);
            parameters.Add("position", position);
            parameters.Add("item", new Dictionary<string, object>(1) {["file"] = file });

            return RunAsync("Playlist.Insert", parameters);
        }

        public Task InsertGenre(int playlistId, int position, int genreId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("playlistid", playlistId);
            parameters.Add("position", position);
            parameters.Add("item", new Dictionary<string, object>(1) {["genreid"] = genreId });

            return RunAsync("Playlist.Insert", parameters);
        }

        public Task InsertMovie(int playlistId, int position, int movieId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("playlistid", playlistId);
            parameters.Add("position", position);
            parameters.Add("item", new Dictionary<string, object>(1) {["movieid"] = movieId });

            return RunAsync("Playlist.Insert", parameters);
        }

        public Task InsertMusicVideo(int playlistId, int position, int musicVideoId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("playlistid", playlistId);
            parameters.Add("position", position);
            parameters.Add("item", new Dictionary<string, object>(1) {["musicvideoid"] = musicVideoId });

            return RunAsync("Playlist.Insert", parameters);
        }

        public Task Insert(int playlistId, int position, string directory, MediaType media, bool recursive)
        {
            Dictionary<string, object> item = new Dictionary<string, object>(3);
            item.Add("directory", directory);
            item.Add("media", media.ToString());
            item.Add("recursive", recursive);

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("playlistid", playlistId);
            parameters.Add("position", position);
            parameters.Add("item", item);

            return RunAsync("Playlist.Insert", parameters);
        }

        public Task InsertSong(int playlistId, int position, int songId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("playlistid", playlistId);
            parameters.Add("position", position);
            parameters.Add("item", new Dictionary<string, object>(1) {["songid"] = songId });

            return RunAsync("Playlist.Insert", parameters);
        }

        public Task Remove(int playlistId, int position)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("playlistid", playlistId);
            parameters.Add("position", position);

            return RunAsync("Playlist.Remove", parameters);
        }

        public Task Swap(int playlistId, int position1, int position2)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("playlistid", playlistId);
            parameters.Add("position1", position1);
            parameters.Add("position2", position2);

            return RunAsync("Playlist.Swap", parameters);
        }

    }
}
