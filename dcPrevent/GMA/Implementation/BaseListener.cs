using dcPrevent.GMA.WinApi;
using System;

namespace dcPrevent.GMA.Implementation {

    internal abstract class BaseListener : IDisposable {

        protected BaseListener(Subscribe subscribe) => Handle = subscribe(Callback);

        private HookResult Handle { get; }

        public void Dispose() => Handle.Dispose();

        protected abstract bool Callback(CallbackData data);

    }

}