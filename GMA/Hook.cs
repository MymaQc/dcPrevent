using dcPrevent.GMA.Implementation;

namespace dcPrevent.GMA {

    public static class Hook {

        public static IKeyboardMouseEvents AppEvents() => new AppEventFacade();

        public static IKeyboardMouseEvents GlobalEvents() => new GlobalEventFacade();

    }

}