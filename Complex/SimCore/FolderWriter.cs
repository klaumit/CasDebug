using System;
using System.IO;
using NetfXtended.Core;

namespace SimCore
{
    public sealed class FolderWriter : IDisposable
    {
        private readonly string _dir;

        public FolderWriter(string name)
        {
            var dir = Path.GetFileNameWithoutExtension(name);
            _dir = Paths.CreateDir(dir);
        }

        public void WriteLine(MaxiPage page)
        {
            var key = $"mem_{page.Addr}_{page.Size}.hex";
            var file = Path.Combine(_dir, key);
            using var reader = new MemoryStream(page.Raw);
            using var writer = File.CreateText(file);
            HexDump.Format(reader, writer);
        }

        public void Dispose()
        {
        }
    }
}