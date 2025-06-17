namespace SimCore
{
    public static class StringX
    {
        public static bool IsNullOrWhiteSpace(string text)
        {
#if NETFRAMEWORK
            if (text == null)
                return false;
            text = text.Trim();
            if (text.Length == 0)
                return false;
            return true;
#else
            return string.IsNullOrWhiteSpace(text);
#endif
        }
    }
}