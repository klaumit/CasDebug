using System;
using System.Collections.Generic;
using System.IO;

namespace SimCore
{
    public static class PathTool
    {
        public static string GetInstalledPath(Type type)
        {
            var root = GetProjectPath(type);
            root = Path.GetFullPath(Path.Combine(root, "..", "..", "Installed"));
            return root;
        }

        public static string GetProjectPath(Type type)
        {
            var ass = type.Assembly;
            var dll = Path.GetFullPath(ass.Location);
            var root = Path.GetDirectoryName(dll)!;
            var s = Path.DirectorySeparatorChar;
            root = root.Replace($"{s}bin{s}Debug{s}net8.0-windows", "");
            root = root.Replace($"{s}bin{s}Debug{s}net8.0", "");
            return root;
        }

        public static IEnumerable<string> FindFiles(string root, string term = "*.*")
        {
            const SearchOption o = SearchOption.AllDirectories;
            var files = Directory.EnumerateFiles(root, term, o);
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