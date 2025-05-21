using System;
using System.Windows.Forms;
using SimuLoad.Views;

namespace SimuLoad
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}