using System.Runtime.InteropServices;
using System.Text;
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
        private static extern int viewSetHardKey(int a1, int a2, string FileName);

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

        public static void DoSample()
        {
            var init = new ViewPluginInit
            {
                Width = 640,
                Height = 480,
                Scale = 2,
                WindowTitle = Marshal.StringToHGlobalAnsi("MyPlugin"),
                ConfigFilePath = Marshal.StringToHGlobalAnsi("config.ini"),
                HwndParent = hwndParent,
                HwndRender = hwndRender,
                RenderCallback = MyRenderCallback
            };

            bool success = NativeMethods.viewInitPlugin(ref init);

            throw new System.Invalid();
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct ViewPluginInit
    {
        public int Width;
        public int Height;
        public int Scale;
        public IntPtr WindowTitle;
        public IntPtr ConfigFilePath;
        public IntPtr HwndParent;
        public IntPtr HwndRender;
        public RenderCallback RenderCallback;
    }

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate int RenderCallback(uint param1, uint param2);
}