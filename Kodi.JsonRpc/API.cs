using Kodi.JsonRpc.Methods;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;

namespace Kodi.JsonRpc
{
    public class API : IDisposable
    {

        // todo: fully implement disposed. flag & exception on method calls after disposed
        // todo: better logging. sometimes the logging fails and messages dont get flushed so i dont know what messages are failing


        private int _id;
        private MessagePump _pump;
        private StreamWriter _log;
        private bool _isStarted;
        private ConcurrentDictionary<string, AsyncResult> _asyncResults;
        private readonly string _lineSeparator = Environment.NewLine + Environment.NewLine + new string('*', 20) + Environment.NewLine + Environment.NewLine;
        private readonly ExpandoObjectConverter notificationConverter = new ExpandoObjectConverter();


        /* Message Pump  */

        private void _pump_ExceptionReceived(object sender, Exception e)
        {
            Dispose();
            this.ShuttingDown?.Invoke(this, EventArgs.Empty);
        }

        private void _pump_MessageReceived(object sender, string e)
        {
            this.MessageReceived?.Invoke(this, e);


            JObject responseObj = JObject.Parse(e);

            if (responseObj["method"] != null)
                HandleNotification(responseObj);

            else if (responseObj["error"] != null)
                HandleError(responseObj);

            else if (responseObj["id"] != null)
                HandleResponse(responseObj);
        }

        private void HandleNotification(JObject responseObj)
        {
            try
            {
                string method = responseObj.Value<string>("method");
                if (_notificationHandlers.ContainsKey(method))
                {
                    NotificationMeta meta = _notificationHandlers[method];
                    meta.Callback?.Invoke(responseObj.SelectToken("params.data").ToObject(meta.Type));
                }
            }
            catch (Exception ex)
            {
                if (_log != null)
                {
                    _log.Write(ex.Message + _lineSeparator);
                    _log.Write("%% Error parsing json %%" + _lineSeparator);
                    _log.Write(responseObj.ToString() + _lineSeparator);
                    _log.Flush();
                }
                throw;
            }
        }

        private void HandleError(JObject responseObj)
        {
            try
            {
                ApiResult result = new ApiResult();
                result.Id = responseObj["id"].ToString();
                result.IsError = true;
                ApiError error = JsonConvert.DeserializeObject<ApiError>(responseObj["error"].ToString());

                // todo: handle error??
            }
            catch (Exception ex)
            {
                if (_log != null)
                {
                    _log.Write(ex.Message + _lineSeparator);
                    _log.Write("%% Error parsing json %%" + _lineSeparator);
                    _log.Write(responseObj.ToString() + _lineSeparator);
                    _log.Flush();
                }
                throw;
            }
        }

        private void HandleResponse(JObject responseObj)
        {

            string id = responseObj["id"].ToString();
            AsyncResult asyncResult;

            if (_asyncResults.TryRemove(id, out asyncResult))
                try
                {
                    ApiResult result = new ApiResult();
                    result.Id = id;

                    var node = responseObj["result"];
                    if (asyncResult.JsonPath == null)
                        result.MessageJson = node.ToString();
                    else
                        result.MessageJson = node.SelectToken(asyncResult.JsonPath).ToString();

                    if (asyncResult.ResultType == null)
                        asyncResult.Set();
                    else
                        asyncResult.Set(JsonConvert.DeserializeObject(result.MessageJson, asyncResult.ResultType));
                }
                catch (Exception ex)
                {
                    if (_log != null)
                    {
                        _log.Write(ex.Message + _lineSeparator);
                        _log.Write("%% Error parsing json %%");
                        if (asyncResult.Result != null) _log.Write(" into result type {" + asyncResult.ResultType.ToString() + "}");
                        _log.Write(_lineSeparator);
                        _log.Write(responseObj.ToString() + _lineSeparator);
                        _log.Flush();
                    }
                    throw;
                }
        }

