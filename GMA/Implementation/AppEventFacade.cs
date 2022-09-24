namespace dcPrevent.GMA.Implementation {

    internal class AppEventFacade : EventFacade {

        protected override MouseListener CreateMouseListener() => new AppMouseListener();

        protected override KeyListener CreateKeyListener() => new AppKeyListener();

    }

}