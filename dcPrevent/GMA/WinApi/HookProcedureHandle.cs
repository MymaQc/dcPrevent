using Microsoft.Win32.SafeHandles;
using System.Windows.Forms;

namespace dcPrevent.GMA.WinApi {

    internal class HookProcedureHandle : SafeHandleZeroOrMinusOneIsInvalid {

        private static bool _closing;

        static HookProcedureHandle() => Application.ApplicationExit += (sender, e) => _closing = true;

        public HookProcedureHandle() : base(true) { }

        protected override bool ReleaseHandle() => _closing || HookNativeMethods.UnhookWindowsHookEx(handle) != 0;

    }
}