namespace SimLoad.Core
{
    public class DisAsmItem
    {
        public DisAsmItem(string m, string a, int l)
        {
            M = m;
            A = a;
            L = l;
        }

        public string M { get; init; }
        public string A { get; init; }
        public int L { get; init; }
    }
}