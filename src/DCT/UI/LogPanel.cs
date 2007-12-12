using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DCT.Util;

namespace DCT.UI
{
    internal partial class LogPanel : UserControl
    {
        internal LogPanel()
        {
            InitializeComponent();
        }

        internal void Clear()
        {
            lstLog.Items.Clear();
            lstAttacks.Items.Clear();
        }

        internal void ClearMost()
        {
            if (lstLog.Items.Count > 100)
            {
                for (int i = lstLog.Items.Count - 1; i > 10; i--)
                {
                    lstLog.Items.RemoveAt(i);
                }
            }
            if (lstAttacks.Items.Count > 100)
            {
                for (int i = lstAttacks.Items.Count - 1; i > 10; i--)
                {
                    lstAttacks.Items.RemoveAt(i);
                }
            }
        }

        internal void Export()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" *** MAIN LOG\r\n");
            foreach (string text in lstLog.Items)
                sb.AppendLine(text);

            sb.AppendLine("\r\n\r\n *** ATTACK LOG\r\n");
            foreach (string text in lstAttacks.Items)
                sb.AppendLine(text);

            FileIO.SaveFileFromString("Export Log", "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
                                      "DCT Log " + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second, sb.ToString());
        }

        internal delegate void LogHandler(string txt);

        internal void Log(string txt)
        {
            if (InvokeRequired)
            {
                Invoke(new LogHandler(Log), txt);
                return;
            }

            try
            {
                lstLog.Items.Insert(0, "[" + DateTime.Now.ToString("T") + "] " + txt);
                if (txt.StartsWith("E:"))
                {
                    MessageBox.Show(txt.Substring(2), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (ObjectDisposedException)
            {
                // prevents problems when frmMain closes
            }
        }

        internal void LogAttack(string txt)
        {
            if (InvokeRequired)
            {
                Invoke(new LogHandler(LogAttack), txt);
                return;
            }

            if (txt.StartsWith("E:"))
            {
                MessageBox.Show(txt.Replace("E: ", ""), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            lstAttacks.Items.Insert(0, "[" + DateTime.Now.ToString("T") + "] " + txt);
        }
    }
}
