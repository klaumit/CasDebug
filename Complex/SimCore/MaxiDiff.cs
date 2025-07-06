using System.Collections.Generic;
using System.IO;
using System.Linq;
using NetfXtended.Core;
using static NetfXtended.Core.Values;
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

            Compare(first, second);

            string[] lines =
            [
                first,
                second
            ];
            Files.WriteLines(tmpName, lines);

            return tmpName;
        }

        private static void Compare(string first, string second)
        {
            using var readerS = new JsonLinesReader<MaxiPage>(second);
            using var readerF = new JsonLinesReader<MaxiPage>(first);
            MaxiPage lineS = null;
            MaxiPage lineF = null;
            while (true)
            {
                if (lineF == null)
                {
                    if (readerF.ReadLine() is not { } itemF) break;
                    lineF = itemF;
                }
                if (lineS == null)
                {
                    if (readerS.ReadLine() is not { } itemS) break;
                    lineS = itemS;
                }
                if (GetKey(lineF).Equals(GetKey(lineS)))
                {
                    // TODO Make diff in array?!
                    lineF = lineS = null;
                    continue;
                }
                var offset = (ParseHexS(lineF.Addr) ?? 0) - (ParseHexS(lineS.Addr) ?? 0);
                if (offset == 0)
                {
                    lineF = lineS = null;
                    continue;
                }
                if (offset < 0)
                    lineF = null;
                else
                    lineS = null;
            }
        }

        private static string GetKey(MaxiPage page)
        {
            return $"{page.Addr}|{page.Size}";
        }

        private static List<MaxiPage> RemoveHashes(string file, string[] hashes)
        {
            var results = new List<MaxiPage>();
            using var reader = new JsonLinesReader<MaxiPage>(file);
            while (reader.ReadLine() is { } line)
            {
                if (!Strings.IsNullOrWhiteSpace(line.Err))
                    continue;
                var hash = line.Sha256;
                if (hashes.Contains(hash))
                    continue;
                results.Add(line);
            }
            return results;
        }

        private static List<string> FindHashes(string file)
        {
            var hashes = new List<string>();
            using var reader = new JsonLinesReader<MaxiPage>(file);
            while (reader.ReadLine() is { } line)
            {
                if (!Strings.IsNullOrWhiteSpace(line.Err))
                    continue;
                var hash = line.Sha256;
                if (hashes.Contains(hash))
                    continue;
                hashes.Add(hash);
            }
            return hashes;
        }
    }
}