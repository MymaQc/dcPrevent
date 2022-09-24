using dcPrevent.GMA.WinApi;

namespace dcPrevent.GMA.Implementation {

    internal class AppMouseListener : MouseListener {

        public AppMouseListener() : base(HookHelper.HookAppMouse) { }

        protected override MouseEventExtArgs GetEventArgs(CallbackData data) => MouseEventExtArgs.FromRawDataApp(data);

    }

}