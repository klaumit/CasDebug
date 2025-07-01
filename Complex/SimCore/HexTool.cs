using System.Collections.Generic;
using System.Linq;
using NetfXtended.Core;

// ReSharper disable ClassNeverInstantiated.Global

namespace SimCore
{
    public static class HexTool
    {
        public static IEnumerable<OneByte> Split(this IEnumerable<string> lines)
        {
            foreach (var line in lines)
            {
                var item = Jsons.ReadPlain<OneLine>(line.TrimEnd(',', ' '));
                var rHex = item.Key;
                if (Values.ParseHexU(rHex) is not { } addr)
                    continue;
                var i = 0;
                var hex = item.Value;
                for (var j = 0; j < hex.Length; j += 2)
                {
                    var part = hex.Substring(j, 2);
                    var dest = addr + i++;
                    if (Values.ParseHexS(part) is not { } bits)
                        continue;
                    yield return new OneByte((uint)dest, (byte)bits, rHex);
                }
            }
        }

        public static string Mask(string text, int number)
        {
            var prefix = text.Substring(0, number);
            var rest = Enumerable.Repeat('0', text.Length - prefix.Length)
                .Select(x => $"{x}").ToArray();
            var masked = prefix + string.Join("", rest);
            return masked;
        }       
    }
}