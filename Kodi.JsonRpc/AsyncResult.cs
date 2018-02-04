using System;
using System.Threading;

namespace Kodi.JsonRpc
{
    internal class AsyncResult
    {
        public AsyncResult(EventWaitHandle waitHandle, string jsonPath) { this.WaitHandle = waitHandle; this.JsonPath = jsonPath; }
        public AsyncResult(EventWaitHandle waitHandle, Type resultType, string jsonPath) { this.WaitHandle = waitHandle; this.ResultType = resultType; this.JsonPath = jsonPath; }
        public void Set() { this.WaitHandle.Set(); }
        public void Set(object result) { this.Result = result; this.WaitHandle.Set(); }
        private EventWaitHandle WaitHandle { get; set; }
        public object Result { get; private set; }
        public Type ResultType { get; private set; }
        public string JsonPath { get; private set; }
    }
}
