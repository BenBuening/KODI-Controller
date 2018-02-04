using System;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace KODI_Controller.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {

        private ICommand _moviesCommand;
        private ICommand _tvShowsCommand;
        private ICommand _configCommand;
        private ICommand _playMovieCommand;
        private ICommand _playTvEpisodeCommand;
        private ICommand _queueMovieCommand;
        private ICommand _queueTvEpisodeCommand;
        private ICommand _queueTvSeasonCommand;
        private ICommand _queueTvShowCommand;

        private ICommand _playPauseCommand;
        private ICommand _stopCommand;
        private ICommand _playlistNext;
        private ICommand _playlistBack;
        private ICommand _selectAudioTrack;
        private ICommand _selectSubtitle;
        private ICommand _selectPlaybackSpeed;
        private ICommand _selectShuffle;
        private ICommand _selectRepeat;
        private ICommand _playPlaylistItemCommand;
        private ICommand _removePlaylistItemCommand;

        private bool _isAppReady;
        private bool _isLoadingMovies;
        private bool _isLoadingTvShows;
        private bool _isPlayerEngaged;
        private bool _isPlayerPaused;
        private int _playProgress;


        // current states?

        /*
               audio tracks/subtitles
                speed/shuffle/repeat
                play/pause/stop/seek/
                playlist forward/back
                info
*/


        private ItemsCollection _libraryItems;
        private ICollectionView _libraryView;
        private string _filterCriteria;
        private ItemsCollection<PlaylistItem> _playlistItems;
        private int _playlistCount;
        private TimeSpan _playlistTotalRuntime;
        private PlayingItem _playingItem;


        public ICommand MoviesCommand
        {
            get { return _moviesCommand; }
            set { _moviesCommand = value; OnPropertyChanged(nameof(this.MoviesCommand)); }
        }
        public ICommand TvShowsCommand
        {
            get { return _tvShowsCommand; }
            set { _tvShowsCommand = value; OnPropertyChanged(nameof(this.TvShowsCommand)); }
        }
        public ICommand ConfigCommand
        {
            get { return _configCommand; }
            set { _configCommand = value; OnPropertyChanged(nameof(this.ConfigCommand)); }
        }
        public ICommand PlayMovieCommand
        {
            get { return _playMovieCommand; }
            set { _playMovieCommand = value; OnPropertyChanged(nameof(this.PlayMovieCommand)); }
        }
        public ICommand PlayTvEpisodeCommand
        {
            get { return _playTvEpisodeCommand; }
            set { _playTvEpisodeCommand = value; OnPropertyChanged(nameof(this.PlayTvEpisodeCommand)); }
        }
        public ICommand QueueMovieCommand
        {
            get { return _queueMovieCommand; }
            set { _queueMovieCommand = value; OnPropertyChanged(nameof(this.QueueMovieCommand)); }
        }
        public ICommand QueueTvEpisodeCommand
        {
            get { return _queueTvEpisodeCommand; }
            set { _queueTvEpisodeCommand = value; OnPropertyChanged(nameof(this.QueueTvEpisodeCommand)); }
        }
        public ICommand QueueTvSeasonCommand
        {
            get { return _queueTvSeasonCommand; }
            set { _queueTvSeasonCommand = value; OnPropertyChanged(nameof(this.QueueTvSeasonCommand)); }
        }
        public ICommand QueueTvShowCommand
        {
            get { return _queueTvShowCommand; }
            set { _queueTvShowCommand = value; OnPropertyChanged(nameof(this.QueueTvShowCommand)); }
        }

        public ICommand PlayPauseCommand
        {
            get { return _playPauseCommand; }
            set { _playPauseCommand = value; OnPropertyChanged(nameof(this.PlayPauseCommand)); }
        }
        public ICommand StopCommand
        {
            get { return _stopCommand; }
            set { _stopCommand = value; OnPropertyChanged(nameof(this.StopCommand)); }
        }
        public ICommand PlaylistNext
        {
            get { return _playlistNext; }
            set { _playlistNext = value; OnPropertyChanged(nameof(this.PlaylistNext)); }
        }
        public ICommand PlaylistBack
        {
            get { return _playlistBack; }
            set { _playlistBack = value; OnPropertyChanged(nameof(this.PlaylistBack)); }
        }
        public ICommand SelectAudioTrack
        {
            get { return _selectAudioTrack; }
            set { _selectAudioTrack = value; OnPropertyChanged(nameof(this.SelectAudioTrack)); }
        }
        public ICommand SelectSubtitle
        {
            get { return _selectSubtitle; }
            set { _selectSubtitle = value; OnPropertyChanged(nameof(this.SelectSubtitle)); }
        }
        public ICommand SelectPlaybackSpeed
        {
            get { return _selectPlaybackSpeed; }
            set { _selectPlaybackSpeed = value; OnPropertyChanged(nameof(this.SelectPlaybackSpeed)); }
        }
        public ICommand SelectShuffle
        {
            get { return _selectShuffle; }
            set { _selectShuffle = value; OnPropertyChanged(nameof(this.SelectShuffle)); }
        }
        public ICommand SelectRepeat
        {
            get { return _selectRepeat; }
            set { _selectRepeat = value; OnPropertyChanged(nameof(this.SelectRepeat)); }
        }
        public ICommand PlayPlaylistItemCommand
        {
            get { return _playPlaylistItemCommand; }
            set { _playPlaylistItemCommand = value; OnPropertyChanged(nameof(this.PlayPlaylistItemCommand)); }
        }
        public ICommand RemovePlaylistItemCommand
        {
            get { return _removePlaylistItemCommand; }
            set { _removePlaylistItemCommand = value; OnPropertyChanged(nameof(this.RemovePlaylistItemCommand)); }
        }

        public bool IsAppReady
        {
            get { return _isAppReady; }
            set { _isAppReady = value; OnPropertyChanged(nameof(this.IsAppReady)); }
        }
        public bool IsLoadingMovies
        {
            get { return _isLoadingMovies; }
            set { _isLoadingMovies = value; OnPropertyChanged(nameof(this.IsLoadingMovies)); }
        }
        public bool IsLoadingTvShows
        {
            get { return _isLoadingTvShows; }
            set { _isLoadingTvShows = value; OnPropertyChanged(nameof(this.IsLoadingTvShows)); }
        }
        public bool IsPlayerEngaged
        {
            get { return _isPlayerEngaged; }
            set { _isPlayerEngaged = value; OnPropertyChanged(nameof(this.IsPlayerEngaged)); }
        }
        public bool IsPlayerPaused
        {
            get { return _isPlayerPaused; }
            set { _isPlayerPaused = value; OnPropertyChanged(nameof(this.IsPlayerPaused)); }
        }
        public int PlayProgress
        {
            get { return _playProgress; }
            set { _playProgress = value; OnPropertyChanged(nameof(this.PlayProgress)); }
        }



        public ItemsCollection LibraryItems
        {
            get { return _libraryItems; }
            set
            {
                _libraryItems = value;
                OnPropertyChanged(nameof(this.LibraryItems));
                if (value == null)
                    this.LibraryView = null;
                else
                {
                    ICollectionView temp = CollectionViewSource.GetDefaultView(value);
                    temp.Filter = LibraryViewFilterPredicate;
                    this.LibraryView = temp;
                }
            }
        }
        public ICollectionView LibraryView
        {
            get { return _libraryView; }
            private set { _libraryView = value; OnPropertyChanged(nameof(this.LibraryView)); }
        }
        public string FilterCriteria
        {
            get { return _filterCriteria; }
            set
            {
                _filterCriteria = value;
                OnPropertyChanged(nameof(this.FilterCriteria));
                this.LibraryView?.Refresh();
            }
        }
        public ItemsCollection<PlaylistItem> PlaylistItems
        {
            get { return _playlistItems; }
            private set { _playlistItems = value; OnPropertyChanged(nameof(this.PlaylistItems)); }
        }
        public int PlaylistCount
        {
            get { return _playlistCount; }
            private set { _playlistCount = value; OnPropertyChanged(nameof(this.PlaylistCount)); }
        }
        public TimeSpan PlaylistTotalRuntime
        {
            get { return _playlistTotalRuntime; }
            private set { _playlistTotalRuntime = value; OnPropertyChanged(nameof(this.PlaylistTotalRuntime)); }
        }
        public PlayingItem PlayingItem
        {
            get { return _playingItem; }
            set { _playingItem = value; OnPropertyChanged(nameof(this.PlayingItem)); }
        }



        public MainWindowViewModel()
        {
            this.PlaylistCount = 0;
            this.PlaylistTotalRuntime = TimeSpan.Zero;
            this.PlaylistItems = new ItemsCollection<PlaylistItem>();
            this.PlaylistItems.PropertyChanged += PlaylistItems_PropertyChanged;
        }

        private bool LibraryViewFilterPredicate(object arg)
        {
            IFilterable item = arg as IFilterable;
            if (item == null)
                return false;
            if (string.IsNullOrEmpty(this.FilterCriteria))
                return true;
            return item.GetFilterableText().IndexOf(this.FilterCriteria, StringComparison.OrdinalIgnoreCase) != -1;
        }

        private void PlaylistItems_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Count") UpdatePlaylistSummaryValues();
        }

        private void UpdatePlaylistSummaryValues()
        {
            TimeSpan runtime = TimeSpan.Zero;
            foreach (var x in this.PlaylistItems) if (x.RunTime.HasValue) runtime += x.RunTime.Value;

            this.PlaylistCount = this.PlaylistItems.Count;
            this.PlaylistTotalRuntime = runtime;
        }

    }

}
