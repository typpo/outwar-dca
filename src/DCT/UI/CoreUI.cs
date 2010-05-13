using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DCT.Outwar;
using DCT.Outwar.World;
using DCT.Pathfinding;
using DCT.Settings;
using DCT.Threading;
using DCT.Util;
using Version=DCT.Security.Version;

namespace DCT.UI
{
    public partial class CoreUI : Form
    {
        internal const int TABINDEX_ATTACK = 0;
        internal const int TABINDEX_FILTERS = 1;
        internal const int TABINDEX_ROOMS = 2;
        internal const int TABINDEX_MOBS = 3;
        internal const int TABINDEX_RAIDS = 4;
        internal const int TABINDEX_SPAWNS = 5;
        internal const int TABINDEX_TRAINER = 6;
        internal const int TABINDEX_TALK = 7;
        internal const int TABINDEX_CHAT = 8;

        private const string TS_ATTACKMODE_PREFIX = "Attack mode: ";

        internal bool DebugVisible
        {
            get { return debugToolStripMenuItem.Visible; }
            set { debugToolStripMenuItem.Visible = value; }
        }

        internal int SelectedTabIndex
        {
            get { return tabs.SelectedIndex; }
        }

        internal TabControl Tabs
        {
            get { return tabs; }
        }

        internal string StatusText
        {
            get { return lblExpRage.Text; }
            set { lblExpRage.Text = value; }
        }

        public static CoreUI Instance { get; private set; }

        internal string Changes { private get; set; }

        internal LogPanel LogPanel { get; private set; }

        internal AccountsPanel AccountsPanel { get; private set; }

        internal ChatUI ChatPanel { get; private set; }

        internal RoomsPanel RoomsPanel { get; private set; }

        internal MobsPanel MobsPanel { get; private set; }

        internal RaidsPanel RaidsPanel { get; private set; }

        internal SpawnsPanel SpawnsPanel { get; private set; }

        public UserEditable Settings { get; private set; }

        private readonly AttackPanel mAttackPanel;
        internal MainPanel MainPanel
        {
            get { return mAttackPanel.MainPanel; }
        }
        private readonly FiltersPanel mFiltersPanel;
        private readonly TrainPanel mTrainPanel;

        private readonly TalkPanel mTalkPanel;
        internal TalkPanel TalkPanel
        {
            get { return mTalkPanel; }
        }

