namespace SimLoad.Imports
{
    public class UnLine
    {
        public UnLine(string text, int len)
        {
            Text = text;
            Len = len;
        }

        public string Text { get; init; }
        public int Len { get; init; }
    }
}