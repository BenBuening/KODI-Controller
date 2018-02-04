using Kodi.JsonRpc.GlobalTypes.List.Item;
using Kodi.JsonRpc.GlobalTypes.Video.Details;
using System;

namespace KODI_Controller.ViewModel
{
    internal class PlaylistItem : ViewModelBase
    {

        private string _title;
        private TimeSpan? _runTime;
        private DateTime? _lastPlayed;



        public PlaylistItem(All item)
        {
            this.All = item;
            _runTime = item.RunTime.HasValue ? TimeSpan.FromSeconds(item.RunTime.Value) : (TimeSpan?)null;
            _lastPlayed = item.LastPlayed;

            switch (item.Type)
            {
                case "movie":
                    _title = item.Title;
                    break;

                case "episode":
                    _title = string.Format("{0}, S{1:00}E{2:00} - {3}", item.ShowTitle, item.Season, item.Episode, item.Title);
                    break;
            }
        }

        public All All { get; private set; }

        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(nameof(this.Title)); }
        }

        public TimeSpan? RunTime
        {
            get { return _runTime; }
            set { _runTime = value; OnPropertyChanged(nameof(this.RunTime)); }
        }

        public DateTime? LastPlayed
        {
            get { return _lastPlayed; }
            set { _lastPlayed = value; OnPropertyChanged(nameof(this.LastPlayed)); }
        }

    }
}
