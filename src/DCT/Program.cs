using System;
using System.Windows.Forms;
using System.Diagnostics;
using DCT.UI;

namespace DCT
{
    internal static class Program
    {
        private const int MAX_PROCESSES = 25;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            string proc = Process.GetCurrentProcess().ProcessName;
            Process[] processes = Process.GetProcessesByName(proc);

            if (processes.Length > MAX_PROCESSES)
            {
                MessageBox.Show(string.Format("Too many at once.", MAX_PROCESSES), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new CoreUI());
        }
    }
}