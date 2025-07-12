using System.IO;
using NetfXtended.Core;

namespace SimCore
{
    public static class HashTool
    {
        public static string GetHash(this byte[] buffer)
        {
            using var stream = new MemoryStream(buffer);
            return Hashes.Hash(stream);
        }
    }
}