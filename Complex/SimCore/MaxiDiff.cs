using System.IO;
using System.Linq;
using NetfXtended.Core;
using static SimCore.PathTool;

// ReSharper disable RedundantArgumentDefaultValue

namespace SimCore
{
    public static class MaxiDiff
    {
        public static string Diff(string first, string second, string toFile = null)
        {
            var firstFile = Path.GetFileNameWithoutExtension(first);
            var procId = int.Parse(firstFile.Split('_').First());

            var tmpName = toFile ?? GetNamedFile("diff", procId, ".json");

            string[] lines =
            [
                first,
                second
            ];
            Files.WriteLines(tmpName, lines);

            return tmpName;
        }
    }
}