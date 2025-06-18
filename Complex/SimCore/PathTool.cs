using System;
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
    }
}