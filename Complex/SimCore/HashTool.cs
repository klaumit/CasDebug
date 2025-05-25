using System;
using System.IO;
using System.Security.Cryptography;

namespace SimCore
{
    public static class HashTool
    {
        public static string Hash(string file)
        {
            byte[] hash;
            using (var stream = File.OpenRead(file))
                hash = SHA256.HashData(stream);
            var hashTxt = Convert.ToHexString(hash);
            return hashTxt;
        }
    }
}