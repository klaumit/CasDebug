using System.Collections.Generic;

namespace SimHash
{
    public class OneFile
    {
        public string Sha256 { get; set; }
        public IList<FileStat> Files { get; } = new List<FileStat>();
    }
}