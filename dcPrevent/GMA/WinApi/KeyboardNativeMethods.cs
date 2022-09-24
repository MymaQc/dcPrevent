using dcPrevent.GMA.Implementation;
using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace dcPrevent.GMA.WinApi {

    internal static class KeyboardNativeMethods {

        public const byte Shift = 16;
        public const byte Capital = 20;
        public const byte Numlock = 144;
        public const byte Lshift = 160;
        public const byte Rshift = 161;
        public const byte Lcontrol = 162;
        public const byte Rcontrol = 163;
        public const byte Lmenu = 164;
        public const byte Rmenu = 165;
        public const byte Lwin = 91;
        public const byte Rwin = 92;
        public const byte Scroll = 145;
        public const byte Insert = 45;
        public const byte Control = 17;
        public const byte Menu = 18;
        public const byte Packet = 231;
        private static int _lastVirtualKeyCode;
        private static int _lastScanCode;
        private static byte[] _lastKeyState = new byte[byte.MaxValue];
        private static bool _lastIsDead;

        internal static void TryGetCharFromKeyboardState(int virtualKeyCode, int fuState, out char[] chars) {
            IntPtr activeKeyboard = GetActiveKeyboard();
            int scanCode = MapVirtualKeyEx(virtualKeyCode, 0, activeKeyboard);
            TryGetCharFromKeyboardState(virtualKeyCode, scanCode, fuState, activeKeyboard, out chars);
        }

        internal static void TryGetCharFromKeyboardState(int virtualKeyCode, int scanCode, int fuState, out char[] chars) {
            IntPtr activeKeyboard = GetActiveKeyboard();
            TryGetCharFromKeyboardState(virtualKeyCode, scanCode, fuState, activeKeyboard, out chars);
        }

        private static void TryGetCharFromKeyboardState(int virtualKeyCode, int scanCode, int fuState, IntPtr dwhkl, out char[] chars) {
            StringBuilder pwszBuff1 = new StringBuilder(64);
            KeyboardState current = KeyboardState.GetCurrent();
            byte[] nativeState = current.GetNativeState();
            bool flag = false;
            if (current.IsDown(Keys.ShiftKey)) {
                nativeState[16] = 128;
            }
            if (current.IsToggled(Keys.Capital)) {
                nativeState[20] = 1;
            }
            switch (ToUnicodeEx(virtualKeyCode, scanCode, nativeState, pwszBuff1, pwszBuff1.Capacity, fuState, dwhkl)) {
                case -1:
                    flag = true;
                    ClearKeyboardBuffer(virtualKeyCode, scanCode, dwhkl);
                    chars = null;
                    break;
                case 0:
                    chars = null;
                    break;
                case 1:
                    if (pwszBuff1.Length > 0) {
                        chars = new[] { pwszBuff1[0] };
                        break;
                    }
                    chars = null;
                    break;
                default:
                    if (pwszBuff1.Length > 1) {
                        chars = new[] { pwszBuff1[0], pwszBuff1[1] };
                        break;
                    }
                    chars = new[] { pwszBuff1[0] };
                    break;
            }
            if (_lastVirtualKeyCode != 0 && _lastIsDead) {
                if (chars == null) { 
                    return;
                }
                StringBuilder pwszBuff2 = new StringBuilder(5);
                ToUnicodeEx(_lastVirtualKeyCode, _lastScanCode, _lastKeyState, pwszBuff2, pwszBuff2.Capacity, 0, dwhkl);
                _lastIsDead = false;
                _lastVirtualKeyCode = 0;
            } else {
                _lastScanCode = scanCode;
                _lastVirtualKeyCode = virtualKeyCode;
                _lastIsDead = flag;
                _lastKeyState = (byte[])nativeState.Clone();
            }
        }

        private static void ClearKeyboardBuffer(int vk, int sc, IntPtr hkl) {
            StringBuilder pwszBuff = new StringBuilder(10);
            byte[] lpKeyState;
            do {
                lpKeyState = new byte[byte.MaxValue];
            } while (ToUnicodeEx(vk, sc, lpKeyState, pwszBuff, pwszBuff.Capacity, 0, hkl) < 0);
        }

        private static IntPtr GetActiveKeyboard() => GetKeyboardLayout(ThreadNativeMethods.GetWindowThreadProcessId(ThreadNativeMethods.GetForegroundWindow(), out int _));

        [Obsolete("Use ToUnicodeEx instead")]
        [DllImport("user32.dll")]
        public static extern int ToAscii(int uVirtKey, int uScanCode, byte[] lpbKeyState, byte[] lpwTransKey, int fuState);

        [DllImport("user32.dll")]
        private static extern int ToUnicodeEx(int wVirtKey, int wScanCode, byte[] lpKeyState, [MarshalAs(UnmanagedType.LPWStr), Out] StringBuilder pwszBuff, int cchBuff, int wFlags, IntPtr dwhkl);

        [DllImport("user32.dll")]
        public static extern int GetKeyboardState(byte[] pbKeyState);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern short GetKeyState(int vKey);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int MapVirtualKeyEx(int uCode, int uMapType, IntPtr dwhkl);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr GetKeyboardLayout(int dwLayout);

        internal enum MapType {
            MapvkVkToVsc,
            MapvkVscToVk,
            MapvkVkToChar,
            MapvkVscToVkEx,
        }

    }

}