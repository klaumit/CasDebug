using System;
using System.Runtime.InteropServices;
using SimLoad.Core;

namespace SimLoad.Imports
{
    [StructLayout(Defaults.S, CharSet = Defaults.A)]
    public struct ViewPluginCfg
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
}