using dcPrevent.GMA.WinApi;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace dcPrevent.GMA {

    public class KeyPressEventArgsExt : KeyPressEventArgs {
        
        private KeyPressEventArgsExt(char keyChar, int timestamp) : base(keyChar) {
            IsNonChar = keyChar == char.MinValue;
        }

        private KeyPressEventArgsExt(char keyChar) : this(keyChar, Environment.TickCount) { }

        public bool IsNonChar { get; }

        internal static IEnumerable<KeyPressEventArgsExt> FromRawDataApp(CallbackData data) {
            IntPtr wParam = data.WParam;
            IntPtr lParam = data.LParam;
            uint flags = (uint)lParam.ToInt64();
            bool wasKeyDown = (flags & 1073741824U) > 0U;
            bool isKeyReleased = (flags & 2147483648U) > 0U;
            if (!wasKeyDown && !isKeyReleased) yield break;
            int virtualKeyCode = (int)wParam;
            int scanCode = checked((int)(uint)(unchecked((int)flags) & 16711680));
            KeyboardNativeMethods.TryGetCharFromKeyboardState(virtualKeyCode, scanCode, 0, out char[] chars);
            if (chars == null) yield break;
            foreach (var ch in chars) {
                yield return new KeyPressEventArgsExt(ch);
            }
        }

        internal static IEnumerable<KeyPressEventArgsExt> FromRawDataGlobal(CallbackData data) {
            IntPtr wParam = data.WParam;
            IntPtr lParam = data.LParam;
            if ((int)wParam != 256 && (int)wParam != 260) yield break;
            KeyboardHookStruct keyboardHookStruct = (KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyboardHookStruct));
            int virtualKeyCode = keyboardHookStruct.VirtualKeyCode;
            int scanCode = keyboardHookStruct.ScanCode;
            int fuState = keyboardHookStruct.Flags;
            if (virtualKeyCode == 231) {
                char ch = (char)scanCode;
                yield return new KeyPressEventArgsExt(ch, keyboardHookStruct.Time);
            } else {
                KeyboardNativeMethods.TryGetCharFromKeyboardState(virtualKeyCode, scanCode, fuState, out char[] chars);
                if (chars == null) yield break;
                foreach (var current in chars)  {
                    yield return new KeyPressEventArgsExt(current, keyboardHookStruct.Time);
                }
            }
        }

    }

}