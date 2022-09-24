using System;

namespace dcPrevent.GMA.WinApi {

    internal class HookResult : IDisposable {
        
        public HookResult(HookProcedureHandle handle) {
            Handle = handle;
        }

        private HookProcedureHandle Handle { get; }

        public void Dispose() => Handle.Dispose();

    }

}