using System;

namespace SimLoad.Imports
{
    public class PointInt
    {
        public PointInt(IntPtr clear, int on)
        {
            Clear = clear;
            On = on;
        }

        public IntPtr Clear { get; init; }
        public int On { get; init; }
    }
}