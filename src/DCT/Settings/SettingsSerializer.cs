using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using DCT.UI;

namespace DCT.Settings
{
    internal class SettingsSerializer
    {
        internal static void Save()
        {
            try
            {
                StreamWriter sw = new StreamWriter("savedRooms.ini");
                foreach (ListViewItem item in CoreUI.Instance.RoomsView.CheckedItems)
                    sw.WriteLine(item.SubItems[1].Text);

                sw.Flush();
                sw.Close();

                sw = new StreamWriter("savedMobs.ini");
                foreach (ListViewItem item in CoreUI.Instance.MobsView.CheckedItems)
                    sw.WriteLine(item.SubItems[1].Text);

                sw.Flush();
                sw.Close();

                sw = new StreamWriter("savedRaids.ini");
                foreach (ListViewItem item in CoreUI.Instance.AdventuresView.CheckedItems)
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
                if (File.Exists("prefs.dat"))
                {
                    fs = new FileStream("prefs.dat", FileMode.Open);

                    BinaryFormatter formatter = new BinaryFormatter();
                    Hashtable ht = (Hashtable)formatter.Deserialize(fs);

                    foreach (DictionaryEntry de in ht)
                    {
                        switch ((string)de.Key)
                        {
                            case "mobFilters":
                                CoreUI.Instance.Settings.MobFilters = (string[])de.Value;
                                break;
                            case "alertQuests":
                                CoreUI.Instance.Settings.AlertQuests = (bool)de.Value;
                                break;
                            case "autoTrain":
                                CoreUI.Instance.Settings.AutoTrain = (bool)de.Value;
                                break;
                            case "filterMobs":
                                CoreUI.Instance.Settings.FilterMobs = (bool)de.Value;
                                break;
                            case "lvlLimit":
                                CoreUI.Instance.Settings.LvlLimit = (long)de.Value;
                                break;
                            case "lvlLimitMin":
                                CoreUI.Instance.Settings.LvlLimitMin = (long)de.Value;
                                break;
                            case "delay":
                                CoreUI.Instance.Settings.Delay = (int)de.Value;
                                break;
                            case "threadBatchSize":
                                CoreUI.Instance.Settings.MaxThreads = (int)de.Value;
                                break;
                            case "rageLimit":
                                CoreUI.Instance.Settings.RageLimit = (int)de.Value;
                                break;
                            case "stopAtRage":
                                CoreUI.Instance.Settings.StopAtRage = (int)de.Value;
                                break;
                            case "Timeout":
                                CoreUI.Instance.Settings.Timeout = (long)de.Value;
                                break;
                            case "useTimer":
                                CoreUI.Instance.Settings.UseCountdownTimer = (bool)de.Value;
                                break;
                            case "useHourTimer":
                                CoreUI.Instance.Settings.UseHourTimer = (bool)de.Value;
                                break;
                            case "totalEXPG":
                                Globals.ExpGainedTotal = (long)de.Value;
                                break;
                            case "variance":
                                CoreUI.Instance.Settings.Variance = (bool)de.Value;
                                break;
                            case "returnToStart":
                                CoreUI.Instance.Settings.ReturnToStart = (bool)de.Value;
                                break;
                            case "raidInterval":
                                CoreUI.Instance.Settings.RaidInterval = (int)de.Value;
                                break;
                            case "cycleInterval":
                                CoreUI.Instance.Settings.CycleInterval = (int)de.Value;
                                break;
                            case "lastUsername":
                                CoreUI.Instance.Settings.LastUsername = (string)de.Value;
                                break;
                            case "lastPassword":
                                CoreUI.Instance.Settings.LastPassword = (string)de.Value;
                                break;
                            case "clearLogs":
                                CoreUI.Instance.Settings.ClearLogs = (bool)de.Value;
                                break;
                            case "attackMode":
                                CoreUI.Instance.Settings.AttackMode = (int)de.Value;
                                break;
                        }
                    }
                    fs.Flush();
                    fs.Close();
                    File.Delete("prefs.dat");
                }

                string input;

                StreamReader sr = new StreamReader("savedRooms.ini");
                while ((input = sr.ReadLine()) != null)
                {
                    foreach (ListViewItem item in CoreUI.Instance.RoomsView.Items)
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
                    foreach (ListViewItem item in CoreUI.Instance.MobsView.Items)
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
                    foreach (ListViewItem item in CoreUI.Instance.AdventuresView.Items)
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