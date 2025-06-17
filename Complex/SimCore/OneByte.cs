namespace SimCore
{
    public class OneByte
    {
        public OneByte(uint addr, byte val, string off)
        {
            Addr = addr;
            Val = val;
            Off = off;
        }

        public uint Addr { get; init; }
        public byte Val { get; init; }
        public string Off { get; init; }
    }
}