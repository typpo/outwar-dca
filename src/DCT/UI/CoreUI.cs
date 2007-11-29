using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using DCT.Outwar;
using DCT.Outwar.World;
using DCT.Pathfinding;
using DCT.Protocols.Http;
using DCT.Protocols.IRC;
using DCT.Settings;
using DCT.Threading;
using DCT.Util;
using Version=DCT.Security.Version;

namespace DCT.UI
{
    internal partial class CoreUI : Form
    {
        private static CoreUI mInstance;
        internal static CoreUI Instance
        {
            get { return mInstance; }
        }

        private string mChanges;
        internal string Changes
        {
            set { mChanges = value; }
        }

        internal ChatUI Irc
        {
            get { return irc; }
        }

        private AccountsEngine mAccounts;
        internal AccountsEngine Accounts
        {
            get { return mAccounts; }
        }

        private RaidsEngine mRaidsEngine;
        internal RaidsEngine RaidsEngine
        {
            get { return mRaidsEngine; }
        }

        private CountDownTimer mCountdownTimer;
        private AttackingType mCountdownType;

        private delegate void ButtonClickHandler(object sender, EventArgs e);

        internal CoreUI()
        {
            irc = new ChatUI(); // VS always removes this from initializecomponent...
            InitializeComponent();
            mAccounts = new AccountsEngine();
            mRaidsEngine = new RaidsEngine(mAccounts);
            mInstance = this;

            this.Text = "Typpo's DC Tool - [www.typpo.us] - v" + Version.Id;

            foreach (string s in Server.NamesList)
            {
                ListViewGroup grp = new ListViewGroup(s);
                lvAccounts.Groups.Add(grp);
            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            StartDialog ff = new StartDialog();
            ff.ShowDialog();
            ff.Dispose();
            if (Globals.Terminate)
            {
                Application.Exit();
            }

            BuildViews();
            SettingsSerializer.Get();
            SyncSettings();
            irc.Init();
            Log("Started.");
            LogAttack("No attacks yet...");
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Globals.AttackOn = false;
            Globals.AttackMode = false;
            Globals.Terminate = true;

            if (lvPathfind.Items.Count > 0)
            {
                SettingsSerializer.Save();
            }

            Application.Exit();
            Process.GetCurrentProcess().Kill();
        }

        private void chkVault_CheckedChanged(object sender, EventArgs e)
        {
            UserEditable.UseVault = chkVault.Checked;
        }

        private void chkAttackPause_CheckedChanged(object sender, EventArgs e)
        {
            UserEditable.AttackPause = chkAttackPause.Checked;
        }

        private void cmbPause_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbPause.Text)
            {
                case "Both":
                    UserEditable.PauseAt = 2;
                    break;
                case "Vault":
                    UserEditable.PauseAt = 1;
                    break;
                case "Pack":
                    UserEditable.PauseAt = 0;
                    break;
            }
        }

        private void numLevel_ValueChanged(object sender, EventArgs e)
        {
            int tmp = (int)numLevel.Value;
            UserEditable.LvlLimit = tmp;
            if (tmp <= UserEditable.LvlLimitMin)
            {
                tmp = (int)(UserEditable.LvlLimitMin + 1);
                numLevel.Value = tmp;
            }
        }

        private void numLevelMin_ValueChanged(object sender, EventArgs e)
        {
            int tmp = (int)numLevelMin.Value;
            UserEditable.LvlLimitMin = tmp;
            if (tmp >= UserEditable.LvlLimit)
            {
                tmp = (int)(UserEditable.LvlLimit - 1);
                numLevelMin.Value = tmp;
            }
        }

        private void numRageStop_ValueChanged(object sender, EventArgs e)
        {
            UserEditable.StopAtRage = (int)numRageStop.Value;
        }

        private void numRageLimit_ValueChanged(object sender, EventArgs e)
        {
            UserEditable.RageLimit = (int)numRageLimit.Value;
        }

