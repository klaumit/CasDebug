using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using NetfXtended.Core;
using SimLoad.Core;
using static SimLoad.Core.Defaults;

// ReSharper disable LocalizableElement

namespace SimLoad.Imports
{
    public static class DlSim32A
    {
        [DllImport("dlsim32a", EntryPoint = "_DLSIM_ModelLoadMemory@4",
            CallingConvention = Cc, CharSet = A)]
        private static extern int ModelLoadMemory(ref uint param);

        [DllImport("dlsim32a", EntryPoint = "_DLSIM_ModelLoad@4",
            CallingConvention = Cc, CharSet = A)]
        private static extern void ModelLoad([MarshalAs(UnmanagedType.LPStr)] string param);

        [DllImport("dlsim32a", EntryPoint = "_DLSIM_Init@4",
            CallingConvention = Cc, CharSet = A)]
        private static extern void Init(int param);

        public static void MaybeDoIt()
        {
            Init(0);

            var dir = Applet.GetPathOf(typeof(DlSim32A));
            var root = Path.GetFullPath(Paths.Combine(dir, "..", ".."));
            root = Paths.Combine(root, "Installed", "CASIO", "PV3S1600");
            root = Paths.Combine(root, "SIM", "PV-S1600.dlm");
            ModelLoad(root);

            uint myParam = 123;
            var res = ModelLoadMemory(ref myParam);
            MessageBox.Show(" ? " + res);
        }
    }
}