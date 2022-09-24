using System;
using System.Runtime.InteropServices;

namespace dcPrevent.GMA.WinApi {

    internal static class ThreadNativeMethods {

        [DllImport("kernel32")]
        internal static extern int GetCurrentThreadId();

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern int GetWindowThreadProcessId(IntPtr handle, out int processId);

    }

}