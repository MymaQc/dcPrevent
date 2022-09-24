using dcPrevent.GMA.WinApi;
using System.Collections.Generic;

namespace dcPrevent.GMA.Implementation {

    internal class GlobalKeyListener : KeyListener {

        public GlobalKeyListener() : base(HookHelper.HookGlobalKeyboard) { }

        protected override IEnumerable<KeyPressEventArgsExt> GetPressEventArgs(CallbackData data) {
            return KeyPressEventArgsExt.FromRawDataGlobal(data);
        }

        protected override KeyEventArgsExt GetDownUpEventArgs(CallbackData data) => KeyEventArgsExt.FromRawDataGlobal(data);

    }

}