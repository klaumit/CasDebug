using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;

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

        public static string Dump(Process process, string toFile = null)
        {
            var tmpName = toFile ?? SystemTool.GetTmpFile(".json");

            GetSystemInfo(out var sysInfo);
            var minAddr = sysInfo.lpMinimumApplicationAddress;
            var maxAddr = sysInfo.lpMaximumApplicationAddress;

            var hProcess = OpenProcess(PROCESS_ACCESS, false, (uint)process.Id);
            if (hProcess == IntPtr.Zero)
                throw new Win32Exception(Marshal.GetLastWin32Error());

            var mbiSize = (uint)Marshal.SizeOf(typeof(MEMORY_BASIC_INFORMATION));
            var list = new List<MaxiPage>();
            const ulong maxReadSize = 1024 * 1024;

            while (minAddr.ToInt32() < maxAddr.ToInt32())
            {
                var result = VirtualQueryEx(hProcess, minAddr, out var memInfo, mbiSize);
                if (result == IntPtr.Zero)
                    break;

                var regionSize = memInfo.RegionSize;
                var toRead = Math.Min((ulong)regionSize.ToInt32(), maxReadSize);
                var buffer = new byte[toRead];

                var addr = memInfo.BaseAddress.ToInt32().ToString("X8");
                var size = memInfo.RegionSize.ToInt32().ToString("X8");
                var attr = $"{memInfo.Protect} | {memInfo.State} | {memInfo.Type}";

                if (ReadProcessMemory(hProcess, memInfo.BaseAddress, buffer, (UIntPtr)toRead, out var bytesRead))
                {
                    var hex = HexTool.ToHexString(buffer);
                    var sub = hex.Substring(0, (int)bytesRead * 2);
                    list.Add(new(hex: sub, attr: attr, addr: addr, size: size, err: null));
                }
                else
                {
                    var error = new Win32Exception(Marshal.GetLastWin32Error());
                    list.Add(new(err: error.Message, attr: attr, addr: addr, size: size, hex: null));
                }

                minAddr = (IntPtr)(minAddr.ToInt32() + memInfo.RegionSize.ToInt32());
            }

            JsonTool.WriteJson(list, tmpName);
            return tmpName;
        }
    }
}