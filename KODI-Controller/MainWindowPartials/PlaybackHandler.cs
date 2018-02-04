using Kodi.JsonRpc.GlobalTypes.Player;
using Kodi.JsonRpc.GlobalTypes.Player.Notifications;
using KODI_Controller.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KODI_Controller
{
    public partial class MainWindow
    {

        private void SetUpPlaybackHandlers()
        {
            this.API.Player.OnPlay += Player_OnPlay;
            this.API.Player.OnStop += Player_OnStop;
            //this.API.Player.OnPause += (x, y) => { MessageBox.Show("pause"); };
            //this.API.Player.OnSeek += (x, y) => MessageBox.Show("seek");

            this.ViewModel.PlayMovieCommand = new RelayCommand<MovieItem>(PlayMovie);
            this.ViewModel.PlayTvEpisodeCommand = new RelayCommand<TvEpisodeItem>(PlayTvEpisode);
            this.ViewModel.QueueMovieCommand = new RelayCommand<MovieItem>(QueueMovie);
            this.ViewModel.QueueTvEpisodeCommand = new RelayCommand<TvEpisodeItem>(QueueTvEpisode);
            this.ViewModel.QueueTvSeasonCommand = new RelayCommand<TvSeasonItem>(QueueTvSeason);
            this.ViewModel.QueueTvShowCommand = new RelayCommand<TvShowItem>(QueueTvShow);
            this.ViewModel.PlayPauseCommand = new RelayCommand(PlayPause);
            this.ViewModel.StopCommand = new RelayCommand(Stop);
            this.ViewModel.PlaylistBack = new RelayCommand(PlaylistBack);
            this.ViewModel.PlaylistNext = new RelayCommand(PlaylistNext);
        }

        private void Player_OnStop(object sender, OnStopEventData e)
        {
            this.Dispatcher.BeginInvoke(new Action<OnStopEventData>(HandleOnStop), e);
        }

        private void Player_OnPlay(object sender, OnPlayEventData e)
        {
            this.Dispatcher.Invoke(new Action<OnPlayEventData>(HandleOnPlay), e);
        }

        private void HandleOnPlay(OnPlayEventData e)
        {
            this.ViewModel.IsPlayerEngaged = true;
            this.ViewModel.PlayingItem = new PlayingItem() { Id = e.Item.Id, Type = e.Item.Type.ToString() };
        }

        private void HandleOnStop(OnStopEventData e)
        {
            this.ViewModel.IsPlayerEngaged = false;
            this.ViewModel.PlayingItem = null;
        }


        private async void PlayMovie(MovieItem item)
        {
            await this.API.Playlist.Clear(_videoPlaylistId);
            await this.API.Playlist.AddMovie(_videoPlaylistId, item.Movie.MovieId);
            await this.API.Player.Open(_videoPlaylistId, 0);
        }

        private async void PlayTvEpisode(TvEpisodeItem item)
        {
            await this.API.Playlist.Clear(_videoPlaylistId);
            await this.API.Playlist.AddEpisode(_videoPlaylistId, item.TvEpisode.EpisodeId);
            await this.API.Player.Open(_videoPlaylistId, 0);
        }

        private async void QueueMovie(MovieItem item)
        {
            await this.API.Playlist.AddMovie(_videoPlaylistId, item.Movie.MovieId);
        }

        private async void QueueTvEpisode(TvEpisodeItem item)
        {
            await this.API.Playlist.AddEpisode(_videoPlaylistId, item.TvEpisode.EpisodeId);
        }

        private async void QueueTvSeason(TvSeasonItem item)
        {
            var properties = new List<Kodi.JsonRpc.GlobalTypes.Video.EpisodeProperty>() { Kodi.JsonRpc.GlobalTypes.Video.EpisodeProperty.season, Kodi.JsonRpc.GlobalTypes.Video.EpisodeProperty.episode };
            var results = await this.API.VideoLibrary.GetEpisodes(item.TvSeason.TvShowId, item.TvSeason.SeasonNumber, properties);
            results.Episodes.Sort((x, y) => { int result = x.Season - y.Season; if (result == 0) result = x.EpisodeNumber - y.EpisodeNumber; return result; });
            foreach (var episode in results.Episodes)
                await this.API.Playlist.AddEpisode(_videoPlaylistId, episode.EpisodeId);
        }

        private async void QueueTvShow(TvShowItem item)
        {
            var properties = new List<Kodi.JsonRpc.GlobalTypes.Video.EpisodeProperty>() { Kodi.JsonRpc.GlobalTypes.Video.EpisodeProperty.season, Kodi.JsonRpc.GlobalTypes.Video.EpisodeProperty.episode };
            var results = await this.API.VideoLibrary.GetEpisodes(item.TvShow.TvShowId, properties);
            results.Episodes.Sort((x, y) => { int result = x.Season - y.Season; if (result == 0) result = x.EpisodeNumber - y.EpisodeNumber; return result; });
            foreach (var episode in results.Episodes)
                await this.API.Playlist.AddEpisode(_videoPlaylistId, episode.EpisodeId);
        }

        private async void PlayPause()
        {
            await this.API.Player.PlayPause(_videoPlayerId);
        }

        private async void Stop()
        {
            await this.API.Player.Stop(_videoPlayerId);
        }

        private async void PlaylistBack()
        {
            await this.API.Player.Move(_videoPlayerId, MoveDirection.left);
        }

        private async void PlaylistNext()
        {
            await this.API.Player.Move(_videoPlayerId, MoveDirection.right);
        }




        private async Task LoadCurrentlyPlaying()
        {
            var players = await this.API.Player.GetActivePlayers();
            if (players.Count > 0)
            {
                Kodi.JsonRpc.GlobalTypes.Player.Player player = players[0];
                var item = await this.API.Player.GetItem(player.PlayerId, null);
                if (item != null)
                {
                    this.ViewModel.IsPlayerEngaged = true;
                    this.ViewModel.PlayingItem = new PlayingItem() { Id = item.Id, Type = item.Type };
                }
            }

            return;
        }

    }
}
