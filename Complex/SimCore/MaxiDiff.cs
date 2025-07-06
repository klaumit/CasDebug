using System.Diagnostics;
using NetfXtended.Core;
using static SimCore.PathTool;

// ReSharper disable RedundantArgumentDefaultValue

namespace SimCore
{
    public static class MaxiDiff
    {
        public static string Diff(Process proc, string first, string second, string toFile = null)
        {
            var tmpName = toFile ?? GetNamedFile("diff", proc, ".json");

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