        private void LogMessage(object sender, string message)
        {
            _log?.Write(message + _lineSeparator);
        }


        /* Utility */

        private Dictionary<string, object> BuildMessageObject(string method, Dictionary<string, object> parameters)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            result.Add("id", (++_id).ToString());
            result.Add("jsonrpc", "2.0");
            result.Add("method", method);
            result.ConditionalAdd("params", parameters);

            return result;
        }



        public Addons Addons { get; private set; }
        public Application Application { get; private set; }
        public AudioLibrary AudioLibrary { get; private set; }
        public Files Files { get; private set; }
        public Gui Gui { get; private set; }
        public Input Input { get; private set; }
        public Methods.JsonRpc JsonRpc { get; private set; }
        public Player Player { get; private set; }
        public Playlist Playlist { get; private set; }
        public PVR PVR { get; private set; }
        public Methods.System System { get; private set; }
        public VideoLibrary VideoLibrary { get; private set; }
        public int TimeoutMilliseconds { get; set; }


        public event EventHandler CommunicationsFailure;
        public event EventHandler<string> MessageReceived;
        public event EventHandler Connected;
        public event EventHandler ShuttingDown;


        public API()
        {
            _asyncResults = new ConcurrentDictionary<string, AsyncResult>();
            _notificationHandlers = new Dictionary<string, NotificationMeta>();

            this.Addons = new Addons(this);
            this.Application = new Application(this);
            this.AudioLibrary = new AudioLibrary(this);
            this.Files = new Files(this);
            this.Gui = new Gui(this);
            this.Input = new Input(this);
            this.JsonRpc = new Methods.JsonRpc(this);
            this.Player = new Player(this);
            this.Playlist = new Playlist(this);
            this.PVR = new PVR(this);
            this.System = new Methods.System(this);
            this.VideoLibrary = new VideoLibrary(this);

            this.TimeoutMilliseconds = 50000;
            this.MessageReceived += LogMessage;
        }

        public void Start(ApiConfig settings)
        {
            if (!_isStarted)
            {
                _isStarted = true;

                try { _log = File.AppendText(string.Format(@"c:\_temp\kodi-{0:yyyy-MM-dd-HH-mm-ss}.txt", DateTime.Now)); }
                catch { _log = null; }

                _pump = new MessagePump();
                _pump.MessageReceived += _pump_MessageReceived;
                _pump.ExceptionReceived += _pump_ExceptionReceived;
                _pump.Start(settings.IpAddress, settings.Port);

                this.Connected?.Invoke(this, EventArgs.Empty);
            }
        }

        public void Dispose()
        {
            _log?.Dispose();
            _pump.Stop();
        }



        //internal void SendMessage(string method, Dictionary<string, object> parameters)
        //{
        //    var messageObject = BuildMessageObject(method, parameters);
        //    string message = JsonConvert.SerializeObject(messageObject);
        //    LogMessage(this, message);
        //    _pump.Send(message);
        //}

        internal void SendMessage(string method, Dictionary<string, object> parameters, AsyncResult asyncResult)
        {
            var messageObject = BuildMessageObject(method, parameters);
            string id = (string)messageObject["id"];
            string message = JsonConvert.SerializeObject(messageObject);
            LogMessage(this, message);
            _asyncResults.TryAdd(id, asyncResult);
            _pump.Send(message);
        }



        private class NotificationMeta
        {
            public Type Type { get; set; }
            public Action<object> Callback { get; set; }
            public NotificationMeta(Type type, Action<object> callback) { this.Type = type; this.Callback = callback; }
        }

        private Dictionary<string, NotificationMeta> _notificationHandlers;

        internal void RegisterNotificationHandler(string notificationName, Type callbackParamType, Action<object> callback)
        {
            if (_isStarted)
                throw new ApplicationException("cannot register new notification handlers after starting messaging");

            _notificationHandlers[notificationName] = new NotificationMeta(callbackParamType, callback);
        }

    }
}
