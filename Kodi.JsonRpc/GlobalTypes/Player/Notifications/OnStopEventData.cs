using Kodi.JsonRpc.GlobalTypes.Notifications;

namespace Kodi.JsonRpc.GlobalTypes.Player.Notifications
{
    public class OnStopEventData
    {
        public bool End { get; set; }
        public ItemData Item { get; set; }

    }
}
