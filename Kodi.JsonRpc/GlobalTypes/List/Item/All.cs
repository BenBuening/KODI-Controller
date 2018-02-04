namespace Kodi.JsonRpc.GlobalTypes.List.Item
{
    public class All : Kodi.JsonRpc.GlobalTypes.List.Item.Base
    {
        public string ChannelType { get; set; } // "tv"
        public string Channel { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int? ChannelNumber { get; set; }
        public bool? Hidden { get; set; }
        public bool? Locked { get; set; }

        public All() { }

        public All(Video.Details.Movie movie)
        {
            this.Type = "movie";
            this.PlotOutline = movie.PlotOutline;
            this.SortTitle = movie.SortTitle;
            this.Id = movie.MovieId;
            this.Cast = movie.Cast;
            this.Votes = movie.Votes;
            this.ShowLink = movie.ShowLink;
            this.Top250 = movie.Top250;
            this.Trailer = movie.Trailer;
            this.Year = movie.Year;
            this.Country = movie.Country;
            this.Studio = movie.Studio;
            this.Set = movie.Set;
            this.Genre = movie.Genre;
            this.MPAA = movie.MPAA;
            this.SetId = movie.SetId;
            this.Rating = movie.Rating;
            this.Tag = movie.Tag;
            this.TagLine = movie.TagLine;
            this.Writer = movie.Writer;
            this.OriginalTitle = movie.OriginalTitle;
            this.ImdbNumber = movie.ImdbNumber;
            this.StreamDetails = movie.StreamDetails;
            this.Director = movie.Director;
            this.Resume = movie.Resume;
            this.RunTime = movie.RunTime;
            this.DateAdded = movie.DateAdded;
            this.File = movie.File;
            this.LastPlayed = movie.LastPlayed;
            this.Plot = movie.Plot;
            this.Title = movie.Title;
            this.Art = movie.Art;
            this.PlayCount = movie.PlayCount;
            this.FanArt = movie.FanArt;
            this.Thumbnail = movie.Thumbnail;
            this.Label = movie.Label;
        }

        public All(Video.Details.Episode episode)
        {
            this.Type = "episode";
            this.Cast = episode.Cast;
            this.ProductionCode = episode.ProductionCode;
            this.Rating = episode.Rating;
            this.Votes = episode.Votes;
            this.Episode = episode.EpisodeNumber;
            this.ShowTitle = episode.ShowTitle;
            this.Id = episode.EpisodeId;
            this.TvShowId = episode.TvShowId;
            this.Season = episode.Season;
            this.FirstAired = episode.FirstAired;
            //this.UniqueId = episode.UniqueId;
            this.Writer = episode.Writer;
            this.OriginalTitle = episode.OriginalTitle;
            this.StreamDetails = episode.StreamDetails;
            this.Director = episode.Director;
            this.Resume = episode.Resume;
            this.RunTime = episode.RunTime;
            this.DateAdded = episode.DateAdded;
            this.File = episode.File;
            this.LastPlayed = episode.LastPlayed;
            this.Plot = episode.Plot;
            this.Title = episode.Title;
            this.Art = episode.Art;
            this.PlayCount = episode.PlayCount;
            this.FanArt = episode.FanArt;
            this.Thumbnail = episode.Thumbnail;
            this.Label = episode.Label;
        }

    }
}
