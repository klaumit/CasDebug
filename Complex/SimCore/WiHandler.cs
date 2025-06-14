using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SimCore
{
    public record OneWindow(IntPtr Handle, string Class, string Text, uint ProcId, uint ThreadId);

    public static class WiHandler
    {
        [DllImport("user32", SetLastError = true)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder strBld, int maxCount);

        private static string GetWindowText(IntPtr hWnd)
        {
            var bld = new StringBuilder(512);
            GetWindowText(hWnd, bld, bld.Capacity);
            var text = bld.ToString();
            return ValTool.TrimOrNull(text);
        }

        [DllImport("user32", SetLastError = true)]
        private static extern int GetClassName(IntPtr hWnd, StringBuilder strBld, int maxCount);

        private static string GetClassName(IntPtr hWnd)
        {
            var bld = new StringBuilder(512);
            GetClassName(hWnd, bld, bld.Capacity);
            var text = bld.ToString();
            return ValTool.TrimOrNull(text);
        }

        [DllImport("user32", SetLastError = true)]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint procId);

        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        [DllImport("user32", SetLastError = true)]
        private static extern int EnumWindows(EnumWindowsProc lpEnumFunc, int lParam);

        private static List<OneWindow> EnumWindows()
        {
            var windows = new List<OneWindow>();
            EnumWindows(OnWindowEnum, 0);
            return windows;

            bool OnWindowEnum(IntPtr hWnd, IntPtr lParam)
            {
                var clazz = GetClassName(hWnd);
                var title = GetWindowText(hWnd);
                var tId = GetWindowThreadProcessId(hWnd, out var pId);
                windows.Add(new(hWnd, clazz, title, pId, tId));
                return true;
            }
        }

        public static string Dump(Process process, string toFile = null)
        {
            var tmpName = toFile ?? SystemTool.GetTmpFile(".json");

            var list = new List<object>();

            var pid = process.Id;
            var topWnd = EnumWindows().Where(p => p.ProcId==pid);
            list.AddRange(topWnd.Select(y => (object)y));







            JsonTool.WriteJson(list, tmpName);
            return tmpName;
        }
    }
}