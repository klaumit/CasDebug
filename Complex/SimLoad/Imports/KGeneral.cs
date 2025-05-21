using System;
using System.Runtime.InteropServices;
using System.Text;
using static SimLoad.Core.Defaults;

namespace SimLoad.Imports
{
    public static class KGeneral
    {
        [DllImport("kgeneral", CallingConvention = Cc, CharSet = A)]
        private static extern bool KeyviewForm(int a1);

        [DllImport("kgeneral", CallingConvention = Cc, CharSet = A)]
        private static extern byte keyAnalizingInfo(string fileName);

        [DllImport("kgeneral", CallingConvention = Cc, CharSet = A)]
        private static extern IntPtr keyClosePlugin();

        [DllImport("kgeneral", CallingConvention = Cc, CharSet = A)]
        private static extern int keyGetPluginInformation([Out] StringBuilder destination, uint bufferSize);

        [DllImport("kgeneral", CallingConvention = Cc, CharSet = A)]
        private static extern void keyGetPluginName([Out] StringBuilder nameBuffer);

        [DllImport("kgeneral", CallingConvention = Cc, CharSet = A)]
        private static extern IntPtr keyGetPluginWindow();

        [DllImport("kgeneral", CallingConvention = Cc, CharSet = A)]
        private static extern int keyInitPlugin(IntPtr a1, IntPtr hWndParent, int a3, int a4);

        [DllImport("kgeneral", CallingConvention = Cc, CharSet = A)]
        private static extern int keySetPluginProparties();

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

        public static bool LoadKeyConfig()
        {
            var path = "config.dat";
            var result = keyAnalizingInfo(path);
            return result == 1;
        }

        public static IntPtr ClosePlugin()
        {
            var result = keyClosePlugin();
            return result;
        }

        private static bool ViewFlag = false;

        public static bool HideView()
        {
            var result = KeyviewForm(ViewFlag ? 1 : 0);
            ViewFlag = !ViewFlag;
            return result;
        }

        public static IntPtr GetPluginWindow()
        {
            var result = keyGetPluginWindow();
            return result;
        }

        public static IntPtr InitPlugin(IntPtr hWnd, IntPtr parent)
        {
            var result = keyInitPlugin(hWnd, parent, 0, 0);
            return result;
        }

        public static int SetPluginProperties()
        {
            var result = keySetPluginProparties();
            return result;
        }
    }
}