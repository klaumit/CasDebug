namespace SimCore
{
    public static class StringX
    {
        public static bool IsNullOrWhiteSpace(string text)
        {
#if NETFRAMEWORK
            if (text == null)
                return true;
            text = text.Trim();
            if (text.Length == 0)
                return true;
            return false;
#else
            return string.IsNullOrWhiteSpace(text);
#endif
        }
    }
}