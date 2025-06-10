using System.Diagnostics;

namespace SimCore
{
    public static class SystemTool
    {
        public static void Open(string file, string? folder = null)
        {
            var info = new ProcessStartInfo
            {
                FileName = file,
                UseShellExecute = true
            };
            if (!string.IsNullOrWhiteSpace(folder))
                info.WorkingDirectory = folder;
            _ = Process.Start(info);
        }
    }
}