        public CoreUI()
        {
            InitializeComponent();

            // silver vs2008 toolstrip look
            TanColorTable colorTable = new TanColorTable();
            colorTable.UseSystemColors = true;
            toolStrip.Renderer = new ToolStripProfessionalRenderer(colorTable);

            // fill panels
            AccountsPanel = new AccountsPanel(this);
            AccountsPanel.Dock = DockStyle.Fill;
            splitLeftRight.Panel1.Controls.Add(AccountsPanel);

            LogPanel = new LogPanel();
            LogPanel.Dock = DockStyle.Fill;
            splitTopBottom.Panel1.Controls.Add(LogPanel);

            mAttackPanel = new AttackPanel(this);
            mAttackPanel.Dock = DockStyle.Fill;
            tabs.TabPages[TABINDEX_ATTACK].Controls.Add(mAttackPanel);

            /*
            MainPanel = new MainPanel(this);
            MainPanel.Dock = DockStyle.Fill;
            splitLeftRight2.Panel1.Controls.Add(MainPanel);
            */

            mFiltersPanel = new FiltersPanel(this);
            mFiltersPanel.Dock = DockStyle.Fill;
            tabs.TabPages[TABINDEX_FILTERS].Controls.Add(mFiltersPanel);

            RoomsPanel = new RoomsPanel(this);
            RoomsPanel.Dock = DockStyle.Fill;
            tabs.TabPages[TABINDEX_ROOMS].Controls.Add(RoomsPanel);

            MobsPanel = new MobsPanel(this);
            MobsPanel.Dock = DockStyle.Fill;
            tabs.TabPages[TABINDEX_MOBS].Controls.Add(MobsPanel);

            RaidsPanel = new RaidsPanel(this);
            RaidsPanel.Dock = DockStyle.Fill;
            tabs.TabPages[TABINDEX_RAIDS].Controls.Add(RaidsPanel);

            SpawnsPanel = new SpawnsPanel(this);
            SpawnsPanel.Dock = DockStyle.Fill;
            tabs.TabPages[TABINDEX_SPAWNS].Controls.Add(SpawnsPanel);

            mTrainPanel = new TrainPanel(this);
            mTrainPanel.Dock = DockStyle.Fill;
            tabs.TabPages[TABINDEX_TRAINER].Controls.Add(mTrainPanel);

            mTalkPanel = new TalkPanel(this);
            mTalkPanel.Dock = DockStyle.Fill;
            tabs.TabPages[TABINDEX_TALK].Controls.Add(mTalkPanel);

            ChatPanel = new ChatUI(this);
            ChatPanel.Dock = DockStyle.Fill;
            tabs.TabPages[TABINDEX_CHAT].Controls.Add(ChatPanel);
            
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

        internal void BuildViews()
        {
            RoomsPanel.BuildView();
            MobsPanel.BuildView();
            RaidsPanel.BuildView();
            SpawnsPanel.BuildView();
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
                StatusText =
                    string.Format("Exp: {0:n0}      Rage: {1:n0}", AccountsPanel.Engine.MainAccount.Exp,
                                  AccountsPanel.Engine.MainAccount.Rage);

                Account a = AccountsPanel.Engine.MainAccount;
                int i = AccountsPanel.Engine.Accounts.IndexOf(a);
                AccountsPanel.Accounts[i].SubItems[0].Text = a.Name;
                AccountsPanel.Accounts[i].SubItems[1].Text = a.Mover.Location == null ? "-" : a.Mover.Location.Id.ToString();
                AccountsPanel.Accounts[i].SubItems[2].Text = a.Mover.MobsAttacked.ToString();
                AccountsPanel.Accounts[i].SubItems[3].Text = a.Mover.ExpGained.ToString();
                AccountsPanel.Accounts[i].SubItems[4].Text = a.Mover.MobsAttacked == 0 ? "-" : (a.Mover.ExpGained / a.Mover.MobsAttacked).ToString();
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
            if (InvokeRequired)
            {
                Invoke(new ToggleHandler(Toggle), on);
                return;
            }

            // Main Panel
            MainPanel.StopAfterEnabled = on;

            // ACCOUNTS
            AccountsPanel.AccountsEnabled = on;

            // ADVENTURES
            RaidsPanel.MoveEnabled = on;

            // MOVE TAB
            RoomsPanel.PathfindEnabled = on;
            
            // TRAINING TAB
            mTrainPanel.TrainEnabled = on;

            // SPAWN TAB
            SpawnsPanel.SpawnsEnabled = on;

            // TALK TAB
            TalkPanel.TalkEnabled = on;

            // MOB TAB
            MobsPanel.MobsEnabled = on;
        }

        internal void ToggleAttack(bool on)
        {
            if (InvokeRequired)
            {
                Invoke(new ToggleHandler(ToggleAttack), on);
                return;
            }

            Toggle(!on);

            AttackingOn = on;
            Globals.AttackMode = on;

            if (CountdownTimer != null && on)
            {
                CountdownTimer.Stop();
            }
        }

        internal void SyncSettings()
        {
            // Menu bar

            clearLogsPeriodicallyToolStripMenuItem.Checked = Settings.ClearLogs;
            showSystrayIconWhenOpenToolStripMenuItem.Checked = Settings.NotifyVisible;

            // Login panel

            AccountsPanel.Username = Settings.LastUsername;
            AccountsPanel.Password = Settings.LastPassword;

            // Filters panel

            mFiltersPanel.FiltersText = string.Empty;
            foreach (string str in Settings.MobFilters)
            {
                mFiltersPanel.FiltersText += str + "\r\n";
            }

            // Main panel

            MainPanel.UseCountdownTimer = Settings.UseCountdownTimer;
            MainPanel.UseHourTimer = Settings.UseHourTimer;
            MainPanel.CountdownValue = Settings.CycleInterval;
            MainPanel.StopAfter = Settings.StopAfter;
            MainPanel.StopAfterVal = Settings.StopAfterVal;
            MainPanel.StopAfterMode = Settings.StopAfterMode;
            MainPanel.InitStopAfter();

            SyncAttackMode();

            // Training panel

            mTrainPanel.AutoTrain = Settings.AutoTrain;

            // Filters panel

            mFiltersPanel.FiltersEnabled = Settings.FilterMobs;
            mFiltersPanel.UpdateTab();

            // Attack panel

            mAttackPanel.LevelMax = Settings.LvlLimit;
            mAttackPanel.LevelMin = Settings.LvlLimitMin;
            mAttackPanel.StopBelowRage = Settings.StopBelowRage;
            mAttackPanel.RageLimit = Settings.RageLimit;
            mAttackPanel.ReturnToStart = Settings.ReturnToStart;

            // Spawns panel
            SpawnsPanel.AttackSpawns = Settings.AttackSpawns;
            SpawnsPanel.IgnoreSpawnRage = Settings.IgnoreSpawnRage;
        }

        /// <summary>
        /// Sets option buttons as per AttackMode setting
        /// </summary>
        internal void SyncAttackMode()
        {
            switch (Settings.AttackMode)
            {
                case AttackingType.CurrentArea:
                    cmbAttackMode.SelectedIndex = 0;
                    break;
                case AttackingType.MultiArea:
                    cmbAttackMode.SelectedIndex = 1;
                    break;
                case AttackingType.Mobs:
                    cmbAttackMode.SelectedIndex = 2;
                    break;
                case AttackingType.Rooms:
                    cmbAttackMode.SelectedIndex = 3;
                    break;
                default: throw new Exception("Your settings are corrupt; no such attack mode.");
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Program by Typpo (www.typpo.us), version {0}{1}.\n\nThis particular copy of the program has gained you {2} EXP.",
                Version.Full, Version.Beta, (Globals.ExpGained + Globals.ExpGainedTotal));
            if (AccountsPanel.Engine.MainAccount != null && AccountsPanel.Engine.MainAccount.Mover.MobsAttacked > 1)
            {
                sb.AppendFormat("\n\n{0} has been attacking mobs for an average of {1} exp per attack.",
                    AccountsPanel.Engine.MainAccount.Name, (AccountsPanel.Engine.MainAccount.Mover.ExpGained / AccountsPanel.Engine.MainAccount.Mover.MobsAttacked));
            }
            MessageBox.Show(sb.ToString(), "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            t.SelectionLength = 0;
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

        private void exportSpawnLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SpawnsPanel.Export();
        }

        private void clearSpawnLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SpawnsPanel.Clear();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        #region ACTIONS MENUBAR
        private void reportABugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Feedback.frmBugReporter(this).Show();
        }

        private void inputRgsessidToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AccountsPanel.ShowRgSessIdDialog();
        }

