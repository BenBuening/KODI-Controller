using System.Dynamic;

namespace Kodi.JsonRpc
{
    public class NotificationReceivedEventArgs
    {
        public string MethodPrefix { get; private set; }
        public string Method { get; private set; }
        public dynamic Data { get; private set; }
        public NotificationReceivedEventArgs(string methodPrefix, string method, ExpandoObject data)
        {
            this.MethodPrefix = methodPrefix;
            this.Method = method;
            this.Data = data;
        }
    }
}
