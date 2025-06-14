using System;
using System.Linq;
using System.Windows.Forms;
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
            timer.Enabled = true;
            ClearLog();
            LoadFolder();
        }

        private void LoadFolder()
        {
            var root = PathTool.GetInstalledPath(GetType());
            rootFldTb.Enabled = false;
            rootFldTb.Text = root;

            var files = PathTool.FindFiles(root, "*si*.exe");
            foreach (var file in files)
            {
                var item = new SimExeItem(file, 0);
                var prjs = PathTool.FindFiles(item.Dir, "*.cpj").ToArray();
                if (prjs.Length == 0)
                    prjs = PathTool.FindFiles(item.Dir, "*.dlp").ToArray();
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
            var ts = DateTime.Now.ToString("u").TrimEnd('Z', ' ');
            var line = $"[{ts}] {text.Trim()}";
            logBox.Items.Insert(0, line);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            AddToLog("Sample tick for you!");
        }

        private void openRootBtn_Click(object sender, EventArgs e)
        {
            var dir = rootFldTb.Text;
            SystemTool.Open(dir);
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
            SystemTool.Open("cmd.exe", dir);
        }

        private SimExeItem SelectedItem
            => simLstV.SelectedItems.Cast<SimExeItem>().SingleOrDefault();

        private void miniDumpBtn_Click(object sender, EventArgs e)
        {
            if (SelectedItem is not { } item) return;
            var dmpFile = MiniDump.Dump(item.Proc);
            SystemTool.Open(dmpFile);
        }

        private void screenBtn_Click(object sender, EventArgs e)
        {
            if (SelectedItem is not { } item) return;
            var picFile = ScreenShot.Shoot(item.Proc);
            SystemTool.Open(picFile);
        }

        private void maxiDmpBtn_Click(object sender, EventArgs e)
        {
            if (SelectedItem is not { } item) return;
            var djFile = MaxiDump.Dump(item.Proc);
            SystemTool.Open(djFile);
        }
        
        private void handleBtn_Click(object sender, EventArgs e)
        {
            if (SelectedItem is not { } item) return;
            var hdFile = WiHandler.Dump(item.Proc);
            SystemTool.Open(hdFile);
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