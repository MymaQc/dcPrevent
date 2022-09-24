using dcPrevent.GMA.Implementation;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace dcPrevent.GMA.WinApi {

    internal static class HookHelper {

        public static HookResult HookAppMouse(Callback callback) => HookApp(7, callback);

        public static HookResult HookAppKeyboard(Callback callback) => HookApp(2, callback);

        public static HookResult HookGlobalMouse(Callback callback) => HookGlobal(14, callback);

        public static HookResult HookGlobalKeyboard(Callback callback) => HookGlobal(13, callback);

        private static HookResult HookApp(int hookId, Callback callback) {
            IntPtr hookProcedure(int code, IntPtr param, IntPtr lParam) => HookProcedure(code, param, lParam, callback);
            HookProcedureHandle handle = HookNativeMethods.SetWindowsHookEx(hookId, hookProcedure, IntPtr.Zero, ThreadNativeMethods.GetCurrentThreadId());
            if (handle.IsInvalid) {
                ThrowLastUnmanagedErrorAsException();
            }
            return new HookResult(handle);
        }

        private static HookResult HookGlobal(int hookId, Callback callback) {
            IntPtr hookProcedure(int code, IntPtr param, IntPtr lParam) => HookProcedure(code, param, lParam, callback);
            ProcessModule processModule = Process.GetCurrentProcess().MainModule;
            if (processModule == null) {
                return null;
            }
            HookProcedureHandle handle = HookNativeMethods.SetWindowsHookEx(hookId, hookProcedure, processModule.BaseAddress, 0);
            if (handle.IsInvalid) {
                ThrowLastUnmanagedErrorAsException();
            }
            return new HookResult(handle);
        }

        private static IntPtr HookProcedure(int nCode, IntPtr wParam, IntPtr lParam, Callback callback) {
            if (nCode != 0) {
                return CallNextHookEx(nCode, wParam, lParam);
            }
            CallbackData data = new CallbackData(wParam, lParam);
            return !callback(data) ? new IntPtr(-1) : CallNextHookEx(nCode, wParam, lParam);
        }

        private static IntPtr CallNextHookEx(int nCode, IntPtr wParam, IntPtr lParam) => HookNativeMethods.CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);

        private static void ThrowLastUnmanagedErrorAsException() => throw new Win32Exception(Marshal.GetLastWin32Error());

    }

}