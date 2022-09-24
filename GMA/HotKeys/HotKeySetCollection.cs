using System.Collections.Generic;

namespace dcPrevent.GMA.HotKeys {

    public sealed class HotKeySetCollection : List<HotKeySet> {

        private KeyChainHandler keyChain;

        public new void Add(HotKeySet hks) {
            keyChain += hks.OnKey;
            base.Add(hks);
        }

        public new void Remove(HotKeySet hks) {
            keyChain -= hks.OnKey;
            base.Remove(hks);
        }

        internal void OnKey(KeyEventArgsExt e) {
            keyChain?.Invoke(e);
        }

        private delegate void KeyChainHandler(KeyEventArgsExt kex);

    }

}