using Kodi.JsonRpc.GlobalTypes.Video.Details;
using System;

namespace KODI_Controller.ViewModel
{
    internal class MovieItem : ViewModelBase, IHasThumb, IFilterable
    {

        private string _title;
        private int? _year;

        private string _runTime;
        private string _thumbnailPath;



        public MovieItem(Movie movie)
        {
            this.Movie = movie;
            _title = movie.Title;
            _year = movie.Year;
            _runTime = TimeSpan.FromSeconds(movie.RunTime.GetValueOrDefault()).ToString("g");
        }

        public Movie Movie { get; private set; }

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


        public string RunTime
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
            return this.Movie.Thumbnail;
        }

        public string GetFilterableText()
        {
            return this.Title;
        }

    }
}
