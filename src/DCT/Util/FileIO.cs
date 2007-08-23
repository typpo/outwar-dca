using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace DCT.Util
{
    internal class FileIO
    {
        internal static string LoadFileToString(string title)
        {
            return LoadFileToString(title, "All Files|*.*");
        }

        internal static string LoadFileToString(string title, string browsemask)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = title;
            dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            dlg.Filter = browsemask;
            dlg.CheckFileExists = true;
            dlg.CheckPathExists = true;
            dlg.Multiselect = false;
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() ==
                DialogResult.OK)
            {
                TextReader tr = new StreamReader(dlg.OpenFile());
                StringBuilder sb = new StringBuilder();
                string s;
                while ((s = tr.ReadLine()) != null)
                {
                    sb.AppendLine(s);
                }
                return sb.ToString();
            }
            return null;
        }

        internal static void SaveFileFromString(string title, string suggestedFileName, string s)
        {
            SaveFileFromString(title, "All Files|*.*", suggestedFileName, s);
        }

        internal static void SaveFileFromString(string title, string browsemask, string suggestedFileName, string s)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = suggestedFileName;
            dlg.Title = title;
            dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            dlg.Filter = browsemask;
            dlg.OverwritePrompt = true;
            dlg.CheckFileExists = false;
            dlg.CheckPathExists = true;
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                TextWriter tw = new StreamWriter(dlg.OpenFile());
                tw.Write(s);
                tw.Flush();
                tw.Close();
            }
        }
    }
}