
#if NETFRAMEWORK
namespace System.Runtime.CompilerServices
{
    public static class IsExternalInit { }
}

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
    }
}
#endif
