using System.Collections.Generic;
using System.Text;

namespace SimCore
{
    using System;
    using System.IO;

    public static class FileX
    {
        public static IEnumerable<string> ReadLines(string file, Encoding enc)
        {
#if NETFRAMEWORK
            return File.ReadAllLines(file, enc);
#else
            return File.ReadLines(file, enc);
#endif
        }
    }
}