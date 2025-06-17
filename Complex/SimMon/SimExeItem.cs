using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using SimCore;

namespace SimMon
{
    internal class SimExeItem : ListViewItem, ISimExeItem
    {
        public SimExeKind Kind { get; }
        public string File { get; }
        public string Dir => Path.GetDirectoryName(File);
        public List<string> Projects { get; }
        public Process Proc { get; private set; }
        public bool IsRunning => Proc is { HasExited: false };
        public OneWindow Main { get; set; }
        public OneLoad Loaded { get; set; }

        public SimExeItem(string file, int imgIdx) : base(SimTool.ToLabel(file), imgIdx)
        {
            File = file;
            Projects = new List<string>();
            Kind = SimTool.GetExeKind(file);
        }

        public void Start()
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