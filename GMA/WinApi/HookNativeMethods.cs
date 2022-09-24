using System;
using System.Runtime.InteropServices;

namespace dcPrevent.GMA.WinApi {

    internal static class HookNativeMethods {

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr CallNextHookEx(IntPtr idHook, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        internal static extern HookProcedureHandle SetWindowsHookEx(int idHook, HookProcedure lpfn, IntPtr hMod, int dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        internal static extern int UnhookWindowsHookEx(IntPtr idHook);

    }

}