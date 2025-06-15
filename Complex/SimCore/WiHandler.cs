using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SimCore
{
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

        private delegate bool EnumWindowsProc(IntPtr hWnd, ref IntPtr lParam);

        [DllImport("user32", SetLastError = true)]
        private static extern int EnumWindows(EnumWindowsProc lpFunc, ref IntPtr lParam);

        private static List<OneWindow> EnumWindows()
        {
            var windows = new List<OneWindow>();
            var ptr = IntPtr.Zero;
            EnumWindows(OnWindowEnum, ref ptr);
            return windows;

            bool OnWindowEnum(IntPtr hWnd, ref IntPtr lParam)
            {
                var clazz = GetClassName(hWnd);
                var title = GetWindowText(hWnd);
                var tId = GetWindowThreadProcessId(hWnd, out var pId);
                windows.Add(new(hWnd, clazz, title, pId, tId));
                return true;
            }
        }

        [DllImport("user32", SetLastError = true)]
        private static extern bool EnumChildWindows(IntPtr hWndParent, EnumWindowsProc lpFunc, ref IntPtr lParam);

        private static List<OneWindow> EnumChildWindows(IntPtr parent, bool recursive)
        {
            var windows = new List<OneWindow>();
            var ptr = IntPtr.Zero;
            EnumChildWindows(parent, OnChildWindowEnum, ref ptr);
            return windows;

            bool OnChildWindowEnum(IntPtr hWnd, ref IntPtr lParam)
            {
                var clazz = GetClassName(hWnd);
                var title = GetWindowText(hWnd);
                var tId = GetWindowThreadProcessId(hWnd, out var pId);
                windows.Add(new(hWnd, clazz, title, pId, tId, parent));
                if (recursive)
                {
                    var sub = EnumChildWindows(hWnd, true);
                    windows.AddRange(sub);
                }
                return true;
            }
        }

        public static string Dump(Process process, string toFile = null)
        {
            var tmpName = toFile ?? SystemTool.GetTmpFile(".json");

            var list = new List<OneWindow>();

            var pid = process.Id;
            foreach (var window in EnumWindows().Where(p => p.ProcId == pid))
            {
                list.Add(window);
                foreach (var child in EnumChildWindows(window.Handle, true))
                {
                    list.Add(child);
                }
            }

            JsonTool.WriteJson(list, tmpName);
            return tmpName;
        }

        public static List<OneWindow> FindByClass(string[] classNames)
        {
            var list = new List<OneWindow>();
            var o = StringComparison.InvariantCultureIgnoreCase;
            foreach (var window in EnumWindows())
            foreach (var className in classNames)
                if (window.Class.Equals(className, o))
                    list.Add(window);
            return list;
        }
    }
}