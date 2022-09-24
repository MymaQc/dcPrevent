﻿using dcPrevent.GMA.WinApi;
using System.Collections.Generic;

namespace dcPrevent.GMA.Implementation {

    internal class AppKeyListener : KeyListener {

        public AppKeyListener() : base(HookHelper.HookAppKeyboard) { }

        protected override IEnumerable<KeyPressEventArgsExt> GetPressEventArgs(CallbackData data) {
            return KeyPressEventArgsExt.FromRawDataApp(data);
        }

        protected override KeyEventArgsExt GetDownUpEventArgs(CallbackData data) => KeyEventArgsExt.FromRawDataApp(data);

    }

}