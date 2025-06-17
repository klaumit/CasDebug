namespace SimCore
{
    public class MaxiPage
    {
        public MaxiPage(string hex, string attr, string addr, string size, string err)
        {
            Hex = hex;
            Attr = attr;
            Addr = addr;
            Size = size;
            Err = err;
        }

        public string Addr { get; init; }
        public string Size { get; init; }
        public string Attr { get; init; }
        public string Hex { get; init; }
        public string Err { get; init; }
    }
}