using System;
using System.IO;
using SimCore;

#if NETFRAMEWORK
#else
using PathX = System.IO.Path;
#endif

namespace SimHash
{
    internal static class Program
    {
        private static void Main()
        {
            var root = PathTool.GetInstalledPath(typeof(Program));
            Console.WriteLine($"Root = {root}");

            var hashDir = PathTool.CreateDir(root.Replace("/Installed", "/Hashed"));
            Console.WriteLine($"Hash = {hashDir}");

            var files = PathTool.FindFiles(root);
            foreach (var file in files)
            {
                var local = PathX.GetRelativePath(root, file);
                Console.Write($" * {local}");

                var hashTxt = HashTool.Hash(file);
                Console.WriteLine($" => {hashTxt}");

                var binFile = Path.Combine(hashDir, $"{hashTxt}.b");
                if (!File.Exists(binFile))
                    File.Copy(file, binFile, overwrite: false);

                var txtFile = Path.Combine(hashDir, $"{hashTxt}.json");
                var info = new FileInfo(file);
                var of = JsonTool.ReadJson<OneFile>(txtFile);
                of.Sha256 = hashTxt;
                of.Files.Add(new FileStat(local, info.Length, info.LastWriteTime));
                JsonTool.WriteJson(of, txtFile);
            }

            Console.WriteLine("Done.");
        }
    }
}