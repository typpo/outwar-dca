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
    public partial class CoreUI : Form
    {
        internal const int TABINDEX_FILTERS = 1;
        internal const int TABINDEX_CHAT = 7;

        internal int SelectedTabIndex
        {
            get { return tabs.SelectedIndex; }
        }

        internal TabControl Tabs
        {
            get { return tabs; }
        }


        public static CoreUI Instance { get; private set; }

        internal string Changes { private get; set; }

        internal MainPanel MainPanel { get; private set; }

        internal LogPanel LogPanel { get; private set; }

        internal AccountsPanel AccountsPanel { get; private set; }

        internal ChatUI ChatPanel { get; private set; }

        internal RoomsPanel RoomsPanel { get; private set; }

        internal MobsPanel MobsPanel { get; private set; }

        internal RaidsPanel RaidsPanel { get; private set; }

        public UserEditable Settings { get; private set; }

        private AttackPanel mAttackPanel;
        private FiltersPanel mFiltersPanel;
        private TrainPanel mTrainPanel;
        private QuestsPanel mQuestsPanel;

        public CoreUI()
        {
            InitializeComponent();

            AccountsPanel = new AccountsPanel(this);
            AccountsPanel.Dock = DockStyle.Fill;
            splitLeftRight.Panel1.Controls.Add(AccountsPanel);

            LogPanel = new LogPanel();
            LogPanel.Dock = DockStyle.Fill;
            splitLeftRight2.Panel2.Controls.Add(LogPanel);

            mAttackPanel = new AttackPanel(this);
            mAttackPanel.Dock = DockStyle.Fill;
            tabs.TabPages[0].Controls.Add(mAttackPanel);

            MainPanel = new MainPanel(this);
            MainPanel.Dock = DockStyle.Fill;
            splitLeftRight2.Panel1.Controls.Add(MainPanel);

            mFiltersPanel = new FiltersPanel(this);
            mFiltersPanel.Dock = DockStyle.Fill;
            tabs.TabPages[1].Controls.Add(mFiltersPanel);

            RoomsPanel = new RoomsPanel(this);
            RoomsPanel.Dock = DockStyle.Fill;
            tabs.TabPages[2].Controls.Add(RoomsPanel);

            MobsPanel = new MobsPanel(this);
            MobsPanel.Dock = DockStyle.Fill;
            tabs.TabPages[3].Controls.Add(MobsPanel);

            RaidsPanel = new RaidsPanel(this);
            RaidsPanel.Dock = DockStyle.Fill;
            tabs.TabPages[4].Controls.Add(RaidsPanel);

            mTrainPanel = new TrainPanel(this);
            mTrainPanel.Dock = DockStyle.Fill;
            tabs.TabPages[5].Controls.Add(mTrainPanel);

            mQuestsPanel = new QuestsPanel(this);
            mQuestsPanel.Dock = DockStyle.Fill;
            tabs.TabPages[6].Controls.Add(mQuestsPanel);

            ChatPanel = new ChatUI(this);
            ChatPanel.Dock = DockStyle.Fill;
            tabs.TabPages[7].Controls.Add(ChatPanel);
            
            Instance = this;
            Settings = ConfigSerializer.ReadFile("config.xml");

            this.Text = "Typpo's DC Tool - [www.typpo.us] - v" + Version.Id;

            foreach (string s in Server.NamesList)
            {
                ListViewGroup grp = new ListViewGroup(s);
                AccountsPanel.Groups.Add(grp);
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
            MobsPanel.CalcMobRage();
            ChatPanel.Init();

            LogPanel.Log("Started.");
            LogPanel.LogAttack("No attacks yet...");
        }

        private void CoreUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            Globals.AttackOn = false;
            Globals.AttackMode = false;
            Globals.Terminate = true;

            if (RoomsPanel.Rooms.Count > 0)
            {
                RegistryUtil.Save();
                IniWriter.Save();
                ConfigSerializer.WriteFile("config.xml", Settings);
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
            RoomsPanel.BuildView();
            MobsPanel.BuildView();
            RaidsPanel.BuildView();
        }

        internal void UpdateDisplay()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(UpdateDisplay));
                return;
            }

            lblMisc.Text = "Experience gained: " + Globals.ExpGained;

            if (AccountsPanel.Engine.MainAccount != null)
            {
                MainPanel.StatusText =
                    string.Format("Exp: {0:n0}      Rage: {1:n0}", AccountsPanel.Engine.MainAccount.Exp,
                                  AccountsPanel.Engine.MainAccount.Rage);

                Account a = AccountsPanel.Engine.MainAccount;
                int i = AccountsPanel.Engine.Accounts.IndexOf(a);
                AccountsPanel.Accounts[i].SubItems[0].Text = a.Name;
                AccountsPanel.Accounts[i].SubItems[1].Text = a.Mover.Location == null ? "-" : a.Mover.Location.Id.ToString();
                AccountsPanel.Accounts[i].SubItems[2].Text = a.Mover.MobsAttacked.ToString();
                AccountsPanel.Accounts[i].SubItems[3].Text = a.Mover.ExpGained.ToString();
                AccountsPanel.Accounts[i].SubItems[4].Text = a.Mover.MobsAttacked == 0 ? "-" : (a.Mover.ExpGained / a.Mover.MobsAttacked).ToString();
                //mAccountsPanel.Accounts[i].SubItems[5].Text = a.Mover.MobsAttacked == 0 ? "-" : (a.Mover.RageUsed / a.Mover.MobsAttacked).ToString();
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
            if (ChatPanel.StatusLabel.Text.StartsWith("Not"))
            {
                LogPanel.Log("E: Not connected to authorization server.");
                Application.Exit();
                return;
            }

            if (InvokeRequired)
            {
                Invoke(new ToggleHandler(Toggle), on);
                return;
            }
            // ACCOUNTS
            AccountsPanel.ChangeAllowed = on;

            // ADVENTURES
            RaidsPanel.MoveEnabled = on;

            // MOVE TAB
            RoomsPanel.PathfindEnabled = on;
            
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

            MainPanel.AttackingOn = on;
            Globals.AttackMode = on;

            if (MainPanel.CountdownTimer != null && on)
            {
                MainPanel.CountdownTimer.Stop();
            }

            if (ChatPanel.StatusLabel.Text.StartsWith("Not"))
            {
                LogPanel.Log("E: Not connected to authorization server.");
                Application.Exit();
                return;
            }
        }

        private void SyncSettings()
        {
            clearLogsPeriodicallyToolStripMenuItem.Checked = Settings.ClearLogs;
            showSystrayIconWhenOpenToolStripMenuItem.Checked = Settings.NotifyVisible;

            AccountsPanel.Username = Settings.LastUsername;
            AccountsPanel.Password = Settings.LastPassword;

            foreach (string str in Settings.MobFilters)
            {
                mFiltersPanel.FiltersText += str + "\r\n";
            }

            mAttackPanel.StopAtRage = Settings.StopBelowRage;
            mAttackPanel.RageLimit = Settings.RageLimit;
            mAttackPanel.ReturnToStart = Settings.ReturnToStart;
            MainPanel.UseCountdown = Settings.UseCountdownTimer;
            MainPanel.UseHourTimer = Settings.UseHourTimer;
            MainPanel.CountdownValue = Settings.CycleInterval;

            MainPanel.SyncAttackMode();

            if (Settings.AlertQuests)
                mQuestsPanel.AlertQuest = true;
            else if (Settings.AutoQuest)
                mQuestsPanel.AutoQuest = true;
            else
                mQuestsPanel.NothingQuest = true;

            mTrainPanel.AutoTrain = Settings.AutoTrain;

            mFiltersPanel.FiltersEnabled = Settings.FilterMobs;
            mFiltersPanel.UpdateTab();

            mAttackPanel.LevelMax = Settings.LvlLimit;
            mAttackPanel.LevelMin = Settings.LvlLimitMin;
            mAttackPanel.StopAtRage = Settings.StopBelowRage;

            try
            {
                mAttackPanel.ThreadDelay = Settings.Delay;
            }
            catch (ArgumentOutOfRangeException)
            {
                Settings.Delay = 0;
                mAttackPanel.ThreadDelay = Settings.Delay;
            }

            mAttackPanel.Variance = Settings.Variance;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Program by Typpo (www.typpo.us).\nTrue version " + Version.Full
                + ".\n\nThis particular copy of the program has gained you "
                + (Globals.ExpGained + Globals.ExpGainedTotal) + " EXP."
                +
                (AccountsPanel.Engine.MainAccount == null
                     ? string.Empty
                     : "\n\n" + AccountsPanel.Engine.MainAccount.Name + " has been attacking mobs for an average of "
                       +
                       (AccountsPanel.Engine.MainAccount.Mover.MobsAttacked < 1
                            ? "N/A"
                            : (AccountsPanel.Engine.MainAccount.Mover.ExpGained
                               / AccountsPanel.Engine.MainAccount.Mover.MobsAttacked).ToString()) + " exp per attack.")
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
            t.Text = Changes.Replace("\n", "\r\n");
            t.ScrollBars = ScrollBars.Both;
            t.ReadOnly = true;
            f.Show();
        }

        private void exportLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Toggle(false);
            LogPanel.Export();
            Toggle(true);
        }

        private void clearLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LogPanel.Clear();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void openInBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AccountsPanel.Engine.MainAccount == null)
            {
                MessageBox.Show("You haven't logged in on an account yet.", "Open In Browser", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                Process.Start("http://" + AccountsPanel.Engine.MainAccount.Server + ".outwar.com/?rg_sess_id=" + AccountsPanel.Engine.RgSessId);
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

        // Notification icon stuff

        private void mNotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (!this.Visible && AccountsPanel.Engine.MainAccount != null)
            {
                mNotifyIcon.ShowBalloonTip(1000, "Account Stats",
                    string.Format("Exp: {0:n0}\nRage: {1:n0}\nExp Gained: {2:n0}\n{3}\n\nDouble-click to open", AccountsPanel.Engine.MainAccount.Exp, AccountsPanel.Engine.MainAccount.Rage, Globals.ExpGained, MainPanel.TimeLeft)
                    , ToolTipIcon.Info);
            }
            else
            {
                ToggleSystray();
            }
        }

        private void minimizeToTrayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleSystray();
        }

        private void mNotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            ToggleSystray();
        }

        private void ToggleSystray()
        {
            if (this.Visible)
            {
                mNotifyIcon.Visible = true;
                this.Hide();
            }
            else
            {
                Show();
                if (!Settings.NotifyVisible)
                    mNotifyIcon.Visible = false;
            }
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

        private void showSystrayIconWhenOpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mNotifyIcon.Visible = Settings.NotifyVisible = showSystrayIconWhenOpenToolStripMenuItem.Checked;
        }

        private void tabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedTabIndex == TABINDEX_CHAT) // chat
            {
                Tabs.TabPages[TABINDEX_CHAT].Text = "Chat";
                ChatPanel.ScrollToBottom();
            }
        }
    }
}