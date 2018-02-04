using System;
using Kodi.JsonRpc.GlobalTypes.Video.Details;

namespace KODI_Controller.ViewModel
{
    internal class TvEpisodeItem : ViewModelBase, IHasThumb, IFilterable
    {

        private string _title;
        private int? _year;
        private TimeSpan? _runTime;

        private string _thumbnailPath;



        public TvEpisodeItem(Episode tvEpisode, TvSeasonItem parent)
        {
            this.Parent = parent;
            this.TvEpisode = tvEpisode;
            _title = tvEpisode.EpisodeNumber.ToString() + ". " + tvEpisode.Title;
            _runTime = tvEpisode.RunTime.HasValue ? TimeSpan.FromSeconds(tvEpisode.RunTime.Value) : (TimeSpan?)null;
        }

        public Episode TvEpisode { get; private set; }
        public TvSeasonItem Parent { get; private set; }


        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(nameof(this.Title)); }
        }

        public int? Year
        {
            get { return _year; }
            set { _year = value; OnPropertyChanged(nameof(this.Year)); }
        }

        public TimeSpan? RunTime
        {
            get { return _runTime; }
            set { _runTime = value; OnPropertyChanged(nameof(this.RunTime)); }
        }


        public string ThumbnailPath
        {
            get { return _thumbnailPath; }
            set { _thumbnailPath = value; OnPropertyChanged(nameof(this.ThumbnailPath)); }
        }

        public string GetRawThumbUrl()
        {
            return this.TvEpisode.Thumbnail;
        }

        public string GetFilterableText()
        {
            return this.Parent.GetFilterableText();
        }

    }
}
