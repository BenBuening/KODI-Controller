using Kodi.JsonRpc.GlobalTypes.Audio.Details;
using Kodi.JsonRpc.GlobalTypes.Global;
using Kodi.JsonRpc.GlobalTypes.Item.Details;
using Kodi.JsonRpc.GlobalTypes.List;
using Kodi.JsonRpc.GlobalTypes.List.Item;
using Kodi.JsonRpc.GlobalTypes.Player;
using Kodi.JsonRpc.GlobalTypes.Player.Notifications;
using Kodi.JsonRpc.GlobalTypes.Player.Property;
using Kodi.JsonRpc.GlobalTypes.Player.Responses;
using Kodi.JsonRpc.GlobalTypes.PVR.Details;
using Kodi.JsonRpc.GlobalTypes.Video.Details;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kodi.JsonRpc.Methods
{
    public class Player : MethodLibraryBase
    {

        #region Child Object Definitions

        public class OpenOptions
        {
            public bool? Shuffled { get; set; }
            public RepeatOption? Repeat { get; set; }
            public Resume Resume { get; set; }
            internal Dictionary<string, object> ToSerializable()
            {
                Dictionary<string, object> result = new Dictionary<string, object>();
                result.ConditionalAdd("shuffled", this.Shuffled);
                result.ConditionalAdd("repeat", this.Repeat);
                if (this.Resume != null)
                    if (this.Resume.Bool.HasValue)
                        result.Add("resume", this.Resume.Bool);
                    else if (this.Resume.PositionPercent.HasValue)
                        result.Add("resume", this.Resume.PositionPercent);
                    else if (this.Resume.PositionTime != null)
                        result.Add("resume", this.Resume.PositionTime);
                return result;
            }
        }

        public class Resume
        {
            public bool? Bool { get; private set; }
            public double? PositionPercent { get; private set; }
            public Time PositionTime { get; private set; }
            public Resume(bool value) { this.Bool = value; }
            public Resume(double positionPercent) { this.PositionPercent = positionPercent; }
            public Resume(Time positionTime) { this.PositionTime = positionTime; }
        }

        #endregion


        
        internal Player(API api)
            : base(api)
        {
            api.RegisterNotificationHandler("Player.OnPause", typeof(OnPauseEventData), HandleOnPause);
            api.RegisterNotificationHandler("Player.OnPlay", typeof(OnPlayEventData), HandleOnPlay);
            api.RegisterNotificationHandler("Player.OnPropertyChanged", typeof(OnPropertyChangedEventParams), HandleOnPropertyChanged);
            api.RegisterNotificationHandler("Player.OnSeek", typeof(OnSeekEventData), HandleOnSeek);
            api.RegisterNotificationHandler("Player.OnSpeedChanged", typeof(OnSpeedChangedEventData), HandleOnSpeedChanged);
            api.RegisterNotificationHandler("Player.OnStop", typeof(OnStopEventData), HandleOnStop);
        }

        private void HandleOnPause(object arg)
        {
            this.OnPause?.Invoke(this, (OnPauseEventData)arg);
        }

        private void HandleOnPlay(object arg)
        {
            this.OnPlay?.Invoke(this, (OnPlayEventData)arg);
        }

        private void HandleOnPropertyChanged(object arg)
        {
            this.OnPropertyChanged?.Invoke(this, (OnPropertyChangedEventParams)arg);
        }

        private void HandleOnSeek(object arg)
        {
            this.OnSeek?.Invoke(this, (OnSeekEventData)arg);
        }

        private void HandleOnSpeedChanged(object arg)
        {
            this.OnSpeedChanged?.Invoke(this, (OnSpeedChangedEventData)arg);
        }

        private void HandleOnStop(object arg)
        {
            this.OnStop?.Invoke(this, (OnStopEventData)arg);
        }




        public event EventHandler<OnPauseEventData> OnPause;
        public event EventHandler<OnPlayEventData> OnPlay;
        public event EventHandler<OnPropertyChangedEventParams> OnPropertyChanged;
        public event EventHandler<OnSeekEventData> OnSeek;
        public event EventHandler<OnSpeedChangedEventData> OnSpeedChanged;
        public event EventHandler<OnStopEventData> OnStop;


        public Task<List<GlobalTypes.Player.Player>> GetActivePlayers()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            return RunAsync<List<GlobalTypes.Player.Player>>("Player.GetActivePlayers", parameters);
        }

        public Task<All> GetItem(int playerId, List<AllProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("playerid", playerId);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<All>("Player.GetItem", parameters, "item");
        }

        public Task<Value> GetProperties(int playerId, List<ValueProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("playerid", playerId);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<Value>("Player.GetProperties", parameters);
        }

        public Task GoTo(int playerId, GoToTarget to)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("playerid", playerId);
            parameters.ConditionalAdd("to", to);

            return RunAsync("Player.GoTo", parameters);
        }

        public Task GoTo(int playerId, int positionInPlaylist)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("playerid", playerId);
            parameters.ConditionalAdd("to", positionInPlaylist);

            return RunAsync("Player.GoTo", parameters);
        }

        public Task Move(int playerId, MoveDirection direction)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("playerid", playerId);
            parameters.ConditionalAdd("direction", direction.ToString());

            return RunAsync("Player.Move", parameters);
        }

        public Task Open(int playlistId, int playlistPosition, OpenOptions options)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("item", new Dictionary<string, object>(2) {["playlistid"] = playlistId, ["position"] = playlistPosition });
            parameters.ConditionalAdd("options", options?.ToSerializable());

            return RunAsync("Player.Open", parameters);
        }

        public Task Open(string filePath, OpenOptions options)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("item", new Dictionary<string, object>(1) {["file"] = filePath });
            parameters.ConditionalAdd("options", options?.ToSerializable());

            return RunAsync("Player.Open", parameters);
        }

        public Task Open(Movie movie, OpenOptions options)
        {
            return OpenMovie(movie.MovieId, options);
        }

        public Task Open(Episode episode, OpenOptions options)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("item", new Dictionary<string, object>(1) {["episodeid"] = episode.EpisodeId });
            parameters.ConditionalAdd("options", options?.ToSerializable());

            return RunAsync("Player.Open", parameters);
        }

        public Task Open(MusicVideo musicVideo, OpenOptions options)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("item", new Dictionary<string, object>(1) {["musicvideoid"] = musicVideo.MusicVideoId });
            parameters.ConditionalAdd("options", options?.ToSerializable());

            return RunAsync("Player.Open", parameters);
        }

        public Task Open(Album album, OpenOptions options)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("item", new Dictionary<string, object>(1) {["albumid"] = album.AlbumId });
            parameters.ConditionalAdd("options", options?.ToSerializable());

            return RunAsync("Player.Open", parameters);
        }

        public Task Open(Artist artist, OpenOptions options)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("item", new Dictionary<string, object>(1) {["artistid"] = artist.ArtistId });
            parameters.ConditionalAdd("options", options?.ToSerializable());

            return RunAsync("Player.Open", parameters);
        }

        public Task Open(Genre genre, OpenOptions options)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("item", new Dictionary<string, object>(1) {["genreid"] = genre.GenreId });
            parameters.ConditionalAdd("options", options?.ToSerializable());

            return RunAsync("Player.Open", parameters);
        }

        public Task Open(Song song, OpenOptions options)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("item", new Dictionary<string, object>(1) {["songid"] = song.SongId });
            parameters.ConditionalAdd("options", options?.ToSerializable());

            return RunAsync("Player.Open", parameters);
        }

        public Task Open(string directory, MediaType media, bool recursive, OpenOptions options)
        {
            Dictionary<string, object> item = new Dictionary<string, object>(3);
            item.Add("directory", directory);
            item.Add("media", media);
            item.Add("recursive", recursive);

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("item", item);
            parameters.ConditionalAdd("options", options?.ToSerializable());

            return RunAsync("Player.Open", parameters);
        }

        public Task Open(string path, bool random, bool recursive, OpenOptions options)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("item", new Dictionary<string, object>(3) {["path"] = path, ["random"] = random, ["recursive"] = recursive });
            parameters.ConditionalAdd("options", options?.ToSerializable());

            return RunAsync("Player.Open", parameters);
        }

        //public void Open(string partymode, OpenOptions options)
        //{
        //    Dictionary<string, object> parameters = new Dictionary<string, object>();
        //    parameters.Add("item", new Dictionary<string, object>(1) {["file"] = filePath });
        //    parameters.ConditionalAdd("options", options?.ToSerializable());

        //    FireAndForget("Player.Open", parameters);
        //}

        public Task Open(Channel channel, OpenOptions options)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("item", new Dictionary<string, object>(1) {["channelid"] = channel.ChannelId });
            parameters.ConditionalAdd("options", options?.ToSerializable());

            return RunAsync("Player.Open", parameters);
        }

        public Task Open()
        {
            return RunAsync("Player.Open", null);
        }

        public Task Open(int playlistId, int playlistPosition)
        {
            return Open(playlistId, playlistPosition, null);
        }

        public Task Open(string filePath)
        {
            return Open(filePath, null);
        }

        public Task Open(Movie movie)
        {
            return OpenMovie(movie.MovieId, null);
        }

        public Task Open(Episode episode)
        {
            return Open(episode, null);
        }

        public Task Open(MusicVideo musicVideo)
        {
            return Open(musicVideo, null);
        }

        public Task Open(Album album)
        {
            return Open(album, null);
        }

        public Task Open(Artist artist)
        {
            return Open(artist, null);
        }

        public Task Open(Genre genre)
        {
            return Open(genre, null);
        }

        public Task Open(Song song)
        {
            return Open(song, null);
        }

        public Task Open(string directory, MediaType media, bool recursive)
        {
            return Open(directory, media, recursive, null);
        }

        public Task Open(string path, bool random, bool recursive)
        {
            return Open(path, random, recursive, null);
        }

        //public void Open(string partymode, OpenOptions options)
        //{
        //    Dictionary<string, object> parameters = new Dictionary<string, object>();
        //    parameters.Add("item", new Dictionary<string, object>(1) {["file"] = filePath });
        //    parameters.ConditionalAdd("options", options?.ToSerializable());

        //    FireAndForget("Player.Open", parameters);
        //}

        public Task Open(Channel channel)
        {
            return Open(channel, null);
        }

        public Task OpenMovie(int movieId, OpenOptions options)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("item", new Dictionary<string, object>(1) {["movieid"] = movieId });
            parameters.ConditionalAdd("options", options?.ToSerializable());

            return RunAsync("Player.Open", parameters);
        }

        public Task OpenMovie(int movieId)
        {
            return OpenMovie(movieId, null);
        }

        public Task<Speed> PlayPause(int playerId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("playerid", playerId);

            return RunAsync<Speed>("Player.PlayPause", parameters);
        }

        public Task<Speed> PlayPause(int playerId, bool play)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("playerid", playerId);
            parameters.Add("play", play);

            return RunAsync<Speed>("Player.PlayPause", parameters);
        }

        public Task<Speed> PlayPause(int playerId, ToggleOption play)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("playerid", playerId);
            parameters.Add("play", play.ToString());

            return RunAsync<Speed>("Player.PlayPause", parameters);
        }

        public Task Rotate(int playerId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("playerid", playerId);

            return RunAsync("Player.Rotate", parameters);
        }

        public Task Rotate(int playerId, PlayRotate value)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("playerid", playerId);
            parameters.ConditionalAdd("value", value.ToString());

            return RunAsync("Player.Rotate", parameters);
        }

        public Task<SeekResponse> Seek(int playerId, double positionPercent)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("playerid", playerId);
            parameters.Add("value", positionPercent);

            return RunAsync<SeekResponse>("Player.Seek", parameters);
        }

        public Task<SeekResponse> Seek(int playerId, Time positionTime)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("playerid", playerId);
            parameters.Add("value", positionTime);

            return RunAsync<SeekResponse>("Player.Seek", parameters);
        }

        public Task<SeekResponse> Seek(int playerId, SeekOption seekType)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("playerid", playerId);
            parameters.Add("value", seekType.ToString());

            return RunAsync<SeekResponse>("Player.Seek", parameters);
        }

        public Task SetAudioStream(int playerId, AudioStreamOption stream)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("playerid", playerId);
            parameters.Add("stream", stream.ToString());

            return RunAsync("Player.SetAudioStream", parameters);
        }

        public Task SetAudioStream(int playerId, int streamIndex)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("playerid", playerId);
            parameters.Add("stream", streamIndex);

            return RunAsync("Player.SetAudioStream", parameters);
        }

        public Task SetPartymode(int playerId, ToggleOption partyMode)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("playerid", playerId);
            parameters.Add("partyMode", partyMode.ToString());

            return RunAsync("Player.SetPartymode", parameters);
        }

        public Task SetPartymode(int playerId, bool partyMode)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("playerid", playerId);
            parameters.Add("partyMode", partyMode);

            return RunAsync("Player.SetPartymode", parameters);
        }

        public Task SetRepeat(int playerId, RepeatOption repeat)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("playerid", playerId);
            parameters.Add("repeat", repeat.ToString());

            return RunAsync("Player.SetRepeat", parameters);
        }

        public Task SetShuffle(int playerId, bool shuffle)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("playerid", playerId);
            parameters.Add("shuffle", shuffle);

            return RunAsync("Player.SetShuffle", parameters);
        }

        public Task SetShuffle(int playerId, ToggleOption shuffle)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("playerid", playerId);
            parameters.Add("shuffle", shuffle.ToString());

            return RunAsync("Player.SetShuffle", parameters);
        }

        public Task<Speed> SetSpeed(int playerId, PlaybackSpeed speed)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("playerid", playerId);
            parameters.Add("speed", (int)speed);

            return RunAsync<Speed>("Player.SetSpeed", parameters);
        }

        public Task SetSubtitle(int playerId, SubtitleOption SubtitleOption)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("playerid", playerId);
            parameters.Add("subtitle", SubtitleOption.ToString());

            return RunAsync("Player.SetSubtitle", parameters);
        }

        public Task SetSubtitle(int playerId, int subtitleIndex)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("playerid", playerId);
            parameters.Add("subtitle", subtitleIndex);

            return RunAsync("Player.SetSubtitle", parameters);
        }

        public Task SetSubtitle(int playerId, SubtitleOption SubtitleOption, bool enable)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("playerid", playerId);
            parameters.Add("subtitle", SubtitleOption.ToString());
            parameters.Add("enable", enable);

            return RunAsync("Player.SetSubtitle", parameters);
        }

        public Task SetSubtitle(int playerId, int subtitleIndex, bool enable)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("playerid", playerId);
            parameters.Add("subtitle", subtitleIndex);
            parameters.Add("enable", enable);

            return RunAsync("Player.SetSubtitle", parameters);
        }

        public Task Stop(int playerId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("playerid", playerId);

            return RunAsync("Player.Stop", parameters);
        }

        public Task Zoom(int playerId, ZoomOption zoom)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("playerId", playerId);
            parameters.Add("zoom", zoom.ToString());

            return RunAsync("Player.Zoom", parameters);
        }

        public Task Zoom(int playerId, int zoomLevel)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("playerId", playerId);
            parameters.Add("zoom", zoomLevel);

            return RunAsync("Player.Zoom", parameters);
        }

    }
}
