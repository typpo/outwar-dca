using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using DCT.UI;

namespace DCT.Settings
{
    internal class IniWriter
    {
        internal static void Save()
        {
            try
            {
                StreamWriter sw = new StreamWriter("savedRooms.ini");
                foreach (ListViewItem item in CoreUI.Instance.RoomsPanel.CheckedRooms)
                    sw.WriteLine(item.SubItems[1].Text);

                sw.Flush();
                sw.Close();

                sw = new StreamWriter("savedMobs.ini");
                foreach (ListViewItem item in CoreUI.Instance.MobsPanel.CheckedMobs)
                    sw.WriteLine(item.SubItems[1].Text);

                sw.Flush();
                sw.Close();

                sw = new StreamWriter("savedRaids.ini");
                foreach (ListViewItem item in CoreUI.Instance.RaidsPanel.CheckedRaids)
                    sw.WriteLine(item.SubItems[1].Text);

                sw.Flush();
                sw.Close();
            }
            catch
            {
            }
        }

        internal static void Get()
        {
            if (!File.Exists("savedMobs.ini")
                ||  !File.Exists("savedRooms.ini") || !File.Exists("savedRaids.ini"))
            {
                return;
            }

            FileStream fs = null;

            try
            {
                string input;

                StreamReader sr = new StreamReader("savedRooms.ini");
                while ((input = sr.ReadLine()) != null)
                {
                    foreach (ListViewItem item in CoreUI.Instance.RoomsPanel.Rooms)
                    {
                        if (item.SubItems[1].Text == input)
                        {
                            item.Checked = true;
                            break;
                        }
                    }
                }
                sr.Close();

                sr = new StreamReader("savedMobs.ini");
                while ((input = sr.ReadLine()) != null)
                {
                    foreach (ListViewItem item in CoreUI.Instance.MobsPanel.Mobs)
                    {
                        if (item.SubItems[1].Text == input)
                        {
                            item.Checked = true;
                            break;
                        }
                    }
                }
                sr.Close();

                sr = new StreamReader("savedRaids.ini");
                while ((input = sr.ReadLine()) != null)
                {
                    foreach (ListViewItem item in CoreUI.Instance.RaidsPanel.Raids)
                    {
                        if (item.SubItems[1].Text == input)
                        {
                            item.Checked = true;
                            break;
                        }
                    }
                }
                sr.Close();
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
        }
    }
}