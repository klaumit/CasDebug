using NetfXtended.Core;

// ReSharper disable RedundantArgumentDefaultValue

namespace SimCore
{
    public static class MaxiDiff
    {
        public static string Diff(string first, string second, string toFile = null)
        {
            var tmpName = toFile ?? Systems.GetTmpFile(".json");

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