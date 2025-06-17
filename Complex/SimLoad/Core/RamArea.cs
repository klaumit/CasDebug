namespace SimLoad.Core
{
    public class RamArea
    {
        public RamArea(uint a1, uint a2, byte r)
        {
            A1 = a1;
            A2 = a2;
            R = r;
        }

        public uint A1 { get; init; }
        public uint A2 { get; init; }
        public byte R { get; init; }
    }
}