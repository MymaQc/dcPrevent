using System.Runtime.InteropServices;

namespace dcPrevent.GMA.Implementation {

    internal static class NativeMethods {

        private const int WM_SM_CXDRAG = 68;
        private const int WM_SM_CYDRAG = 69;

        [DllImport("user32.dll")]
        private static extern int GetSystemMetrics(int index);

        public static int GetXDragThreshold() => GetSystemMetrics(68);

        public static int GetYDragThreshold() => GetSystemMetrics(69);

    }

}