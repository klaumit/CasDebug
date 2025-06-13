using System;
using System.IO;
using System.Security.Cryptography;

namespace SimCore
{
    public static class HashTool
    {
        public static string Hash(string file)
        {
            using var stream = File.OpenRead(file);
            return Hash(stream);
        }

        public static string Hash(Stream stream)
        {
#if NETFRAMEWORK
            using var algo = SHA256.Create();
            var hash = algo.ComputeHash(stream);
            var hashTxt = HexTool.ToHexString(hash);
#else
            var hash = SHA256.HashData(stream);
            var hashTxt = Convert.ToHexString(hash);
#endif
            return hashTxt;
        }
    }
}