        private void showMyRgsessidToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AccountsPanel.Engine.MainAccount == null)
            {
                MessageBox.Show("You haven't logged in on an account yet.", "Open In Browser", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show(
                string.Format("Do you want to copy your rg_sess_id to clipboard?\n\n{0}",
                AccountsPanel.Engine.RgSessId), "rg_sess_id", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Clipboard.SetText(AccountsPanel.Engine.RgSessId);
            }
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
                // eg. http://sigil.outwar.com/?rg_sess_id=256s55neyav04p8wz6hny6jbcgqe9ht0&serverid=1&suid=830713

                Process.Start(string.Format("http://{0}.outwar.com/?rg_sess_id={1}&serverid={2}&suid={3}",
                    AccountsPanel.Engine.MainAccount.Server, AccountsPanel.Engine.RgSessId,
                    Server.NameToId(AccountsPanel.Engine.MainAccount.Server), AccountsPanel.Engine.MainAccount.Id));
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


        private void reloadMapDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Toggle(false);
            Pathfinder.Rooms.Clear();
            Pathfinder.Mobs.Clear();
            Pathfinder.Spawns.Clear();
            Pathfinder.Adventures.Clear();

            LogPanel.Log("Downloading map data...");
            ThreadEngine.DefaultInstance.DoParameterized(Pathfinder.BuildMap, true);

            LogPanel.Log("Rebuilding views...");
            BuildViews();

            LogPanel.Log("Done.");
            Toggle(true);
        }


        #endregion

        #region PREFERENCES MENUBAR
        private void clearLogsPeriodicallyToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            Instance.Settings.ClearLogs = clearLogsPeriodicallyToolStripMenuItem.Checked;
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
                    string.Format("Exp: {0:n0}\nRage: {1:n0}\nExp Gained: {2:n0}\n{3}\n\nDouble-click to open", AccountsPanel.Engine.MainAccount.Exp, AccountsPanel.Engine.MainAccount.Rage, Globals.ExpGained, TimeLeft)
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
        #endregion

        #region Notification Icon

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

        private void mNotifyMenu_Opening(object sender, CancelEventArgs e)
        {
            openToolStripMenuItem.Enabled = !Visible;
        }

        private void showSystrayIconWhenOpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mNotifyIcon.Visible = Settings.NotifyVisible = showSystrayIconWhenOpenToolStripMenuItem.Checked;
        }

