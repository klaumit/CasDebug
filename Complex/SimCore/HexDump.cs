using System.Text;
using System.IO;

// ReSharper disable ClassNeverInstantiated.Global

namespace SimCore
{
    public static class HexDump
    {
        public static void Format(Stream reader, StreamWriter writer)
        {
            var enc = Encoding.ASCII;
            var buffer = new byte[16];
            var idx = 0;
            while (reader.Read(buffer, 0, buffer.Length) is var got && got >= 1)
            {
                writer.Write($"{idx:x8}  ");
                var txt = new StringBuilder();
                for (var i = 0; i < buffer.Length; i++)
                {
                    if (i == buffer.Length / 2)
                        writer.Write(' ');
                    if (i >= got)
                    {
                        writer.Write("   ");
                    }
                    else
                    {
                        writer.Write($"{buffer[i]:x2} ");
                        var sign = enc.GetString(buffer, i, 1);
                        if (char.IsControl(sign[0])) sign = ".";
                        txt.Append(sign);
                    }
                }
                writer.Write($" |{txt}|");
                idx += got;
                writer.WriteLine();
            }
            writer.Write($"{idx:x8}");
            writer.WriteLine();
        }

        public static void Format(string input, string output)
        {
            using var reader = File.OpenRead(input);
            using var writer = File.CreateText(output);
            Format(reader, writer);
        }
    }
}