using Kodi.JsonRpc.GlobalTypes.Video.Details;
using System.Collections.Generic;
using System.Windows.Input;
using System;

namespace KODI_Controller.ViewModel
{
    internal class TvSeasonItem : ViewModelBase, IHasThumb, IFilterable
    {

        private ICommand _toggleExpandedCommand;
        private List<TvEpisodeItem> _children;
        private bool _isExpanded;
        private bool _isLoading;

        private string _title;
        private int? _year;
        private int _episodes;

        private string _runTime;
        private string _thumbnailPath;



        public TvSeasonItem(Season tvSeason, TvShowItem parent)
        {
            this.Parent = parent;
            this.TvSeason = tvSeason;
            _title = "Season " + tvSeason.SeasonNumber.ToString("00");
            _episodes = tvSeason.Episode;
        }

        public Season TvSeason { get; private set; }
        public TvShowItem Parent { get; private set; }


        public ICommand ToggleExpanded
        {
            get { return _toggleExpandedCommand; }
            set { _toggleExpandedCommand = value; OnPropertyChanged(nameof(this.ToggleExpanded)); }
        }

        public List<TvEpisodeItem> Children
        {
            get { return _children; }
            set { _children = value; OnPropertyChanged(nameof(this.Children)); }
        }

        public bool IsExpanded
        {
            get { return _isExpanded; }
            set { _isExpanded = value; }
        }

        public bool IsLoading
        {
            get { return _isLoading; }
            set { _isLoading = value; }
        }


        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(nameof(this.Title)); }
        }

        public int Episodes
        {
            get { return _episodes; }
            set { _episodes = value; OnPropertyChanged(nameof(this.Episodes)); }
        }

        public string ThumbnailPath
        {
            get { return _thumbnailPath; }
            set { _thumbnailPath = value; OnPropertyChanged(nameof(this.ThumbnailPath)); }
        }

        public string GetRawThumbUrl()
        {
            return this.TvSeason.Thumbnail;
        }

        public string GetFilterableText()
        {
            return this.Parent.GetFilterableText();
        }

    }
}
