using System.Collections.Generic;
using System.Linq;

// ReSharper disable ClassNeverInstantiated.Global

namespace SimCore
{
    public static class HexTool
    {
        public static IEnumerable<OneByte> Split(this IEnumerable<string> lines)
        {
            foreach (var line in lines)
            {
                var item = JsonTool.ReadPlain<OneLine>(line.TrimEnd(',', ' '));
                var rHex = item.Key;
                if (ValTool.ParseHexU(rHex) is not { } addr)
                    continue;
                var i = 0;
                var hex = item.Value;
                for (var j = 0; j < hex.Length; j += 2)
                {
                    var part = hex.Substring(j, 2);
                    var dest = addr + i++;
                    if (ValTool.ParseHex(part) is not { } bits)
                        continue;
                    yield return new OneByte((uint)dest, (byte)bits, rHex);
                }
            }
        }

        public record OneByte(uint Addr, byte Val, string Off);

        public record OneLine(string Key, string Value);

        public static string Mask(string text, int number)
        {
            var prefix = text[..number];
            var rest = Enumerable.Repeat('0', text.Length - prefix.Length);
            var masked = prefix + string.Join("", rest);
            return masked;
        }
    }
}