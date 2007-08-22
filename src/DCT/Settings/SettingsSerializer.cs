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
            FileStream fs = new FileStream("prefs.dat", FileMode.OpenOrCreate, FileAccess.Write);

            Hashtable ht = new Hashtable();
            ht.Add("mobFilters", UserEditable.MobFilters);
            ht.Add("useVault", UserEditable.UseVault);
            ht.Add("alertQuests", UserEditable.AlertQuests);
            ht.Add("autoTrain", UserEditable.AutoTrain);
            ht.Add("filterMobs", UserEditable.FilterMobs);
            ht.Add("pauseAt", UserEditable.PauseAt);
            ht.Add("lvlLimit", UserEditable.LvlLimit);
            ht.Add("lvlLimitMin", UserEditable.LvlLimitMin);
            ht.Add("delay", UserEditable.Delay);
            ht.Add("threadBatchSize", UserEditable.MaxThreads);
            ht.Add("rageLimit", UserEditable.RageLimit);
            ht.Add("stopAtRage", UserEditable.StopAtRage);
            ht.Add("Timeout", UserEditable.Timeout);
            ht.Add("attackPause", UserEditable.AttackPause);
            ht.Add("useTimer", UserEditable.UseCountdownTimer);
            ht.Add("useHourTimer", UserEditable.UseHourTimer);
            ht.Add("totalEXPG", (Globals.ExpGainedTotal + Globals.ExpGained));
            ht.Add("variance", UserEditable.Variance);
            ht.Add("returnToStart", UserEditable.ReturnToStart);
            ht.Add("raidInterval", UserEditable.RaidInterval);
            ht.Add("cycleInterval", UserEditable.CycleInterval);
            ht.Add("lastUsername", UserEditable.LastUsername);
            ht.Add("lastPassword", UserEditable.LastPassword);

            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, ht);
            fs.Close();

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
            catch (InvalidOperationException)
            {
            }
        }

        internal static void Get()
        {
            if (!File.Exists("prefs.dat"))
            {
                return;
            }

            FileStream fs = null;

            try
            {
                fs = new FileStream("prefs.dat", FileMode.Open);

                BinaryFormatter formatter = new BinaryFormatter();
                Hashtable ht = (Hashtable) formatter.Deserialize(fs);

                foreach (DictionaryEntry de in ht)
                {
                    switch ((string) de.Key)
                    {
                        case "mobFilters":
                            UserEditable.MobFilters = (string[]) de.Value;
                            break;
                        case "useVault":
                            UserEditable.UseVault = (bool) de.Value;
                            break;
                        case "alertQuests":
                            UserEditable.AlertQuests = (bool) de.Value;
                            break;
                        case "autoTrain":
                            UserEditable.AutoTrain = (bool) de.Value;
                            break;
                        case "filterMobs":
                            UserEditable.FilterMobs = (bool) de.Value;
                            break;
                        case "pauseAt":
                            UserEditable.PauseAt = (int) de.Value;
                            break;
                        case "lvlLimit":
                            UserEditable.LvlLimit = (long) de.Value;
                            break;
                        case "lvlLimitMin":
                            UserEditable.LvlLimitMin = (long) de.Value;
                            break;
                        case "delay":
                            UserEditable.Delay = (int) de.Value;
                            break;
                        case "threadBatchSize":
                            UserEditable.MaxThreads = (int) de.Value;
                            break;
                        case "rageLimit":
                            UserEditable.RageLimit = (int) de.Value;
                            break;
                        case "stopAtRage":
                            UserEditable.StopAtRage = (int) de.Value;
                            break;
                        case "Timeout":
                            UserEditable.Timeout = (long) de.Value;
                            break;
                        case "attackPause":
                            UserEditable.AttackPause = (bool) de.Value;
                            break;
                        case "useTimer":
                            UserEditable.UseCountdownTimer = (bool) de.Value;
                            break;
                        case "useHourTimer":
                            UserEditable.UseHourTimer = (bool)de.Value;
                            break;
                        case "totalEXPG":
                            Globals.ExpGainedTotal = (long) de.Value;
                            break;
                        case "variance":
                            UserEditable.Variance = (bool) de.Value;
                            break;
                        case "returnToStart":
                            UserEditable.ReturnToStart = (bool) de.Value;
                            break;
                        case "raidInterval":
                            UserEditable.RaidInterval = (int) de.Value;
                            break;
                        case "cycleInterval":
                            UserEditable.CycleInterval = (int) de.Value;
                            break;
                        case "lastUsername":
                            UserEditable.LastUsername = (string) de.Value;
                            break;
                        case "lastPassword":
                            UserEditable.LastPassword = (string) de.Value;
                            break;
                    }
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