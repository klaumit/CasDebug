namespace SimCore
{
    public class MaxiPage
    {
        public MaxiPage(int no, string hex, string attr, string addr, string size, string err)
        {
            No = no;
            Hex = hex;
            Attr = attr;
            Addr = addr;
            Size = size;
            Err = err;
        }

        public int No { get; init; }
        public string Addr { get; init; }
        public string Size { get; init; }
        public string Attr { get; init; }
        public string Hex { get; init; }
        public string Err { get; init; }
    }
}