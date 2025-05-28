using System;
using System.Windows.Forms;
using SimLoad.Core;
using SimLoad.Imports;

// ReSharper disable LocalizableElement

namespace SimLoad.Views
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
            KGeneral.ClosePlugin();
            PlugView.ClosePlugin();
            Environment.Exit(0);
        }

        private void tryBtn_Click(object sender, EventArgs e)
        {
            DlSim32A.MaybeDoIt();
        }

        private int OnRender(uint param1, uint param2)
        {
            throw new NotImplementedException();
        }

        private void disBtn_Click(object sender, EventArgs e)
        {
            Applet.DisassembleToJson();
        }

        private void keyInitBtn_Click(object sender, EventArgs e)
        {
            var it = debugBox.Items;
            var keyLoaded = KGeneral.LoadKeyConfig();
            it.Add($"Keys loaded? {keyLoaded}");

            var hWnd = Handle;
            var keyInit = KGeneral.InitPlugin(hWnd, hWnd);
            it.Add($"Keys init? {keyInit}");

            var keyHWnd = KGeneral.GetPluginWindow();
            User32.Resize(keyHWnd, 170, 150);
            it.Add($"Keys window? {keyHWnd}");

            var keyProp = KGeneral.SetPluginProperties();
            it.Add($"Keys props? {keyProp}");
        }

        private void hideKeyBtn_Click(object sender, EventArgs e)
        {
            var it = debugBox.Items;
            var stat = KGeneral.HideView();
            it.Add($"Keys hide? {stat}");
        }

        private void initDispBtn_Click(object sender, EventArgs e)
        {
            var it = debugBox.Items;

            var hWnd = Handle;
            var dspInit = PlugView.InitPlugin(hWnd, hWnd, OnRender);
            it.Add($"Disp init? {dspInit}");

            var dspHWnd = PlugView.GetPluginWindow();
            it.Add($"Disp window? {dspHWnd}");
        }

        private void onOffBtn_Click(object sender, EventArgs e)
        {
            var it = debugBox.Items;
            var dspClear = PlugView.ClearDisp();
            it.Add($"Disp clear? {dspClear}");
        }

        private void refreshBtn_Click(object sender, EventArgs e)
        {
            var it = debugBox.Items;

            var rVal = PlugView.ReWriteDisp();
            it.Add($"Disp rewrite? {rVal}");

            var pVal = PlugView.Refresh();
            it.Add($"Disp refresh? {pVal}");

            var mVal = PlugView.GetMouseStatus();
            it.Add($"Disp {mVal}");

            var vVal = PlugView.GetVRamArea();
            it.Add($"Disp {vVal}");
        }
    }
}