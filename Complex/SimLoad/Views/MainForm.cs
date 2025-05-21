using System;
using System.Windows.Forms;
using SimuLoad.Core;

namespace SimuLoad.Views
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var it = debugBox.Items;
            it.Clear();

            it.Add($"'{KGeneral.GetPluginName()}'");
            it.Add($"'{KGeneral.GetPluginInformation()}'");

            it.Add($"'{PlugView.GetPluginName()}'");
            it.Add($"'{PlugView.GetPluginInformation()}'");

            it.Add($"'{UnAsmSys.GetPluginName()}'");
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void tryBtn_Click(object sender, EventArgs e)
        {

        }

        private void disBtn_Click(object sender, EventArgs e)
        {
            Applet.DisassembleToJson();
        }
    }
}