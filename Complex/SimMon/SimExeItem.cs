using System;
using System.Collections.Generic;
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

        public SimExeItem(string file, int imgIdx) : base(ToLabel(file), imgIdx)
        {
            File = file;
            Projects = new SortedSet<string>();
        }

        private static string ToLabel(string file)
        {
            var dir = Path.GetDirectoryName(file);
            if (dir.EndsWith("\\SIM", StringComparison.InvariantCultureIgnoreCase))
                dir = Path.GetDirectoryName(dir);
            var name = Path.GetFileName(dir);
            return name;
        }

        internal void Start()
        {
            var file = File;
            var dir = Dir;
            SystemTool.Open(file, dir);
        }
    }
}