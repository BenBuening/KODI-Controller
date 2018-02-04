using Kodi.JsonRpc.GlobalTypes.Video.Details;
using System.Collections.Generic;
using System.Windows.Input;
using System;

namespace KODI_Controller.ViewModel
{
    internal class TvShowItem : ViewModelBase, IHasThumb, IFilterable
    {

        private ICommand _toggleExpandedCommand;
        private List<TvSeasonItem> _children;
        private bool _isExpanded;
        private bool _isLoading;

        private string _title;
        private int? _year;
        private int _episodes;

        private string _thumbnailPath;



        public TvShowItem(TvShow show)
        {
            this.TvShow = show;
            _title = show.Title;
            _year = show.Year;
            _episodes = show.Episode;
        }

        public TvShow TvShow { get; private set; }


        public ICommand ToggleExpanded
        {
            get { return _toggleExpandedCommand; }
            set { _toggleExpandedCommand = value; OnPropertyChanged(nameof(this.ToggleExpanded)); }
        }

        public List<TvSeasonItem> Children
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

        public int? Year
        {
            get { return _year; }
            set { _year = value; OnPropertyChanged(nameof(this.Year)); }
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
            return this.TvShow.Thumbnail;
        }

        public string GetFilterableText()
        {
            return this.Title;
        }

    }
}
