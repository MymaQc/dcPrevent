using dcPrevent.GMA.WinApi;
using System.Windows.Forms;

namespace dcPrevent.GMA.Implementation {

    internal class GlobalMouseListener : MouseListener {

        private readonly int systemDoubleClickTime;
        private MouseButtons previousClicked;
        private Point previousClickedPosition;
        private int previousClickedTime;

        public GlobalMouseListener() : base(HookHelper.HookGlobalMouse) {
            systemDoubleClickTime = MouseNativeMethods.GetDoubleClickTime();
        }

        protected override void ProcessDown(ref MouseEventExtArgs e) {
            if (IsDoubleClick(e)) {
                e = e.ToDoubleClickEventArgs();
            }
            base.ProcessDown(ref e);
        }

        protected override void ProcessUp(ref MouseEventExtArgs e) {
            base.ProcessUp(ref e);
            if (e.Clicks == 2) {
                StopDoubleClickWaiting();
            }
            if (e.Clicks != 1) {
                return;
            }
            StartDoubleClickWaiting(e);
        }

        private void StartDoubleClickWaiting(MouseEventExtArgs e) {
            previousClicked = e.Button;
            previousClickedTime = e.Timestamp;
            previousClickedPosition = e.Point;
        }

        private void StopDoubleClickWaiting() {
            previousClicked = MouseButtons.None;
            previousClickedTime = 0;
            previousClickedPosition = new Point(0, 0);
        }

        private bool IsDoubleClick(MouseEventExtArgs e) => e.Button == previousClicked && e.Point == previousClickedPosition && e.Timestamp - previousClickedTime <= systemDoubleClickTime;

        protected override MouseEventExtArgs GetEventArgs(CallbackData data) => MouseEventExtArgs.FromRawDataGlobal(data);

    }

}