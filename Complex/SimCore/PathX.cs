using System.Linq;

namespace SimCore
{
    using System;
    using System.IO;

    public static class PathX
    {
        public static string GetRelativePath(string relativeTo, string path)
        {
            var fromUri = new Uri(AppendDirectorySeparatorChar(relativeTo));
            var toUri = new Uri(path);
            if (fromUri.Scheme != toUri.Scheme)
            {
                return path;
            }
            var relativeUri = fromUri.MakeRelativeUri(toUri);
            var relativePath = Uri.UnescapeDataString(relativeUri.ToString());
            if (toUri.Scheme.Equals("file", StringComparison.OrdinalIgnoreCase))
            {
                relativePath = relativePath.Replace('/', Path.DirectorySeparatorChar);
            }
            return relativePath;
        }

        private static string AppendDirectorySeparatorChar(string path)
        {
            if (!path.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                return path + Path.DirectorySeparatorChar;
            }
            return path;
        }

        public static string Combine(string start, params string[] args)
        {
#if NETFRAMEWORK
            var res = start;
            foreach (var arg in args)
                res = Path.Combine(res, arg);
            return res;
#else
            var opt = new[] { start }.Concat(args).ToArray();
            return Path.Combine(opt);
#endif
        }
    }
}