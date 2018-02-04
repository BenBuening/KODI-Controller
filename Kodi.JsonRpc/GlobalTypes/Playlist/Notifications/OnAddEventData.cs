using Kodi.JsonRpc.GlobalTypes.Notifications;

namespace Kodi.JsonRpc.GlobalTypes.Playlist.Notifications
{
    public class OnAddEventData
    {
        public int PlaylistId { get; set; }
        public ItemData Item { get; set; }
        public int? Position { get; set; }
    }
}
