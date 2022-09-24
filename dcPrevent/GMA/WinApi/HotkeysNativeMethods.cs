using System;
using System.Runtime.InteropServices;

namespace dcPrevent.GMA.WinApi {

    internal static class HotkeysNativeMethods {

        [DllImport("user32.dll")]
        public static extern int RegisterHotKey(IntPtr hwnd, int id, int fsModifiers, int vk);

        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hwnd, int id);

    }

}