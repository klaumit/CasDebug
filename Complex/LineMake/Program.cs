using System;
using System.IO;
using System.Linq;
using System.Text;
using SimCore;

#if NETFRAMEWORK
#else
using PathX = System.IO.Path;
#endif

namespace LineMake
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            BuildOne(args);
            Console.WriteLine();
            BuildBig(args);
        }

        private static void BuildOne(string[] args)
        {
            var root = PathTool.GetProjectPath(typeof(Program));
            Console.WriteLine($"Root   = {root}");

            var inpDir = Path.Combine(root, "Input");
            Console.WriteLine($"Input  = {inpDir}");

            var outDir = PathTool.CreateDir(Path.Combine(root, "Output"));
            Console.WriteLine($"Output = {outDir}");

            var files = PathTool.FindFiles(inpDir, "*.jsonl");
            foreach (var file in files)
            {
                var local = PathX.GetRelativePath(root, file);
                Console.WriteLine($" * {local}");

                var lines = FileX.ReadLines(file, Encoding.UTF8);
                foreach (var line in lines.Split()
                             .OrderBy(l => l.Off)
                             .GroupBy(l => HexTool.Mask(l.Off, 3)))
                {
                    var isAllZero = line.All(l => l.Val == 0);
                    if (isAllZero)
                        continue;

                    var grp = line.Key;
                    var name = Path.GetFileNameWithoutExtension(file);
                    var outFile = Path.Combine(outDir, $"{name}-{grp}.bin");
                    var outLoc = PathX.GetRelativePath(outDir, outFile);
                    Console.WriteLine($"    --> {outLoc}");

                    var start = ValTool.ParseHexU(grp) ?? 0;
                    using var fileOut = File.Create(outFile);
                    var written = 0;
                    foreach (var ob in line)
                    {
                        var obAddr = ob.Addr - start;
                        fileOut.Seek(obAddr, SeekOrigin.Begin);
                        fileOut.WriteByte(ob.Val);
                        written++;
                    }
                    Console.WriteLine($"     => {written} bytes written!");
                }
            }

            Console.WriteLine("Done.");
        }

        private static void BuildBig(string[] args)
        {
            var root = PathTool.GetProjectPath(typeof(Program));
            Console.WriteLine($"Root   = {root}");

            var inpDir = Path.Combine(root, "Input");
            Console.WriteLine($"Input  = {inpDir}");

            var outDir = PathTool.CreateDir(Path.Combine(root, "Output"));
            Console.WriteLine($"Output = {outDir}");

            var files = PathTool.FindFiles(inpDir);
            foreach (var file in files)
            {
                var local = PathX.GetRelativePath(root, file);
                Console.WriteLine($" * {local}");

                var name = Path.GetFileNameWithoutExtension(file);
                var outFile = Path.Combine(outDir, $"{name}.bin");

                var lines = FileX.ReadLines(file, Encoding.UTF8);
                using var fileOut = File.Create(outFile);
                var written = 0;
                foreach (var line in lines.Split())
                {
                    fileOut.Seek(line.Addr, SeekOrigin.Begin);
                    fileOut.WriteByte(line.Val);
                    written++;
                }
                Console.WriteLine($"    => {written} bytes written!");
            }

            Console.WriteLine("Done.");
        }
    }
}