using Kodi.JsonRpc.GlobalTypes.Notifications;

namespace Kodi.JsonRpc.GlobalTypes.Player.Notifications
{
    public class OnSeekEventData
    {
        public ItemData Item { get; set; }
        public PlayerSeek Player { get; set; }
    }
}
