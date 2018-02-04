using Kodi.JsonRpc.GlobalTypes.Notifications;

namespace Kodi.JsonRpc.GlobalTypes.Player.Notifications
{
    public class OnPauseEventData
    {
        public PlayerData Player { get; set; }
        public ItemData Item { get; set; }
    }
}
