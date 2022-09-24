namespace dcPrevent.GMA.Implementation {

    internal class GlobalEventFacade : EventFacade {

        protected override MouseListener CreateMouseListener() => new GlobalMouseListener();

        protected override KeyListener CreateKeyListener() => new GlobalKeyListener();

    }

}