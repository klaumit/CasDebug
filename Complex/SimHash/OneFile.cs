using System.Collections.Generic;

namespace SimHash
{
    public class OneFile
    {
        public string Sha256 { get; set; }
        public ISet<FileStat> Files { get; } = new SortedSet<FileStat>();
    }
}