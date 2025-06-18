using System;
using System.IO;
using NetfXtended.Core;
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

            var hashDir = Paths.CreateDir(root.Replace("/Installed", "/Hashed"));
            Console.WriteLine($"Hash = {hashDir}");

            var files = Paths.FindFiles(root);
            foreach (var file in files)
            {
                var local = Paths.GetRelativePath(root, file);
                Console.Write($" * {local}");

                var hashTxt = Hashes.Hash(file);
                Console.WriteLine($" => {hashTxt}");

                var binFile = Path.Combine(hashDir, $"{hashTxt}.b");
                if (!File.Exists(binFile))
                    File.Copy(file, binFile, overwrite: false);

                var txtFile = Path.Combine(hashDir, $"{hashTxt}.json");
                var info = new FileInfo(file);
                var of = Jsons.ReadJson<OneFile>(txtFile);
                of.Sha256 = hashTxt;
                of.Files.Add(new FileStat(local, info.Length, info.LastWriteTime));
                Jsons.WriteJson(of, txtFile);
            }

            Console.WriteLine("Done.");
        }
    }
}