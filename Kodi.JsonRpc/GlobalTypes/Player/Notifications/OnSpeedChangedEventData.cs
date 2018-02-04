using Kodi.JsonRpc.GlobalTypes.Notifications;

namespace Kodi.JsonRpc.GlobalTypes.Player.Notifications
{
    public class OnSpeedChangedEventData
    {
        public ItemData Item { get; set; }
        public PlayerData Player { get; set; }
    }
}
