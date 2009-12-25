using System.IO;
using System.Windows.Forms;
using DCT.UI;

namespace DCT.Settings
{
    internal class IniWriter
    {
        private const string STR_SavedSpawnsini = "savedSpawns.ini";
        private const string STR_SavedRaidsini = "savedRaids.ini";
        private const string STR_SavedMobsini = "savedMobs.ini";
        private const string STR_SavedRoomsini = "savedRooms.ini";

        internal static void Save()
        {
            try
            {
                StreamWriter sw = new StreamWriter(STR_SavedRoomsini);
                foreach (ListViewItem item in CoreUI.Instance.RoomsPanel.CheckedRooms)
                    sw.WriteLine(item.SubItems[1].Text);

                sw.Flush();
                sw.Close();

                sw = new StreamWriter(STR_SavedMobsini);
                foreach (ListViewItem item in CoreUI.Instance.MobsPanel.CheckedMobs)
                    sw.WriteLine(item.SubItems[1].Text);

                sw.Flush();
                sw.Close();

                sw = new StreamWriter(STR_SavedRaidsini);
                foreach (ListViewItem item in CoreUI.Instance.RaidsPanel.CheckedRaids)
                    sw.WriteLine(item.SubItems[1].Text);

                sw.Flush();
                sw.Close();

                sw = new StreamWriter(STR_SavedSpawnsini);
                foreach (ListViewItem item in CoreUI.Instance.SpawnsPanel.CheckedSpawns)
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
            StreamReader sr = null;
            try
            {
                string input;

                if (File.Exists(STR_SavedRoomsini))
                {
                    sr = new StreamReader(STR_SavedRoomsini);
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
                }

                if (File.Exists(STR_SavedMobsini))
                {
                    sr = new StreamReader(STR_SavedMobsini);
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
                }

                if (File.Exists(STR_SavedRaidsini))
                {
                    sr = new StreamReader(STR_SavedRaidsini);
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

                if (File.Exists(STR_SavedSpawnsini))
                {
                    sr = new StreamReader(STR_SavedSpawnsini);
                    while ((input = sr.ReadLine()) != null)
                    {
                        foreach (ListViewItem item in CoreUI.Instance.SpawnsPanel.Spawns)
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
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }
        }
    }
}