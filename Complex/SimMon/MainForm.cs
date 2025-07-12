using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using NetfXtended.Core;
using NetfXtended.WinForms;
using SimCore;

namespace SimMon
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadIcons();
            timer.Enabled = true;
            ClearLog();
            LoadFolder();
        }

        private void LoadIcons()
        {
            const string simIcon = "Sim.png";
            imgLst.ImageSize = new Size(32, 32);
            imgLst.Images.Add(Images.FindByManifest<MainForm>(simIcon));
            imgLst.Images.SetKeyName(0, simIcon);
        }

        private void LoadFolder()
        {
            var root = PathTool.GetInstalledPath(GetType());
            rootFldTb.Enabled = false;
            rootFldTb.Text = root;

            var files = Paths.FindFiles(root, "*si*.exe");
            foreach (var file in files)
            {
                var item = new SimExeItem(file, 0);
                var mask = item.Kind == SimExeKind.SimSH ? "*.dlp" : "*.cpj";
                var pDir = Path.GetDirectoryName(item.Dir);
                var prjs = Paths.FindFiles(pDir, mask).ToArray();
                item.Projects.AddRange(prjs);
                simLstV.Items.Add(item);
            }
        }

        private void ClearLog()
        {
            logBox.Items.Clear();
        }

        private void AddToLog(string text)
        {
            if (text == null) return;
            var ts = DateTime.Now.ToString("u").TrimEnd('Z', ' ');
            var line = $"[{ts}] {text.Trim()}";
            logBox.Items.Insert(0, line);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            foreach (var window in WiHandler.FindByClass(["ThunderRT6MDIForm", "TMainForm"]))
            {
                if (AllSimExes.FirstOrDefault(s => s.Proc?.Id == window.ProcId) is not { } sim)
                    continue;

                if (sim.Main?.Handle != window.Handle)
                    AddToLog($"Main window of '{sim.Text}' found: #{window.Handle}");
                sim.Main = window;

                var load = SimTool.GetLoadedProject(sim);
                if (load != null && sim.Loaded?.File != load.File)
                    AddToLog($"Loaded project of '{sim.Text}' found: {load.Model} in {load.Sdk}");
                sim.Loaded = load;
            }
        }

        private void openRootBtn_Click(object sender, EventArgs e)
        {
            var dir = rootFldTb.Text;
            Systems.Open(dir);
        }

        private void simLstV_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var listView = (ListView)sender;
            for (var i = 0; i < listView.Items.Count; i++)
            {
                var rect = listView.GetItemRect(i);
                if (!rect.Contains(e.Location))
                    continue;
                var item = listView.Items[i];
                if (item is SimExeItem sei)
                {
                    if (sei.IsRunning)
                        sei.Stop();
                    else
                        sei.Start();
                }
                return;
            }
        }

        private void cmdBtn_Click(object sender, EventArgs e)
        {
            var dir = rootFldTb.Text;
            Systems.Open("cmd.exe", dir);
        }

        private SimExeItem SelectedItem
            => simLstV.SelectedItems.Cast<SimExeItem>().SingleOrDefault(e => e.IsRunning);

        private void miniDumpBtn_Click(object sender, EventArgs e)
        {
            if (SelectedItem is not { } item) return;
            var dmpFile = MiniDump.Dump(item.Proc);
            Systems.Open(dmpFile);
        }

        private void screenBtn_Click(object sender, EventArgs e)
        {
            if (SelectedItem is not { } item) return;
            var picFile = ScreenShot.Shoot(item.Proc);
            Systems.Open(picFile);
        }

        private void maxDirDmpBtn_Click(object sender, EventArgs e)
        {
            if (SelectedItem is not { } item) return;
            var djFile = MaxiDump.Dump2JsonDir(item.Proc);
            var djDir = djFile.Replace(".json", "");
            Systems.Open(djDir);
        }

        private readonly List<string> _maxiDumps = [];

        private void maxiDmpBtn_Click(object sender, EventArgs e)
        {
            if (SelectedItem is not { } item) return;
            var djFile = MaxiDump.Dump2JsonFile(item.Proc);
            _maxiDumps.Insert(0, djFile);
            Systems.Open(djFile);
        }

        private void maxiDiffBtn_Click(object sender, EventArgs e)
        {
            if (_maxiDumps is not { Count: >= 2 } md) return;
            var first = md[0];
            var second = md[1];
            var diFile = MaxiDiff.Diff(first, second);
            Systems.Open(diFile);
        }

        private void handleBtn_Click(object sender, EventArgs e)
        {
            if (SelectedItem is not { } item) return;
            var hdFile = WiHandler.Dump(item.Proc);
            Systems.Open(hdFile);
        }

        private SimExeItem[] AllSimExes => simLstV.Items.Cast<SimExeItem>().ToArray();

        private void CloseAll()
        {
            foreach (var item in AllSimExes)
                if (item.IsRunning)
                    item.Stop();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseAll();
        }
    }
}