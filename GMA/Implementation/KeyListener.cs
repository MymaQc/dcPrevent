using dcPrevent.GMA.WinApi;
using System.Collections.Generic;
using System.Windows.Forms;

namespace dcPrevent.GMA.Implementation {

    internal abstract class KeyListener : BaseListener, IKeyboardEvents {

        protected KeyListener(Subscribe subscribe) : base(subscribe) { }

        public event KeyEventHandler KeyDown;

        public event KeyPressEventHandler KeyPress;

        public event KeyEventHandler KeyUp;

        private void InvokeKeyDown(KeyEventArgsExt e) {
            KeyEventHandler keyDown = KeyDown;
            if (keyDown == null || e.Handled || !e.IsKeyDown) { 
                return; 
            }
            keyDown(this, e);
        }

        private void InvokeKeyPress(KeyPressEventArgsExt e) {
            KeyPressEventHandler keyPress = KeyPress;
            if (keyPress == null || e.Handled || e.IsNonChar) { 
                return; 
            }
            keyPress(this, e);
        }

        private void InvokeKeyUp(KeyEventArgsExt e) {
            KeyEventHandler keyUp = KeyUp;
            if (keyUp == null || e.Handled || !e.IsKeyUp) { 
                return;
            }
            keyUp(this, e);
        }

        protected override bool Callback(CallbackData data) {
            KeyEventArgsExt downUpEventArgs = GetDownUpEventArgs(data);
            IEnumerable<KeyPressEventArgsExt> pressEventArgs = GetPressEventArgs(data);
            InvokeKeyDown(downUpEventArgs);
            foreach (KeyPressEventArgsExt e in pressEventArgs) {
                InvokeKeyPress(e);
            }
            InvokeKeyUp(downUpEventArgs);
            return !downUpEventArgs.Handled;
        }

        protected abstract IEnumerable<KeyPressEventArgsExt> GetPressEventArgs(CallbackData data);

        protected abstract KeyEventArgsExt GetDownUpEventArgs(CallbackData data);

    }

}