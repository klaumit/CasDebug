using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NetfXtended.Core;
using static NetfXtended.Core.Values;
using static SimCore.PathTool;
using SO = System.StringSplitOptions;

// ReSharper disable UseIndexFromEndExpression
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

            var data = Compare(first, second);
            Jsons.WriteJson(data, tmpName);

            return tmpName;
        }

        private static List<OneDiff> Compare(string first, string second)
        {
            var res = new List<OneDiff>();
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
                    if (Compare(lineS, lineF) is { } got)
                        res.Add(got);
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
            return res;
        }

        private static OneDiff Compare(MaxiPage oldP, MaxiPage newP)
        {
            if ((oldP.Sha256 ?? "_").Equals(newP.Sha256 ?? "_"))
                return null;
            var oldB = Compressions.Decompress(oldP.Zip, CompressionKind.Deflate);
            var newB = Compressions.Decompress(newP.Zip, CompressionKind.Deflate);
            var bld = new StringBuilder(" ");
            for (var i = 0; i < oldB.Length && i < newB.Length; i++)
            {
                if (oldB[i] == newB[i])
                {
                    if (bld[bld.Length - 1] != ' ')
                        bld.Append(' ');
                    continue;
                }
                if (bld[bld.Length - 1] == ' ')
                {
                    bld.Append($"°{i:x8}|");
                }
                bld.Append($"{newB[i]:x2}");
            }
            var txt = bld.ToString().Split([" "], SO.RemoveEmptyEntries);
            return new(newP.Addr, txt);
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