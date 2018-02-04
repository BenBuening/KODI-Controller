using Kodi.JsonRpc.GlobalTypes.List;
using Kodi.JsonRpc.GlobalTypes.Video;
using Kodi.JsonRpc.GlobalTypes.Video.Details;
using Kodi.JsonRpc.GlobalTypes.Video.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kodi.JsonRpc.Methods
{
    public class VideoLibrary : MethodLibraryBase
    {

        #region Child Object Definitions

        public class MovieSetParams
        {
            public Limits Limits { get; set; }
            public List<MovieProperty> Properties { get; set; }
            public Sort Sort { get; set; }
        }

        public class Artwork
        {
            public string Banner { get; set; }
            public string FanArt { get; set; }
            public string Poster { get; set; }
            public string Thumb { get; set; }
            internal Dictionary<string, object> ToSerializable()
            {
                Dictionary<string, object> result = new Dictionary<string, object>(4);
                result.ConditionalAdd("banner", this.Banner);
                result.ConditionalAdd("fanart", this.FanArt);
                result.ConditionalAdd("poster", this.Poster);
                result.ConditionalAdd("thumb", this.Thumb);
                return result;
            }
        }

        public class EpisodeDetails
        {
            public int? EpisodeId { get; set; }
            public string Title { get; set; }
            public int? PlayCount { get; set; }
            public int? RunTime { get; set; }
            public List<string> Director { get; set; }
            public string Plot { get; set; }
            public double? Rating { get; set; }
            public string Votes { get; set; }
            public string LastPlayed { get; set; }
            public List<string> Writer { get; set; }
            public string FirstAired { get; set; }
            public string ProductionCode { get; set; }
            public int? Season { get; set; }
            public int? Episode { get; set; }
            public string OriginalTitle { get; set; }
            public string Thumbnail { get; set; }
            public string FanArt { get; set; }
            public Artwork Art { get; set; }
        }

        public class MovieDetails
        {
            public int? MovieId { get; set; }
            public string Title { get; set; }
            public int? PlayCount { get; set; }
            public int? RunTime { get; set; }
            public List<string> Director { get; set; }
            public List<string> Studio { get; set; }
            public int? Year { get; set; }
            public string Plot { get; set; }
            public List<string> Genre { get; set; }
            public double? Rating { get; set; }
            public string MPAA { get; set; }
            public string ImdbNumber { get; set; }
            public string Votes { get; set; }
            public string LastPlayed { get; set; }
            public string OriginalTitle { get; set; }
            public string Trailer { get; set; }
            public string TagLine { get; set; }
            public string PlotOutline { get; set; }
            public List<string> Writer { get; set; }
            public List<string> Country { get; set; }
            public int? Top205 { get; set; }
            public string SortTitle { get; set; }
            public string Set { get; set; }
            public object ShowLink { get; set; }
            public string Thumbnail { get; set; }
            public string FanArt { get; set; }
            public List<string> Tag { get; set; }
            public Artwork Art { get; set; }
        }

        public class MusicVideoDetails
        {
            public int? MusicVideoId { get; set; }
            public string Title { get; set; }
            public int? PlayCount { get; set; }
            public int? RunTime { get; set; }
            public List<string> Director { get; set; }
            public List<string> Studio { get; set; }
            public int? Year { get; set; }
            public string Plot { get; set; }
            public string Album { get; set; }
            public List<string> Artist { get; set; }
            public List<string> Genre { get; set; }
            public int? Track { get; set; }
            public string LastPlayed { get; set; }
            public string Thumbnail { get; set; }
            public string FanArt { get; set; }
            public List<string> Tag { get; set; }
            public Artwork Art { get; set; }
        }

        public class TvShowDetails
        {
            public int TvShowId { get; set; }
            public string Title { get; set; }
            public int? PlayCount { get; set; }
            public List<string> Studio { get; set; }
            public string Plot { get; set; }
            public List<string> Genre { get; set; }
            public double? Rating { get; set; }
            public string MPAA { get; set; }
            public string ImdbNumber { get; set; }
            public string Premiered { get; set; }
            public string Votes { get; set; }
            public string LastPlayed { get; set; }
            public string OriginalTitle { get; set; }
            public string SortTitle { get; set; }
            public string EpisodeGuide { get; set; }
            public string Thumbnail { get; set; }
            public string FanArt { get; set; }
            public List<string> Tag { get; set; }
            public Artwork Art { get; set; }
        }

        public class MovieFilter
        {
            public enum FilterType
            {
                none,
                actor,
                country,
                director,
                genre,
                genreid,
                set,
                setid,
                studio,
                tag,
                year
            }

            public FilterType Type { get; private set; }
            public string Value { get; private set; }
            public MovieFilter(FilterType type, string value) { this.Type = type; this.Value = value; }
            public MovieFilter(FilterType type, int value)
            {
                if (type == FilterType.year && type != FilterType.genreid && type != FilterType.setid)
                    type = FilterType.none;
                this.Type = type;
                this.Value = value.ToString();
            }

            internal Dictionary<string, object> ToSerializable()
            {
                if (this.Type == FilterType.none) return null;

                Dictionary<string, object> result = new Dictionary<string, object>(1);
                result.ConditionalAdd(this.Type.ToString(), this.Value);
                return result;
            }
        }

        public class EpisodeFilter
        {
            public enum FilterType
            {
                none,
                actor,
                director,
                genre,
                genreid,
                year
            }

            public FilterType Type { get; private set; }
            public string Value { get; private set; }
            public EpisodeFilter(FilterType type, string value) { this.Type = type; this.Value = value; }
            public EpisodeFilter(FilterType type, int value)
            {
                if (type == FilterType.year && type != FilterType.genreid)
                    type = FilterType.none;
                this.Type = type;
                this.Value = value.ToString();
            }

            internal Dictionary<string, object> ToSerializable()
            {
                if (this.Type == FilterType.none) return null;

                Dictionary<string, object> result = new Dictionary<string, object>(1);
                result.ConditionalAdd(this.Type.ToString(), this.Value);
                return result;
            }
        }

        public class MusicVideoFilter
        {
            public enum FilterType
            {
                none,
                artist,
                director,
                genre,
                genreid,
                studio,
                tag,
                year
            }

            public FilterType Type { get; private set; }
            public string Value { get; private set; }
            public MusicVideoFilter(FilterType type, string value) { this.Type = type; this.Value = value; }
            public MusicVideoFilter(FilterType type, int value)
            {
                if (type == FilterType.year && type != FilterType.genreid)
                    type = FilterType.none;
                this.Type = type;
                this.Value = value.ToString();
            }

            internal Dictionary<string, object> ToSerializable()
            {
                if (this.Type == FilterType.none) return null;

                Dictionary<string, object> result = new Dictionary<string, object>(1);
                result.ConditionalAdd(this.Type.ToString(), this.Value);
                return result;
            }
        }

        public class TVShowFilter
        {
            public enum FilterType
            {
                none,
                actor,
                genre,
                genreid,
                studio,
                tag,
                year
            }

            public FilterType Type { get; private set; }
            public string Value { get; private set; }
            public TVShowFilter(FilterType type, string value) { this.Type = type; this.Value = value; }
            public TVShowFilter(FilterType type, int value)
            {
                if (type == FilterType.year && type != FilterType.genreid)
                    type = FilterType.none;
                this.Type = type;
                this.Value = value.ToString();
            }

            internal Dictionary<string, object> ToSerializable()
            {
                if (this.Type == FilterType.none) return null;

                Dictionary<string, object> result = new Dictionary<string, object>(1);
                result.ConditionalAdd(this.Type.ToString(), this.Value);
                return result;
            }
        }

        #endregion



        internal VideoLibrary(API api) : base(api)
        {
            api.RegisterNotificationHandler("VideoLibrary.OnCleanFinished", typeof(object), HandleOnCleanFinished);
            api.RegisterNotificationHandler("VideoLibrary.OnCleanStarted", typeof(object), HandleOnCleanStarted);
            api.RegisterNotificationHandler("VideoLibrary.OnRemove", typeof(object), HandleOnRemove);
            api.RegisterNotificationHandler("VideoLibrary.OnScanFinished", typeof(object), HandleOnScanFinished);
            api.RegisterNotificationHandler("VideoLibrary.OnScanStarted", typeof(object), HandleOnScanStarted);
            api.RegisterNotificationHandler("VideoLibrary.OnUpdate", typeof(object), HandleOnUpdate);
        }

        private void HandleOnCleanFinished(object arg)
        {
            this.OnCleanFinished?.Invoke(this, EventArgs.Empty);
        }

        private void HandleOnCleanStarted(object arg)
        {
            this.OnCleanStarted?.Invoke(this, EventArgs.Empty);
        }

        private void HandleOnRemove(object arg)
        {
            this.OnRemove?.Invoke(this, EventArgs.Empty);
        }

        private void HandleOnScanFinished(object arg)
        {
            this.OnScanFinished?.Invoke(this, EventArgs.Empty);
        }

        private void HandleOnScanStarted(object arg)
        {
            this.OnScanStarted?.Invoke(this, EventArgs.Empty);
        }

        private void HandleOnUpdate(object arg)
        {
            this.OnUpdate?.Invoke(this, EventArgs.Empty);
        }




        public IReadOnlyList<MovieProperty> CommonMovieSummaryProperties { get; private set; }
            = new List<MovieProperty>(5)
            {
                        MovieProperty.originaltitle,
                        MovieProperty.runtime,
                        MovieProperty.sorttitle,
                        MovieProperty.title,
                        MovieProperty.thumbnail
            };

        public IReadOnlyList<MovieProperty> CommonMovieProperties { get; private set; }
            = new List<MovieProperty>(25)
            {
                MovieProperty.art,
                MovieProperty.dateadded,
                MovieProperty.fanart,
                MovieProperty.file,
                MovieProperty.genre,
                MovieProperty.imdbnumber,
                MovieProperty.mpaa,
                MovieProperty.lastplayed,
                MovieProperty.originaltitle,
                MovieProperty.playcount,
                MovieProperty.plot,
                MovieProperty.plotoutline,
                MovieProperty.rating,
                MovieProperty.resume,
                MovieProperty.runtime,
                MovieProperty.set,
                MovieProperty.setid,
                MovieProperty.sorttitle,
                MovieProperty.streamdetails,
                MovieProperty.tag,
                MovieProperty.tagline,
                MovieProperty.thumbnail,
                MovieProperty.title,
                MovieProperty.votes,
                MovieProperty.year
            };


        public IReadOnlyList<TvShowProperty> CommonTvShowsSummaryProperties { get; private set; }
            = new List<TvShowProperty>(6)
            {
                TvShowProperty.episode,
                TvShowProperty.originaltitle,
                TvShowProperty.sorttitle,
                TvShowProperty.thumbnail,
                TvShowProperty.title,
                TvShowProperty.year
            };

        public IReadOnlyList<TvShowProperty> CommonTvShowProperties { get; private set; }
            = new List<TvShowProperty>(22)
            {
                TvShowProperty.title,
                TvShowProperty.genre,
                TvShowProperty.year,
                TvShowProperty.rating,
                TvShowProperty.plot,
                TvShowProperty.studio,
                TvShowProperty.mpaa,
                TvShowProperty.playcount,
                TvShowProperty.episode,
                TvShowProperty.imdbnumber,
                TvShowProperty.premiered,
                TvShowProperty.votes,
                TvShowProperty.lastplayed,
                TvShowProperty.thumbnail,
                TvShowProperty.originaltitle,
                TvShowProperty.sorttitle,
                TvShowProperty.episodeguide,
                TvShowProperty.season,
                TvShowProperty.watchedepisodes,
                TvShowProperty.dateadded,
                TvShowProperty.tag,
                TvShowProperty.art
            };


        public IReadOnlyList<EpisodeProperty> CommonEpisodeSummaryProperties { get; private set; }
            = new List<EpisodeProperty>(7)
            {
                EpisodeProperty.episode,
                EpisodeProperty.firstaired,
                EpisodeProperty.runtime,
                EpisodeProperty.season,
                EpisodeProperty.thumbnail,
                EpisodeProperty.title,
                EpisodeProperty.tvshowid,
            };

        public IReadOnlyList<EpisodeProperty> CommonEpisodeProperties { get; private set; }
            = new List<EpisodeProperty>(24)
            {
                EpisodeProperty.art,
                EpisodeProperty.dateadded,
                EpisodeProperty.director,
                EpisodeProperty.episode,
                EpisodeProperty.fanart,
                EpisodeProperty.file,
                EpisodeProperty.firstaired,
                EpisodeProperty.lastplayed,
                EpisodeProperty.originaltitle,
                EpisodeProperty.playcount,
                EpisodeProperty.plot,
                EpisodeProperty.productioncode,
                EpisodeProperty.rating,
                EpisodeProperty.resume,
                EpisodeProperty.runtime,
                EpisodeProperty.season,
                EpisodeProperty.showtitle,
                EpisodeProperty.streamdetails,
                EpisodeProperty.thumbnail,
                EpisodeProperty.title,
                EpisodeProperty.tvshowid,
                EpisodeProperty.uniqueid,
                EpisodeProperty.votes,
                EpisodeProperty.writer,
            };


        public IReadOnlyList<MusicVideoProperty> CommonMusicVideoSummaryProperties { get; private set; }
            = new List<MusicVideoProperty>()
            {
                MusicVideoProperty.album,
                MusicVideoProperty.art,
                MusicVideoProperty.artist,
                MusicVideoProperty.genre,
                MusicVideoProperty.runtime,
                MusicVideoProperty.title,
                MusicVideoProperty.track
            };

        public IReadOnlyList<MusicVideoProperty> CommonMusicVideoProperties { get; private set; }
            = new List<MusicVideoProperty>()
            {
                MusicVideoProperty.album,
                MusicVideoProperty.art,
                MusicVideoProperty.artist,
                MusicVideoProperty.dateadded,
                MusicVideoProperty.director,
                MusicVideoProperty.genre,
                MusicVideoProperty.lastplayed,
                MusicVideoProperty.playcount,
                MusicVideoProperty.resume,
                MusicVideoProperty.runtime,
                MusicVideoProperty.streamdetails,
                MusicVideoProperty.thumbnail,
                MusicVideoProperty.title,
                MusicVideoProperty.track,
                MusicVideoProperty.year
            };


        public IReadOnlyList<SeasonProperty> CommonSeasonProperties { get; private set; }
            = new List<SeasonProperty>()
            {
                SeasonProperty.art,
                SeasonProperty.episode,
                SeasonProperty.fanart,
                SeasonProperty.playcount,
                SeasonProperty.season,
                SeasonProperty.showtitle,
                SeasonProperty.thumbnail,
                SeasonProperty.tvshowid,
                SeasonProperty.watchedepisodes
            };




        public event EventHandler OnCleanFinished;
        public event EventHandler OnCleanStarted;
        public event EventHandler OnRemove;
        public event EventHandler OnScanFinished;
        public event EventHandler OnScanStarted;
        public event EventHandler OnUpdate;




        public Task<Episode> GetEpisodeDetails(int episodeId)
        {
            return GetEpisodeDetails(episodeId, this.CommonEpisodeProperties);
        }

        public Task<Episode> GetEpisodeDetails(int episodeId, IEnumerable<EpisodeProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("episodeid", episodeId);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<Episode>("VideoLibrary.GetEpisodeDetails", parameters, "episodedetails");
        }



        public Task<GetEpisodesResponse> GetEpisodes()
        {
            return GetEpisodes(this.CommonEpisodeSummaryProperties);
        }

        public Task<GetEpisodesResponse> GetEpisodes(IEnumerable<EpisodeProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<GetEpisodesResponse>("VideoLibrary.GetEpisodes", parameters);
        }

        public Task<GetEpisodesResponse> GetEpisodes(int tvShowId)
        {
            return GetEpisodes(tvShowId, this.CommonEpisodeSummaryProperties);
        }

        public Task<GetEpisodesResponse> GetEpisodes(int tvShowId, IEnumerable<EpisodeProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("tvshowid", tvShowId);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<GetEpisodesResponse>("VideoLibrary.GetEpisodes", parameters);
        }

        public Task<GetEpisodesResponse> GetEpisodes(int tvShowId, int season)
        {
            return GetEpisodes(tvShowId, season, this.CommonEpisodeSummaryProperties);
        }

        public Task<GetEpisodesResponse> GetEpisodes(int tvShowId, int season, IEnumerable<EpisodeProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("tvshowid", tvShowId);
            parameters.Add("season", season);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<GetEpisodesResponse>("VideoLibrary.GetEpisodes", parameters);
        }

        public Task<GetEpisodesResponse> GetEpisodes(Limits limits)
        {
            return GetEpisodes(limits, this.CommonEpisodeSummaryProperties);
        }

        public Task<GetEpisodesResponse> GetEpisodes(Limits limits, IEnumerable<EpisodeProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("limits", limits);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<GetEpisodesResponse>("VideoLibrary.GetEpisodes", parameters);
        }

        public Task<GetEpisodesResponse> GetEpisodes(int tvShowId, Limits limits)
        {
            return GetEpisodes(tvShowId, limits, this.CommonEpisodeSummaryProperties);
        }

        public Task<GetEpisodesResponse> GetEpisodes(int tvShowId, Limits limits, IEnumerable<EpisodeProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("tvshowid", tvShowId);
            parameters.ConditionalAdd("limits", limits);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<GetEpisodesResponse>("VideoLibrary.GetEpisodes", parameters);
        }

        public Task<GetEpisodesResponse> GetEpisodes(int tvShowId, int season, Limits limits)
        {
            return GetEpisodes(tvShowId, season, limits, this.CommonEpisodeSummaryProperties);
        }

        public Task<GetEpisodesResponse> GetEpisodes(int tvShowId, int season, Limits limits, IEnumerable<EpisodeProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("tvshowid", tvShowId);
            parameters.Add("season", season);
            parameters.ConditionalAdd("limits", limits);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<GetEpisodesResponse>("VideoLibrary.GetEpisodes", parameters);
        }

        public Task<GetEpisodesResponse> GetEpisodes(Sort sort)
        {
            return GetEpisodes(sort, this.CommonEpisodeSummaryProperties);
        }

        public Task<GetEpisodesResponse> GetEpisodes(Sort sort, IEnumerable<EpisodeProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("sort", sort);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<GetEpisodesResponse>("VideoLibrary.GetEpisodes", parameters);
        }

        public Task<GetEpisodesResponse> GetEpisodes(int tvShowId, Sort sort)
        {
            return GetEpisodes(tvShowId, sort, this.CommonEpisodeSummaryProperties);
        }

        public Task<GetEpisodesResponse> GetEpisodes(int tvShowId, Sort sort, IEnumerable<EpisodeProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("tvshowid", tvShowId);
            parameters.ConditionalAdd("sort", sort);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<GetEpisodesResponse>("VideoLibrary.GetEpisodes", parameters);
        }

        public Task<GetEpisodesResponse> GetEpisodes(int tvShowId, int season, Sort sort)
        {
            return GetEpisodes(tvShowId, season, sort, this.CommonEpisodeSummaryProperties);
        }

        public Task<GetEpisodesResponse> GetEpisodes(int tvShowId, int season, Sort sort, IEnumerable<EpisodeProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("tvshowid", tvShowId);
            parameters.Add("season", season);
            parameters.ConditionalAdd("sort", sort);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<GetEpisodesResponse>("VideoLibrary.GetEpisodes", parameters);
        }

        public Task<GetEpisodesResponse> GetEpisodes(Limits limits, Sort sort)
        {
            return GetEpisodes(limits, sort, this.CommonEpisodeSummaryProperties);
        }

        public Task<GetEpisodesResponse> GetEpisodes(Limits limits, Sort sort, IEnumerable<EpisodeProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("limits", limits);
            parameters.ConditionalAdd("sort", sort);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<GetEpisodesResponse>("VideoLibrary.GetEpisodes", parameters);
        }

        public Task<GetEpisodesResponse> GetEpisodes(int tvShowId, Limits limits, Sort sort)
        {
            return GetEpisodes(tvShowId, limits, sort, this.CommonEpisodeSummaryProperties);
        }

        public Task<GetEpisodesResponse> GetEpisodes(int tvShowId, Limits limits, Sort sort, IEnumerable<EpisodeProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("tvshowid", tvShowId);
            parameters.ConditionalAdd("limits", limits);
            parameters.ConditionalAdd("sort", sort);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<GetEpisodesResponse>("VideoLibrary.GetEpisodes", parameters);
        }

        public Task<GetEpisodesResponse> GetEpisodes(int tvShowId, int season, Limits limits, Sort sort)
        {
            return GetEpisodes(tvShowId, season, limits, sort, this.CommonEpisodeSummaryProperties);
        }

        public Task<GetEpisodesResponse> GetEpisodes(int tvShowId, int season, Limits limits, Sort sort, IEnumerable<EpisodeProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("tvshowid", tvShowId);
            parameters.Add("season", season);
            parameters.ConditionalAdd("limits", limits);
            parameters.ConditionalAdd("sort", sort);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<GetEpisodesResponse>("VideoLibrary.GetEpisodes", parameters);
        }

        public Task<GetEpisodesResponse> GetEpisodes(EpisodeFilter filter)
        {
            return GetEpisodes(filter, this.CommonEpisodeSummaryProperties);
        }

        public Task<GetEpisodesResponse> GetEpisodes(EpisodeFilter filter, IEnumerable<EpisodeProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("filter", filter?.ToSerializable());
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<GetEpisodesResponse>("VideoLibrary.GetEpisodes", parameters);
        }

        public Task<GetEpisodesResponse> GetEpisodes(int tvShowId, EpisodeFilter filter)
        {
            return GetEpisodes(tvShowId, filter, this.CommonEpisodeSummaryProperties);
        }

        public Task<GetEpisodesResponse> GetEpisodes(int tvShowId, EpisodeFilter filter, IEnumerable<EpisodeProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("tvshowid", tvShowId);
            parameters.ConditionalAdd("filter", filter?.ToSerializable());
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<GetEpisodesResponse>("VideoLibrary.GetEpisodes", parameters);
        }

        public Task<GetEpisodesResponse> GetEpisodes(int tvShowId, int season, EpisodeFilter filter)
        {
            return GetEpisodes(tvShowId, season, filter, this.CommonEpisodeSummaryProperties);
        }

        public Task<GetEpisodesResponse> GetEpisodes(int tvShowId, int season, EpisodeFilter filter, IEnumerable<EpisodeProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("tvshowid", tvShowId);
            parameters.Add("season", season);
            parameters.ConditionalAdd("filter", filter?.ToSerializable());
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<GetEpisodesResponse>("VideoLibrary.GetEpisodes", parameters);
        }

        public Task<GetEpisodesResponse> GetEpisodes(Limits limits, EpisodeFilter filter)
        {
            return GetEpisodes(limits, filter, this.CommonEpisodeSummaryProperties);
        }

        public Task<GetEpisodesResponse> GetEpisodes(Limits limits, EpisodeFilter filter, IEnumerable<EpisodeProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("limits", limits);
            parameters.ConditionalAdd("filter", filter?.ToSerializable());
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<GetEpisodesResponse>("VideoLibrary.GetEpisodes", parameters);
        }

        public Task<GetEpisodesResponse> GetEpisodes(int tvShowId, Limits limits, EpisodeFilter filter)
        {
            return GetEpisodes(tvShowId, limits, filter, this.CommonEpisodeSummaryProperties);
        }

        public Task<GetEpisodesResponse> GetEpisodes(int tvShowId, Limits limits, EpisodeFilter filter, IEnumerable<EpisodeProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("tvshowid", tvShowId);
            parameters.ConditionalAdd("limits", limits);
            parameters.ConditionalAdd("filter", filter?.ToSerializable());
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<GetEpisodesResponse>("VideoLibrary.GetEpisodes", parameters);
        }

        public Task<GetEpisodesResponse> GetEpisodes(int tvShowId, int season, Limits limits, EpisodeFilter filter)
        {
            return GetEpisodes(tvShowId, season, limits, filter, this.CommonEpisodeSummaryProperties);
        }

        public Task<GetEpisodesResponse> GetEpisodes(int tvShowId, int season, Limits limits, EpisodeFilter filter, IEnumerable<EpisodeProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("tvshowid", tvShowId);
            parameters.Add("season", season);
            parameters.ConditionalAdd("limits", limits);
            parameters.ConditionalAdd("filter", filter?.ToSerializable());
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<GetEpisodesResponse>("VideoLibrary.GetEpisodes", parameters);
        }

        public Task<GetEpisodesResponse> GetEpisodes(Sort sort, EpisodeFilter filter)
        {
            return GetEpisodes(sort, filter, this.CommonEpisodeSummaryProperties);
        }

        public Task<GetEpisodesResponse> GetEpisodes(Sort sort, EpisodeFilter filter, IEnumerable<EpisodeProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("sort", sort);
            parameters.ConditionalAdd("filter", filter?.ToSerializable());
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<GetEpisodesResponse>("VideoLibrary.GetEpisodes", parameters);
        }

        public Task<GetEpisodesResponse> GetEpisodes(int tvShowId, Sort sort, EpisodeFilter filter)
        {
            return GetEpisodes(tvShowId, sort, filter, this.CommonEpisodeSummaryProperties);
        }

        public Task<GetEpisodesResponse> GetEpisodes(int tvShowId, Sort sort, EpisodeFilter filter, IEnumerable<EpisodeProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("tvshowid", tvShowId);
            parameters.ConditionalAdd("sort", sort);
            parameters.ConditionalAdd("filter", filter?.ToSerializable());
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<GetEpisodesResponse>("VideoLibrary.GetEpisodes", parameters);
        }

        public Task<GetEpisodesResponse> GetEpisodes(int tvShowId, int season, Sort sort, EpisodeFilter filter)
        {
            return GetEpisodes(tvShowId, season, sort, filter, this.CommonEpisodeSummaryProperties);
        }

        public Task<GetEpisodesResponse> GetEpisodes(int tvShowId, int season, Sort sort, EpisodeFilter filter, IEnumerable<EpisodeProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("tvshowid", tvShowId);
            parameters.Add("season", season);
            parameters.ConditionalAdd("sort", sort);
            parameters.ConditionalAdd("filter", filter?.ToSerializable());
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<GetEpisodesResponse>("VideoLibrary.GetEpisodes", parameters);
        }

        public Task<GetEpisodesResponse> GetEpisodes(Limits limits, Sort sort, EpisodeFilter filter)
        {
            return GetEpisodes(limits, sort, filter, this.CommonEpisodeSummaryProperties);
        }

        public Task<GetEpisodesResponse> GetEpisodes(Limits limits, Sort sort, EpisodeFilter filter, IEnumerable<EpisodeProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("limits", limits);
            parameters.ConditionalAdd("sort", sort);
            parameters.ConditionalAdd("filter", filter?.ToSerializable());
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<GetEpisodesResponse>("VideoLibrary.GetEpisodes", parameters);
        }

        public Task<GetEpisodesResponse> GetEpisodes(int tvShowId, Limits limits, Sort sort, EpisodeFilter filter)
        {
            return GetEpisodes(tvShowId, limits, sort, filter, this.CommonEpisodeSummaryProperties);
        }

        public Task<GetEpisodesResponse> GetEpisodes(int tvShowId, Limits limits, Sort sort, EpisodeFilter filter, IEnumerable<EpisodeProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("tvshowid", tvShowId);
            parameters.ConditionalAdd("limits", limits);
            parameters.ConditionalAdd("sort", sort);
            parameters.ConditionalAdd("filter", filter?.ToSerializable());
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<GetEpisodesResponse>("VideoLibrary.GetEpisodes", parameters);
        }

        public Task<GetEpisodesResponse> GetEpisodes(int tvShowId, int season, Limits limits, Sort sort, EpisodeFilter filter)
        {
            return GetEpisodes(tvShowId, season, limits, sort, filter, this.CommonEpisodeSummaryProperties);
        }

        public Task<GetEpisodesResponse> GetEpisodes(int tvShowId, int season, Limits limits, Sort sort, EpisodeFilter filter, IEnumerable<EpisodeProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("tvshowid", tvShowId);
            parameters.Add("season", season);
            parameters.ConditionalAdd("limits", limits);
            parameters.ConditionalAdd("sort", sort);
            parameters.ConditionalAdd("filter", filter?.ToSerializable());
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<GetEpisodesResponse>("VideoLibrary.GetEpisodes", parameters);
        }



        public Task<GetGenresResponse> GetGenres(GenreType type, Limits limits, Sort sort, IEnumerable<GenreProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("type", type.ToString());
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString().ToLower()).ToList());
            parameters.ConditionalAdd("limits", limits);
            parameters.ConditionalAdd("sort", sort);

            return RunAsync<GetGenresResponse>("VideoLibrary.GetGenres", parameters);
        }

        public Task<GetGenresResponse> GetGenres(GenreType type, IEnumerable<GenreProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("type", type.ToString());
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString().ToLower()).ToList());

            return RunAsync<GetGenresResponse>("VideoLibrary.GetGenres", parameters);
        }

        public Task<GetGenresResponse> GetGenres(GenreType type, Limits limits, IEnumerable<GenreProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("type", type.ToString());
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString().ToLower()).ToList());
            parameters.ConditionalAdd("limits", limits);

            return RunAsync<GetGenresResponse>("VideoLibrary.GetGenres", parameters);
        }

        

        public Task<MovieSetExtended> GetMovieSetDetails(int setId, IEnumerable<MovieSetProperty> movieSetProperties, MovieSetParams movies)
        {
            Dictionary<string, object> movieParams = new Dictionary<string, object>();
            movieParams.ConditionalAdd("limits", movies.Limits);
            movieParams.ConditionalAdd("properties", movies.Properties?.Select(x => x.ToString()).ToList());
            movieParams.ConditionalAdd("sort", movies.Sort);

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("setid", setId);
            parameters.ConditionalAdd("properties", movieSetProperties?.Select(x => x.ToString()).ToList());
            parameters.ConditionalAdd("movies", movieParams);

            return RunAsync<MovieSetExtended>("VideoLibrary.GetMovieSetDetails", parameters);
        }

        public Task<MovieSetExtended> GetMovieSetDetails(int setId, IEnumerable<MovieSetProperty> movieSetProperties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("setid", setId);
            parameters.ConditionalAdd("properties", movieSetProperties?.Select(x => x.ToString()).ToList());

            return RunAsync<MovieSetExtended>("VideoLibrary.GetMovieSetDetails", parameters);
        }



        public Task<GetMovieSetsResponse> GetMovieSets(Limits limits, Sort sort, IEnumerable<MovieSetProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());
            parameters.ConditionalAdd("limits", limits);
            parameters.ConditionalAdd("sort", sort);

            return RunAsync<GetMovieSetsResponse>("VideoLibrary.GetMovieSets", parameters);
        }

        public Task<GetMovieSetsResponse> GetMovieSets(IEnumerable<MovieSetProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<GetMovieSetsResponse>("VideoLibrary.GetMovieSets", parameters);
        }

        public Task<GetMovieSetsResponse> GetMovieSets(Limits limits, IEnumerable<MovieSetProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());
            parameters.ConditionalAdd("limits", limits);

            return RunAsync<GetMovieSetsResponse>("VideoLibrary.GetMovieSets", parameters);
        }



        public Task<Movie> GetMovieDetails(int movieId)
        {
            return GetMovieDetails(movieId, this.CommonMovieProperties);
        }

        public Task<Movie> GetMovieDetails(int movieId, IEnumerable<MovieProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("movieid", movieId);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<Movie>("VideoLibrary.GetMovieDetails", parameters, "moviedetails");
        }
        


        public Task<GetMoviesResponse> GetMovies()
        {
            return GetMovies(this.CommonMovieSummaryProperties);
        }

        public Task<GetMoviesResponse> GetMovies(MovieFilter filter)
        {
            return GetMovies(this.CommonMovieSummaryProperties, filter);
        }

        public Task<GetMoviesResponse> GetMovies(Sort sort)
        {
            return GetMovies(sort, this.CommonMovieSummaryProperties);
        }

        public Task<GetMoviesResponse> GetMovies(Sort sort, MovieFilter filter)
        {
            return GetMovies(sort, this.CommonMovieSummaryProperties, filter);
        }

        public Task<GetMoviesResponse> GetMovies(Limits limits)
        {
            return GetMovies(limits, this.CommonMovieSummaryProperties);
        }

        public Task<GetMoviesResponse> GetMovies(Limits limits, MovieFilter filter)
        {
            return GetMovies(limits, this.CommonMovieSummaryProperties, filter);
        }

        public Task<GetMoviesResponse> GetMovies(IEnumerable<MovieProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString().ToLower()).ToList());

            return RunAsync<GetMoviesResponse>("VideoLibrary.GetMovies", parameters);
        }

        public Task<GetMoviesResponse> GetMovies(IEnumerable<MovieProperty> properties, MovieFilter filter)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString().ToLower()).ToList());
            parameters.ConditionalAdd("filter", filter?.ToSerializable());

            return RunAsync<GetMoviesResponse>("VideoLibrary.GetMovies", parameters);
        }

        public Task<GetMoviesResponse> GetMovies(Sort sort, IEnumerable<MovieProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString().ToLower()).ToList());
            parameters.ConditionalAdd("sort", sort);

            return RunAsync<GetMoviesResponse>("VideoLibrary.GetMovies", parameters);
        }

        public Task<GetMoviesResponse> GetMovies(Sort sort, IEnumerable<MovieProperty> properties, MovieFilter filter)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString().ToLower()).ToList());
            parameters.ConditionalAdd("sort", sort);
            parameters.ConditionalAdd("filter", filter?.ToSerializable());

            return RunAsync<GetMoviesResponse>("VideoLibrary.GetMovies", parameters);
        }

        public Task<GetMoviesResponse> GetMovies(Limits limits, IEnumerable<MovieProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("limits", limits);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString().ToLower()).ToList());

            return RunAsync<GetMoviesResponse>("VideoLibrary.GetMovies", parameters);
        }

        public Task<GetMoviesResponse> GetMovies(Limits limits, IEnumerable<MovieProperty> properties, MovieFilter filter)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("limits", limits);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString().ToLower()).ToList());
            parameters.ConditionalAdd("filter", filter?.ToSerializable());

            return RunAsync<GetMoviesResponse>("VideoLibrary.GetMovies", parameters);
        }

        public Task<GetMoviesResponse> GetMovies(Limits limits, Sort sort, IEnumerable<MovieProperty> properties, MovieFilter filter)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("limits", limits);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString().ToLower()).ToList());
            parameters.ConditionalAdd("sort", sort);
            parameters.ConditionalAdd("filter", filter?.ToSerializable());

            return RunAsync<GetMoviesResponse>("VideoLibrary.GetMovies", parameters);
        }



        public Task<MusicVideo> GetMusicVideoDetails(int musicVideoId)
        {
            return GetMusicVideoDetails(musicVideoId, this.CommonMusicVideoProperties);
        }

        public Task<MusicVideo> GetMusicVideoDetails(int musicVideoId, IEnumerable<MusicVideoProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("musicvideoid", musicVideoId);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<MusicVideo>("VideoLibrary.GetMusicVideoDetails", parameters);
        }

        public Task<GetMusicVideosResponse> GetMusicVideos(Limits limits, Sort sort, MusicVideoFilter filter)
        {
            return GetMusicVideos(limits, sort, filter, this.CommonMusicVideoSummaryProperties);
        }

        public Task<GetMusicVideosResponse> GetMusicVideos(Limits limits, Sort sort, MusicVideoFilter filter, IEnumerable<MusicVideoProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("limits", limits);
            parameters.ConditionalAdd("sort", sort);
            parameters.ConditionalAdd("filter", filter);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<GetMusicVideosResponse>("VideoLibrary.GetMusicVideos", parameters);
        }

        public Task<GetMusicVideosResponse> GetMusicVideos()
        {
            return GetMusicVideos(this.CommonMusicVideoSummaryProperties);
        }

        public Task<GetMusicVideosResponse> GetMusicVideos(IEnumerable<MusicVideoProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<GetMusicVideosResponse>("VideoLibrary.GetMusicVideos", parameters);
        }

        public Task<GetMusicVideosResponse> GetMusicVideos(Limits limits)
        {
            return GetMusicVideos(limits, this.CommonMusicVideoSummaryProperties);
        }

        public Task<GetMusicVideosResponse> GetMusicVideos(Limits limits, IEnumerable<MusicVideoProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("limits", limits);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<GetMusicVideosResponse>("VideoLibrary.GetMusicVideos", parameters);
        }

        public Task<GetMusicVideosResponse> GetMusicVideos(Sort sort)
        {
            return GetMusicVideos(sort, this.CommonMusicVideoSummaryProperties);
        }

        public Task<GetMusicVideosResponse> GetMusicVideos(Sort sort, IEnumerable<MusicVideoProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("sort", sort);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<GetMusicVideosResponse>("VideoLibrary.GetMusicVideos", parameters);
        }

        public Task<GetMusicVideosResponse> GetMusicVideos(Limits limits, Sort sort)
        {
            return GetMusicVideos(limits, sort, this.CommonMusicVideoSummaryProperties);
        }

        public Task<GetMusicVideosResponse> GetMusicVideos(Limits limits, Sort sort, IEnumerable<MusicVideoProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("limits", limits);
            parameters.ConditionalAdd("sort", sort);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<GetMusicVideosResponse>("VideoLibrary.GetMusicVideos", parameters);
        }

        public Task<GetMusicVideosResponse> GetMusicVideos(MusicVideoFilter filter)
        {
            return GetMusicVideos(filter, this.CommonMusicVideoSummaryProperties);
        }

        public Task<GetMusicVideosResponse> GetMusicVideos(MusicVideoFilter filter, IEnumerable<MusicVideoProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("filter", filter);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<GetMusicVideosResponse>("VideoLibrary.GetMusicVideos", parameters);
        }

        public Task<GetMusicVideosResponse> GetMusicVideos(Limits limits, MusicVideoFilter filter)
        {
            return GetMusicVideos(limits, filter, this.CommonMusicVideoSummaryProperties);
        }

        public Task<GetMusicVideosResponse> GetMusicVideos(Limits limits, MusicVideoFilter filter, IEnumerable<MusicVideoProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("limits", limits);
            parameters.ConditionalAdd("filter", filter);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<GetMusicVideosResponse>("VideoLibrary.GetMusicVideos", parameters);
        }



        public Task<GetEpisodesResponse> GetRecentlyAddedEpisodes(Limits limits, Sort sort)
        {
            return GetRecentlyAddedEpisodes(limits, sort, this.CommonEpisodeSummaryProperties);
        }

        public Task<GetEpisodesResponse> GetRecentlyAddedEpisodes(Limits limits, Sort sort, IEnumerable<EpisodeProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("limits", limits);
            parameters.ConditionalAdd("sort", sort);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<GetEpisodesResponse>("VideoLibrary.GetRecentlyAddedEpisodes", parameters);
        }

        public Task<GetEpisodesResponse> GetRecentlyAddedEpisodes()
        {
            return GetRecentlyAddedEpisodes(this.CommonEpisodeSummaryProperties);
        }

        public Task<GetEpisodesResponse> GetRecentlyAddedEpisodes(IEnumerable<EpisodeProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<GetEpisodesResponse>("VideoLibrary.GetRecentlyAddedEpisodes", parameters);
        }

        public Task<GetEpisodesResponse> GetRecentlyAddedEpisodes(Limits limits)
        {
            return GetRecentlyAddedEpisodes(limits, this.CommonEpisodeSummaryProperties);
        }

        public Task<GetEpisodesResponse> GetRecentlyAddedEpisodes(Limits limits, IEnumerable<EpisodeProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("limits", limits);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<GetEpisodesResponse>("VideoLibrary.GetRecentlyAddedEpisodes", parameters);
        }

        public Task<GetMoviesResponse> GetRecentlyAddedMovies(Limits limits, Sort sort)
        {
            return GetRecentlyAddedMovies(limits, sort, this.CommonMovieSummaryProperties);
        }

        public Task<GetMoviesResponse> GetRecentlyAddedMovies(Limits limits, Sort sort, IEnumerable<MovieProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("limits", limits);
            parameters.ConditionalAdd("sort", sort);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<GetMoviesResponse>("VideoLibrary.GetRecentlyAddedMovies", parameters);
        }

        public Task<GetMoviesResponse> GetRecentlyAddedMovies()
        {
            return GetRecentlyAddedMovies(this.CommonMovieSummaryProperties);
        }

        public Task<GetMoviesResponse> GetRecentlyAddedMovies(IEnumerable<MovieProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<GetMoviesResponse>("VideoLibrary.GetRecentlyAddedMovies", parameters);
        }

        public Task<GetMoviesResponse> GetRecentlyAddedMovies(Limits limits)
        {
            return GetRecentlyAddedMovies(limits, this.CommonMovieSummaryProperties);
        }

        public Task<GetMoviesResponse> GetRecentlyAddedMovies(Limits limits, IEnumerable<MovieProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("limits", limits);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<GetMoviesResponse>("VideoLibrary.GetRecentlyAddedMovies", parameters);
        }

        public Task<GetMusicVideosResponse> GetRecentlyAddedMusicVideos(Limits limits, Sort sort)
        {
            return GetRecentlyAddedMusicVideos(limits, sort, this.CommonMusicVideoSummaryProperties);
        }

        public Task<GetMusicVideosResponse> GetRecentlyAddedMusicVideos(Limits limits, Sort sort, IEnumerable<MusicVideoProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("limits", limits);
            parameters.ConditionalAdd("sort", sort);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<GetMusicVideosResponse>("VideoLibrary.GetRecentlyAddedMusicVideos", parameters);
        }

        public Task<GetMusicVideosResponse> GetRecentlyAddedMusicVideos()
        {
            return GetRecentlyAddedMusicVideos(this.CommonMusicVideoSummaryProperties);
        }

        public Task<GetMusicVideosResponse> GetRecentlyAddedMusicVideos(IEnumerable<MusicVideoProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<GetMusicVideosResponse>("VideoLibrary.GetRecentlyAddedMusicVideos", parameters);
        }

        public Task<GetMusicVideosResponse> GetRecentlyAddedMusicVideos(Limits limits)
        {
            return GetRecentlyAddedMusicVideos(limits, this.CommonMusicVideoSummaryProperties);
        }

        public Task<GetMusicVideosResponse> GetRecentlyAddedMusicVideos(Limits limits, IEnumerable<MusicVideoProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("limits", limits);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<GetMusicVideosResponse>("VideoLibrary.GetRecentlyAddedMusicVideos", parameters);
        }



        public Task<GetSeasonsResponse> GetSeasons(int tvShowId, Limits limits, Sort sort)
        {
            return GetSeasons(tvShowId, limits, sort, this.CommonSeasonProperties);
        }

        public Task<GetSeasonsResponse> GetSeasons(int tvShowId, Limits limits, Sort sort, IEnumerable<SeasonProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("tvshowid", tvShowId);
            parameters.ConditionalAdd("limits", limits);
            parameters.ConditionalAdd("sort", sort);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<GetSeasonsResponse>("VideoLibrary.GetSeasons", parameters);
        }

        public Task<GetSeasonsResponse> GetSeasons(int tvShowId)
        {
            return GetSeasons(tvShowId, this.CommonSeasonProperties);
        }

        public Task<GetSeasonsResponse> GetSeasons(int tvShowId, IEnumerable<SeasonProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("tvshowid", tvShowId);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<GetSeasonsResponse>("VideoLibrary.GetSeasons", parameters);
        }

        public Task<GetSeasonsResponse> GetSeasons(int tvShowId, Limits limits)
        {
            return GetSeasons(tvShowId, limits, this.CommonSeasonProperties);
        }

        public Task<GetSeasonsResponse> GetSeasons(int tvShowId, Limits limits, IEnumerable<SeasonProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("tvshowid", tvShowId);
            parameters.ConditionalAdd("limits", limits);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<GetSeasonsResponse>("VideoLibrary.GetSeasons", parameters);
        }

        public Task<GetSeasonsResponse> GetSeasons(int tvShowId, Sort sort)
        {
            return GetSeasons(tvShowId, sort, this.CommonSeasonProperties);
        }

        public Task<GetSeasonsResponse> GetSeasons(int tvShowId, Sort sort, IEnumerable<SeasonProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("tvshowid", tvShowId);
            parameters.ConditionalAdd("sort", sort);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<GetSeasonsResponse>("VideoLibrary.GetSeasons", parameters);
        }



        public Task<TvShow> GetTVShowDetails(int tvShowId)
        {
            return GetTVShowDetails(tvShowId, this.CommonTvShowProperties);
        }

        public Task<TvShow> GetTVShowDetails(int tvShowId, IEnumerable<TvShowProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("tvshowid", tvShowId);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<TvShow>("VideoLibrary.GetTVShowDetails", parameters);
        }


        public Task<GetTVShowsResponse> GetTVShows()
        {
            return GetTVShows(this.CommonTvShowsSummaryProperties);
        }

        public Task<GetTVShowsResponse> GetTVShows(Limits limits)
        {
            return GetTVShows(limits, this.CommonTvShowsSummaryProperties);
        }

        public Task<GetTVShowsResponse> GetTVShows(Sort sort)
        {
            return GetTVShows(sort, this.CommonTvShowsSummaryProperties);
        }

        public Task<GetTVShowsResponse> GetTVShows(Limits limits, Sort sort)
        {
            return GetTVShows(limits, sort, this.CommonTvShowsSummaryProperties);
        }

        public Task<GetTVShowsResponse> GetTVShows(TVShowFilter filter)
        {
            return GetTVShows(filter, this.CommonTvShowsSummaryProperties);
        }

        public Task<GetTVShowsResponse> GetTVShows(Limits limits, TVShowFilter filter)
        {
            return GetTVShows(limits, filter, this.CommonTvShowsSummaryProperties);
        }

        public Task<GetTVShowsResponse> GetTVShows(Limits limits, Sort sort, TVShowFilter filter)
        {
            return GetTVShows(limits, sort, filter, this.CommonTvShowsSummaryProperties);
        }

        public Task<GetTVShowsResponse> GetTVShows(IEnumerable<TvShowProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<GetTVShowsResponse>("VideoLibrary.GetTVShows", parameters);
        }

        public Task<GetTVShowsResponse> GetTVShows(Limits limits, IEnumerable<TvShowProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("limits", limits);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<GetTVShowsResponse>("VideoLibrary.GetTVShows", parameters);
        }

        public Task<GetTVShowsResponse> GetTVShows(Sort sort, IEnumerable<TvShowProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("sort", sort);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<GetTVShowsResponse>("VideoLibrary.GetTVShows", parameters);
        }

        public Task<GetTVShowsResponse> GetTVShows(Limits limits, Sort sort, IEnumerable<TvShowProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("limits", limits);
            parameters.ConditionalAdd("sort", sort);
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<GetTVShowsResponse>("VideoLibrary.GetTVShows", parameters);
        }

        public Task<GetTVShowsResponse> GetTVShows(TVShowFilter filter, IEnumerable<TvShowProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("filter", filter?.ToSerializable());
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<GetTVShowsResponse>("VideoLibrary.GetTVShows", parameters);
        }

        public Task<GetTVShowsResponse> GetTVShows(Limits limits, TVShowFilter filter, IEnumerable<TvShowProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("limits", limits);
            parameters.ConditionalAdd("filter", filter?.ToSerializable());
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<GetTVShowsResponse>("VideoLibrary.GetTVShows", parameters);
        }

        public Task<GetTVShowsResponse> GetTVShows(Limits limits, Sort sort, TVShowFilter filter, IEnumerable<TvShowProperty> properties)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("limits", limits);
            parameters.ConditionalAdd("sort", sort);
            parameters.ConditionalAdd("filter", filter?.ToSerializable());
            parameters.ConditionalAdd("properties", properties?.Select(x => x.ToString()).ToList());

            return RunAsync<GetTVShowsResponse>("VideoLibrary.GetTVShows", parameters);
        }



        public Task Clean()
        {
            return RunAsync("VideoLibrary.Clean", null);
        }

        //public void Export(object options)
        //{
        //    throw new NotImplementedException();
        //    //"params": { "options": { "overwrite": false, "actorthumbs":true, "images":true } }
        //    /*"path": {
        //          "description": "Path to the directory to where the data should be exported", 
        //          "minLength": 1, 
        //          "required": true, 
        //          "type": "string"
        //        }
        //    */
        //}

        public Task RemoveEpisode(int episodeId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("episodeid", episodeId);

            return RunAsync("VideoLibrary.RemoveEpisode", parameters);
        }

        public Task RemoveMovie(int movieId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("movieid", movieId);

            return RunAsync("VideoLibrary.RemoveMovie", parameters);
        }

        public Task RemoveMusicVideo(int musicVideoId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("musicvideoid", musicVideoId);

            return RunAsync("VideoLibrary.RemoveMusicVideo", parameters);
        }

        public Task RemoveTVShow(int tvShowId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("tvShowid", tvShowId);

            return RunAsync("VideoLibrary.RemoveTVShow", parameters);
        }

        public Task Scan()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            return RunAsync("VideoLibrary.Scan", parameters);
        }

        public Task Scan(string directory)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("directory", directory);

            return RunAsync("VideoLibrary.Scan", parameters);
        }

        public Task Scan(bool showDialogs)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("showdialogs", showDialogs);

            return RunAsync("VideoLibrary.Scan", parameters);
        }

        public Task Scan(string directory, bool showDialogs)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.ConditionalAdd("directory", directory);
            parameters.Add("showdialogs", showDialogs);

            return RunAsync("VideoLibrary.Scan", parameters);
        }

        public Task SetEpisodeDetails(EpisodeDetails details)
        {
            if (details != null)
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.ConditionalAdd("details", details.Art?.ToSerializable());
                parameters.ConditionalAdd("director", details.Director);
                parameters.ConditionalAdd("episode", details.Episode);
                parameters.ConditionalAdd("episodeid", details.EpisodeId);
                parameters.ConditionalAdd("fanart", details.FanArt);
                parameters.ConditionalAdd("firstaired", details.FirstAired);
                parameters.ConditionalAdd("lastplayed", details.LastPlayed);
                parameters.ConditionalAdd("originaltitle", details.OriginalTitle);
                parameters.ConditionalAdd("playcount", details.PlayCount);
                parameters.ConditionalAdd("plot", details.Plot);
                parameters.ConditionalAdd("productioncode", details.ProductionCode);
                parameters.ConditionalAdd("rating", details.Rating);
                parameters.ConditionalAdd("runtime", details.RunTime);
                parameters.ConditionalAdd("season", details.Season);
                parameters.ConditionalAdd("thumbnail", details.Thumbnail);
                parameters.ConditionalAdd("title", details.Title);
                parameters.ConditionalAdd("votes", details.Votes);
                parameters.ConditionalAdd("writer", details.Writer);

                return RunAsync("VideoLibrary.SetEpisodeDetails", parameters);
            }

            return Task.FromResult(0);
        }

        public Task SetMovieDetails(MovieDetails details)
        {
            if (details != null)
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.ConditionalAdd("art", details.Art?.ToSerializable());
                parameters.ConditionalAdd("country", details.Country);
                parameters.ConditionalAdd("director", details.Director);
                parameters.ConditionalAdd("fanart", details.FanArt);
                parameters.ConditionalAdd("genre", details.Genre);
                parameters.ConditionalAdd("imdbnumber", details.ImdbNumber);
                parameters.ConditionalAdd("lastplayed", details.LastPlayed);
                parameters.ConditionalAdd("movieid", details.MovieId);
                parameters.ConditionalAdd("mpaa", details.MPAA);
                parameters.ConditionalAdd("originaltitle", details.OriginalTitle);
                parameters.ConditionalAdd("playcount", details.PlayCount);
                parameters.ConditionalAdd("plot", details.Plot);
                parameters.ConditionalAdd("plotoutline", details.PlotOutline);
                parameters.ConditionalAdd("rating", details.Rating);
                parameters.ConditionalAdd("runtime", details.RunTime);
                parameters.ConditionalAdd("set", details.Set);
                parameters.ConditionalAdd("showlink", details.ShowLink);
                parameters.ConditionalAdd("sorttitle", details.SortTitle);
                parameters.ConditionalAdd("studio", details.Studio);
                parameters.ConditionalAdd("tag", details.Tag);
                parameters.ConditionalAdd("tagline", details.TagLine);
                parameters.ConditionalAdd("thumbnail", details.Thumbnail);
                parameters.ConditionalAdd("title", details.Title);
                parameters.ConditionalAdd("top205", details.Top205);
                parameters.ConditionalAdd("trailer", details.Trailer);
                parameters.ConditionalAdd("votes", details.Votes);
                parameters.ConditionalAdd("writer", details.Writer);
                parameters.ConditionalAdd("year", details.Year);

                return RunAsync("VideoLibrary.SetMovieDetails", parameters);
            }

            return Task.FromResult(0);
        }

        public Task SetMusicVideoDetails(MusicVideoDetails details)
        {
            if (details != null)
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.ConditionalAdd("album", details.Album);
                parameters.ConditionalAdd("art", details.Art?.ToSerializable());
                parameters.ConditionalAdd("artist", details.Artist);
                parameters.ConditionalAdd("director", details.Director);
                parameters.ConditionalAdd("fanart", details.FanArt);
                parameters.ConditionalAdd("genre", details.Genre);
                parameters.ConditionalAdd("lastplayed", details.LastPlayed);
                parameters.ConditionalAdd("musicvideoid", details.MusicVideoId);
                parameters.ConditionalAdd("playcount", details.PlayCount);
                parameters.ConditionalAdd("plot", details.Plot);
                parameters.ConditionalAdd("runtime", details.RunTime);
                parameters.ConditionalAdd("studio", details.Studio);
                parameters.ConditionalAdd("tag", details.Tag);
                parameters.ConditionalAdd("thumbnail", details.Thumbnail);
                parameters.ConditionalAdd("title", details.Title);
                parameters.ConditionalAdd("track", details.Track);
                parameters.ConditionalAdd("year", details.Year);

                return RunAsync("VideoLibrary.SetMusicVideoDetails", parameters);
            }

            return Task.FromResult(0);
        }

        public Task SetTVShowDetails(TvShowDetails details)
        {
            if (details != null)
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.ConditionalAdd("art", details.Art?.ToSerializable());
                parameters.ConditionalAdd("episodeguide", details.EpisodeGuide);
                parameters.ConditionalAdd("fanart", details.FanArt);
                parameters.ConditionalAdd("genre", details.Genre);
                parameters.ConditionalAdd("imdbnumber", details.ImdbNumber);
                parameters.ConditionalAdd("lastplayed", details.LastPlayed);
                parameters.ConditionalAdd("mpaa", details.MPAA);
                parameters.ConditionalAdd("originaltitle", details.OriginalTitle);
                parameters.ConditionalAdd("playcount", details.PlayCount);
                parameters.ConditionalAdd("plot", details.Plot);
                parameters.ConditionalAdd("premiered", details.Premiered);
                parameters.ConditionalAdd("rating", details.Rating);
                parameters.ConditionalAdd("sorttitle", details.SortTitle);
                parameters.ConditionalAdd("studio", details.Studio);
                parameters.ConditionalAdd("tag", details.Tag);
                parameters.ConditionalAdd("thumbnail", details.Thumbnail);
                parameters.ConditionalAdd("title", details.Title);
                parameters.ConditionalAdd("tvshowid", details.TvShowId);
                parameters.ConditionalAdd("votes", details.Votes);

                return RunAsync("VideoLibrary.SetTVShowDetails", parameters);
            }

            return Task.FromResult(0);
        }

    }
}
