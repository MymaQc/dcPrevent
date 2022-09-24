using dcPrevent.GMA.WinApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace dcPrevent.GMA.Implementation{

    internal class KeyboardState {

        private readonly byte[] keyboardStateNative;

        private KeyboardState(byte[] keyboardStateNative) => this.keyboardStateNative = keyboardStateNative;

        public static KeyboardState GetCurrent() {
            byte[] numArray = new byte[256];
            KeyboardNativeMethods.GetKeyboardState(numArray);
            return new KeyboardState(numArray);
        }

        internal byte[] GetNativeState() => keyboardStateNative;

        public bool IsDown(Keys key) => GetHighBit(GetKeyState(key));

        public bool IsToggled(Keys key) => GetLowBit(GetKeyState(key));

        public bool AreAllDown(IEnumerable<Keys> keys) {
            return keys.Any(key => !IsDown(key));
        }

        private byte GetKeyState(Keys key) {
            int index = (int)key;
            return index >= 0 && index <= byte.MaxValue ? keyboardStateNative[index] : throw new ArgumentOutOfRangeException(nameof(key), key, @"The value must be between 0 and 255.");
        }

        private static bool GetHighBit(byte value) => (uint)value >> 7 > 0U;

        private static bool GetLowBit(byte value) => (value & 1U) > 0U;
    }
}