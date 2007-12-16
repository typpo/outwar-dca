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

        internal MainPanel MainPanel
        {
            get { return mMainPanel; }
        }

        internal LogPanel LogPanel
        {
            get { return mLogPanel; }
        }

        internal AccountsPanel AccountsPanel
        {
            get { return mAccountsPanel; }
        }

        internal ChatUI ChatPanel
        {
            get { return mChat; }
        }

        internal RoomsPanel RoomsPanel
        {
            get { return mRoomsPanel; }
        }

        internal MobsPanel MobsPanel
        {
            get { return mMobsPanel; }
        }

        internal RaidsPanel RaidsPanel
        {
            get { return mRaidsPanel; }
        }

        private UserEditable mUserEditable;
        internal UserEditable Settings
        {
            get { return mUserEditable; }
        }

        private MainPanel mMainPanel;
        private AccountsPanel mAccountsPanel;
        private LogPanel mLogPanel;
        private AttackPanel mAttackPanel;
        private FiltersPanel mFiltersPanel;
        private RoomsPanel mRoomsPanel;
        private MobsPanel mMobsPanel;
        private RaidsPanel mRaidsPanel;
        private TrainPanel mTrainPanel;
        private QuestsPanel mQuestsPanel;
        private ChatUI mChat;

        internal CoreUI()
        {
            InitializeComponent();

            mAccountsPanel = new AccountsPanel(this);
            mAccountsPanel.Dock = DockStyle.Fill;
            splitLeftRight.Panel1.Controls.Add(mAccountsPanel);

            mLogPanel = new LogPanel();
            mLogPanel.Dock = DockStyle.Fill;
            splitLeftRight2.Panel2.Controls.Add(mLogPanel);

            mAttackPanel = new AttackPanel(this);
            mAttackPanel.Dock = DockStyle.Fill;
            tabs.TabPages[0].Controls.Add(mAttackPanel);

            mMainPanel = new MainPanel(this);
            mMainPanel.Dock = DockStyle.Fill;
            splitLeftRight2.Panel1.Controls.Add(mMainPanel);

            mFiltersPanel = new FiltersPanel(this);
            mFiltersPanel.Dock = DockStyle.Fill;
            tabs.TabPages[1].Controls.Add(mFiltersPanel);

            mRoomsPanel = new RoomsPanel(this);
            mRoomsPanel.Dock = DockStyle.Fill;
            tabs.TabPages[2].Controls.Add(mRoomsPanel);

            mMobsPanel = new MobsPanel(this);
            mMobsPanel.Dock = DockStyle.Fill;
            tabs.TabPages[3].Controls.Add(mMobsPanel);

            mRaidsPanel = new RaidsPanel(this);
            mRaidsPanel.Dock = DockStyle.Fill;
            tabs.TabPages[4].Controls.Add(mRaidsPanel);

            mTrainPanel = new TrainPanel(this);
            mTrainPanel.Dock = DockStyle.Fill;
            tabs.TabPages[5].Controls.Add(mTrainPanel);

            mQuestsPanel = new QuestsPanel(this);
            mQuestsPanel.Dock = DockStyle.Fill;
            tabs.TabPages[6].Controls.Add(mQuestsPanel);

            mChat = new ChatUI(this);
            mChat.Dock = DockStyle.Fill;
            tabs.TabPages[7].Controls.Add(mChat);
            
            mInstance = this;
            mUserEditable = ConfigSerializer.ReadFile("config.xml");

            this.Text = "Typpo's DC Tool - [www.typpo.us] - v" + Version.Id;

            foreach (string s in Server.NamesList)
            {
                ListViewGroup grp = new ListViewGroup(s);
                mAccountsPanel.Groups.Add(grp);
            }
        }

        private void CoreUI_Load(object sender, EventArgs e)
        {
            StartDialog ff = new StartDialog();
            ff.ShowDialog();
            ff.Dispose();
            if (Globals.Terminate)
            {
                Application.Exit();
            }

            BuildViews();
            RegistryUtil.Load();
            IniWriter.Get();
            SyncSettings();
            mMobsPanel.CalcMobRage();
            mChat.Init();

            mLogPanel.Log("Started.");
            mLogPanel.LogAttack("No attacks yet...");
        }

        private void CoreUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            Globals.AttackOn = false;
            Globals.AttackMode = false;
            Globals.Terminate = true;

            if (mRoomsPanel.Rooms.Count > 0)
            {
                RegistryUtil.Save();
                IniWriter.Save();
                ConfigSerializer.WriteFile("config.xml", mUserEditable);
            }

            // clean up notifyicon
            if (mNotifyIcon != null)
            {
                mNotifyIcon.Visible = false;
                mNotifyIcon.Dispose();
                mNotifyIcon = null;
            }

            Application.Exit();
            Process.GetCurrentProcess().Kill();
        }

        private void BuildViews()
        {
            mRoomsPanel.BuildView();
            mMobsPanel.BuildView();
            mRaidsPanel.BuildView();
        }

        internal void UpdateDisplay()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(UpdateDisplay));
                return;
            }

            lblMisc.Text = "Experience gained: " + Globals.ExpGained;

            if (mAccountsPanel.Engine.MainAccount != null)
            {
                mMainPanel.StatusText =
                    string.Format("Exp: {0:n0}      Rage: {1:n0}", mAccountsPanel.Engine.MainAccount.Exp,
                                  mAccountsPanel.Engine.MainAccount.Rage);

                int i = mAccountsPanel.Engine.Accounts.IndexOf(mAccountsPanel.Engine.MainAccount);
                mAccountsPanel.Accounts[i].SubItems[0].Text = mAccountsPanel.Engine.MainAccount.Name;
                mAccountsPanel.Accounts[i].SubItems[1].Text = mAccountsPanel.Engine.MainAccount.Socket.Status;
                mAccountsPanel.Accounts[i].SubItems[2].Text = mAccountsPanel.Engine.MainAccount.Mover.Location == null ? "-" : mAccountsPanel.Engine.MainAccount.Mover.Location.Id.ToString();
                mAccountsPanel.Accounts[i].SubItems[3].Text = mAccountsPanel.Engine.MainAccount.Mover.SavedRooms.Count.ToString();
                mAccountsPanel.Accounts[i].SubItems[4].Text = mAccountsPanel.Engine.MainAccount.Mover.MobsAttacked.ToString();
                mAccountsPanel.Accounts[i].SubItems[5].Text = mAccountsPanel.Engine.MainAccount.Mover.ExpGained.ToString();
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
            if (mChat.StatusLabel.Text.StartsWith("Not"))
            {
                mLogPanel.Log("E: Not connected to authorization server.");
                Application.Exit();
                return;
            }

            if (InvokeRequired)
            {
                Invoke(new ToggleHandler(Toggle), on);
                return;
            }
            // ACCOUNTS
            mAccountsPanel.ChangeAllowed = on;

            // ADVENTURES
            mRaidsPanel.MoveEnabled = on;

            // MOVE TAB
            mRoomsPanel.PathfindEnabled = on;
            
            // TRAINING TAB
            mTrainPanel.TrainEnabled = on;
        }

        internal void ToggleAttack(bool on)
        {
            if (InvokeRequired)
            {
                Invoke(new ToggleHandler(ToggleAttack), on);
                return;
            }

            Toggle(!on);

            mMainPanel.AttackingOn = on;
            Globals.AttackMode = on;

            if (mMainPanel.CountdownTimer != null && on)
            {
                mMainPanel.CountdownTimer.Stop();
                //lblTimeLeft.Text = "Time Left: N/A";
            }

            if (mChat.StatusLabel.Text.StartsWith("Not"))
            {
                mLogPanel.Log("E: Not connected to authorization server.");
                Application.Exit();
                return;
            }
        }

        private void SyncSettings()
        {
            clearLogsPeriodicallyToolStripMenuItem.Checked = CoreUI.Instance.Settings.ClearLogs;

            mAccountsPanel.Username = CoreUI.Instance.Settings.LastUsername;
            mAccountsPanel.Password = CoreUI.Instance.Settings.LastPassword;

            foreach (string str in CoreUI.Instance.Settings.MobFilters)
            {
                mFiltersPanel.FiltersText = str + "\r\n";
            }

            mAttackPanel.StopAtRage = CoreUI.Instance.Settings.StopAtRage;
            mAttackPanel.RageLimit = CoreUI.Instance.Settings.RageLimit;
            mAttackPanel.ReturnToStart = CoreUI.Instance.Settings.ReturnToStart;
            mMainPanel.UseCountdown = CoreUI.Instance.Settings.UseCountdownTimer;
            mMainPanel.UseHourTimer = CoreUI.Instance.Settings.UseHourTimer;
            mMainPanel.CountdownValue = CoreUI.Instance.Settings.CycleInterval;

            mMainPanel.SyncAttackMode();

            if (CoreUI.Instance.Settings.AlertQuests)
                mQuestsPanel.AlertQuest = true;
            else if (CoreUI.Instance.Settings.AutoQuest)
                mQuestsPanel.AutoQuest = true;
            else
                mQuestsPanel.NothingQuest = true;

            mTrainPanel.AutoTrain = CoreUI.Instance.Settings.AutoTrain;

            mFiltersPanel.FiltersEnabled = CoreUI.Instance.Settings.FilterMobs;

            mAttackPanel.LevelMax = CoreUI.Instance.Settings.LvlLimit;
            mAttackPanel.LevelMin = CoreUI.Instance.Settings.LvlLimitMin;
            mAttackPanel.StopAtRage = CoreUI.Instance.Settings.StopAtRage;

            try
            {
                mAttackPanel.ThreadDelay = CoreUI.Instance.Settings.Delay;
            }
            catch (ArgumentOutOfRangeException)
            {
                CoreUI.Instance.Settings.Delay = 0;
                mAttackPanel.ThreadDelay = CoreUI.Instance.Settings.Delay;
            }

            mAttackPanel.Variance = CoreUI.Instance.Settings.Variance;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Program by Typpo (www.typpo.us)."
                + "\n\nThis particular copy of the program has gained you "
                + (Globals.ExpGained + Globals.ExpGainedTotal) + " EXP."
                +
                (mAccountsPanel.Engine.MainAccount == null
                     ? string.Empty
                     : "\n\n" + mAccountsPanel.Engine.MainAccount.Name + " has been attacking mobs for an average of "
                       +
                       (mAccountsPanel.Engine.MainAccount.Mover.MobsAttacked < 1
                            ? "N/A"
                            : (mAccountsPanel.Engine.MainAccount.Mover.ExpGained
                               / mAccountsPanel.Engine.MainAccount.Mover.MobsAttacked).ToString()) + " exp per attack.")
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
            mLogPanel.Export();
            Toggle(true);
        }

        private void clearLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mLogPanel.Clear();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void openInBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mAccountsPanel.Engine.MainAccount == null)
            {
                MessageBox.Show("You haven't logged in on an account yet.", "Open In Browser", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                Process.Start("http://" + mAccountsPanel.Engine.MainAccount.Server + ".outwar.com/?rg_sess_id=" + mAccountsPanel.Engine.RgSessId);
            }
            catch { }   // firefox crash
        }

        private void openTyppousToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("http://typpo.us/");
            }
            catch { }   // firefox crash
        }

        private void clearLogsPeriodicallyToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            CoreUI.Instance.Settings.ClearLogs = clearLogsPeriodicallyToolStripMenuItem.Checked;
        }

        private void CoreUI_ResizeBegin(object sender, EventArgs e)
        {
            SuspendLayout();
        }

        private void CoreUI_ResizeEnd(object sender, EventArgs e)
        {
            if (Width < 500) Width = 500;
            if (Height < 400) Height = 400;

            ResumeLayout();
        }

        private void minimizeToTrayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        // Notification icon stuff

        private void mNotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.Visible)
                this.Hide();
            else
                this.Show();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void mNotifyMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            openToolStripMenuItem.Enabled = !Visible;
        }
    }
}