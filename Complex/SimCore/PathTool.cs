using System;
using System.Collections.Generic;
using System.IO;

namespace SimCore
{
    public static class PathTool
    {
        public static string GetInstalledPath(Type type)
        {
            var dll = Path.GetFullPath(type.Assembly.Location);
            var root = Path.GetDirectoryName(dll)!;
            root = root.Replace("/bin/Debug/net8.0", "");
            root = Path.GetFullPath(Path.Combine(root, "..", "..", "Installed"));
            return root;
        }

        public static IEnumerable<string> FindFiles(string root)
        {
            const SearchOption o = SearchOption.AllDirectories;
            var files = Directory.EnumerateFiles(root, "*.*", o);
            return files;
        }

        public static string CreateDir(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path!);
            return path;
        }
    }
}