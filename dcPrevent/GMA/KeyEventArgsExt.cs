using dcPrevent.GMA.WinApi;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace dcPrevent.GMA {

    public class KeyEventArgsExt : KeyEventArgs {
        
        private KeyEventArgsExt(Keys keyData) : base(keyData) { }

        private KeyEventArgsExt(Keys keyData, bool isKeyDown, bool isKeyUp) : this(keyData) {
            IsKeyDown = isKeyDown;
            IsKeyUp = isKeyUp;
        }

        public bool IsKeyDown { get; }

        public bool IsKeyUp { get; }

        internal static KeyEventArgsExt FromRawDataApp(CallbackData data) {
            IntPtr wparam = data.WParam;
            IntPtr lparam = data.LParam;
            uint int64 = (uint)lparam.ToInt64();
            bool flag1 = (int64 & 1073741824U) > 0U;
            bool flag2 = (int64 & 2147483648U) > 0U;
            Keys keyData = AppendModifierStates((Keys)(int)wparam);
            bool isKeyDown = !flag2;
            bool isKeyUp = flag1 & flag2;
            return new KeyEventArgsExt(keyData, isKeyDown, isKeyUp);
        }

        internal static KeyEventArgsExt FromRawDataGlobal(CallbackData data) {
            IntPtr wparam = data.WParam;
            KeyboardHookStruct structure = (KeyboardHookStruct)Marshal.PtrToStructure(data.LParam, typeof(KeyboardHookStruct));
            Keys keyData = AppendModifierStates((Keys)structure.VirtualKeyCode);
            int num = (int)wparam;
            bool isKeyDown = num == 256 || num == 260;
            bool isKeyUp = num == 257 || num == 261;
            return new KeyEventArgsExt(keyData, isKeyDown, isKeyUp);
        }

        private static bool CheckModifier(int vKey) => (KeyboardNativeMethods.GetKeyState(vKey) & 32768) > 0;

        private static Keys AppendModifierStates(Keys keyData) {
            bool flag1 = CheckModifier(17);
            bool flag2 = CheckModifier(16);
            bool flag3 = CheckModifier(18);
            return keyData | (flag1 ? Keys.Control : Keys.None) | (flag2 ? Keys.Shift : Keys.None) | (flag3 ? Keys.Alt : Keys.None);
        }

    }

}