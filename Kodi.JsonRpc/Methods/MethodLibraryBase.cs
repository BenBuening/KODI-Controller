using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Kodi.JsonRpc.Methods
{
    public abstract class MethodLibraryBase
    {

        protected API Api { get; private set; }

        protected MethodLibraryBase(API api)
        {
            this.Api = api;
        }

        protected Task<T> RunAsync<T>(string methodName, Dictionary<string, object> parameters)
        {
            return RunAsync<T>(methodName, parameters, null);
        }

        protected Task<T> RunAsync<T>(string methodName, Dictionary<string, object> parameters, string jsonPath)
        {
            EventWaitHandle waitHandle = new EventWaitHandle(false, EventResetMode.ManualReset);

            return Task.Factory.StartNew<T>(delegate ()
            {
                AsyncResult result = new AsyncResult(waitHandle, typeof(T), jsonPath);

                this.Api.SendMessage(methodName, parameters, result);
                if (waitHandle.WaitOne(this.Api.TimeoutMilliseconds))
                    return (T)result.Result;
                else
                    throw new TimeoutException();
            });
        }

        protected Task RunAsync(string methodName, Dictionary<string, object> parameters)
        {
            return RunAsync(methodName, parameters, null);
        }

        protected Task RunAsync(string methodName, Dictionary<string, object> parameters, string jsonPath)
        {
            EventWaitHandle waitHandle = new EventWaitHandle(false, EventResetMode.ManualReset);

            return Task.Factory.StartNew(delegate ()
            {
                AsyncResult result = new AsyncResult(waitHandle, jsonPath);

                this.Api.SendMessage(methodName, parameters, result);
                if (!waitHandle.WaitOne(this.Api.TimeoutMilliseconds))
                    throw new TimeoutException();
            });
        }

    }
}
