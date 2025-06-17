using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SimCore
{
    public static class PathTool
    {
        public static string GetInstalledPath(Type type)
        {
            var root = GetProjectPath(type);
            root = Path.GetFullPath(PathX.Combine(root, "..", "..", "Installed"));
            return root;
        }

        public static string GetProjectPath(Type type)
        {
            var ass = type.Assembly;
            var dll = Path.GetFullPath(ass.Location);
            var root = Path.GetDirectoryName(dll)!;
            var s = Path.DirectorySeparatorChar;
            var tmp = $"{s}bin{s}x86{s}";
            if (root.Contains(tmp))
            {
                var root1 = root.Split([tmp], 2, StringSplitOptions.None);
                root = root1.First();
            }
            tmp = $"{s}bin{s}Debug{s}";
            if (root.Contains(tmp))
            {
                var root2 = root.Split([tmp], 2, StringSplitOptions.None);
                root = root2.First();
            }
            return root;
        }

        public static IEnumerable<string> FindFiles(string root, string term = "*.*")
        {
            const SearchOption o = SearchOption.AllDirectories;
            var files = Directory.GetFiles(root, term, o);
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