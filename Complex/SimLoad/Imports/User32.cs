using System;
using System.Runtime.InteropServices;

namespace SimLoad.Imports
{
    internal static class User32
    {
        [DllImport("user32", SetLastError = true)]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter,
            int x, int y, int cx, int cy, uint uFlags);

        private static readonly IntPtr HWND_TOP = IntPtr.Zero;

        private const uint SWP_NOZORDER = 0x0004;
        private const uint SWP_NOACTIVATE = 0x0010;
        private const uint SWP_NOMOVE = 0x0002;

        public static bool Resize(IntPtr hWnd, int width, int height)
        {
            return SetWindowPos(hWnd, HWND_TOP, 0, 0,
                width, height, SWP_NOZORDER | SWP_NOACTIVATE | SWP_NOMOVE
            );
        }
    }
}