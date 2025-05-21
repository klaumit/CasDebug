using System.Runtime.InteropServices;
using System.Text;
using static SimuLoad.Core.Defaults;

namespace SimuLoad.Core
{
    public static class KGeneral
    {
        [DllImport("kgeneral", CallingConvention = Cc, CharSet = A)]
        private static extern void keyAnalizingInfo([Out] StringBuilder nameBuffer);

        [DllImport("kgeneral", CallingConvention = Cc, CharSet = A)]
        private static extern void keyClosePlugin([Out] StringBuilder nameBuffer);

        [DllImport("kgeneral", CallingConvention = Cc, CharSet = A)]
        private static extern int keyGetPluginInformation([Out] StringBuilder destination, uint bufferSize);

        [DllImport("kgeneral", CallingConvention = Cc, CharSet = A)]
        private static extern void keyGetPluginName([Out] StringBuilder nameBuffer);

        [DllImport("kgeneral", CallingConvention = Cc, CharSet = A)]
        private static extern void keyGetPluginWindow([Out] StringBuilder nameBuffer);

        [DllImport("kgeneral", CallingConvention = Cc, CharSet = A)]
        private static extern void keyInitPlugin([Out] StringBuilder nameBuffer);

        [DllImport("kgeneral", CallingConvention = Cc, CharSet = A)]
        private static extern void keySetPluginProparties([Out] StringBuilder nameBuffer);

        public static string GetPluginInformation()
        {
            const int bufferLength = 256;
            var buffer = new StringBuilder(bufferLength);
            _ = keyGetPluginInformation(buffer, bufferLength);
            return buffer.ToString();
        }

        public static string GetPluginName()
        {
            var buffer = new StringBuilder(64);
            keyGetPluginName(buffer);
            return buffer.ToString();
        }
    }
}