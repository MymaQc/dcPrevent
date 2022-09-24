using dcPrevent.GMA.WinApi;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace dcPrevent.GMA {

    public class MouseEventExtArgs : MouseEventArgs {
        private MouseEventExtArgs(MouseButtons buttons, int clicks, Point point, int delta, int timestamp, bool isMouseButtonDown, bool isMouseButtonUp) : base(buttons, clicks, point.X, point.Y, delta) {
            IsMouseButtonDown = isMouseButtonDown;
            IsMouseButtonUp = isMouseButtonUp;
            Timestamp = timestamp;
        }

        public bool Handled { get; set; }

        public bool WheelScrolled => Delta != 0;

        public bool IsMouseButtonDown { get; }

        public bool IsMouseButtonUp { get; }

        public int Timestamp { get; }

        internal Point Point => new Point(X, Y);

        internal static MouseEventExtArgs FromRawDataApp(CallbackData data) => FromRawDataUniversal(data.WParam, ((AppMouseStruct)Marshal.PtrToStructure(data.LParam, typeof(AppMouseStruct))).ToMouseStruct());

        internal static MouseEventExtArgs FromRawDataGlobal(CallbackData data) => FromRawDataUniversal(data.WParam, (MouseStruct)Marshal.PtrToStructure(data.LParam, typeof(MouseStruct)));

        private static MouseEventExtArgs FromRawDataUniversal(IntPtr wParam, MouseStruct mouseInfo) {
            MouseButtons buttons = MouseButtons.None;
            short delta = 0;
            int clicks = 0;
            bool isMouseButtonDown = false;
            bool isMouseButtonUp = false;
            long num = (long)wParam - 513L;
            if ((ulong)num > 13UL){                
                return new MouseEventExtArgs(buttons, clicks, mouseInfo.Point, delta, mouseInfo.Timestamp, false, false);
            }
            switch ((uint)num) {
                case 0:
                    isMouseButtonDown = true;
                    buttons = MouseButtons.Left;
                    clicks = 1;
                    break;
                case 1:
                    isMouseButtonUp = true;
                    buttons = MouseButtons.Left;
                    clicks = 1;
                    break;
                case 2:
                    isMouseButtonDown = true;
                    buttons = MouseButtons.Left;
                    clicks = 2;
                    break;
                case 3:
                    isMouseButtonDown = true;
                    buttons = MouseButtons.Right;
                    clicks = 1;
                    break;
                case 4:
                    isMouseButtonUp = true;
                    buttons = MouseButtons.Right;
                    clicks = 1;
                    break;
                case 5:
                    isMouseButtonDown = true;
                    buttons = MouseButtons.Right;
                    clicks = 2;
                    break;
                case 6:
                    isMouseButtonDown = true;
                    buttons = MouseButtons.Middle;
                    clicks = 1;
                    break;
                case 7:
                    isMouseButtonUp = true;
                    buttons = MouseButtons.Middle;
                    clicks = 1;
                    break;
                case 8:
                    isMouseButtonDown = true;
                    buttons = MouseButtons.Middle;
                    clicks = 2;
                    break;
                case 9:
                    delta = mouseInfo.MouseData;
                    break;
                case 10:
                    buttons = mouseInfo.MouseData == 1 ? MouseButtons.XButton1 : MouseButtons.XButton2;
                    isMouseButtonDown = true;
                    clicks = 1;
                    break;
                case 11:
                    buttons = mouseInfo.MouseData == 1 ? MouseButtons.XButton1 : MouseButtons.XButton2;
                    isMouseButtonUp = true;
                    clicks = 1;
                    break;
                case 12:
                    isMouseButtonDown = true;
                    buttons = mouseInfo.MouseData == 1 ? MouseButtons.XButton1 : MouseButtons.XButton2;
                    clicks = 2;
                    break;
                case 13:
                    delta = mouseInfo.MouseData;
                    break;
            }
            return new MouseEventExtArgs(buttons, clicks, mouseInfo.Point, delta, mouseInfo.Timestamp, isMouseButtonDown, isMouseButtonUp);
        }

        internal MouseEventExtArgs ToDoubleClickEventArgs() => new MouseEventExtArgs(Button, 2, Point, Delta, Timestamp, IsMouseButtonDown, IsMouseButtonUp);

    }

}