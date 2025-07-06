using System;
using System.Diagnostics;
using System.IO;
using NetfXtended.Core;

namespace SimCore
{
    public static class PathTool
    {
        public static string GetInstalledPath(Type type)
        {
            var root = Paths.GetProjectPath(type);
            root = Path.GetFullPath(Paths.Combine(root, "..", "..", "Installed"));
            return root;
        }

        public static string GetNamedFile(string prefix, Process proc, string suffix)
            => GetNamedFile(prefix, proc.Id, suffix);

        public static string GetNamedFile(string prefix, int procId, string suffix)
        {
            var now = DateTime.Now;
            var dt = now.ToString("u").Replace("-", "").Replace(":", "").Replace(' ', '_').TrimEnd('Z');
            var txt = $"{procId}_{prefix}_{dt}{suffix}";
            return txt;
        }
    }
}