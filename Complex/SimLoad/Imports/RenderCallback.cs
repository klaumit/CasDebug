using System.Runtime.InteropServices;
using SimLoad.Core;

namespace SimLoad.Imports
{
    [UnmanagedFunctionPointer(Defaults.Cc)]
    public delegate int RenderCallback(uint param1, uint param2);
}