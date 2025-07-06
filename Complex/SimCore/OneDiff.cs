namespace SimCore
{
    public class OneDiff
    {
        public OneDiff(string offset, string[] values)
        {
            Offset = offset;
            Values = values;
        }

        public string Offset { get; init; }
        public string[] Values { get; init; }
    }
}