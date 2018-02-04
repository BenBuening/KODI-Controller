namespace Kodi.JsonRpc.GlobalTypes.Player.Notifications
{
    public class PlayerSeek : Kodi.JsonRpc.GlobalTypes.Player.Notifications.PlayerData
    {
        public Kodi.JsonRpc.GlobalTypes.Global.Time SeekOffset { get; set; }
        public Kodi.JsonRpc.GlobalTypes.Global.Time Time { get; set; }
    }
}
