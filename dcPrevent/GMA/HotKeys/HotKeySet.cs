using dcPrevent.GMA.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace dcPrevent.GMA.HotKeys {

    public class HotKeySet {
        
        private readonly Dictionary<Keys, bool> hotKeyState;
        private readonly Dictionary<Keys, Keys> remapping;
        private int hotKeyDownCount;
        private int remappingCount;

        public HotKeySet(IEnumerable<Keys> hotkeys) {
            hotKeyState = new Dictionary<Keys, bool>();
            remapping = new Dictionary<Keys, Keys>();
            HotKeys = hotkeys;
            InitializeKeys();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        private IEnumerable<Keys> HotKeys { get; }

        private bool HotKeysActivated => hotKeyDownCount == hotKeyState.Count - remappingCount;

        private static bool Enabled => true;

        public event HotKeyHandler OnHotKeysDownHold;

        public event HotKeyHandler OnHotKeysUp;

        public event HotKeyHandler OnHotKeysDownOnce;

        private void InvokeHotKeyHandler(HotKeyHandler hotKeyDelegate) {
            hotKeyDelegate?.Invoke(this, new HotKeyArgs(DateTime.Now));
        }

        private void InitializeKeys() {
            foreach (Keys hotKey in HotKeys) {
                if (hotKeyState.ContainsKey(hotKey)) {
                    hotKeyState.Add(hotKey, false);
                }
                hotKeyState[hotKey] = KeyboardState.GetCurrent().IsDown(hotKey);
            }
        }

        public bool UnregisterExclusiveOrKey(Keys anyKeyInTheExclusiveOrSet) {
            Keys exclusiveOrPrimaryKey = GetExclusiveOrPrimaryKey(anyKeyInTheExclusiveOrSet);
            if (exclusiveOrPrimaryKey == Keys.None || !remapping.ContainsValue(exclusiveOrPrimaryKey)) {
                return false;
            }
            List<Keys> keysList = (from keyValuePair in remapping where keyValuePair.Value == exclusiveOrPrimaryKey select keyValuePair.Key).ToList();
            foreach (Keys key in keysList) {
                remapping.Remove(key);
            }
            --remappingCount;
            return true;
        }

        public Keys RegisterExclusiveOrKey(IEnumerable<Keys> orKeySet) {
            IEnumerable<Keys> keysEnumerable = orKeySet as Keys[] ?? orKeySet.ToArray();
            if (keysEnumerable.Any(orKey => !hotKeyState.ContainsKey(orKey)))  {
                return Keys.None;
            }
            int num = 0;
            Keys keys = Keys.None;
            foreach (Keys orKey in keysEnumerable) {
                if (num == 0) {
                    keys = orKey;
                }
                remapping[orKey] = keys;
                ++num;
            }
            ++remappingCount;
            return keys;
        }

        private Keys GetExclusiveOrPrimaryKey(Keys k) => remapping.ContainsKey(k) ? remapping[k] : Keys.None;

        private Keys GetPrimaryKey(Keys k) => remapping.ContainsKey(k) ? remapping[k] : k;

        internal void OnKey(KeyEventArgsExt kex) {
            if (!Enabled) { 
                return;
            }
            Keys primaryKey = GetPrimaryKey(kex.KeyCode);
            if (kex.IsKeyDown) {
                OnKeyDown(primaryKey);
            } else {
                OnKeyUp(primaryKey);
            }
        }

        private void OnKeyDown(Keys k) {
            if (HotKeysActivated) {
                InvokeHotKeyHandler(OnHotKeysDownHold);
            } else {
                if (!hotKeyState.ContainsKey(k) || hotKeyState[k]) {
                    return;
                }
                hotKeyState[k] = true;
                ++hotKeyDownCount;
                if (HotKeysActivated) {
                    InvokeHotKeyHandler(OnHotKeysDownOnce);
                }
            }
        }

        private void OnKeyUp(Keys k) {
            if (!hotKeyState.ContainsKey(k) || !hotKeyState[k]) { 
                return; 
            }
            bool hotKeysActivated = HotKeysActivated;
            hotKeyState[k] = false;
            --hotKeyDownCount;
            if (hotKeysActivated) {
                InvokeHotKeyHandler(OnHotKeysUp);
            }
        }

        public delegate void HotKeyHandler(object sender, HotKeyArgs e);

    }

}