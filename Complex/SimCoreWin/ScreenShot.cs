using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using static SimCore.PathTool;

namespace SimCore
{
    public static class ScreenShot
    {
        [DllImport("user32")]
        private static extern int GetSystemMetrics(int nIndex);

        private const int SM_CXSCREEN = 0;
        private const int SM_CYSCREEN = 1;

        [DllImport("user32")]
        private static extern IntPtr GetDesktopWindow();

        [DllImport("user32")]
        private static extern IntPtr GetWindowDC(IntPtr hWnd);

        [DllImport("gdi32")]
        private static extern bool BitBlt(IntPtr hdcDest, int nXDest, int nYDest,
            int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);

        [DllImport("user32")]
        private static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        private const int SRCCOPY = 0x00CC0020;

        public static string Shoot(Process process, string toFile = null)
        {
            var tmpName = toFile ?? GetNamedFile("shot", process, ".png");

            var screenWidth = GetSystemMetrics(SM_CXSCREEN);
            var screenHeight = GetSystemMetrics(SM_CYSCREEN);

            using var bmp = new Bitmap(screenWidth, screenHeight);
            using var g = Graphics.FromImage(bmp);
            var desktopWnd = GetDesktopWindow();
            var desktopDC = GetWindowDC(desktopWnd);
            var bmpDC = g.GetHdc();

            BitBlt(bmpDC, 0, 0, screenWidth, screenHeight, desktopDC, 0, 0, SRCCOPY);

            g.ReleaseHdc(bmpDC);
            ReleaseDC(desktopWnd, desktopDC);
            g.Dispose();

            bmp.Save(tmpName, ImageFormat.Png);
            return tmpName;
        }
    }
}