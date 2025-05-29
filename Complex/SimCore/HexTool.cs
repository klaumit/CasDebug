using System.Collections.Generic;

namespace SimCore
{
    public static class HexTool
    {
        public static IEnumerable<(uint addr, byte val)> Split(this IEnumerable<string> lines)
        {
            foreach (var line in lines)
            {
                var item = JsonTool.ReadPlain<OneLine>(line.TrimEnd(',', ' '));
                if (ValTool.ParseHexU(item.Key) is not { } addr)
                    continue;
                var i = 0;
                var hex = item.Value;
                for (var j = 0; j < hex.Length; j += 2)
                {
                    var part = hex.Substring(j, 2);
                    var dest = addr + i++;
                    if (ValTool.ParseHex(part) is not { } bits)
                        continue;
                    yield return ((uint)dest, (byte)bits);
                }
            }
        }

        public record OneLine(
            string Key,
            string Value
        );
    }
}