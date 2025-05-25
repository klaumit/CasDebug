using System;
using System.IO;
using System.Security.Cryptography;

namespace SimHash
{
    internal static class Program
    {
        private static void Main()
        {
            var dll = Path.GetFullPath(typeof(Program).Assembly.Location);
            var root = Path.GetDirectoryName(dll)!;
            root = root.Replace("/bin/Debug/net8.0", "");
            root = Path.GetFullPath(Path.Combine(root, "..", "..", "Installed"));
            Console.WriteLine($"Root = {root}");

            var hashDir = root.Replace("/Installed", "/Hashed");
            if (!Directory.Exists(hashDir)) Directory.CreateDirectory(hashDir);
            Console.WriteLine($"Hash = {hashDir}");

            const SearchOption o = SearchOption.AllDirectories;
            var files = Directory.EnumerateFiles(root, "*.*", o);
            foreach (var file in files)
            {
                var local = Path.GetRelativePath(root, file);
                Console.Write($" * {local}");

                byte[] hash;
                using (var stream = File.OpenRead(file))
                    hash = SHA256.HashData(stream);
                var hashTxt = Convert.ToHexString(hash);
                Console.WriteLine($" => {hashTxt}");

                var binFile = Path.Combine(hashDir, $"{hashTxt}.b");
                if (!File.Exists(binFile))
                    File.Copy(file, binFile, overwrite: false);

                var txtFile = Path.Combine(hashDir, $"{hashTxt}.json");
                var of = JsonTool.ReadJson<OneFile>(txtFile);
                var info = new FileInfo(file);
                of.Files.Add(new FileStat(local, info.Length, info.LastWriteTime));
                JsonTool.WriteJson(of, txtFile);
            }

            Console.WriteLine("Done.");
        }
    }
}