using System;
using System.Runtime.InteropServices;
using System.Text;
using SimLoad.Core;
using static SimLoad.Core.Defaults;

namespace SimLoad.Imports
{
    public static class PlugView
    {
        [DllImport("plugview", CallingConvention = Cc, CharSet = A)]
        private static extern IntPtr viewClearDisp();

        [DllImport("plugview", CallingConvention = Cc, CharSet = A)]
        private static extern IntPtr viewClosePlugin();

        [DllImport("plugview", CallingConvention = Cc, CharSet = A)]
        private static extern int viewDispON(byte a1);

        [DllImport("plugview", CallingConvention = Cc, CharSet = A)]
        private static extern int viewGetMouseStatus(out uint a1, out int a2, out int a3);

        [DllImport("plugview", CallingConvention = Cc, CharSet = A)]
        private static extern int viewGetPluginInformation([Out] StringBuilder destination, uint bufferSize);

        [DllImport("plugview", CallingConvention = Cc, CharSet = A)]
        private static extern void viewGetPluginName([Out] StringBuilder nameBuffer);

        [DllImport("plugview", CallingConvention = Cc, CharSet = A)]
        private static extern IntPtr viewGetPluginWindow();

        [DllImport("plugview", CallingConvention = Cc, CharSet = A)]
        private static extern byte viewGetVRAMArea(out uint a1, out uint a2);

        [DllImport("plugview", CallingConvention = Cc, CharSet = A)]
        private static extern bool viewInitPlugin(ref ViewPluginCfg init);

        [DllImport("plugview", CallingConvention = Cc, CharSet = A)]
        private static extern byte viewIsUseIO(int a1);

        [DllImport("plugview", CallingConvention = Cc, CharSet = A)]
        private static extern byte viewIsUseMem(int a1, int a2);

        [DllImport("plugview", CallingConvention = Cc, CharSet = A)]
        private static extern byte viewReWriteDisp();

        [DllImport("plugview", CallingConvention = Cc, CharSet = A)]
        private static extern short viewReadIOPortEx(ushort a1, int a2);

        [DllImport("plugview", CallingConvention = Cc, CharSet = A)]
        private static extern int viewReadMemPortEx(int a1, int a2, int a3);

        [DllImport("plugview", CallingConvention = Cc, CharSet = A)]
        private static extern short viewReadVram(int a1);

        [DllImport("plugview", CallingConvention = Cc, CharSet = A)]
        private static extern IntPtr viewRefresh();

        [DllImport("plugview", CallingConvention = Cc, CharSet = A)]
        private static extern int viewSetDisplaySize(int a1, int a2, int a3, int a4);

        [DllImport("plugview", CallingConvention = Cc, CharSet = A)]
        private static extern int viewSetHardKey(int a1, int a2, string fileName);

        [DllImport("plugview", CallingConvention = Cc, CharSet = A)]
        private static extern byte viewSetIOPortEx(int a1, int a2, int a3);

        [DllImport("plugview", CallingConvention = Cc, CharSet = A)]
        private static extern byte viewSetMemPortEx(byte a1, int a2, short a3, int a4);

        [DllImport("plugview", CallingConvention = Cc, CharSet = A)]
        private static extern byte viewWriteVram(int a1, int a2);

        [DllImport("plugview", CallingConvention = Cc, CharSet = A)]
        private static extern uint viewWriteVramEx(uint a1, byte a2);

        public static string GetPluginInformation()
        {
            const int bufferLength = 256;
            var buffer = new StringBuilder(bufferLength);
            _ = viewGetPluginInformation(buffer, bufferLength);
            return buffer.ToString();
        }

        public static string GetPluginName()
        {
            var buffer = new StringBuilder(64);
            viewGetPluginName(buffer);
            return buffer.ToString();
        }

        public static bool InitPlugin(IntPtr hWndParent, IntPtr hWndRender, RenderCallback callback)
        {
            var init = new ViewPluginCfg
            {
                Width = 300,
                Height = 300,
                Scale = 1,
                WindowTitle = Marshal.StringToHGlobalAnsi("Monitor"),
                ConfigFilePath = Marshal.StringToHGlobalAnsi("config.ini"),
                HwndParent = hWndParent,
                HwndRender = hWndRender,
                RenderCallback = callback
            };
            var success = viewInitPlugin(ref init);
            return success;
        }

        public static IntPtr ClosePlugin()
        {
            var result = viewClosePlugin();
            return result;
        }

        public static IntPtr Refresh()
        {
            var result = viewRefresh();
            return result;
        }

        public static byte ReWriteDisp()
        {
            var result = viewReWriteDisp();
            return result;
        }

        private static bool ViewFlag = false;

        public static PointInt ClearDisp()
        {
            var res1 = viewClearDisp();
            var res2 = viewDispON(ViewFlag ? (byte)1 : (byte)0);
            ViewFlag = !ViewFlag;
            return new(res1, res2);
        }

        public static IntPtr GetPluginWindow()
        {
            var result = viewGetPluginWindow();
            return result;
        }

        public static MouseStatus GetMouseStatus()
        {
            var result = viewGetMouseStatus(out var a, out var b, out var c);
            return new MouseStatus(a, b, c, result);
        }

        public static RamArea GetVRamArea()
        {
            var result = viewGetVRAMArea(out var a, out var b);
            return new RamArea(a, b, result);
        }
    }
}