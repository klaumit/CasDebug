using System;
using System.Windows.Forms;
using SimLoad.Views;

namespace SimLoad
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