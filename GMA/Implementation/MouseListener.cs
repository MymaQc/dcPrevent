using dcPrevent.GMA.WinApi;
using System;
using System.Windows.Forms;

namespace dcPrevent.GMA.Implementation {

    internal abstract class MouseListener : BaseListener, IMouseEvents {

        private readonly int xDragThreshold;
        private readonly int yDragThreshold;
        private readonly ButtonSet doubleDown;
        private readonly ButtonSet singleDown;
        private bool isDragging;
        private Point previousPosition;
        private Point dragStartPosition;
        private readonly Point uninitialisedPoint = new Point(-1, -1);

        protected MouseListener(Subscribe subscribe) : base(subscribe) {
            xDragThreshold = NativeMethods.GetXDragThreshold();
            yDragThreshold = NativeMethods.GetYDragThreshold();
            isDragging = false;
            previousPosition = uninitialisedPoint;
            dragStartPosition = uninitialisedPoint;
            doubleDown = new ButtonSet();
            singleDown = new ButtonSet();
        }

        protected override bool Callback(CallbackData data) {
            MouseEventExtArgs eventArgs = GetEventArgs(data);
            if (eventArgs.IsMouseButtonDown) {
                ProcessDown(ref eventArgs);
            }
            if (eventArgs.IsMouseButtonUp) {
                ProcessUp(ref eventArgs);
            }
            if (eventArgs.WheelScrolled) {
                ProcessWheel(ref eventArgs);
            }
            if (HasMoved(eventArgs.Point)) {
                ProcessMove(ref eventArgs);
            }
            ProcessDrag(ref eventArgs);
            return !eventArgs.Handled;
        }

        protected abstract MouseEventExtArgs GetEventArgs(CallbackData data);

        private void ProcessWheel(ref MouseEventExtArgs e) {
            OnWheel(e);
            OnWheelExt(e);
        }

        protected virtual void ProcessDown(ref MouseEventExtArgs e) {
            OnDown(e);
            OnDownExt(e);
            if (e.Handled) { 
                return;
            }
            if (e.Clicks == 2) {
                doubleDown.Add(e.Button);
            }
            if (e.Clicks != 1) {
                singleDown.Add(e.Button);
            }
        }

        protected virtual void ProcessUp(ref MouseEventExtArgs e) {
            if (singleDown.Contains(e.Button)) {
                OnUp(e);
                OnUpExt(e);
                if (e.Handled) {
                    return;
                }
                OnClick(e);
                singleDown.Remove(e.Button);
            }
            if (!doubleDown.Contains(e.Button)) {
                return;
            }
            e = e.ToDoubleClickEventArgs();
            OnUp(e);
            OnUpExt(e);
            OnDoubleClick(e);
            doubleDown.Remove(e.Button);
        }

        private void ProcessMove(ref MouseEventExtArgs e) {
            previousPosition = e.Point;
            OnMove(e);
            OnMoveExt(e);
        }

        private void ProcessDrag(ref MouseEventExtArgs e) {
            if (singleDown.Contains(MouseButtons.Left)) {
                if (dragStartPosition.Equals(uninitialisedPoint)) {
                    dragStartPosition = e.Point;
                }
                ProcessDragStarted(ref e);
            } else {
                dragStartPosition = uninitialisedPoint;
                ProcessDragFinished(ref e);
            }
        }

        private void ProcessDragStarted(ref MouseEventExtArgs e) {
            if (isDragging) { 
                return; 
            }
            isDragging = Math.Abs(e.Point.X - dragStartPosition.X) > xDragThreshold | Math.Abs(e.Point.Y - dragStartPosition.Y) > yDragThreshold;
            if (!isDragging) {
                return;
            }
            OnDragStarted(e);
            OnDragStartedExt(e);
        }

        private void ProcessDragFinished(ref MouseEventExtArgs e) {
            if (!isDragging) {
                return;
            }
            OnDragFinished(e);
            OnDragFinishedExt(e);
            isDragging = false;
        }

        private bool HasMoved(Point actualPoint) => previousPosition != actualPoint;

        public event MouseEventHandler MouseMove;

        public event EventHandler<MouseEventExtArgs> MouseMoveExt;

        public event MouseEventHandler MouseClick;

        public event MouseEventHandler MouseDown;

        public event EventHandler<MouseEventExtArgs> MouseDownExt;

        public event MouseEventHandler MouseUp;

        public event EventHandler<MouseEventExtArgs> MouseUpExt;

        public event MouseEventHandler MouseWheel;

        public event EventHandler<MouseEventExtArgs> MouseWheelExt;

        public event MouseEventHandler MouseDoubleClick;

        public event MouseEventHandler MouseDragStarted;

        public event EventHandler<MouseEventExtArgs> MouseDragStartedExt;

        public event MouseEventHandler MouseDragFinished;

        public event EventHandler<MouseEventExtArgs> MouseDragFinishedExt;

        private void OnMove(MouseEventArgs e) {
            MouseEventHandler mouseMove = MouseMove;
            mouseMove?.Invoke(this, e);
        }

        private void OnMoveExt(MouseEventExtArgs e) {
            EventHandler<MouseEventExtArgs> mouseMoveExt = MouseMoveExt;
            mouseMoveExt?.Invoke(this, e);
        }

        private void OnClick(MouseEventArgs e) {
            MouseEventHandler mouseClick = MouseClick;
            mouseClick?.Invoke(this, e);
        }

        private void OnDown(MouseEventArgs e) {
            MouseEventHandler mouseDown = MouseDown;
            mouseDown?.Invoke(this, e);
        }

        private void OnDownExt(MouseEventExtArgs e) {
            EventHandler<MouseEventExtArgs> mouseDownExt = MouseDownExt;
            mouseDownExt?.Invoke(this, e);
        }

        private void OnUp(MouseEventArgs e) {
            MouseEventHandler mouseUp = MouseUp;
            mouseUp?.Invoke(this, e);
        }

        private void OnUpExt(MouseEventExtArgs e) {
            EventHandler<MouseEventExtArgs> mouseUpExt = MouseUpExt;
            mouseUpExt?.Invoke(this, e);
        }

        private void OnWheel(MouseEventArgs e) {
            MouseEventHandler mouseWheel = MouseWheel;
            mouseWheel?.Invoke(this, e);
        }

        private void OnWheelExt(MouseEventExtArgs e) {
            EventHandler<MouseEventExtArgs> mouseWheelExt = MouseWheelExt;
            mouseWheelExt?.Invoke(this, e);
        }

        private void OnDoubleClick(MouseEventArgs e) {
            MouseEventHandler mouseDoubleClick = MouseDoubleClick;
            mouseDoubleClick?.Invoke(this, e);
        }

        private void OnDragStarted(MouseEventArgs e) {
            MouseEventHandler mouseDragStarted = MouseDragStarted;
            mouseDragStarted?.Invoke(this, e);
        }

        private void OnDragStartedExt(MouseEventExtArgs e) {
            EventHandler<MouseEventExtArgs> mouseDragStartedExt = MouseDragStartedExt;
            mouseDragStartedExt?.Invoke(this, e);
        }

        private void OnDragFinished(MouseEventArgs e) {
            MouseEventHandler mouseDragFinished = MouseDragFinished;
            mouseDragFinished?.Invoke(this, e);
        }

        private void OnDragFinishedExt(MouseEventExtArgs e) {
            EventHandler<MouseEventExtArgs> mouseDragFinishedExt = MouseDragFinishedExt;
            mouseDragFinishedExt?.Invoke(this, e);
        }

    }

}