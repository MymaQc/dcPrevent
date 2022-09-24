namespace dcPrevent.GMA.WinApi {

    internal struct KeyboardHookStruct {

        public readonly int VirtualKeyCode;
        public readonly int ScanCode;
        public readonly int Flags;
        public readonly int Time;
        private int extraInfo;

        public KeyboardHookStruct(int virtualKeyCode, int scanCode, int flags, int time, int extraInfo)  {
            VirtualKeyCode = virtualKeyCode;
            ScanCode = scanCode;
            Flags = flags;
            Time = time;
            this.extraInfo = extraInfo;
        }
        
    }

}