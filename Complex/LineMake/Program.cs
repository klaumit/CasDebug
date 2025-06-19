using System;
using System.IO;
using System.Linq;
using System.Text;
using NetfXtended.Core;
using SimCore;

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
            var root = Paths.GetProjectPath(typeof(Program));
            Console.WriteLine($"Root   = {root}");

            var inpDir = Path.Combine(root, "Input");
            Console.WriteLine($"Input  = {inpDir}");

            var outDir = Paths.CreateDir(Path.Combine(root, "Output"));
            Console.WriteLine($"Output = {outDir}");

            var files = Paths.FindFiles(inpDir, "*.jsonl");
            foreach (var file in files)
            {
                var local = Paths.GetRelativePath(root, file);
                Console.WriteLine($" * {local}");

                var lines = Files.ReadLines(file, Encoding.UTF8);
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
                    var outLoc = Paths.GetRelativePath(outDir, outFile);
                    Console.WriteLine($"    --> {outLoc}");

                    var start = Values.ParseHexU(grp) ?? 0;
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
            var root = Paths.GetProjectPath(typeof(Program));
            Console.WriteLine($"Root   = {root}");

            var inpDir = Path.Combine(root, "Input");
            Console.WriteLine($"Input  = {inpDir}");

            var outDir = Paths.CreateDir(Path.Combine(root, "Output"));
            Console.WriteLine($"Output = {outDir}");

            var files = Paths.FindFiles(inpDir);
            foreach (var file in files)
            {
                var local = Paths.GetRelativePath(root, file);
                Console.WriteLine($" * {local}");

                var name = Path.GetFileNameWithoutExtension(file);
                var outFile = Path.Combine(outDir, $"{name}.bin");

                var lines = Files.ReadLines(file, Encoding.UTF8);
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