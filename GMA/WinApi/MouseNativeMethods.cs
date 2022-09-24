using System.Runtime.InteropServices;

namespace dcPrevent.GMA.WinApi {

    internal static class MouseNativeMethods {

        [DllImport("user32")]
        internal static extern int GetDoubleClickTime();

    }

}
