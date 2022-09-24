using System;

namespace dcPrevent.GMA.WinApi {

    internal readonly struct CallbackData {
        
        public CallbackData(IntPtr wParam, IntPtr lParam) {
            WParam = wParam;
            LParam = lParam;
        }

        public IntPtr WParam { get; }

        public IntPtr LParam { get; }
        
    }

}