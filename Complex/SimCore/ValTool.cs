using System.Globalization;

namespace SimCore
{
    public static class ValTool
    {
        public static int? ParseHex(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return null;
            return int.Parse(text, NumberStyles.HexNumber);
        }

        public static uint? ParseHexU(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return null;
            return uint.Parse(text, NumberStyles.HexNumber);
        }

        public static string TrimOrNull(string text)
        {
            if (text == null) return null;
            text = text.Trim();
            return text.Length == 0 ? null : text;
        }
    }
}