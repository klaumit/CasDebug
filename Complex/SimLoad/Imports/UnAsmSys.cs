using System;
using System.Runtime.InteropServices;
using System.Text;
using static SimLoad.Core.Defaults;

namespace SimLoad.Imports
{
    public record UnLine(string Text, int Len);

    public static class UnAsmSys
    {
        [DllImport("unasmsys", CallingConvention = Cc, CharSet = A)]
        private static extern int Unasm1Line(IntPtr outStrBuffer, short mode, IntPtr codePtr);

        [DllImport("unasmsys", CallingConvention = Cc, CharSet = A)]
        private static extern void unasmGetPluginName([Out] StringBuilder nameBuffer);

        public static UnLine DisAsmLine(byte[] codeBytes)
        {
            const int bufferSize = 256;
            var buffer = Marshal.AllocHGlobal(bufferSize);
            var codePtr = Marshal.AllocHGlobal(codeBytes.Length);
            Marshal.Copy(codeBytes, 0, codePtr, codeBytes.Length);
            try
            {
                var result = Unasm1Line(buffer, 0x1234, codePtr);
                var output = Marshal.PtrToStringAnsi(buffer);
                return new(output, result);
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
                Marshal.FreeHGlobal(codePtr);
            }
        }

        public static string GetPluginName()
        {
            var buffer = new StringBuilder(64);
            unasmGetPluginName(buffer);
            return buffer.ToString();
        }
    }
}