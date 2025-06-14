using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace SimCore
{
    public static class MaxiDump
    {
        public static string Dump(Process process, string toFile = null)
        {






            throw new InvalidOperationException();
        }
    }
}

/*
       var tmpName = toFile ?? SystemTool.GetTmpFile(".dmp");
       using var fs = new FileStream(tmpName, FileMode.Create, FileAccess.Write, FileShare.None);

       var success = MiniDumpWriteDump(           process.Handle,           (uint)process.Id,
           fs.SafeFileHandle.DangerousGetHandle(),           MINIDUMP_TYPE.MiniDumpWithFullMemory,
           IntPtr.Zero,           IntPtr.Zero,           IntPtr.Zero);

       if (!success)
       {
           throw new Win32Exception(Marshal.GetLastWin32Error());
       }
       return tmpName;
*/
