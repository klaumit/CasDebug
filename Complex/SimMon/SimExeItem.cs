using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using SimCore;

namespace SimMon
{
    internal class SimExeItem : ListViewItem
    {
        public string File { get; }
        public string Dir => Path.GetDirectoryName(File);
        public ISet<string> Projects { get; }
        public Process Proc { get; private set; }
        public bool IsRunning => Proc is { HasExited: false };
        public OneWindow Main { get; set; }

        public SimExeItem(string file, int imgIdx) : base(ToLabel(file), imgIdx)
        {
            File = file;
            Projects = new SortedSet<string>();
        }

        private static string ToLabel(string file)
        {
            var dir = Path.GetDirectoryName(file)!;
            if (dir.EndsWith("\\SIM", StringComparison.InvariantCultureIgnoreCase))
                dir = Path.GetDirectoryName(dir);
            var name = Path.GetFileName(dir);
            return name;
        }

        internal void Start()
        {
            Proc = SystemTool.Open(File, Dir);
        }

        public void Stop()
        {
#if NETFRAMEWORK
            Proc.Kill();
#else
            Proc.Kill(true);
#endif
        }
    }
}