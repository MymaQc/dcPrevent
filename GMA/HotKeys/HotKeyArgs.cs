using System;

namespace dcPrevent.GMA.HotKeys {

    public sealed class HotKeyArgs : EventArgs {

        private readonly DateTime dateTime;

        public HotKeyArgs(DateTime triggeredAt) => dateTime = triggeredAt;

        public DateTime Time => dateTime;

    }

}