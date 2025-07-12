namespace SimCore
{
    public class MaxiPage
    {
        public MaxiPage(int no, string attr, string addr, string size, string hex = null,
            string err = null, string sha256 = null, byte[] zip = null, byte[] raw = null)
        {
            No = no;
            Attr = attr;
            Addr = addr;
            Size = size;
            Hex = hex;
            Err = err;
            Sha256 = sha256;
            Zip = zip;
            Raw = raw;
        }

        public int No { get; init; }
        public string Attr { get; init; }
        public string Addr { get; init; }
        public string Size { get; init; }
        public string Hex { get; init; }
        public string Err { get; init; }
        public string Sha256 { get; init; }
        public byte[] Zip { get; init; }
        public byte[] Raw { get; init; }
    }
}