using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using NetfXtended.Core;
using static SimCore.PathTool;

// ReSharper disable RedundantArgumentDefaultValue

namespace SimCore
{
    public static class MaxiDump
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct SYSTEM_INFO
        {
            public ushort wProcessorArchitecture;
            public ushort wReserved;
            public uint dwPageSize;
            public IntPtr lpMinimumApplicationAddress;
            public IntPtr lpMaximumApplicationAddress;
            public IntPtr dwActiveProcessorMask;
            public uint dwNumberOfProcessors;
            public uint dwProcessorType;
            public uint dwAllocationGranularity;
            public ushort wProcessorLevel;
            public ushort wProcessorRevision;
        }

        [DllImport("kernel32", SetLastError = true)]
        private static extern void GetSystemInfo(out SYSTEM_INFO lpSystemInfo);

        private const uint PROCESS_VM_READ = 0x0010;
        private const uint PROCESS_QUERY_INFORMATION = 0x0400;
        private const uint PROCESS_VM_OPERATION = 0x0008;
        private const uint PROCESS_VM_WRITE = 0x0020;

        private const uint PROCESS_ACCESS = PROCESS_VM_READ | PROCESS_QUERY_INFORMATION;

        [DllImport("kernel32", SetLastError = true)]
        private static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, uint dwProcessId);

        private struct MEMORY_BASIC_INFORMATION
        {
            public IntPtr BaseAddress;
            public IntPtr AllocationBase;
            public AllocationProtectEnum AllocationProtect;
            public IntPtr RegionSize;
            public StateEnum State;
            public AllocationProtectEnum Protect;
            public TypeEnum Type;
        }

        private enum AllocationProtectEnum : uint
        {
            PAGE_EXECUTE = 0x00000010,
            PAGE_EXECUTE_READ = 0x00000020,
            PAGE_EXECUTE_READWRITE = 0x00000040,
            PAGE_EXECUTE_WRITECOPY = 0x00000080,
            PAGE_NOACCESS = 0x00000001,
            PAGE_READONLY = 0x00000002,
            PAGE_READWRITE = 0x00000004,
            PAGE_WRITECOPY = 0x00000008,
            PAGE_GUARD = 0x00000100,
            PAGE_NOCACHE = 0x00000200,
            PAGE_WRITECOMBINE = 0x00000400
        }

        private enum StateEnum : uint
        {
            MEM_COMMIT = 0x1000,
            MEM_FREE = 0x10000,
            MEM_RESERVE = 0x2000
        }

        private enum TypeEnum : uint
        {
            MEM_IMAGE = 0x1000000,
            MEM_MAPPED = 0x40000,
            MEM_PRIVATE = 0x20000
        }

        [DllImport("kernel32", SetLastError = true)]
        private static extern IntPtr VirtualQueryEx(IntPtr hProcess, IntPtr lpAddress,
            out MEMORY_BASIC_INFORMATION lpBuffer, uint dwLength);

        [DllImport("kernel32", SetLastError = true)]
        private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress,
            [Out] byte[] lpBuffer, UIntPtr dwSize, out UIntPtr lpNumberOfBytesRead);

        private const ulong MaxReadSize = 34 * 1024 * 1024;
        private static readonly SYSTEM_INFO SysInfo;
        private static readonly Dictionary<uint, IntPtr> HProcesses;
        private static readonly uint MbiSize;

        static MaxiDump()
        {
            GetSystemInfo(out SysInfo);
            HProcesses = new Dictionary<uint, IntPtr>();
            MbiSize = (uint)Marshal.SizeOf(typeof(MEMORY_BASIC_INFORMATION));
        }

        private static IntPtr GetProcHandle(uint pid)
        {
            if (!HProcesses.TryGetValue(pid, out var hProcess))
            {
                hProcess = OpenProcess(PROCESS_ACCESS, false, pid);
                if (hProcess == IntPtr.Zero)
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                HProcesses[pid] = hProcess;
            }
            return hProcess;
        }

        private delegate IDisposable CreateList(string name);

        private delegate void DoPage(IDisposable list, MaxiPage page);

        private static IDisposable CreateWriter(string name)
        {
            var list = new JsonLinesWriter<MaxiPage>(name);
            return list;
        }

        private static void WriteJsonLine(IDisposable raw, MaxiPage page)
        {
            var list = (JsonLinesWriter<MaxiPage>)raw;
            list.WriteLine(page);
        }

        private static DoPage WriteZipBytes(DoPage after)
        {
            return (list, p) =>
            {
                var buffer = p.Raw;
                var hash = buffer.GetHash();
                var zip = Compressions.Compress(buffer, CompressionKind.Deflate);
                after(list, new(p.No, p.Attr, p.Addr, p.Size, sha256: hash, zip: zip));
            };
        }

        private static string Dump(Process process, CreateList lister, DoPage onOkay, DoPage onFail,
            string toFile = null)
        {
            var tmpName = toFile ?? GetNamedFile("maxi", process, ".json");
            var hProcess = GetProcHandle((uint)process.Id);

            using var list = lister(tmpName);
            var minAddr = SysInfo.lpMinimumApplicationAddress;
            var maxAddr = SysInfo.lpMaximumApplicationAddress;
            var nr = 0;

            while (minAddr.ToInt32() < maxAddr.ToInt32())
            {
                var result = VirtualQueryEx(hProcess, minAddr, out var memInfo, MbiSize);
                if (result == IntPtr.Zero)
                    break;

                var toRead = Math.Min((ulong)memInfo.RegionSize.ToInt32(), MaxReadSize);
                var buffer = new byte[toRead];

                var addr = memInfo.BaseAddress.ToInt32().ToString("X8");
                var size = memInfo.RegionSize.ToInt32().ToString("X8");
                var attr = $"{memInfo.Protect} | {memInfo.State} | {memInfo.Type}";

                nr++;
                if (ReadProcessMemory(hProcess, memInfo.BaseAddress, buffer, (UIntPtr)toRead, out var bytesRead))
                {
                    var dstLen = (int)bytesRead;
                    if (buffer.Length != dstLen)
                        throw new InvalidOperationException($" {buffer.Length} != {dstLen} ");
                    onOkay(list, new(nr, attr, addr, size, raw: buffer));
                }
                else
                {
                    var error = new Win32Exception(Marshal.GetLastWin32Error());
                    onFail(list, new(nr, attr, addr, size, err: error.Message));
                }

                minAddr = (IntPtr)(minAddr.ToInt32() + memInfo.RegionSize.ToInt32());
            }

            return tmpName;
        }

        public static string Dump2JsonFile(Process process)
            => Dump(process, CreateWriter, WriteZipBytes(WriteJsonLine), WriteJsonLine);

        public static string Dump2JsonDir(Process process)
            => Dump(process, null, null, null);
    }
}