        #endregion

        private void tabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (SelectedTabIndex)
            {
                case TABINDEX_ATTACK:
                    Tabs.TabPages[TABINDEX_ATTACK].Text = "Attack";
                    break;
                case TABINDEX_SPAWNS:
                    Tabs.TabPages[TABINDEX_SPAWNS].Text = "Spawns";
                    break;
                case TABINDEX_TALK:
                    mTalkPanel.Refresh();
                    break;
                case TABINDEX_CHAT:
                    Tabs.TabPages[TABINDEX_CHAT].Text = "Chat";
                    ChatPanel.ScrollToBottom();
                    ChatPanel.UpdateNames();
                    break;
            }
        }
        # region Secret menubar

        private void spiderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartSpider(InputBox.Prompt("Spider", "Enter bound.  Press cancel for boundless."));
        }

        internal void StartSpider(string str)
        {
            ToggleAttack(true);

            ThreadEngine.DefaultInstance.DoParameterized(new ThreadEngine.ParameterizedThreadHandler(
                AccountsPanel.Engine.MainAccount.Mover.Spider), str);

            Instance.BuildViews();

            ToggleAttack(false);
        }

        private void benchmarkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(Pathfinder.Benchmark);
            t.Start(1000);
        }


        private void getPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int s = int.Parse(InputBox.Prompt("Path test", "Source"));
            int d = int.Parse(InputBox.Prompt("Path test", "Dest"));
            List<int> res = Pathfinder.BFS(s, d);

            StringBuilder sb = new StringBuilder();
            foreach (int i in res)
            {
                sb.AppendFormat("{0} ", i);
            }
            if (MessageBox.Show(
     string.Format("Do you want to copy to clipboard?\n\n{0}",
     sb), "Path test", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Clipboard.SetText(sb.ToString());
            }
        }

        private void exportRoomsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pathfinder.ExportRooms();
        }

        private void exportMobsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pathfinder.ExportMobs();
        }

        private void clearDatabasesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pathfinder.Rooms.Clear();
            Pathfinder.Mobs.Clear();
        }

        private void writeSerializeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigSerializer.WriteFile("config.xml", Settings);
        }

        private void loginThroughToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmLoginServer(this).ShowDialog();
        }
        #endregion

        #region TOOLSTRIP

        private void cmbAttackMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbAttackMode.SelectedItem.ToString())
            {
                case "current area":
                    Settings.AttackMode = AttackingType.CurrentArea;
                    break;
                case "multi area":
                    Settings.AttackMode = AttackingType.MultiArea;
                    break;
                case "mobs":
                    Settings.AttackMode = AttackingType.Mobs;
                    break;
                case "rooms":
                    Settings.AttackMode = AttackingType.Rooms;
                    break;
            }
        }

        #endregion

        #region TS BUTTONS

        internal string TimeLeft
        {
            get { return lblTimeLeft.Text; }
        }

        internal bool AttackingOn
        {
            get { return btnStart.Enabled; }
            set
            {
                btnStart.Enabled = !value;
                btnStop.Enabled = value;
                btnStartTimer.Enabled = !value;
            }
        }

        internal CountDownTimer CountdownTimer
        {
            get { return mCountdownTimer; }
        }

        private CountDownTimer mCountdownTimer;
        private AttackingType mCountdownType;

        private void btnStart_Click(object sender, EventArgs e)
        {
            StartAttacking();
        }

        internal void StartAttacking()
        {
            switch (Settings.AttackMode)
            {
                case AttackingType.CurrentArea: AttackArea(); break;
                case AttackingType.MultiArea: AttackAreas(); break;
                case AttackingType.Mobs: AttackMobs(); break;
                case AttackingType.Rooms: AttackRooms(); break;
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            StopAttacking(true);
        }

        private delegate void StopAttackingHandler(bool timeroff);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeroff">true if timer should be disabled</param>
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

                if (timeroff && (MainPanel.UseCountdownTimer || MainPanel.UseHourTimer))
                {
                    MainPanel.UseCountdownTimer = false;
                    MainPanel.UseHourTimer = false;

                    if (tabs.SelectedIndex != TABINDEX_ATTACK)
                    {
                        Tabs.TabPages[TABINDEX_ATTACK].Text = "Attack (*)";
                    }
                }
            }
        }

        private void btnStartTimer_Click(object sender, EventArgs e)
        {
            if (AccountsPanel.Accounts.Count < 1)
            {
                MessageBox.Show("You need to login before setting a timer.", "Start Timer", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            else if (!Settings.UseCountdownTimer && !Settings.UseHourTimer)
            {
                MessageBox.Show("Choose a timer.", "Start Timer", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            Countdown(Settings.AttackMode);
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
            
            // check stopafter conditions
            if (Settings.StopAfter)
            {
                switch (Settings.StopAfterMode)
                {
                    case UserEditable.StopAfterType.Minutes:
                        if (MainPanel.StopAfterTimeFinished)
                        {
                            LogPanel.Log(string.Format("Reached time limit of {0} minutes", Settings.StopAfterVal));
                            MainPanel.StopAfterFinish();
                            StopAttacking(true);
                            return;
                        }
                        break;
                    case UserEditable.StopAfterType.Runs:
                        if (MainPanel.StopAfterCounterFinished)
                        {
                            LogPanel.Log(string.Format("Reached {0} runs", Settings.StopAfterVal));
                            MainPanel.StopAfterFinish();
                            StopAttacking(true);
                            return;
                        }
                        break;
                }
            }

            // timer setup
            int countFor;
            if (Settings.UseCountdownTimer)
            {
                countFor = (MainPanel.CountdownValue) * 60;
            }
            else
            {
                countFor = SecondsUntilHour();
            }

            mCountdownTimer = new CountDownTimer(countFor);

            mCountdownTimer.Interval = 1000;
            mCountdownTimer.Tick += t_Tick;
            mCountdownTimer.Started += t_Started;
            mCountdownTimer.Stopped += t_Stopped;
            mCountdownType = type;

            mCountdownTimer.Start();

            if (Settings.ClearLogs)
                LogPanel.ClearMost();
        }

        internal int SecondsUntilHour()
        {
            return (61 - DateTime.Now.Minute) * 60;
        }

        private void t_Stopped(object sender, EventArgs e)
        {
            if (Globals.AttackMode || !(Settings.UseCountdownTimer || Settings.UseHourTimer))
            {
                return;
            }

            Toggle(false);
            ToggleAttack(true);

            lblTimeLeft.Text = "Time left: 0:00";

            if (Settings.StopAfter)
            {
                switch (Settings.StopAfterMode)
                {
                    case UserEditable.StopAfterType.Minutes:
                        // this is here because countdown takes time!
                        if (MainPanel.StopAfterTimeFinished)
                        {
                            LogPanel.Log(string.Format("Reached time limit of {0} minutes", Settings.StopAfterVal));
                            MainPanel.StopAfterFinish();
                            StopAttacking(true);
                            return;
                        }
                        break;
                    case UserEditable.StopAfterType.Runs:
                        // this might happen if user started a run with a countdown instead of an attack
                        if (MainPanel.StopAfterCounterFinished)
                        {
                            LogPanel.Log(string.Format("Reached {0} runs", Settings.StopAfterVal));
                            MainPanel.StopAfterFinish();
                            StopAttacking(true);
                            return;
                        }
                        MainPanel.StopAfterCounter++;
                        break;
                }
            }

            switch (mCountdownType)
            {
                case AttackingType.CurrentArea:
                    AttackArea();
                    break;
                case AttackingType.MultiArea:
                    AttackAreas();
                    break;
                case AttackingType.Mobs:
                    AttackMobs();
                    break;
                case AttackingType.Rooms:
                    AttackRooms();
                    break;
            }
        }

        private void t_Started(object sender, EventArgs e)
        {
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

            if (!Settings.UseCountdownTimer && !Settings.UseHourTimer)
            {
                mCountdownTimer.Stop();
            }
        }

        #endregion

        /// <summary>
        /// Collects debugging data about the current state of the program.
        /// </summary>
        /// <returns></returns>
        internal string CollectData()
        {
            StringBuilder sb = new StringBuilder("State summary:\n\n");

            sb.AppendFormat("{0} accounts loaded\n", AccountsPanel.Engine.Count);
            sb.AppendFormat("{0} accounts selected\n", AccountsPanel.lvAccounts.SelectedIndices.Count);
            sb.AppendFormat("{0} accounts checked\n\n", AccountsPanel.lvAccounts.CheckedIndices.Count);

            sb.AppendFormat("{0} rooms loaded\n", RoomsPanel.Rooms.Count);
            sb.AppendFormat("{0} rooms checked:\n", RoomsPanel.CheckedRooms.Count);
            foreach (ListViewItem i in RoomsPanel.CheckedRooms)
            {
                // name, id
                sb.AppendFormat("\t{0} ({1})\n", i.SubItems[0].Text, i.SubItems[1].Text);
            }
            sb.Append("\n\n");

            sb.AppendFormat("{0} mobs loaded\n", MobsPanel.Mobs.Count);
            sb.AppendFormat("{0} mobs checked:\n", MobsPanel.CheckedMobs.Count);
            foreach (ListViewItem i in MobsPanel.CheckedMobs)
            {
                // name, id, roomid
                sb.AppendFormat("\t{0} ({1}, {2})\n", i.SubItems[0].Text, i.SubItems[1].Text, i.SubItems[2].Text);
            }
            sb.Append("\n\n");

            sb.AppendFormat("{0} spawns loaded\n", SpawnsPanel.Spawns.Count);
            sb.AppendFormat("{0} spawns checked:\n", SpawnsPanel.CheckedSpawns.Count);
            foreach (ListViewItem i in SpawnsPanel.CheckedSpawns)
            {
                // name, roomid
                sb.AppendFormat("\t{0} ({1})\n", i.SubItems[0].Text, i.SubItems[2].Text);
            }
            sb.Append("\n\n");

            // settings data
            sb.Append("Settings serialization:\n\n");
            sb.Append(new ConfigSerializer().StringSerialize(Settings));

            return sb.ToString();
        }
    }
}