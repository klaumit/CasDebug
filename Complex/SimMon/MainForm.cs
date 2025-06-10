using System;
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
            for (int i = 0; i < listView.Items.Count; i++)
            {
                var rect = listView.GetItemRect(i);
                if (rect.Contains(e.Location))
                {
                    var item = listView.Items[i];
                    if (item is SimExeItem sei)
                    {
                        sei.Start();
                    }
                    return;
                }
            }
        }

        private void cmdBtn_Click(object sender, EventArgs e)
        {
            var dir = rootFldTb.Text;
            SystemTool.Open("cmd.exe", dir);
        }
    }
}