        private void numTimer_ValueChanged(object sender, EventArgs e)
        {
            UserEditable.CycleInterval = (int)numCountdown.Value;
            if (mCountdownTimer != null && UserEditable.UseCountdownTimer)
            {
                mCountdownTimer.CurrentCountdown = ((int)numCountdown.Value) * 60;
            }
        }

        private void lnkStartCountdown_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (mAccounts.Count < 1)
            {
                MessageBox.Show("You need to login before setting a timer.", "Start Timer", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            else if (!UserEditable.UseCountdownTimer && !UserEditable.UseHourTimer)
            {
                MessageBox.Show("Choose a timer.", "Start Timer", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            if (optCountdownSingle.Checked)
                Countdown(AttackingType.Single);
            else if (optCountdownMulti.Checked)
                Countdown(AttackingType.Multi);
            else
                Countdown(AttackingType.Mobs);
        }

        private delegate void CountdownHandler(AttackingType type);

        internal void Countdown(AttackingType type)
        {
            // pass to UI thread
            if (InvokeRequired)
            {
                Invoke(new CountdownHandler(Countdown), type);
                return;
            }

            int countFor;
            if (UserEditable.UseCountdownTimer)
            {
                countFor = ((int)numCountdown.Value) * 60;
            }
            else
            {
                countFor = SecondsUntilHour();
            }

            mCountdownTimer = new CountDownTimer(countFor);

            mCountdownTimer.Interval = 1000;
            mCountdownTimer.Tick += new EventHandler(t_Tick);
            mCountdownTimer.Started += new EventHandler(t_Started);
            mCountdownTimer.Stopped += new EventHandler(t_Stopped);
            mCountdownType = type;

            mCountdownTimer.Start();

            if (UserEditable.ClearLogs)
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
        }

        private int SecondsUntilHour()
        {
            return (61 - DateTime.Now.Minute) * 60;
        }

        private void t_Stopped(object sender, EventArgs e)
        {
            if (Globals.AttackMode || !(UserEditable.UseCountdownTimer || UserEditable.UseHourTimer))
            {
                return;
            }

            Toggle(false);
            ToggleAttack(true);

            switch (mCountdownType)
            {
                case AttackingType.Single:
                    AttackArea();
                    break;
                case AttackingType.Multi:
                    AttackAreas();
                    break;
                case AttackingType.Mobs:
                    AttackMobs();
                    break;
            }
        }

        private void t_Started(object sender, EventArgs e)
        {
            if (irc.StatusLabel.Text.StartsWith("Not"))
            {
                Log("E: Not connected to authorization server.");
                Application.Exit();
                return;
            }

            UpdateCountdown();
        }

        private void t_Tick(object sender, EventArgs e)
        {
            UpdateCountdown();
        }

        private void UpdateCountdown()
        {
            int s = mCountdownTimer.CurrentCountdown;
            string s2 = (s % 60).ToString();
            lblTimeLeft.Text = "Time left: " + (s / 60) + ":" + (s2.Length == 1 ? "0" + s2 : s2);

            if (!UserEditable.UseCountdownTimer && !UserEditable.UseHourTimer)
            {
                mCountdownTimer.Stop();
            }
        }

        private void lnkMobLoad_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string s = FileIO.LoadFileToString("Import Mobs");
            if (s == null)
            {
                return;
            }

            foreach (string l in s.Split(new char[] { ',', '\n', '\r', ';', '\t' }))
            {
                SelectMobsByName(l);
            }
        }

        private void btnPotionMobsSelect_Click(object sender, EventArgs e)
        {
            List<string> check = new List<string>();
            switch (cmbPotionMobs.Text.ToLower())
            {
                case "kinetic":
                    check.Add("Deadly Ripscale");
                    check.Add("Poison Drake");
                    check.Add("Enraged Centaur");
                    check.Add("Earth Troll");
                    check.Add("Evil Sherpa");
                    if (MessageBox.Show("Do you want to select Entropic Horrors and Belligerent Zombies too (lower drop rates)?", "Select Potion Mobs", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        check.Add("Entropic Horror");
                        check.Add("Belligerent Zombie");
                    }
                    break;
                case "fire":
                    check.Add("Haunter");
                    check.Add("Forgotten Warrior");
                    check.Add("Lost Demon");
                    break;
                case "holy":
                    check.Add("Fallen Angel");
                    check.Add("Apparitional Veteran");
                    check.Add("Rabid Wallabee");
                    break;
                case "shadow":
                    check.Add("Caustic Corpse");
                    check.Add("Infuriated Savage");
                    check.Add("Choleric Ancient");
                    break;
                case "arcane":
                    check.Add("Spectral Warrior");
                    check.Add("Ancient Deserter");
                    check.Add("Bearded Recluse");
                    break;
                default:
                    MessageBox.Show("Choose a preset option.", "Select Potion Mobs", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
            }
            foreach (string m in check)
            {
                SelectMobsByName(m);
            }
        }

        private void lnkLoadRooms_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string s = FileIO.LoadFileToString("Import Rooms");
            if (s == null)
            {
                return;
            }
            foreach (string l in s.Split(new char[] { ',', '\n', '\r', ';', '\t' }))
            {
                SelectRoomsByName(l);
            }
        }

        private void SelectRoomsByName(string name)
        {
            foreach (ListViewItem item in lvPathfind.Items.Find(name, false))
            {
                item.Checked = true;
            }
        }

        private void SelectMobsByName(string name)
        {
            foreach (ListViewItem item in lvMobs.Items.Find(name, false))
            {
                item.Checked = true;
            }
        }

        private void chkAutoTrain_CheckedChanged(object sender, EventArgs e)
        {
            UserEditable.AutoTrain = chkAutoTrain.Checked;
        }

        private void optQuestsAuto_CheckedChanged(object sender, EventArgs e)
        {
            if (optQuestsAuto.Checked)
            {
                UserEditable.AlertQuests = false;
                UserEditable.AutoQuest = true;
            }
            else if (optQuestsAlert.Checked)
            {
                UserEditable.AlertQuests = true;
                UserEditable.AutoQuest = false;
            }
            else
            {
                UserEditable.AlertQuests = false;
                UserEditable.AutoQuest = false;
            }
        }

        private void optQuestsAlert_CheckedChanged(object sender, EventArgs e)
        {
            optQuestsAuto_CheckedChanged(null, null);
        }

        private void optQuestsNothing_CheckedChanged(object sender, EventArgs e)
        {
            optQuestsAuto_CheckedChanged(null, null);
        }

        private void numTimeout_ValueChanged(object sender, EventArgs e)
        {
            UserEditable.Timeout = (int)numTimeout.Value;
        }

        private void numThreadDelay_ValueChanged(object sender, EventArgs e)
        {
            UserEditable.Delay = (int)numThreadDelay.Value;
        }

        private void chkVariance_CheckedChanged(object sender, EventArgs e)
        {
            UserEditable.Variance = chkVariance.Checked;
        }

        private void chkFilter_CheckedChanged(object sender, EventArgs e)
        {
            txtFilters.Enabled = chkFilter.Checked;
            UserEditable.FilterMobs = chkFilter.Checked;
        }

        private void txtFilters_TextChanged(object sender, EventArgs e)
        {
            UserEditable.MobFilters =
                txtFilters.Text.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

        private void btnFilterSave_Click(object sender, EventArgs e)
        {
            Toggle(false);

            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "All files (*.*)|*.*";
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {
                    StreamWriter wText = new StreamWriter(myStream);

                    wText.Write(txtFilters.Text);

                    wText.Flush();
                    wText.Close();
                    myStream.Close();
                }
            }

            Toggle(true);
        }

        private void btnFilterLoad_Click(object sender, EventArgs e)
        {
            Toggle(false);

            OpenFileDialog DlgOpen = new OpenFileDialog();

            DlgOpen.Filter = "Any File|*.*";
            DlgOpen.Title = "Select a User Account File";

            if (DlgOpen.ShowDialog() == DialogResult.OK)
            {
                StreamReader myFile = new StreamReader(DlgOpen.FileName);

                string input;
                while ((input = myFile.ReadLine()) != null)
                {
                    txtFilters.Text = input + "\r\n";
                }

                myFile.Close();
            }

            Toggle(true);
        }

        private void lvAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            SyncMainConnection();
        }

        private void btnAttackStart_Click(object sender, EventArgs e)
        {
            if (optCountdownSingle.Checked)
                AttackArea();
            else if (optCountdownMulti.Checked)
                AttackAreas();
            else
                AttackMobs();
        }

        private void btnAttackStop_Click(object sender, EventArgs e)
        {
            StopAttacking(true);
        }

        private delegate void StopAttackingHandler(bool timeroff);

        internal void StopAttacking(bool timeroff)
        {
            if (InvokeRequired)
            {
                Invoke(new StopAttackingHandler(StopAttacking), timeroff);
                return;
            }

            if (Globals.AttackOn || Globals.AttackMode)
            {
                Globals.AttackOn = false;
                ToggleAttack(false);

                if (timeroff)
                {
                    chkCountdownTimer.Checked = false;
                    chkHourTimer.Checked = false;
                }
            }
        }

        private void btnPathfind_Click(object sender, EventArgs e)
        {
            if (lvAccounts.CheckedItems.Count < 1)
            {
                Log("E: Check the accounts you want to move");
                return;
            }

            Toggle(false);

            int room;
            if (optPathfindID.Checked)
            {
                room = (int)numPathfindId.Value;
            }
            else // take from listview
            {
                room = int.Parse(lvPathfind.FocusedItem.SubItems[1].Text);
            }

            InvokePathfind(room);
        }

        private void btnAdventuresGo_Click(object sender, EventArgs e)
        {
            if (lvAccounts.CheckedItems.Count < 1)
            {
                Log("E: Check the accounts you want to move.");
            }
            else if (lvAdventures.FocusedItem == null)
            {
                Log("E: Choose an adventure to move to.");
            }
            else
            {
                Toggle(false);
                optPathfindID.Checked = true;
                int room = int.Parse(lvAdventures.FocusedItem.SubItems[1].Text);

                InvokeAdventures(room);
            }
        }

        private void btnTrain_Click(object sender, EventArgs e)
        {
            if (lvAccounts.CheckedItems.Count < 1)
            {
                Log("E: Check the accounts you want to move");
                return;
            }

            Toggle(false);

            foreach (int index in lvAccounts.CheckedIndices)
            {
                mAccounts[index].Mover.RefreshRoom();
                mAccounts[index].Mover.ReturnToStartHandler.SetOriginal();
                ThreadEngine.DefaultInstance.Enqueue(mAccounts[index].Mover.Train);
            }

            // TODO this needs to go
            ThreadEngine.DefaultInstance.ProcessAll();

            if (chkTrainReturn.Checked)
            {
                foreach (int index in lvAccounts.CheckedIndices)
                {
                    InvokeReturn(index);
                }
            }

            Toggle(true);
        }

        private void SyncMainConnection()
        {
            if (lvAccounts.CheckedIndices.Count > 0)
            {
                mAccounts.SetMain(lvAccounts.CheckedIndices[0]);
            }
            else if (lvAccounts.FocusedItem != null)
            {
                mAccounts.SetMain(lvAccounts.FocusedItem.Index);
            }

            UpdateDisplay();
        }

        private void BuildViews()
        {
            ListViewItem tmp;

            foreach (MappedRoom rm in Pathfinder.Rooms)
            {
                if (rm != null)
                {
                    tmp = new ListViewItem(new string[] { rm.Name, rm.Id.ToString() });
                    tmp.Name = rm.Name;
                    lvPathfind.Items.Add(tmp);
                }
            }

            foreach (MappedMob mb in Pathfinder.Mobs)
            {
                if (mb != null)
                {
                    tmp = new ListViewItem(
                            new string[]
                                {
                                    mb.Name, mb.Id.ToString(), mb.Room.ToString(),
                                    mb.Level.ToString(), mb.Rage.ToString()
                                });
                    tmp.Name = mb.Name;
                    lvMobs.Items.Add(tmp);
                }
            }

            SortedList<string, int> l = Pathfinder.Adventures;
            for (int i = 0; i < l.Count; i++)
            {
                tmp = new ListViewItem(
                        new string[]
                            {
                                l.Keys[i], l.Values[i].ToString()
                            });
                tmp.Name = l.Keys[i];
                lvAdventures.Items.Add(tmp);
            }
        }

        private int mSortColumn;

        private void lvPathfind_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            //lvPathfind.ListViewItemSorter = new ListViewItemComparer(e.Column);
            //lvPathfind.Sort();

            // Determine whether the column is the same as the last column clicked.
            if (e.Column != mSortColumn)
            {
                // Set the sort column to the new column.
                mSortColumn = e.Column;
                // Set the sort order to ascending by default.
                lvPathfind.Sorting = SortOrder.Ascending;
            }
            else
            {
                // Determine what the last sort order was and change it.
                if (lvPathfind.Sorting == SortOrder.Ascending)
                    lvPathfind.Sorting = SortOrder.Descending;
                else
                    lvPathfind.Sorting = SortOrder.Ascending;
            }


            // Set the ListViewItemSorter property to a new ListViewItemComparer
            // object.
            this.lvPathfind.ListViewItemSorter = new RoomsViewItemComparer(e.Column,
                                                                           lvPathfind.Sorting);

            // Call the sort method to manually sort.
            lvPathfind.Sort();
        }

        private void lnkUncheckAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (ListViewItem item in lvPathfind.CheckedItems)
            {
                item.Checked = false;
            }
        }

