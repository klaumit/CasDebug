namespace SimLoad.Core
{
    public class MouseStatus
    {
        public MouseStatus(uint a1, int a2, int a3, int r)
        {
            A1 = a1;
            A2 = a2;
            A3 = a3;
            R = r;
        }

        public uint A1 { get; init; }
        public int A2 { get; init; }
        public int A3 { get; init; }
        public int R { get; init; }
    }
}