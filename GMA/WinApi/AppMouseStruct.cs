using System;
using System.Runtime.InteropServices;

namespace dcPrevent.GMA.WinApi {

    [StructLayout(LayoutKind.Explicit)]
    internal readonly struct AppMouseStruct {

        [FieldOffset(0)] 
        private readonly Point Point;
        [FieldOffset(22)] 
        private readonly short MouseData_x86;
        [FieldOffset(34)] 
        private readonly short MouseData_x64;

        public MouseStruct ToMouseStruct() => new MouseStruct() {
            Point = Point,
            MouseData = IntPtr.Size == 4 ? MouseData_x86 : MouseData_x64,
            Timestamp = Environment.TickCount
        };

    }

}