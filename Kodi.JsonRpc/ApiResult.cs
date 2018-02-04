namespace Kodi.JsonRpc
{
    public class ApiResult
    {
        public string Id { get; set; }
        public string MessageJson { get; set; }
        public bool IsError { get; internal set; }
    }
}
