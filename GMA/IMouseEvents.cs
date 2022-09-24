using System;
using System.Windows.Forms;

namespace dcPrevent.GMA {

    public interface IMouseEvents {

        event MouseEventHandler MouseMove;

        event EventHandler<MouseEventExtArgs> MouseMoveExt;

        event MouseEventHandler MouseClick;

        event MouseEventHandler MouseDown;

        event EventHandler<MouseEventExtArgs> MouseDownExt;

        event MouseEventHandler MouseUp;

        event EventHandler<MouseEventExtArgs> MouseUpExt;

        event MouseEventHandler MouseWheel;

        event EventHandler<MouseEventExtArgs> MouseWheelExt;

        event MouseEventHandler MouseDoubleClick;

        event MouseEventHandler MouseDragStarted;

        event EventHandler<MouseEventExtArgs> MouseDragStartedExt;

        event MouseEventHandler MouseDragFinished;

        event EventHandler<MouseEventExtArgs> MouseDragFinishedExt;

    }

}