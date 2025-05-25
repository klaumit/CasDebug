using System.Collections.Generic;

namespace SimHash
{
    public class OneFile
    {
        public ISet<FileStat> Files { get; } = new SortedSet<FileStat>();
    }
}