        private void lnkUncheckMobs_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (ListViewItem item in lvMobs.CheckedItems)
            {
                item.Checked = false;
            }
        }

        private int mSortColumn2;

        private void lvMobs_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column != mSortColumn2)
            {
                mSortColumn2 = e.Column;
                lvMobs.Sorting = SortOrder.Ascending;
            }
            else
            {
                if (lvMobs.Sorting == SortOrder.Ascending)
                    lvMobs.Sorting = SortOrder.Descending;
                else
                    lvMobs.Sorting = SortOrder.Ascending;
            }


            lvMobs.ListViewItemSorter = new MobViewItemComparer(e.Column,
                                                                lvMobs.Sorting);

            lvMobs.Sort();
        }

        internal void UpdateDisplay()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(UpdateDisplay));
                return;
            }

            lblStatus.Text = Globals.AttackMode ? "Attacking" : string.Empty;
            lblMisc.Text = "Experience gained: " + Globals.ExpGained;

            if (mAccounts.MainAccount != null)
            {
                lblExpRage.Text =
                    string.Format("Exp: {0:n0}      Rage: {1:n0}", mAccounts.MainAccount.Exp,
                                  mAccounts.MainAccount.Rage);
                lblExpRage.Left = ((pnlAttack.Right - pnlAttack.Left) / 2) - (lblExpRage.Width / 2);

                int i = mAccounts.Accounts.IndexOf(mAccounts.MainAccount);
                lvAccounts.Items[i].SubItems[0].Text = mAccounts.MainAccount.Name;
                lvAccounts.Items[i].SubItems[1].Text = mAccounts.MainAccount.Socket.Status;
                lvAccounts.Items[i].SubItems[2].Text = mAccounts.MainAccount.Mover.Location == null ? "-" : mAccounts.MainAccount.Mover.Location.Id.ToString();
                lvAccounts.Items[i].SubItems[3].Text = mAccounts.MainAccount.Mover.SavedRooms.Count.ToString();
                lvAccounts.Items[i].SubItems[4].Text = mAccounts.MainAccount.Mover.MobsAttacked.ToString();
                lvAccounts.Items[i].SubItems[5].Text = mAccounts.MainAccount.Mover.ExpGained.ToString();
            }
        }

        private void lnkAccountsCheckAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (ListViewItem item in lvAccounts.Items)
            {
                item.Checked = true;
            }
        }

        private void lnkAccountsCheckNone_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (ListViewItem item in lvAccounts.Items)
            {
                item.Checked = false;
            }
        }

        private void lnkAccountsInvert_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (ListViewItem item in lvAccounts.Items)
            {
                item.Checked = !item.Checked;
            }
        }

        internal delegate void UpdateProgressbarHandler(int val, int max);

        internal void UpdateProgressbar(int val, int max)
        {
            if (InvokeRequired)
            {
                Invoke(new UpdateProgressbarHandler(UpdateProgressbar), val, max);
                return;
            }
            try
            {
                if (pgr.Maximum != max)
                {
                    pgr.Maximum = max;
                }
                pgr.Value = val;
            }
            catch (ObjectDisposedException)
            {
                // avoids problems on close
            }
        }

        private delegate void ToggleHandler(bool on);
        internal void Toggle(bool on)
        {
            if (irc.StatusLabel.Text.StartsWith("Not"))
            {
                Log("E: Not connected to authorization server.");
                Application.Exit();
                return;
            }

            if (InvokeRequired)
            {
                Invoke(new ToggleHandler(Toggle), on);
                return;
            }
            // ACCOUNTS
            lnkAccountsCheckAll.Enabled = on;
            lnkAccountsCheckNone.Enabled = on;
            lnkAccountsInvert.Enabled = on;

            // ADVENTURES
            btnAdventuresGo.Enabled = on;

            // MOVE TAB
            btnPathfind.Enabled = on;
            optPathfindID.Enabled = on;
            optPathfindChoose.Enabled = on;
            numPathfindId.Enabled = on;

            // TRAINING TAB
            btnTrain.Enabled = on;
            chkTrainReturn.Enabled = on;
        }

        internal void ToggleAttack(bool on)
        {
            if (InvokeRequired)
            {
                Invoke(new ToggleHandler(ToggleAttack), on);
                return;
            }

            Toggle(!on);

            btnAttackStart.Enabled = !on;
            btnAttackStop.Enabled = on;
            Globals.AttackMode = on;

            if (mCountdownTimer != null && on)
            {
                mCountdownTimer.Stop();
                lblTimeLeft.Text = "Time Left: N/A";
            }

            if (irc.StatusLabel.Text.StartsWith("Not"))
            {
                Log("E: Not connected to authorization server.");
                Application.Exit();
                return;
            }
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

        private void SyncSettings()
        {
            clearLogsPeriodicallyToolStripMenuItem.Checked = UserEditable.ClearLogs;

            txtUsername.Text = UserEditable.LastUsername;
            txtPassword.Text = UserEditable.LastPassword;

            foreach (string str in UserEditable.MobFilters)
            {
                txtFilters.Text = str + "\r\n";
            }

            chkAttackPause.Checked = UserEditable.AttackPause;
            chkVault.Checked = UserEditable.UseVault;

            numRageStop.Value = UserEditable.StopAtRage;
            numRageLimit.Value = UserEditable.RageLimit;
            chkReturnToStart.Checked = UserEditable.ReturnToStart;
            chkCountdownTimer.Checked = UserEditable.UseCountdownTimer;
            chkHourTimer.Checked = UserEditable.UseHourTimer;
            numCountdown.Value = UserEditable.CycleInterval;

            switch (UserEditable.AttackMode)
            {
                case 0: optCountdownSingle.Checked = true;
                    break;
                case 1: optCountdownMulti.Checked = true;
                    break;
                case 2: optCountdownMobs.Checked = true;
                    break;
                default: throw new Exception("Your settings are corrupt; no such attack mode.");
            }

            chkAutoJoin.Checked = UserEditable.AutoJoin;
            numRaidIntervalMin.Value = UserEditable.RaidInterval;

            if (UserEditable.AlertQuests)
                optQuestsAlert.Checked = true;
            else if (UserEditable.AutoQuest)
                optQuestsAuto.Checked = true;
            else
                optQuestsNothing.Checked = true;

            chkAutoTrain.Checked = UserEditable.AutoTrain;

            chkFilter.Checked = UserEditable.FilterMobs;
            if (UserEditable.FilterMobs)
                txtFilters.Enabled = true;

            if (UserEditable.PauseAt == 0)
                cmbPause.Text = "Pack";
            else if (UserEditable.PauseAt == 1)
                cmbPause.Text = "Vault";
            else
                cmbPause.Text = "Both";

            numLevel.Value = UserEditable.LvlLimit;
            numLevelMin.Value = UserEditable.LvlLimitMin;
            numRageStop.Value = UserEditable.StopAtRage;

            try
            {
                numThreadDelay.Value = UserEditable.Delay;
            }
            catch (ArgumentOutOfRangeException)
            {
                UserEditable.Delay = 0;
                numThreadDelay.Value = UserEditable.Delay;
            }

            chkVariance.Checked = UserEditable.Variance;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Program by Typpo (www.typpo.us)."
                + "\n\nThis particular copy of the program has gained you "
                + (Globals.ExpGained + Globals.ExpGainedTotal) + " EXP."
                + "\n\nThis instance has an security image accuracy of " + Globals.SecRight + ":"
                + Globals.SecWrong + "."
                +
                (mAccounts.MainAccount == null
                     ? string.Empty
                     : "\n\n" + mAccounts.MainAccount.Name + " has been attacking mobs for an average of "
                       +
                       (mAccounts.MainAccount.Mover.MobsAttacked < 1
                            ? "N/A"
                            : (mAccounts.MainAccount.Mover.ExpGained
                               / mAccounts.MainAccount.Mover.MobsAttacked).ToString()) + " exp per attack.")
                , "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void changesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form f = new Form();
            f.Text = "Build History";
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Width = this.Width - (this.Width / 3);
            f.Height = this.Height - (this.Height / 5);
            TextBox t = new TextBox();
            f.Controls.Add(t);
            t.Dock = DockStyle.Fill;
            t.Multiline = true;
            t.Text = mChanges.Replace("\n", "\r\n");
            t.ScrollBars = ScrollBars.Both;
            t.ReadOnly = true;
            f.Show();
        }

        private void exportLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Toggle(false);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" *** MAIN LOG\r\n");
            foreach (string text in lstLog.Items)
                sb.AppendLine(text);

            sb.AppendLine("\r\n\r\n *** ATTACK LOG\r\n");
            foreach (string text in lstAttacks.Items)
                sb.AppendLine(text);

            FileIO.SaveFileFromString("Export Log", "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
                                      "DCT Log " + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second, sb.ToString());

            Toggle(true);
        }

        private void clearLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lstLog.Items.Clear();
            lstAttacks.Items.Clear();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void openInBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("http://" + mAccounts.MainAccount.Server + ".outwar.com/?rg_sess_id=" + mAccounts.RgSessId);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new ButtonClickHandler(btnLogout_Click), sender, e);
                return;
            }

            Log("Logging out...");
            StopAttacking(false);
            HttpSocket.DefaultInstance.Get("http://outwar.com/index.php?cmd=logout");
            HttpSocket.DefaultInstance.Cookie = string.Empty;
            mAccounts.Accounts.Clear();
            lvAccounts.Items.Clear();
            btnLogin.Enabled = true;
            btnLogout.Enabled = false;
            btnRefresh.Enabled = false;
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && btnLogin.Enabled)
                Login();
        }

        internal void btnRefresh_Click(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new ButtonClickHandler(btnRefresh_Click), sender, e);
                return;
            }

            if (btnLogin.Enabled)
            {
                return;
            }

            btnLogout_Click("refresh", null);
            btnLogin_Click(null, null);
        }

        private void chkReturnToStart_CheckedChanged(object sender, EventArgs e)
        {
            UserEditable.ReturnToStart = chkReturnToStart.Checked;
        }

        private void chkAutoJoin_CheckedChanged(object sender, EventArgs e)
        {
            UserEditable.AutoJoin = chkAutoJoin.Checked;

            if (UserEditable.AutoJoin && lvAccounts.CheckedIndices.Count != 0)
            {
                ProcessRaidsThreaded();
            }
        }

        internal void ProcessRaidsThreaded()
        {
            ThreadEngine.DefaultInstance.Do(ProcessRaids);
        }

        private void ProcessRaids()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(ProcessRaids));
                return;
            }

            List<Account> a = new List<Account>();
            foreach (int index in lvAccounts.CheckedIndices)
            {
                a.Add(mAccounts[index]);
            }

            foreach (ListViewItem i in lvAdventures.CheckedItems)
            {
                mRaidsEngine.Process(i.Text);
            }
            
        }

        private void numRaidInterval_ValueChanged(object sender, EventArgs e)
        {
            UserEditable.RaidInterval = (int)numRaidIntervalMin.Value;
        }

        // TODO switch to only one global
        private void chkCountdownTimer_CheckedChanged(object sender, EventArgs e)
        {
            bool b = chkCountdownTimer.Checked;
            if (b)
            {
                if (mCountdownTimer != null)
                {
                    mCountdownTimer.CurrentCountdown = ((int)numCountdown.Value) * 60;
                }
                chkHourTimer.Checked = UserEditable.UseHourTimer = !b;

            }
            UserEditable.UseCountdownTimer = b;
        }

        private void chkHourTimer_CheckedChanged(object sender, EventArgs e)
        {
            bool b = chkHourTimer.Checked;
            if (b)
            {
                if (mCountdownTimer != null)
                {
                    mCountdownTimer.CurrentCountdown = SecondsUntilHour();
                }
                chkCountdownTimer.Checked = UserEditable.UseCountdownTimer = !b;

            }
            UserEditable.UseHourTimer = b;
        }

        private void UpdateMobRage()
        {
            int r = 0;
            foreach(ListViewItem i in lvMobs.CheckedItems)
            {
                int t = int.Parse(i.SubItems[4].Text);
                if(t > 0)
                    r += t;
            }
            lblMobRage.Text = "Using rage: " + r;
        }

        private void btnMobRage_Click(object sender, EventArgs e)
        {
            btnMobRage.Enabled = false;
            UpdateMobRage();
            btnMobRage.Enabled = true;
        }

        private void clearLogsPeriodicallyToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            UserEditable.ClearLogs = clearLogsPeriodicallyToolStripMenuItem.Checked;
        }

        private void optCountdownSingle_CheckedChanged(object sender, EventArgs e)
        {
            if (optCountdownSingle.Checked)
            {
                UserEditable.AttackMode = 0;
            }
        }

        private void optCountdownMulti_CheckedChanged(object sender, EventArgs e)
        {
            if (optCountdownMulti.Checked)
            {
                UserEditable.AttackMode = 1;
            }
        }

        private void optCountdownMobs_CheckedChanged(object sender, EventArgs e)
        {
            if (optCountdownMobs.Checked)
            {
                UserEditable.AttackMode = 2;
            }
        }
    }
}