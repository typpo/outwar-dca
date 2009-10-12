namespace DCT.UI
{
    partial class CoreUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CoreUI));
            this.ss = new System.Windows.Forms.StatusStrip();
            this.lblMisc = new System.Windows.Forms.ToolStripStatusLabel();
            this.pgr = new System.Windows.Forms.ToolStripProgressBar();
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.changesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.exportLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.exportSpawnLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearSpawnLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inputRgsessidToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showMyRgsessidToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripSeparator();
            this.minimizeToTrayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.openInBrowserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openTyppousToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearLogsPeriodicallyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showSystrayIconWhenOpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spiderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.benchmarkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.exportRoomsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportMobsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
            this.clearDatabasesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
            this.writeSerializeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.mNotifyMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.btnStart = new System.Windows.Forms.ToolStripButton();
            this.btnStop = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnStartTimer = new System.Windows.Forms.ToolStripButton();
            this.lblTimeLeft = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.cmbAttackMode = new System.Windows.Forms.ToolStripComboBox();
            this.splitLeftRight = new System.Windows.Forms.SplitContainer();
            this.splitTopBottom = new System.Windows.Forms.SplitContainer();
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabAttack = new System.Windows.Forms.TabPage();
            this.tabFilters = new System.Windows.Forms.TabPage();
            this.tabRooms = new System.Windows.Forms.TabPage();
            this.tabMobs = new System.Windows.Forms.TabPage();
            this.tabJoiner = new System.Windows.Forms.TabPage();
            this.tabSpawns = new System.Windows.Forms.TabPage();
            this.tabTrainer = new System.Windows.Forms.TabPage();
            this.tabChat = new System.Windows.Forms.TabPage();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.lblExpRage = new System.Windows.Forms.ToolStripLabel();
            this.ss.SuspendLayout();
            this.mnuMain.SuspendLayout();
            this.mNotifyMenu.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.splitLeftRight.Panel2.SuspendLayout();
            this.splitLeftRight.SuspendLayout();
            this.splitTopBottom.Panel2.SuspendLayout();
            this.splitTopBottom.SuspendLayout();
            this.tabs.SuspendLayout();
            this.SuspendLayout();
            // 
            // ss
            // 
            this.ss.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.ss.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblMisc,
            this.pgr});
            this.ss.Location = new System.Drawing.Point(0, 464);
            this.ss.Name = "ss";
            this.ss.Size = new System.Drawing.Size(664, 22);
            this.ss.SizingGrip = false;
            this.ss.TabIndex = 3;
            // 
            // lblMisc
            // 
            this.lblMisc.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMisc.Name = "lblMisc";
            this.lblMisc.Size = new System.Drawing.Size(0, 17);
            // 
            // pgr
            // 
            this.pgr.Name = "pgr";
            this.pgr.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.pgr.Size = new System.Drawing.Size(580, 16);
            this.pgr.Step = 1;
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.actionsToolStripMenuItem,
            this.preferencesToolStripMenuItem,
            this.debugToolStripMenuItem});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.mnuMain.Size = new System.Drawing.Size(664, 24);
            this.mnuMain.TabIndex = 4;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.changesToolStripMenuItem,
            this.toolStripMenuItem3,
            this.exportLogToolStripMenuItem,
            this.clearLogToolStripMenuItem,
            this.toolStripMenuItem5,
            this.exportSpawnLogToolStripMenuItem,
            this.clearSpawnLogToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(157, 22);
            this.toolStripMenuItem2.Text = "About";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // changesToolStripMenuItem
            // 
            this.changesToolStripMenuItem.Name = "changesToolStripMenuItem";
            this.changesToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.changesToolStripMenuItem.Text = "Changes";
            this.changesToolStripMenuItem.Click += new System.EventHandler(this.changesToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(154, 6);
            // 
            // exportLogToolStripMenuItem
            // 
            this.exportLogToolStripMenuItem.Name = "exportLogToolStripMenuItem";
            this.exportLogToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.exportLogToolStripMenuItem.Text = "Export log";
            this.exportLogToolStripMenuItem.Click += new System.EventHandler(this.exportLogToolStripMenuItem_Click);
            // 
            // clearLogToolStripMenuItem
            // 
            this.clearLogToolStripMenuItem.Name = "clearLogToolStripMenuItem";
            this.clearLogToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.clearLogToolStripMenuItem.Text = "Clear log";
            this.clearLogToolStripMenuItem.Click += new System.EventHandler(this.clearLogToolStripMenuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(154, 6);
            // 
            // exportSpawnLogToolStripMenuItem
            // 
            this.exportSpawnLogToolStripMenuItem.Name = "exportSpawnLogToolStripMenuItem";
            this.exportSpawnLogToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.exportSpawnLogToolStripMenuItem.Text = "Export spawn log";
            this.exportSpawnLogToolStripMenuItem.Click += new System.EventHandler(this.exportSpawnLogToolStripMenuItem_Click);
            // 
            // clearSpawnLogToolStripMenuItem
            // 
            this.clearSpawnLogToolStripMenuItem.Name = "clearSpawnLogToolStripMenuItem";
            this.clearSpawnLogToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.clearSpawnLogToolStripMenuItem.Text = "Clear spawn log";
            this.clearSpawnLogToolStripMenuItem.Click += new System.EventHandler(this.clearSpawnLogToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(154, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // actionsToolStripMenuItem
            // 
            this.actionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.inputRgsessidToolStripMenuItem,
            this.showMyRgsessidToolStripMenuItem,
            this.toolStripMenuItem9,
            this.minimizeToTrayToolStripMenuItem,
            this.toolStripMenuItem4,
            this.openInBrowserToolStripMenuItem,
            this.openTyppousToolStripMenuItem});
            this.actionsToolStripMenuItem.Name = "actionsToolStripMenuItem";
            this.actionsToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.actionsToolStripMenuItem.Text = "Actions";
            // 
            // inputRgsessidToolStripMenuItem
            // 
            this.inputRgsessidToolStripMenuItem.Name = "inputRgsessidToolStripMenuItem";
            this.inputRgsessidToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.inputRgsessidToolStripMenuItem.Text = "Input rg_sess_id";
            this.inputRgsessidToolStripMenuItem.Click += new System.EventHandler(this.inputRgsessidToolStripMenuItem_Click);
            // 
            // showMyRgsessidToolStripMenuItem
            // 
            this.showMyRgsessidToolStripMenuItem.Name = "showMyRgsessidToolStripMenuItem";
            this.showMyRgsessidToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.showMyRgsessidToolStripMenuItem.Text = "Show my rg_sess_id";
            this.showMyRgsessidToolStripMenuItem.Click += new System.EventHandler(this.showMyRgsessidToolStripMenuItem_Click);
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(168, 6);
            // 
            // minimizeToTrayToolStripMenuItem
            // 
            this.minimizeToTrayToolStripMenuItem.Name = "minimizeToTrayToolStripMenuItem";
            this.minimizeToTrayToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.minimizeToTrayToolStripMenuItem.Text = "Minimize to tray";
            this.minimizeToTrayToolStripMenuItem.Click += new System.EventHandler(this.minimizeToTrayToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(168, 6);
            // 
            // openInBrowserToolStripMenuItem
            // 
            this.openInBrowserToolStripMenuItem.Name = "openInBrowserToolStripMenuItem";
            this.openInBrowserToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.openInBrowserToolStripMenuItem.Text = "Open in browser";
            this.openInBrowserToolStripMenuItem.Click += new System.EventHandler(this.openInBrowserToolStripMenuItem_Click);
            // 
            // openTyppousToolStripMenuItem
            // 
            this.openTyppousToolStripMenuItem.Name = "openTyppousToolStripMenuItem";
            this.openTyppousToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.openTyppousToolStripMenuItem.Text = "Open typpo.us";
            this.openTyppousToolStripMenuItem.Click += new System.EventHandler(this.openTyppousToolStripMenuItem_Click);
            // 
            // preferencesToolStripMenuItem
            // 
            this.preferencesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearLogsPeriodicallyToolStripMenuItem,
            this.showSystrayIconWhenOpenToolStripMenuItem});
            this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
            this.preferencesToolStripMenuItem.Size = new System.Drawing.Size(77, 20);
            this.preferencesToolStripMenuItem.Text = "Preferences";
            // 
            // clearLogsPeriodicallyToolStripMenuItem
            // 
            this.clearLogsPeriodicallyToolStripMenuItem.Checked = true;
            this.clearLogsPeriodicallyToolStripMenuItem.CheckOnClick = true;
            this.clearLogsPeriodicallyToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.clearLogsPeriodicallyToolStripMenuItem.Name = "clearLogsPeriodicallyToolStripMenuItem";
            this.clearLogsPeriodicallyToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.clearLogsPeriodicallyToolStripMenuItem.Text = "Clear logs periodically";
            this.clearLogsPeriodicallyToolStripMenuItem.CheckedChanged += new System.EventHandler(this.clearLogsPeriodicallyToolStripMenuItem_CheckedChanged);
            // 
            // showSystrayIconWhenOpenToolStripMenuItem
            // 
            this.showSystrayIconWhenOpenToolStripMenuItem.Checked = true;
            this.showSystrayIconWhenOpenToolStripMenuItem.CheckOnClick = true;
            this.showSystrayIconWhenOpenToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showSystrayIconWhenOpenToolStripMenuItem.Name = "showSystrayIconWhenOpenToolStripMenuItem";
            this.showSystrayIconWhenOpenToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.showSystrayIconWhenOpenToolStripMenuItem.Text = "Show systray icon when open";
            this.showSystrayIconWhenOpenToolStripMenuItem.Click += new System.EventHandler(this.showSystrayIconWhenOpenToolStripMenuItem_Click);
            // 
            // debugToolStripMenuItem
            // 
            this.debugToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.spiderToolStripMenuItem,
            this.benchmarkToolStripMenuItem,
            this.toolStripMenuItem6,
            this.exportRoomsToolStripMenuItem,
            this.exportMobsToolStripMenuItem,
            this.toolStripMenuItem7,
            this.clearDatabasesToolStripMenuItem,
            this.toolStripMenuItem8,
            this.writeSerializeToolStripMenuItem});
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            this.debugToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.debugToolStripMenuItem.Text = "Debug";
            this.debugToolStripMenuItem.Visible = false;
            // 
            // spiderToolStripMenuItem
            // 
            this.spiderToolStripMenuItem.Name = "spiderToolStripMenuItem";
            this.spiderToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.spiderToolStripMenuItem.Text = "Spider";
            this.spiderToolStripMenuItem.Click += new System.EventHandler(this.spiderToolStripMenuItem_Click);
            // 
            // benchmarkToolStripMenuItem
            // 
            this.benchmarkToolStripMenuItem.Name = "benchmarkToolStripMenuItem";
            this.benchmarkToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.benchmarkToolStripMenuItem.Text = "Benchmark";
            this.benchmarkToolStripMenuItem.Click += new System.EventHandler(this.benchmarkToolStripMenuItem_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(149, 6);
            // 
            // exportRoomsToolStripMenuItem
            // 
            this.exportRoomsToolStripMenuItem.Name = "exportRoomsToolStripMenuItem";
            this.exportRoomsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exportRoomsToolStripMenuItem.Text = "Export room db";
            this.exportRoomsToolStripMenuItem.Click += new System.EventHandler(this.exportRoomsToolStripMenuItem_Click);
            // 
            // exportMobsToolStripMenuItem
            // 
            this.exportMobsToolStripMenuItem.Name = "exportMobsToolStripMenuItem";
            this.exportMobsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exportMobsToolStripMenuItem.Text = "Export mob db";
            this.exportMobsToolStripMenuItem.Click += new System.EventHandler(this.exportMobsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(149, 6);
            // 
            // clearDatabasesToolStripMenuItem
            // 
            this.clearDatabasesToolStripMenuItem.Name = "clearDatabasesToolStripMenuItem";
            this.clearDatabasesToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.clearDatabasesToolStripMenuItem.Text = "Clear databases";
            this.clearDatabasesToolStripMenuItem.Click += new System.EventHandler(this.clearDatabasesToolStripMenuItem_Click);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(149, 6);
            // 
            // writeSerializeToolStripMenuItem
            // 
            this.writeSerializeToolStripMenuItem.Name = "writeSerializeToolStripMenuItem";
            this.writeSerializeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.writeSerializeToolStripMenuItem.Text = "Write serialize";
            this.writeSerializeToolStripMenuItem.Click += new System.EventHandler(this.writeSerializeToolStripMenuItem_Click);
            // 
            // mNotifyIcon
            // 
            this.mNotifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.mNotifyIcon.BalloonTipTitle = "Current Stats";
            this.mNotifyIcon.ContextMenuStrip = this.mNotifyMenu;
            this.mNotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("mNotifyIcon.Icon")));
            this.mNotifyIcon.Text = "Typpo\'s DCT";
            this.mNotifyIcon.Visible = true;
            this.mNotifyIcon.DoubleClick += new System.EventHandler(this.mNotifyIcon_DoubleClick);
            this.mNotifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mNotifyIcon_MouseClick);
            // 
            // mNotifyMenu
            // 
            this.mNotifyMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.mNotifyMenu.Name = "mNotifyMenu";
            this.mNotifyMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.mNotifyMenu.Size = new System.Drawing.Size(101, 48);
            this.mNotifyMenu.Opening += new System.ComponentModel.CancelEventHandler(this.mNotifyMenu_Opening);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStrip
            // 
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnStart,
            this.btnStop,
            this.toolStripSeparator2,
            this.btnStartTimer,
            this.lblTimeLeft,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.cmbAttackMode,
            this.toolStripSeparator3,
            this.lblExpRage});
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.ShowItemToolTips = false;
            this.toolStrip.Size = new System.Drawing.Size(664, 25);
            this.toolStrip.TabIndex = 6;
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.Image = global::DCT.Properties.Resources.Flag_green;
            this.btnStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(56, 22);
            this.btnStart.Text = "Start";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Image = global::DCT.Properties.Resources.Flag_red;
            this.btnStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(49, 22);
            this.btnStop.Text = "Stop";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnStartTimer
            // 
            this.btnStartTimer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnStartTimer.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartTimer.Image = ((System.Drawing.Image)(resources.GetObject("btnStartTimer.Image")));
            this.btnStartTimer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStartTimer.Name = "btnStartTimer";
            this.btnStartTimer.Size = new System.Drawing.Size(62, 22);
            this.btnStartTimer.Text = "Start timer";
            this.btnStartTimer.Click += new System.EventHandler(this.btnStartTimer_Click);
            // 
            // lblTimeLeft
            // 
            this.lblTimeLeft.Name = "lblTimeLeft";
            this.lblTimeLeft.Size = new System.Drawing.Size(73, 22);
            this.lblTimeLeft.Text = "Time left: N/A";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(71, 22);
            this.toolStripLabel1.Text = "Attack mode:";
            // 
            // cmbAttackMode
            // 
            this.cmbAttackMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAttackMode.DropDownWidth = 121;
            this.cmbAttackMode.Items.AddRange(new object[] {
            "current area",
            "multi area",
            "mobs",
            "rooms"});
            this.cmbAttackMode.Name = "cmbAttackMode";
            this.cmbAttackMode.Size = new System.Drawing.Size(95, 25);
            this.cmbAttackMode.SelectedIndexChanged += new System.EventHandler(this.cmbAttackMode_SelectedIndexChanged);
            // 
            // splitLeftRight
            // 
            this.splitLeftRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitLeftRight.Location = new System.Drawing.Point(0, 49);
            this.splitLeftRight.Name = "splitLeftRight";
            // 
            // splitLeftRight.Panel2
            // 
            this.splitLeftRight.Panel2.Controls.Add(this.splitTopBottom);
            this.splitLeftRight.Size = new System.Drawing.Size(664, 415);
            this.splitLeftRight.SplitterDistance = 229;
            this.splitLeftRight.TabIndex = 7;
            // 
            // splitTopBottom
            // 
            this.splitTopBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitTopBottom.Location = new System.Drawing.Point(0, 0);
            this.splitTopBottom.Name = "splitTopBottom";
            this.splitTopBottom.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitTopBottom.Panel2
            // 
            this.splitTopBottom.Panel2.Controls.Add(this.tabs);
            this.splitTopBottom.Size = new System.Drawing.Size(431, 415);
            this.splitTopBottom.SplitterDistance = 102;
            this.splitTopBottom.TabIndex = 0;
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.tabAttack);
            this.tabs.Controls.Add(this.tabFilters);
            this.tabs.Controls.Add(this.tabRooms);
            this.tabs.Controls.Add(this.tabMobs);
            this.tabs.Controls.Add(this.tabJoiner);
            this.tabs.Controls.Add(this.tabSpawns);
            this.tabs.Controls.Add(this.tabTrainer);
            this.tabs.Controls.Add(this.tabChat);
            this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabs.Location = new System.Drawing.Point(0, 0);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(431, 309);
            this.tabs.TabIndex = 6;
            this.tabs.SelectedIndexChanged += new System.EventHandler(this.tabs_SelectedIndexChanged);
            // 
            // tabAttack
            // 
            this.tabAttack.Location = new System.Drawing.Point(4, 22);
            this.tabAttack.Name = "tabAttack";
            this.tabAttack.Padding = new System.Windows.Forms.Padding(3);
            this.tabAttack.Size = new System.Drawing.Size(423, 283);
            this.tabAttack.TabIndex = 0;
            this.tabAttack.Text = "Attack";
            this.tabAttack.UseVisualStyleBackColor = true;
            // 
            // tabFilters
            // 
            this.tabFilters.Location = new System.Drawing.Point(4, 22);
            this.tabFilters.Name = "tabFilters";
            this.tabFilters.Size = new System.Drawing.Size(423, 273);
            this.tabFilters.TabIndex = 7;
            this.tabFilters.Text = "Filters";
            this.tabFilters.UseVisualStyleBackColor = true;
            // 
            // tabRooms
            // 
            this.tabRooms.Location = new System.Drawing.Point(4, 22);
            this.tabRooms.Name = "tabRooms";
            this.tabRooms.Size = new System.Drawing.Size(423, 273);
            this.tabRooms.TabIndex = 5;
            this.tabRooms.Text = "Rooms";
            this.tabRooms.UseVisualStyleBackColor = true;
            // 
            // tabMobs
            // 
            this.tabMobs.Location = new System.Drawing.Point(4, 22);
            this.tabMobs.Name = "tabMobs";
            this.tabMobs.Size = new System.Drawing.Size(423, 273);
            this.tabMobs.TabIndex = 9;
            this.tabMobs.Text = "Mobs";
            this.tabMobs.UseVisualStyleBackColor = true;
            // 
            // tabJoiner
            // 
            this.tabJoiner.Location = new System.Drawing.Point(4, 22);
            this.tabJoiner.Name = "tabJoiner";
            this.tabJoiner.Size = new System.Drawing.Size(423, 273);
            this.tabJoiner.TabIndex = 11;
            this.tabJoiner.Text = "Raids";
            this.tabJoiner.UseVisualStyleBackColor = true;
            // 
            // tabSpawns
            // 
            this.tabSpawns.Location = new System.Drawing.Point(4, 22);
            this.tabSpawns.Name = "tabSpawns";
            this.tabSpawns.Size = new System.Drawing.Size(423, 273);
            this.tabSpawns.TabIndex = 12;
            this.tabSpawns.Text = "Spawns";
            this.tabSpawns.UseVisualStyleBackColor = true;
            // 
            // tabTrainer
            // 
            this.tabTrainer.Location = new System.Drawing.Point(4, 22);
            this.tabTrainer.Name = "tabTrainer";
            this.tabTrainer.Size = new System.Drawing.Size(423, 273);
            this.tabTrainer.TabIndex = 6;
            this.tabTrainer.Text = "Trainer";
            this.tabTrainer.UseVisualStyleBackColor = true;
            // 
            // tabChat
            // 
            this.tabChat.Location = new System.Drawing.Point(4, 22);
            this.tabChat.Name = "tabChat";
            this.tabChat.Size = new System.Drawing.Size(423, 273);
            this.tabChat.TabIndex = 10;
            this.tabChat.Text = "Chat";
            this.tabChat.UseVisualStyleBackColor = true;
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // lblExpRage
            // 
            this.lblExpRage.Name = "lblExpRage";
            this.lblExpRage.Size = new System.Drawing.Size(19, 22);
            this.lblExpRage.Text = "...";
            // 
            // CoreUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 486);
            this.Controls.Add(this.splitLeftRight);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.ss);
            this.Controls.Add(this.mnuMain);
            this.KeyPreview = true;
            this.MainMenuStrip = this.mnuMain;
            this.Name = "CoreUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.CoreUI_Load);
            this.ResizeBegin += new System.EventHandler(this.CoreUI_ResizeBegin);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CoreUI_FormClosing);
            this.ResizeEnd += new System.EventHandler(this.CoreUI_ResizeEnd);
            this.ss.ResumeLayout(false);
            this.ss.PerformLayout();
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.mNotifyMenu.ResumeLayout(false);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.splitLeftRight.Panel2.ResumeLayout(false);
            this.splitLeftRight.ResumeLayout(false);
            this.splitTopBottom.Panel2.ResumeLayout(false);
            this.splitTopBottom.ResumeLayout(false);
            this.tabs.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip ss;
        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripStatusLabel lblMisc;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem changesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem actionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripProgressBar pgr;
        private System.Windows.Forms.ToolStripMenuItem clearLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openInBrowserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem preferencesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearLogsPeriodicallyToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem openTyppousToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon mNotifyIcon;
        private System.Windows.Forms.ToolStripMenuItem minimizeToTrayToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ContextMenuStrip mNotifyMenu;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showSystrayIconWhenOpenToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem exportSpawnLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearSpawnLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spiderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem benchmarkToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem exportRoomsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportMobsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem clearDatabasesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnStart;
        private System.Windows.Forms.ToolStripButton btnStop;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton btnStartTimer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel lblTimeLeft;
        private System.Windows.Forms.SplitContainer splitLeftRight;
        private System.Windows.Forms.SplitContainer splitTopBottom;
        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tabAttack;
        private System.Windows.Forms.TabPage tabFilters;
        private System.Windows.Forms.TabPage tabRooms;
        private System.Windows.Forms.TabPage tabMobs;
        private System.Windows.Forms.TabPage tabJoiner;
        private System.Windows.Forms.TabPage tabSpawns;
        private System.Windows.Forms.TabPage tabTrainer;
        private System.Windows.Forms.TabPage tabChat;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem writeSerializeToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox cmbAttackMode;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripMenuItem inputRgsessidToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem showMyRgsessidToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel lblExpRage;
    }
}