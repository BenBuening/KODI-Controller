using Kodi.JsonRpc.GlobalTypes.Notifications;
using Kodi.JsonRpc.GlobalTypes.Playlist.Notifications;
using KODI_Controller.ViewModel;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace KODI_Controller
{
    public partial class MainWindow
    {

        private void SetupPlaylistHandlers()
        {
            this.API.Playlist.OnClear += Playlist_OnClear;
            this.API.Playlist.OnAdd += Playlist_OnAdd;
            this.API.Playlist.OnRemove += Playlist_OnRemove;

            this.ViewModel.PlayPlaylistItemCommand = new RelayCommand<PlaylistItem>(PlayPlaylistItem, CanPlayPlaylistItem);
            this.ViewModel.RemovePlaylistItemCommand = new RelayCommand<PlaylistItem>(RemovePlaylistItem);
        }



        private void Playlist_OnClear(object sender, OnClearEventData e)
        {
            this.Dispatcher.BeginInvoke(new Action<OnClearEventData>(HandleOnClear), e);
        }

        private void Playlist_OnAdd(object sender, OnAddEventData e)
        {
            this.Dispatcher.BeginInvoke(new Action<OnAddEventData>(HandleOnAdd), e);
        }

        private void Playlist_OnRemove(object sender, OnRemoveEventData e)
        {
            this.Dispatcher.BeginInvoke(new Action<OnRemoveEventData>(HandleOnRemove), e);
        }

        private void HandleOnClear(OnClearEventData e)
        {
            this.ViewModel.PlaylistItems.Clear();
        }

        private async void HandleOnAdd(OnAddEventData e)
        {
            if (e.PlaylistId == _videoPlaylistId)
            {
                PlaylistItem item;
                switch (e.Item.Type)
                {
                    case ItemData.ItemType.episode:
                        var episode = await this.API.VideoLibrary.GetEpisodeDetails(e.Item.Id);
                        item = new PlaylistItem(new Kodi.JsonRpc.GlobalTypes.List.Item.All(episode));
                        break;

                    case ItemData.ItemType.movie:
                        var movie = await this.API.VideoLibrary.GetMovieDetails(e.Item.Id);
                        item = new PlaylistItem(new Kodi.JsonRpc.GlobalTypes.List.Item.All(movie));
                        break;

                    default:
                        MessageBox.Show("unhandled item type: " + e.Item.Type);
                        return;
                }

                if (e.Position.HasValue)
                    this.ViewModel.PlaylistItems.Insert(e.Position.Value, item);
                else
                    this.ViewModel.PlaylistItems.Add(item);
            }
        }

        private void HandleOnRemove(OnRemoveEventData e)
        {
            if (e.PlaylistId == _videoPlaylistId && e.Position < this.ViewModel.PlaylistItems.Count)
                this.ViewModel.PlaylistItems.RemoveAt(e.Position);
        }

        private async void PlayPlaylistItem(PlaylistItem item)
        {
            await this.API.Player.Open(_videoPlaylistId, this.ViewModel.PlaylistItems.IndexOf(item));
        }

        private bool CanPlayPlaylistItem(PlaylistItem item)
        {
            return !IsPlaying(item);
        }

        private async void RemovePlaylistItem(PlaylistItem item)
        {
            if (IsPlaying(item))
                await this.API.Player.Stop(_videoPlayerId);
            await this.API.Playlist.Remove(_videoPlaylistId, this.ViewModel.PlaylistItems.IndexOf(item));
        }




        private async Task LoadPlaylist()
        {
            var playlists = await this.API.Playlist.GetPlaylists();
            var videoPlaylist = playlists.Single(x => x.Type == Kodi.JsonRpc.GlobalTypes.Playlist.PlaylistType.video);
            _videoPlaylistId = videoPlaylist.PlaylistId;

            var playlistItems = await this.API.Playlist.GetItems(_videoPlaylistId);
            this.ViewModel.PlaylistItems.Clear();
            if (playlistItems?.Items != null)
                this.ViewModel.PlaylistItems.AddRange(playlistItems.Items.Select(x => new PlaylistItem(x)));

            return;
        }

        private bool IsPlaying(PlaylistItem item)
        {
            return item != null && this.ViewModel.PlayingItem != null && this.ViewModel.PlayingItem.Id == item.All.Id && this.ViewModel.PlayingItem.Type == item.All.Type;
        }

    }
}
