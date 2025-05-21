using System.Runtime.InteropServices;
using System.Text;
using static SimuLoad.Core.Defaults;

namespace SimuLoad.Core
{
    public static class PlugView
    {
        [DllImport("plugview", CallingConvention = Cc, CharSet = A)]
        private static extern void viewClearDisp([Out] StringBuilder nameBuffer);

        [DllImport("plugview", CallingConvention = Cc, CharSet = A)]
        private static extern int viewClosePlugin([Out] StringBuilder destination, uint bufferSize);

        [DllImport("plugview", CallingConvention = Cc, CharSet = A)]
        private static extern int viewDispON([Out] StringBuilder destination, uint bufferSize);

        [DllImport("plugview", CallingConvention = Cc, CharSet = A)]
        private static extern int viewGetMouseStatus([Out] StringBuilder destination, uint bufferSize);

        [DllImport("plugview", CallingConvention = Cc, CharSet = A)]
        private static extern int viewGetPluginInformation([Out] StringBuilder destination, uint bufferSize);

        [DllImport("plugview", CallingConvention = Cc, CharSet = A)]
        private static extern void viewGetPluginName([Out] StringBuilder nameBuffer);

        [DllImport("plugview", CallingConvention = Cc, CharSet = A)]
        private static extern int viewGetPluginWindow([Out] StringBuilder destination, uint bufferSize);

        [DllImport("plugview", CallingConvention = Cc, CharSet = A)]
        private static extern int viewGetVRAMArea([Out] StringBuilder destination, uint bufferSize);

        [DllImport("plugview", CallingConvention = Cc, CharSet = A)]
        private static extern int viewInitPlugin([Out] StringBuilder destination, uint bufferSize);

        [DllImport("plugview", CallingConvention = Cc, CharSet = A)]
        private static extern int viewIsUseIO([Out] StringBuilder destination, uint bufferSize);

        [DllImport("plugview", CallingConvention = Cc, CharSet = A)]
        private static extern void viewIsUseMem([Out] StringBuilder nameBuffer);

        [DllImport("plugview", CallingConvention = Cc, CharSet = A)]
        private static extern void viewReWriteDisp([Out] StringBuilder nameBuffer);

        [DllImport("plugview", CallingConvention = Cc, CharSet = A)]
        private static extern void viewReadIOPortEx([Out] StringBuilder nameBuffer);

        [DllImport("plugview", CallingConvention = Cc, CharSet = A)]
        private static extern void viewReadMemPortEx([Out] StringBuilder nameBuffer);

        [DllImport("plugview", CallingConvention = Cc, CharSet = A)]
        private static extern void viewReadVram([Out] StringBuilder nameBuffer);

        [DllImport("plugview", CallingConvention = Cc, CharSet = A)]
        private static extern void viewRefresh([Out] StringBuilder nameBuffer);

        [DllImport("plugview", CallingConvention = Cc, CharSet = A)]
        private static extern void viewSetDisplaySize([Out] StringBuilder nameBuffer);

        [DllImport("plugview", CallingConvention = Cc, CharSet = A)]
        private static extern void viewSetHardKey([Out] StringBuilder nameBuffer);

        [DllImport("plugview", CallingConvention = Cc, CharSet = A)]
        private static extern void viewSetIOPortEx([Out] StringBuilder nameBuffer);

        [DllImport("plugview", CallingConvention = Cc, CharSet = A)]
        private static extern void viewSetMemPortEx([Out] StringBuilder nameBuffer);

        [DllImport("plugview", CallingConvention = Cc, CharSet = A)]
        private static extern void viewWriteVram([Out] StringBuilder nameBuffer);

        [DllImport("plugview", CallingConvention = Cc, CharSet = A)]
        private static extern void viewWriteVramEx([Out] StringBuilder nameBuffer);

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
    }
}