using System;
using System.Windows.Forms;

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
    }
}