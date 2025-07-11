using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using static SimCore.PathTool;

namespace SimCore
{
    public static class MiniDump
    {
        [Flags]
        private enum MINIDUMP_TYPE : uint
        {
            MiniDumpNormal = 0x00000000,
            MiniDumpWithDataSegs = 0x00000001,
            MiniDumpWithFullMemory = 0x00000002,
            MiniDumpWithHandleData = 0x00000004,
            MiniDumpWithThreadInfo = 0x00001000,
        }

        [DllImport("dbghelp", SetLastError = true)]
        private static extern bool MiniDumpWriteDump(
            IntPtr hProcess,
            uint processId,
            IntPtr hFile,
            MINIDUMP_TYPE dumpType,
            IntPtr exceptionParam,
            IntPtr userStreamParam,
            IntPtr callbackParam
        );

        public static string Dump(Process process, string toFile = null)
        {
            var tmpName = toFile ?? GetNamedFile("mini", process, ".dmp");
            using var fs = new FileStream(tmpName, FileMode.Create, FileAccess.Write, FileShare.None);

            var success = MiniDumpWriteDump(
                process.Handle,
                (uint)process.Id,
                fs.SafeFileHandle.DangerousGetHandle(),
                MINIDUMP_TYPE.MiniDumpWithFullMemory,
                IntPtr.Zero,
                IntPtr.Zero,
                IntPtr.Zero);

            if (!success)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            return tmpName;
        }
    }
}