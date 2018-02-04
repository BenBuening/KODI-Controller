namespace Kodi.JsonRpc.GlobalTypes.Player.Responses
{
    public class SeekResponse
    {
        public double Percentage { get; set; }
        public Global.Time Time { get; set; }
        public Global.Time TotalTime { get; set; }
    }
}
