using System.Diagnostics;

namespace SimCore
{
    public static class SystemTool
    {
        public static void Open(string folder)
        {
            var info = new ProcessStartInfo
            {
                FileName = folder,
                UseShellExecute = true
            };
            _ = Process.Start(info);
        }
    }
}