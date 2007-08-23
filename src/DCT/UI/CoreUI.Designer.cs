using ChatUI=DCT.Protocols.IRC.ChatUI;

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
            irc = new ChatUI();
            this.ss = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblMisc = new System.Windows.Forms.ToolStripStatusLabel();
            this.pgr = new System.Windows.Forms.ToolStripProgressBar();
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.changesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openInBrowserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabAttack = new System.Windows.Forms.TabPage();
            this.grpSettings = new System.Windows.Forms.GroupBox();
            this.chkVariance = new System.Windows.Forms.CheckBox();
            this.numThreadDelay = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.numTimeout = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.chkReturnToStart = new System.Windows.Forms.CheckBox();
            this.numRageLimit = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.numLevelMin = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.numRageStop = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.chkVault = new System.Windows.Forms.CheckBox();
            this.numLevel = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbPause = new System.Windows.Forms.ComboBox();
            this.chkAttackPause = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabFilters = new System.Windows.Forms.TabPage();
            this.btnFilterLoad = new System.Windows.Forms.Button();
            this.btnFilterSave = new System.Windows.Forms.Button();
            this.txtFilters = new System.Windows.Forms.TextBox();
            this.chkFilter = new System.Windows.Forms.CheckBox();
            this.tabRooms = new System.Windows.Forms.TabPage();
            this.lnkLoadRooms = new System.Windows.Forms.LinkLabel();
            this.optPathfindChoose = new System.Windows.Forms.RadioButton();
            this.btnPathfind = new System.Windows.Forms.Button();
            this.numPathfindId = new System.Windows.Forms.NumericUpDown();
            this.lvPathfind = new System.Windows.Forms.ListView();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.optPathfindID = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.lnkUncheckRooms = new System.Windows.Forms.LinkLabel();
            this.tabMobs = new System.Windows.Forms.TabPage();
            this.lnkMobLoad = new System.Windows.Forms.LinkLabel();
            this.lnkUncheckMobs = new System.Windows.Forms.LinkLabel();
            this.lvMobs = new System.Windows.Forms.ListView();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.tabJoiner = new System.Windows.Forms.TabPage();
            this.numRaidInterval = new System.Windows.Forms.NumericUpDown();
            this.label15 = new System.Windows.Forms.Label();
            this.chkAutoJoin = new System.Windows.Forms.CheckBox();
            this.btnAdventuresGo = new System.Windows.Forms.Button();
            this.lvAdventures = new System.Windows.Forms.ListView();
            this.clmName = new System.Windows.Forms.ColumnHeader();
            this.clmRoomID = new System.Windows.Forms.ColumnHeader();
            this.label16 = new System.Windows.Forms.Label();
            this.tabFormer = new System.Windows.Forms.TabPage();
            this.chkAutoForm = new System.Windows.Forms.CheckBox();
            this.cmbFormer = new System.Windows.Forms.ComboBox();
            this.tabTrainer = new System.Windows.Forms.TabPage();
            this.chkAutoTrain = new System.Windows.Forms.CheckBox();
            this.chkTrainReturn = new System.Windows.Forms.CheckBox();
            this.lblTrain = new System.Windows.Forms.Label();
            this.btnTrain = new System.Windows.Forms.Button();
            this.tabQuests = new System.Windows.Forms.TabPage();
            this.optQuestsNothing = new System.Windows.Forms.RadioButton();
            this.optQuestsAlert = new System.Windows.Forms.RadioButton();
            this.optQuestsAuto = new System.Windows.Forms.RadioButton();
            this.tabChat = new System.Windows.Forms.TabPage();
            this.lstLog = new System.Windows.Forms.ListBox();
            this.lstAttacks = new System.Windows.Forms.ListBox();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.grpConnections = new System.Windows.Forms.GroupBox();
            this.chkRemember = new System.Windows.Forms.CheckBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lnkAccountsInvert = new System.Windows.Forms.LinkLabel();
            this.lnkAccountsCheckNone = new System.Windows.Forms.LinkLabel();
            this.lnkAccountsCheckAll = new System.Windows.Forms.LinkLabel();
            this.lvAccounts = new System.Windows.Forms.ListView();
            this.clmCharName = new System.Windows.Forms.ColumnHeader();
            this.clmStatus = new System.Windows.Forms.ColumnHeader();
            this.clmInRoom = new System.Windows.Forms.ColumnHeader();
            this.clmRooms = new System.Windows.Forms.ColumnHeader();
            this.clmMobs = new System.Windows.Forms.ColumnHeader();
            this.clmEXP = new System.Windows.Forms.ColumnHeader();
            this.pnlAttack = new System.Windows.Forms.Panel();
            this.chkHourTimer = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnAttackStart = new System.Windows.Forms.Button();
            this.btnAttackStop = new System.Windows.Forms.Button();
            this.optCountdownMobs = new System.Windows.Forms.RadioButton();
            this.optCountdownMulti = new System.Windows.Forms.RadioButton();
            this.optCountdownSingle = new System.Windows.Forms.RadioButton();
            this.lnkStartCountdown = new System.Windows.Forms.LinkLabel();
            this.lblTimeLeft = new System.Windows.Forms.Label();
            this.numCountdown = new System.Windows.Forms.NumericUpDown();
            this.lblTimer = new System.Windows.Forms.Label();
            this.chkCountdownTimer = new System.Windows.Forms.CheckBox();
            this.lblExpRage = new System.Windows.Forms.Label();
            this.ss.SuspendLayout();
            this.mnuMain.SuspendLayout();
            this.tabAttack.SuspendLayout();
            this.grpSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numThreadDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRageLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLevelMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRageStop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLevel)).BeginInit();
            this.tabs.SuspendLayout();
            this.tabFilters.SuspendLayout();
            this.tabRooms.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPathfindId)).BeginInit();
            this.tabMobs.SuspendLayout();
            this.tabJoiner.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRaidInterval)).BeginInit();
            this.tabFormer.SuspendLayout();
            this.tabTrainer.SuspendLayout();
            this.tabQuests.SuspendLayout();
            this.tabChat.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.grpConnections.SuspendLayout();
            this.pnlAttack.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCountdown)).BeginInit();
            this.SuspendLayout();
            // 
            // irc
            // 
            this.irc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.irc.Location = new System.Drawing.Point(0, 0);
            this.irc.Name = "irc";
            this.irc.Size = new System.Drawing.Size(426, 229);
            this.irc.TabIndex = 0;
            // 
            // ss
            // 
            this.ss.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.lblMisc,
            this.pgr});
            this.ss.Location = new System.Drawing.Point(0, 463);
            this.ss.Name = "ss";
            this.ss.Size = new System.Drawing.Size(664, 22);
            this.ss.SizingGrip = false;
            this.ss.TabIndex = 3;
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 17);
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
            this.pgr.Size = new System.Drawing.Size(580, 16);
            this.pgr.Step = 1;
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.actionsToolStripMenuItem});
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
            this.exportLogToolStripMenuItem,
            this.clearLogToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(134, 22);
            this.toolStripMenuItem2.Text = "About";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // changesToolStripMenuItem
            // 
            this.changesToolStripMenuItem.Name = "changesToolStripMenuItem";
            this.changesToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.changesToolStripMenuItem.Text = "Changes";
            this.changesToolStripMenuItem.Click += new System.EventHandler(this.changesToolStripMenuItem_Click);
            // 
            // exportLogToolStripMenuItem
            // 
            this.exportLogToolStripMenuItem.Name = "exportLogToolStripMenuItem";
            this.exportLogToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.exportLogToolStripMenuItem.Text = "Export log";
            this.exportLogToolStripMenuItem.Click += new System.EventHandler(this.exportLogToolStripMenuItem_Click);
            // 
            // clearLogToolStripMenuItem
            // 
            this.clearLogToolStripMenuItem.Name = "clearLogToolStripMenuItem";
            this.clearLogToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.clearLogToolStripMenuItem.Text = "Clear log";
            this.clearLogToolStripMenuItem.Click += new System.EventHandler(this.clearLogToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(131, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // actionsToolStripMenuItem
            // 
            this.actionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openInBrowserToolStripMenuItem});
            this.actionsToolStripMenuItem.Name = "actionsToolStripMenuItem";
            this.actionsToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.actionsToolStripMenuItem.Text = "Actions";
            // 
            // openInBrowserToolStripMenuItem
            // 
            this.openInBrowserToolStripMenuItem.Name = "openInBrowserToolStripMenuItem";
            this.openInBrowserToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.openInBrowserToolStripMenuItem.Text = "Open in browser";
            this.openInBrowserToolStripMenuItem.Click += new System.EventHandler(this.openInBrowserToolStripMenuItem_Click);
            // 
            // tabAttack
            // 
            this.tabAttack.Controls.Add(this.grpSettings);
            this.tabAttack.Location = new System.Drawing.Point(4, 22);
            this.tabAttack.Name = "tabAttack";
            this.tabAttack.Padding = new System.Windows.Forms.Padding(3);
            this.tabAttack.Size = new System.Drawing.Size(426, 229);
            this.tabAttack.TabIndex = 0;
            this.tabAttack.Text = "Attack";
            this.tabAttack.UseVisualStyleBackColor = true;
            // 
            // grpSettings
            // 
            this.grpSettings.Controls.Add(this.chkVariance);
            this.grpSettings.Controls.Add(this.numThreadDelay);
            this.grpSettings.Controls.Add(this.label9);
            this.grpSettings.Controls.Add(this.label8);
            this.grpSettings.Controls.Add(this.numTimeout);
            this.grpSettings.Controls.Add(this.label5);
            this.grpSettings.Controls.Add(this.chkReturnToStart);
            this.grpSettings.Controls.Add(this.numRageLimit);
            this.grpSettings.Controls.Add(this.label13);
            this.grpSettings.Controls.Add(this.numLevelMin);
            this.grpSettings.Controls.Add(this.label12);
            this.grpSettings.Controls.Add(this.numRageStop);
            this.grpSettings.Controls.Add(this.label11);
            this.grpSettings.Controls.Add(this.chkVault);
            this.grpSettings.Controls.Add(this.numLevel);
            this.grpSettings.Controls.Add(this.label4);
            this.grpSettings.Controls.Add(this.cmbPause);
            this.grpSettings.Controls.Add(this.chkAttackPause);
            this.grpSettings.Controls.Add(this.label14);
            this.grpSettings.Location = new System.Drawing.Point(6, 6);
            this.grpSettings.Name = "grpSettings";
            this.grpSettings.Size = new System.Drawing.Size(414, 217);
            this.grpSettings.TabIndex = 4;
            this.grpSettings.TabStop = false;
            this.grpSettings.Text = "Settings";
            // 
            // chkVariance
            // 
            this.chkVariance.AutoSize = true;
            this.chkVariance.Checked = true;
            this.chkVariance.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkVariance.Location = new System.Drawing.Point(198, 104);
            this.chkVariance.Name = "chkVariance";
            this.chkVariance.Size = new System.Drawing.Size(130, 17);
            this.chkVariance.TabIndex = 36;
            this.chkVariance.Text = "Global timing variance";
            this.chkVariance.UseVisualStyleBackColor = true;
            this.chkVariance.CheckedChanged += new System.EventHandler(this.chkVariance_CheckedChanged);
            // 
            // numThreadDelay
            // 
            this.numThreadDelay.Location = new System.Drawing.Point(273, 44);
            this.numThreadDelay.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numThreadDelay.Name = "numThreadDelay";
            this.numThreadDelay.Size = new System.Drawing.Size(81, 20);
            this.numThreadDelay.TabIndex = 34;
            this.numThreadDelay.ThousandsSeparator = true;
            this.numThreadDelay.ValueChanged += new System.EventHandler(this.numThreadDelay_ValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(195, 46);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 13);
            this.label9.TabIndex = 35;
            this.label9.Text = "Thread delay:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(180, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 13);
            this.label8.TabIndex = 33;
            this.label8.Text = "Advanced:";
            // 
            // numTimeout
            // 
            this.numTimeout.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numTimeout.Location = new System.Drawing.Point(288, 73);
            this.numTimeout.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numTimeout.Minimum = new decimal(new int[] {
            4000,
            0,
            0,
            0});
            this.numTimeout.Name = "numTimeout";
            this.numTimeout.Size = new System.Drawing.Size(75, 20);
            this.numTimeout.TabIndex = 31;
            this.numTimeout.ThousandsSeparator = true;
            this.numTimeout.Value = new decimal(new int[] {
            8000,
            0,
            0,
            0});
            this.numTimeout.ValueChanged += new System.EventHandler(this.numTimeout_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(195, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 13);
            this.label5.TabIndex = 29;
            this.label5.Text = "Request timeout:";
            // 
            // chkReturnToStart
            // 
            this.chkReturnToStart.AutoSize = true;
            this.chkReturnToStart.Location = new System.Drawing.Point(15, 75);
            this.chkReturnToStart.Name = "chkReturnToStart";
            this.chkReturnToStart.Size = new System.Drawing.Size(133, 30);
            this.chkReturnToStart.TabIndex = 28;
            this.chkReturnToStart.Text = "Return to starting room\r\nafter attack run";
            this.chkReturnToStart.UseVisualStyleBackColor = true;
            this.chkReturnToStart.CheckedChanged += new System.EventHandler(this.chkReturnToStart_CheckedChanged);
            // 
            // numRageLimit
            // 
            this.numRageLimit.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numRageLimit.Location = new System.Drawing.Point(73, 152);
            this.numRageLimit.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numRageLimit.Name = "numRageLimit";
            this.numRageLimit.Size = new System.Drawing.Size(67, 18);
            this.numRageLimit.TabIndex = 17;
            this.numRageLimit.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numRageLimit.ValueChanged += new System.EventHandler(this.numRageLimit_ValueChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(8, 153);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(65, 13);
            this.label13.TabIndex = 16;
            this.label13.Text = "Attack up to";
            // 
            // numLevelMin
            // 
            this.numLevelMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numLevelMin.Location = new System.Drawing.Point(33, 128);
            this.numLevelMin.Name = "numLevelMin";
            this.numLevelMin.Size = new System.Drawing.Size(36, 18);
            this.numLevelMin.TabIndex = 3;
            this.numLevelMin.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numLevelMin.ValueChanged += new System.EventHandler(this.numLevelMin_ValueChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(75, 129);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(25, 13);
            this.label12.TabIndex = 15;
            this.label12.Text = "and";
            // 
            // numRageStop
            // 
            this.numRageStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numRageStop.Location = new System.Drawing.Point(105, 175);
            this.numRageStop.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numRageStop.Name = "numRageStop";
            this.numRageStop.Size = new System.Drawing.Size(43, 18);
            this.numRageStop.TabIndex = 5;
            this.numRageStop.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numRageStop.ValueChanged += new System.EventHandler(this.numRageStop_ValueChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 178);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(95, 13);
            this.label11.TabIndex = 14;
            this.label11.Text = "Stop with rage left:";
            // 
            // chkVault
            // 
            this.chkVault.AutoSize = true;
            this.chkVault.Enabled = false;
            this.chkVault.Location = new System.Drawing.Point(11, 19);
            this.chkVault.Name = "chkVault";
            this.chkVault.Size = new System.Drawing.Size(116, 17);
            this.chkVault.TabIndex = 0;
            this.chkVault.Text = "Send items to vault";
            this.chkVault.UseVisualStyleBackColor = true;
            this.chkVault.CheckedChanged += new System.EventHandler(this.chkVault_CheckedChanged);
            // 
            // numLevel
            // 
            this.numLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numLevel.Location = new System.Drawing.Point(105, 128);
            this.numLevel.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numLevel.Name = "numLevel";
            this.numLevel.Size = new System.Drawing.Size(36, 18);
            this.numLevel.TabIndex = 4;
            this.numLevel.Value = new decimal(new int[] {
            62,
            0,
            0,
            0});
            this.numLevel.ValueChanged += new System.EventHandler(this.numLevel_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Attack between levels:";
            // 
            // cmbPause
            // 
            this.cmbPause.Enabled = false;
            this.cmbPause.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPause.FormattingEnabled = true;
            this.cmbPause.Items.AddRange(new object[] {
            "Both",
            "Pack",
            "Vault"});
            this.cmbPause.Location = new System.Drawing.Point(91, 39);
            this.cmbPause.Name = "cmbPause";
            this.cmbPause.Size = new System.Drawing.Size(57, 20);
            this.cmbPause.TabIndex = 2;
            this.cmbPause.Text = "Both";
            this.cmbPause.SelectedIndexChanged += new System.EventHandler(this.cmbPause_SelectedIndexChanged);
            // 
            // chkAttackPause
            // 
            this.chkAttackPause.AutoSize = true;
            this.chkAttackPause.Checked = true;
            this.chkAttackPause.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAttackPause.Enabled = false;
            this.chkAttackPause.Location = new System.Drawing.Point(11, 42);
            this.chkAttackPause.Name = "chkAttackPause";
            this.chkAttackPause.Size = new System.Drawing.Size(83, 17);
            this.chkAttackPause.TabIndex = 1;
            this.chkAttackPause.Text = "Pause if full:";
            this.chkAttackPause.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(140, 153);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(28, 13);
            this.label14.TabIndex = 18;
            this.label14.Text = "rage";
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.tabAttack);
            this.tabs.Controls.Add(this.tabFilters);
            this.tabs.Controls.Add(this.tabRooms);
            this.tabs.Controls.Add(this.tabMobs);
            this.tabs.Controls.Add(this.tabJoiner);
            this.tabs.Controls.Add(this.tabFormer);
            this.tabs.Controls.Add(this.tabTrainer);
            this.tabs.Controls.Add(this.tabQuests);
            this.tabs.Controls.Add(this.tabChat);
            this.tabs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabs.Location = new System.Drawing.Point(6, 200);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(434, 255);
            this.tabs.TabIndex = 5;
            // 
            // tabFilters
            // 
            this.tabFilters.Controls.Add(this.btnFilterLoad);
            this.tabFilters.Controls.Add(this.btnFilterSave);
            this.tabFilters.Controls.Add(this.txtFilters);
            this.tabFilters.Controls.Add(this.chkFilter);
            this.tabFilters.Location = new System.Drawing.Point(4, 22);
            this.tabFilters.Name = "tabFilters";
            this.tabFilters.Size = new System.Drawing.Size(426, 229);
            this.tabFilters.TabIndex = 7;
            this.tabFilters.Text = "Filters";
            this.tabFilters.UseVisualStyleBackColor = true;
            // 
            // btnFilterLoad
            // 
            this.btnFilterLoad.Location = new System.Drawing.Point(220, 193);
            this.btnFilterLoad.Name = "btnFilterLoad";
            this.btnFilterLoad.Size = new System.Drawing.Size(75, 23);
            this.btnFilterLoad.TabIndex = 3;
            this.btnFilterLoad.Text = "Load Filters";
            this.btnFilterLoad.UseVisualStyleBackColor = true;
            this.btnFilterLoad.Click += new System.EventHandler(this.btnFilterLoad_Click);
            // 
            // btnFilterSave
            // 
            this.btnFilterSave.Location = new System.Drawing.Point(139, 193);
            this.btnFilterSave.Name = "btnFilterSave";
            this.btnFilterSave.Size = new System.Drawing.Size(75, 23);
            this.btnFilterSave.TabIndex = 2;
            this.btnFilterSave.Text = "Save Filters";
            this.btnFilterSave.UseVisualStyleBackColor = true;
            this.btnFilterSave.Click += new System.EventHandler(this.btnFilterSave_Click);
            // 
            // txtFilters
            // 
            this.txtFilters.Enabled = false;
            this.txtFilters.Location = new System.Drawing.Point(73, 37);
            this.txtFilters.Multiline = true;
            this.txtFilters.Name = "txtFilters";
            this.txtFilters.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtFilters.Size = new System.Drawing.Size(278, 150);
            this.txtFilters.TabIndex = 1;
            this.txtFilters.TextChanged += new System.EventHandler(this.txtFilters_TextChanged);
            // 
            // chkFilter
            // 
            this.chkFilter.AutoSize = true;
            this.chkFilter.Location = new System.Drawing.Point(27, 14);
            this.chkFilter.Name = "chkFilter";
            this.chkFilter.Size = new System.Drawing.Size(367, 17);
            this.chkFilter.TabIndex = 0;
            this.chkFilter.Text = "Only attack mobs with these words in their names (separate by new line):";
            this.chkFilter.UseVisualStyleBackColor = true;
            this.chkFilter.CheckedChanged += new System.EventHandler(this.chkFilter_CheckedChanged);
            // 
            // tabRooms
            // 
            this.tabRooms.Controls.Add(this.lnkLoadRooms);
            this.tabRooms.Controls.Add(this.optPathfindChoose);
            this.tabRooms.Controls.Add(this.btnPathfind);
            this.tabRooms.Controls.Add(this.numPathfindId);
            this.tabRooms.Controls.Add(this.lvPathfind);
            this.tabRooms.Controls.Add(this.optPathfindID);
            this.tabRooms.Controls.Add(this.label6);
            this.tabRooms.Controls.Add(this.lnkUncheckRooms);
            this.tabRooms.Location = new System.Drawing.Point(4, 22);
            this.tabRooms.Name = "tabRooms";
            this.tabRooms.Size = new System.Drawing.Size(426, 229);
            this.tabRooms.TabIndex = 5;
            this.tabRooms.Text = "Rooms";
            this.tabRooms.UseVisualStyleBackColor = true;
            // 
            // lnkLoadRooms
            // 
            this.lnkLoadRooms.AutoSize = true;
            this.lnkLoadRooms.Location = new System.Drawing.Point(74, 216);
            this.lnkLoadRooms.Name = "lnkLoadRooms";
            this.lnkLoadRooms.Size = new System.Drawing.Size(82, 13);
            this.lnkLoadRooms.TabIndex = 6;
            this.lnkLoadRooms.TabStop = true;
            this.lnkLoadRooms.Text = "Import rooms list";
            this.lnkLoadRooms.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLoadRooms_LinkClicked);
            // 
            // optPathfindChoose
            // 
            this.optPathfindChoose.AutoSize = true;
            this.optPathfindChoose.Checked = true;
            this.optPathfindChoose.Location = new System.Drawing.Point(244, 9);
            this.optPathfindChoose.Name = "optPathfindChoose";
            this.optPathfindChoose.Size = new System.Drawing.Size(99, 17);
            this.optPathfindChoose.TabIndex = 2;
            this.optPathfindChoose.TabStop = true;
            this.optPathfindChoose.Text = "Choose from list";
            this.optPathfindChoose.UseVisualStyleBackColor = true;
            // 
            // btnPathfind
            // 
            this.btnPathfind.Location = new System.Drawing.Point(384, 6);
            this.btnPathfind.Name = "btnPathfind";
            this.btnPathfind.Size = new System.Drawing.Size(31, 23);
            this.btnPathfind.TabIndex = 3;
            this.btnPathfind.Text = "Go";
            this.btnPathfind.UseVisualStyleBackColor = true;
            this.btnPathfind.Click += new System.EventHandler(this.btnPathfind_Click);
            // 
            // numPathfindId
            // 
            this.numPathfindId.Location = new System.Drawing.Point(154, 9);
            this.numPathfindId.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numPathfindId.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numPathfindId.Name = "numPathfindId";
            this.numPathfindId.Size = new System.Drawing.Size(54, 20);
            this.numPathfindId.TabIndex = 1;
            this.numPathfindId.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lvPathfind
            // 
            this.lvPathfind.CheckBoxes = true;
            this.lvPathfind.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader1});
            this.lvPathfind.FullRowSelect = true;
            this.lvPathfind.GridLines = true;
            this.lvPathfind.Location = new System.Drawing.Point(3, 35);
            this.lvPathfind.MultiSelect = false;
            this.lvPathfind.Name = "lvPathfind";
            this.lvPathfind.Size = new System.Drawing.Size(412, 178);
            this.lvPathfind.TabIndex = 4;
            this.lvPathfind.UseCompatibleStateImageBehavior = false;
            this.lvPathfind.View = System.Windows.Forms.View.Details;
            this.lvPathfind.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvPathfind_ColumnClick);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Name";
            this.columnHeader2.Width = 250;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "ID";
            this.columnHeader1.Width = 125;
            // 
            // optPathfindID
            // 
            this.optPathfindID.AutoSize = true;
            this.optPathfindID.Location = new System.Drawing.Point(80, 9);
            this.optPathfindID.Name = "optPathfindID";
            this.optPathfindID.Size = new System.Drawing.Size(77, 17);
            this.optPathfindID.TabIndex = 0;
            this.optPathfindID.Text = "Room ID#:";
            this.optPathfindID.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Move to:";
            // 
            // lnkUncheckRooms
            // 
            this.lnkUncheckRooms.AutoSize = true;
            this.lnkUncheckRooms.Location = new System.Drawing.Point(3, 216);
            this.lnkUncheckRooms.Name = "lnkUncheckRooms";
            this.lnkUncheckRooms.Size = new System.Drawing.Size(65, 13);
            this.lnkUncheckRooms.TabIndex = 5;
            this.lnkUncheckRooms.TabStop = true;
            this.lnkUncheckRooms.Text = "Uncheck All";
            this.lnkUncheckRooms.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkUncheckAll_LinkClicked);
            // 
            // tabMobs
            // 
            this.tabMobs.Controls.Add(this.lnkMobLoad);
            this.tabMobs.Controls.Add(this.lnkUncheckMobs);
            this.tabMobs.Controls.Add(this.lvMobs);
            this.tabMobs.Location = new System.Drawing.Point(4, 22);
            this.tabMobs.Name = "tabMobs";
            this.tabMobs.Size = new System.Drawing.Size(426, 229);
            this.tabMobs.TabIndex = 9;
            this.tabMobs.Text = "Mobs";
            this.tabMobs.UseVisualStyleBackColor = true;
            // 
            // lnkMobLoad
            // 
            this.lnkMobLoad.AutoSize = true;
            this.lnkMobLoad.Location = new System.Drawing.Point(74, 216);
            this.lnkMobLoad.Name = "lnkMobLoad";
            this.lnkMobLoad.Size = new System.Drawing.Size(79, 13);
            this.lnkMobLoad.TabIndex = 2;
            this.lnkMobLoad.TabStop = true;
            this.lnkMobLoad.Text = "Import mobs list";
            this.lnkMobLoad.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkMobLoad_LinkClicked);
            // 
            // lnkUncheckMobs
            // 
            this.lnkUncheckMobs.AutoSize = true;
            this.lnkUncheckMobs.Location = new System.Drawing.Point(3, 216);
            this.lnkUncheckMobs.Name = "lnkUncheckMobs";
            this.lnkUncheckMobs.Size = new System.Drawing.Size(65, 13);
            this.lnkUncheckMobs.TabIndex = 1;
            this.lnkUncheckMobs.TabStop = true;
            this.lnkUncheckMobs.Text = "Uncheck All";
            this.lnkUncheckMobs.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkUncheckMobs_LinkClicked);
            // 
            // lvMobs
            // 
            this.lvMobs.CheckBoxes = true;
            this.lvMobs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7});
            this.lvMobs.FullRowSelect = true;
            this.lvMobs.GridLines = true;
            this.lvMobs.Location = new System.Drawing.Point(3, 0);
            this.lvMobs.Name = "lvMobs";
            this.lvMobs.Size = new System.Drawing.Size(414, 213);
            this.lvMobs.TabIndex = 0;
            this.lvMobs.UseCompatibleStateImageBehavior = false;
            this.lvMobs.View = System.Windows.Forms.View.Details;
            this.lvMobs.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvMobs_ColumnClick);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Name";
            this.columnHeader3.Width = 100;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "ID";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Room";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Level";
            this.columnHeader6.Width = 80;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Rage";
            this.columnHeader7.Width = 80;
            // 
            // tabJoiner
            // 
            this.tabJoiner.Controls.Add(this.numRaidInterval);
            this.tabJoiner.Controls.Add(this.label15);
            this.tabJoiner.Controls.Add(this.chkAutoJoin);
            this.tabJoiner.Controls.Add(this.btnAdventuresGo);
            this.tabJoiner.Controls.Add(this.lvAdventures);
            this.tabJoiner.Controls.Add(this.label16);
            this.tabJoiner.Location = new System.Drawing.Point(4, 22);
            this.tabJoiner.Name = "tabJoiner";
            this.tabJoiner.Size = new System.Drawing.Size(426, 229);
            this.tabJoiner.TabIndex = 11;
            this.tabJoiner.Text = "Joiner";
            this.tabJoiner.UseVisualStyleBackColor = true;
            // 
            // numRaidInterval
            // 
            this.numRaidInterval.Enabled = false;
            this.numRaidInterval.Location = new System.Drawing.Point(183, 24);
            this.numRaidInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRaidInterval.Name = "numRaidInterval";
            this.numRaidInterval.Size = new System.Drawing.Size(46, 20);
            this.numRaidInterval.TabIndex = 7;
            this.numRaidInterval.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRaidInterval.ValueChanged += new System.EventHandler(this.numRaidInterval_ValueChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(51, 26);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(229, 13);
            this.label15.TabIndex = 6;
            this.label15.Text = "Check for new raids every                   minute(s)";
            // 
            // chkAutoJoin
            // 
            this.chkAutoJoin.AutoSize = true;
            this.chkAutoJoin.Enabled = false;
            this.chkAutoJoin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAutoJoin.Location = new System.Drawing.Point(14, 8);
            this.chkAutoJoin.Name = "chkAutoJoin";
            this.chkAutoJoin.Size = new System.Drawing.Size(421, 17);
            this.chkAutoJoin.TabIndex = 5;
            this.chkAutoJoin.Text = "Auto Joining - move to/join all checked checked raids when spawned";
            this.chkAutoJoin.UseVisualStyleBackColor = true;
            this.chkAutoJoin.CheckedChanged += new System.EventHandler(this.chkAutoJoin_CheckedChanged);
            // 
            // btnAdventuresGo
            // 
            this.btnAdventuresGo.Location = new System.Drawing.Point(240, 52);
            this.btnAdventuresGo.Name = "btnAdventuresGo";
            this.btnAdventuresGo.Size = new System.Drawing.Size(31, 23);
            this.btnAdventuresGo.TabIndex = 4;
            this.btnAdventuresGo.Text = "Go";
            this.btnAdventuresGo.UseVisualStyleBackColor = true;
            this.btnAdventuresGo.Click += new System.EventHandler(this.btnAdventuresGo_Click);
            // 
            // lvAdventures
            // 
            this.lvAdventures.CheckBoxes = true;
            this.lvAdventures.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmName,
            this.clmRoomID});
            this.lvAdventures.FullRowSelect = true;
            this.lvAdventures.Location = new System.Drawing.Point(0, 81);
            this.lvAdventures.MultiSelect = false;
            this.lvAdventures.Name = "lvAdventures";
            this.lvAdventures.Size = new System.Drawing.Size(417, 145);
            this.lvAdventures.TabIndex = 1;
            this.lvAdventures.UseCompatibleStateImageBehavior = false;
            this.lvAdventures.View = System.Windows.Forms.View.Details;
            // 
            // clmName
            // 
            this.clmName.Text = "Name";
            this.clmName.Width = 200;
            // 
            // clmRoomID
            // 
            this.clmRoomID.Text = "Room ID";
            this.clmRoomID.Width = 100;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(11, 59);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(223, 13);
            this.label16.TabIndex = 0;
            this.label16.Text = "Move all checked accounts to selected room:";
            // 
            // tabFormer
            // 
            this.tabFormer.Controls.Add(this.chkAutoForm);
            this.tabFormer.Controls.Add(this.cmbFormer);
            this.tabFormer.Location = new System.Drawing.Point(4, 22);
            this.tabFormer.Name = "tabFormer";
            this.tabFormer.Size = new System.Drawing.Size(426, 229);
            this.tabFormer.TabIndex = 12;
            this.tabFormer.Text = "Former";
            this.tabFormer.UseVisualStyleBackColor = true;
            // 
            // chkAutoForm
            // 
            this.chkAutoForm.AutoSize = true;
            this.chkAutoForm.Enabled = false;
            this.chkAutoForm.Location = new System.Drawing.Point(3, 18);
            this.chkAutoForm.Name = "chkAutoForm";
            this.chkAutoForm.Size = new System.Drawing.Size(297, 17);
            this.chkAutoForm.TabIndex = 1;
            this.chkAutoForm.Text = "Use this account to form checked raids at the \'Joiner\' tab:";
            this.chkAutoForm.UseVisualStyleBackColor = true;
            // 
            // cmbFormer
            // 
            this.cmbFormer.Enabled = false;
            this.cmbFormer.FormattingEnabled = true;
            this.cmbFormer.Location = new System.Drawing.Point(23, 41);
            this.cmbFormer.Name = "cmbFormer";
            this.cmbFormer.Size = new System.Drawing.Size(134, 21);
            this.cmbFormer.TabIndex = 0;
            this.cmbFormer.Text = "Choose...";
            // 
            // tabTrainer
            // 
            this.tabTrainer.Controls.Add(this.chkAutoTrain);
            this.tabTrainer.Controls.Add(this.chkTrainReturn);
            this.tabTrainer.Controls.Add(this.lblTrain);
            this.tabTrainer.Controls.Add(this.btnTrain);
            this.tabTrainer.Location = new System.Drawing.Point(4, 22);
            this.tabTrainer.Name = "tabTrainer";
            this.tabTrainer.Size = new System.Drawing.Size(426, 229);
            this.tabTrainer.TabIndex = 6;
            this.tabTrainer.Text = "Trainer";
            this.tabTrainer.UseVisualStyleBackColor = true;
            // 
            // chkAutoTrain
            // 
            this.chkAutoTrain.AutoSize = true;
            this.chkAutoTrain.Checked = true;
            this.chkAutoTrain.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoTrain.Location = new System.Drawing.Point(14, 12);
            this.chkAutoTrain.Name = "chkAutoTrain";
            this.chkAutoTrain.Size = new System.Drawing.Size(362, 17);
            this.chkAutoTrain.TabIndex = 0;
            this.chkAutoTrain.Text = "Automatically train accounts that need leveling in rooms with bartenders";
            this.chkAutoTrain.UseVisualStyleBackColor = true;
            this.chkAutoTrain.CheckedChanged += new System.EventHandler(this.chkAutoTrain_CheckedChanged);
            // 
            // chkTrainReturn
            // 
            this.chkTrainReturn.AutoSize = true;
            this.chkTrainReturn.Location = new System.Drawing.Point(51, 70);
            this.chkTrainReturn.Name = "chkTrainReturn";
            this.chkTrainReturn.Size = new System.Drawing.Size(132, 17);
            this.chkTrainReturn.TabIndex = 1;
            this.chkTrainReturn.Text = "Move back afterwards";
            this.chkTrainReturn.UseVisualStyleBackColor = true;
            // 
            // lblTrain
            // 
            this.lblTrain.AutoSize = true;
            this.lblTrain.Location = new System.Drawing.Point(11, 54);
            this.lblTrain.Name = "lblTrain";
            this.lblTrain.Size = new System.Drawing.Size(344, 13);
            this.lblTrain.TabIndex = 1;
            this.lblTrain.Text = "The checked accounts will be moved to the nearest trainer and trained.";
            // 
            // btnTrain
            // 
            this.btnTrain.Location = new System.Drawing.Point(51, 93);
            this.btnTrain.Name = "btnTrain";
            this.btnTrain.Size = new System.Drawing.Size(66, 23);
            this.btnTrain.TabIndex = 2;
            this.btnTrain.Text = "Go";
            this.btnTrain.UseVisualStyleBackColor = true;
            this.btnTrain.Click += new System.EventHandler(this.btnTrain_Click);
            // 
            // tabQuests
            // 
            this.tabQuests.Controls.Add(this.optQuestsNothing);
            this.tabQuests.Controls.Add(this.optQuestsAlert);
            this.tabQuests.Controls.Add(this.optQuestsAuto);
            this.tabQuests.Location = new System.Drawing.Point(4, 22);
            this.tabQuests.Name = "tabQuests";
            this.tabQuests.Size = new System.Drawing.Size(426, 229);
            this.tabQuests.TabIndex = 8;
            this.tabQuests.Text = "Quests";
            this.tabQuests.UseVisualStyleBackColor = true;
            // 
            // optQuestsNothing
            // 
            this.optQuestsNothing.AutoSize = true;
            this.optQuestsNothing.Checked = true;
            this.optQuestsNothing.Location = new System.Drawing.Point(19, 63);
            this.optQuestsNothing.Name = "optQuestsNothing";
            this.optQuestsNothing.Size = new System.Drawing.Size(77, 17);
            this.optQuestsNothing.TabIndex = 2;
            this.optQuestsNothing.TabStop = true;
            this.optQuestsNothing.Text = "Do nothing";
            this.optQuestsNothing.UseVisualStyleBackColor = true;
            this.optQuestsNothing.CheckedChanged += new System.EventHandler(this.optQuestsNothing_CheckedChanged);
            // 
            // optQuestsAlert
            // 
            this.optQuestsAlert.AutoSize = true;
            this.optQuestsAlert.Location = new System.Drawing.Point(19, 40);
            this.optQuestsAlert.Name = "optQuestsAlert";
            this.optQuestsAlert.Size = new System.Drawing.Size(195, 17);
            this.optQuestsAlert.TabIndex = 1;
            this.optQuestsAlert.Text = "Ask me if I want to accept the quest";
            this.optQuestsAlert.UseVisualStyleBackColor = true;
            this.optQuestsAlert.CheckedChanged += new System.EventHandler(this.optQuestsAlert_CheckedChanged);
            // 
            // optQuestsAuto
            // 
            this.optQuestsAuto.AutoSize = true;
            this.optQuestsAuto.Location = new System.Drawing.Point(19, 17);
            this.optQuestsAuto.Name = "optQuestsAuto";
            this.optQuestsAuto.Size = new System.Drawing.Size(157, 17);
            this.optQuestsAuto.TabIndex = 0;
            this.optQuestsAuto.Text = "Accept quests automatically";
            this.optQuestsAuto.UseVisualStyleBackColor = true;
            this.optQuestsAuto.CheckedChanged += new System.EventHandler(this.optQuestsAuto_CheckedChanged);
            // 
            // tabChat
            // 
            this.tabChat.Controls.Add(this.irc);
            this.tabChat.Location = new System.Drawing.Point(4, 22);
            this.tabChat.Name = "tabChat";
            this.tabChat.Size = new System.Drawing.Size(426, 229);
            this.tabChat.TabIndex = 10;
            this.tabChat.Text = "Chat";
            this.tabChat.UseVisualStyleBackColor = true;
            // 
            // lstLog
            // 
            this.lstLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstLog.FormattingEnabled = true;
            this.lstLog.HorizontalScrollbar = true;
            this.lstLog.ItemHeight = 12;
            this.lstLog.Location = new System.Drawing.Point(229, 24);
            this.lstLog.Name = "lstLog";
            this.lstLog.ScrollAlwaysVisible = true;
            this.lstLog.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.lstLog.Size = new System.Drawing.Size(206, 112);
            this.lstLog.TabIndex = 7;
            this.lstLog.TabStop = false;
            // 
            // lstAttacks
            // 
            this.lstAttacks.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstAttacks.FormattingEnabled = true;
            this.lstAttacks.HorizontalScrollbar = true;
            this.lstAttacks.ItemHeight = 12;
            this.lstAttacks.Location = new System.Drawing.Point(229, 142);
            this.lstAttacks.Name = "lstAttacks";
            this.lstAttacks.ScrollAlwaysVisible = true;
            this.lstAttacks.Size = new System.Drawing.Size(206, 52);
            this.lstAttacks.TabIndex = 8;
            this.lstAttacks.TabStop = false;
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.grpConnections);
            this.pnlRight.Location = new System.Drawing.Point(436, 22);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(228, 439);
            this.pnlRight.TabIndex = 9;
            // 
            // grpConnections
            // 
            this.grpConnections.Controls.Add(this.chkRemember);
            this.grpConnections.Controls.Add(this.btnRefresh);
            this.grpConnections.Controls.Add(this.btnLogout);
            this.grpConnections.Controls.Add(this.btnLogin);
            this.grpConnections.Controls.Add(this.txtPassword);
            this.grpConnections.Controls.Add(this.txtUsername);
            this.grpConnections.Controls.Add(this.label2);
            this.grpConnections.Controls.Add(this.label1);
            this.grpConnections.Controls.Add(this.lnkAccountsInvert);
            this.grpConnections.Controls.Add(this.lnkAccountsCheckNone);
            this.grpConnections.Controls.Add(this.lnkAccountsCheckAll);
            this.grpConnections.Controls.Add(this.lvAccounts);
            this.grpConnections.Location = new System.Drawing.Point(3, 3);
            this.grpConnections.Name = "grpConnections";
            this.grpConnections.Size = new System.Drawing.Size(222, 433);
            this.grpConnections.TabIndex = 11;
            this.grpConnections.TabStop = false;
            this.grpConnections.Text = "Connections";
            // 
            // chkRemember
            // 
            this.chkRemember.AutoSize = true;
            this.chkRemember.Checked = true;
            this.chkRemember.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRemember.Location = new System.Drawing.Point(9, 384);
            this.chkRemember.Name = "chkRemember";
            this.chkRemember.Size = new System.Drawing.Size(94, 17);
            this.chkRemember.TabIndex = 12;
            this.chkRemember.Text = "Remember me";
            this.chkRemember.UseVisualStyleBackColor = true;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Enabled = false;
            this.btnRefresh.Location = new System.Drawing.Point(82, 405);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(61, 23);
            this.btnRefresh.TabIndex = 11;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Enabled = false;
            this.btnLogout.Location = new System.Drawing.Point(149, 405);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(61, 23);
            this.btnLogout.TabIndex = 10;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(15, 405);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(61, 23);
            this.btnLogin.TabIndex = 9;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(100, 357);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(110, 20);
            this.txtPassword.TabIndex = 8;
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyDown);
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(100, 336);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(110, 20);
            this.txtUsername.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 360);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Rampid Password:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 339);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Rampid Username:";
            // 
            // lnkAccountsInvert
            // 
            this.lnkAccountsInvert.AutoSize = true;
            this.lnkAccountsInvert.Location = new System.Drawing.Point(128, 317);
            this.lnkAccountsInvert.Name = "lnkAccountsInvert";
            this.lnkAccountsInvert.Size = new System.Drawing.Size(68, 13);
            this.lnkAccountsInvert.TabIndex = 4;
            this.lnkAccountsInvert.TabStop = true;
            this.lnkAccountsInvert.Text = "Check Invert";
            this.lnkAccountsInvert.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkAccountsInvert_LinkClicked);
            // 
            // lnkAccountsCheckNone
            // 
            this.lnkAccountsCheckNone.AutoSize = true;
            this.lnkAccountsCheckNone.Location = new System.Drawing.Point(55, 317);
            this.lnkAccountsCheckNone.Name = "lnkAccountsCheckNone";
            this.lnkAccountsCheckNone.Size = new System.Drawing.Size(67, 13);
            this.lnkAccountsCheckNone.TabIndex = 3;
            this.lnkAccountsCheckNone.TabStop = true;
            this.lnkAccountsCheckNone.Text = "Check None";
            this.lnkAccountsCheckNone.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkAccountsCheckNone_LinkClicked);
            // 
            // lnkAccountsCheckAll
            // 
            this.lnkAccountsCheckAll.AutoSize = true;
            this.lnkAccountsCheckAll.Location = new System.Drawing.Point(0, 317);
            this.lnkAccountsCheckAll.Name = "lnkAccountsCheckAll";
            this.lnkAccountsCheckAll.Size = new System.Drawing.Size(52, 13);
            this.lnkAccountsCheckAll.TabIndex = 2;
            this.lnkAccountsCheckAll.TabStop = true;
            this.lnkAccountsCheckAll.Text = "Check All";
            this.lnkAccountsCheckAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkAccountsCheckAll_LinkClicked);
            // 
            // lvAccounts
            // 
            this.lvAccounts.CheckBoxes = true;
            this.lvAccounts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmCharName,
            this.clmStatus,
            this.clmInRoom,
            this.clmRooms,
            this.clmMobs,
            this.clmEXP});
            this.lvAccounts.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvAccounts.FullRowSelect = true;
            this.lvAccounts.GridLines = true;
            this.lvAccounts.Location = new System.Drawing.Point(6, 19);
            this.lvAccounts.MultiSelect = false;
            this.lvAccounts.Name = "lvAccounts";
            this.lvAccounts.Size = new System.Drawing.Size(216, 295);
            this.lvAccounts.TabIndex = 1;
            this.lvAccounts.UseCompatibleStateImageBehavior = false;
            this.lvAccounts.View = System.Windows.Forms.View.Details;
            this.lvAccounts.SelectedIndexChanged += new System.EventHandler(this.lvAccounts_SelectedIndexChanged);
            // 
            // clmCharName
            // 
            this.clmCharName.Text = "Name";
            // 
            // clmStatus
            // 
            this.clmStatus.Text = "Status";
            this.clmStatus.Width = 65;
            // 
            // clmInRoom
            // 
            this.clmInRoom.Text = "In";
            this.clmInRoom.Width = 30;
            // 
            // clmRooms
            // 
            this.clmRooms.Text = "Rooms";
            this.clmRooms.Width = 30;
            // 
            // clmMobs
            // 
            this.clmMobs.Text = "Mobs";
            this.clmMobs.Width = 30;
            // 
            // clmEXP
            // 
            this.clmEXP.Text = "EXPG";
            this.clmEXP.Width = 30;
            // 
            // pnlAttack
            // 
            this.pnlAttack.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAttack.Controls.Add(this.chkHourTimer);
            this.pnlAttack.Controls.Add(this.label10);
            this.pnlAttack.Controls.Add(this.btnAttackStart);
            this.pnlAttack.Controls.Add(this.btnAttackStop);
            this.pnlAttack.Controls.Add(this.optCountdownMobs);
            this.pnlAttack.Controls.Add(this.optCountdownMulti);
            this.pnlAttack.Controls.Add(this.optCountdownSingle);
            this.pnlAttack.Controls.Add(this.lnkStartCountdown);
            this.pnlAttack.Controls.Add(this.lblTimeLeft);
            this.pnlAttack.Controls.Add(this.numCountdown);
            this.pnlAttack.Controls.Add(this.lblTimer);
            this.pnlAttack.Controls.Add(this.chkCountdownTimer);
            this.pnlAttack.Location = new System.Drawing.Point(10, 44);
            this.pnlAttack.Name = "pnlAttack";
            this.pnlAttack.Size = new System.Drawing.Size(208, 149);
            this.pnlAttack.TabIndex = 30;
            // 
            // chkHourTimer
            // 
            this.chkHourTimer.AutoSize = true;
            this.chkHourTimer.Location = new System.Drawing.Point(16, 127);
            this.chkHourTimer.Name = "chkHourTimer";
            this.chkHourTimer.Size = new System.Drawing.Size(162, 17);
            this.chkHourTimer.TabIndex = 42;
            this.chkHourTimer.Text = "Attack after the hour change";
            this.chkHourTimer.UseVisualStyleBackColor = true;
            this.chkHourTimer.CheckedChanged += new System.EventHandler(this.chkHourTimer_CheckedChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(4, 3);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(94, 13);
            this.label10.TabIndex = 32;
            this.label10.Text = "Auto attacking:";
            // 
            // btnAttackStart
            // 
            this.btnAttackStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAttackStart.Location = new System.Drawing.Point(128, 29);
            this.btnAttackStart.Name = "btnAttackStart";
            this.btnAttackStart.Size = new System.Drawing.Size(75, 23);
            this.btnAttackStart.TabIndex = 39;
            this.btnAttackStart.Text = "Start";
            this.btnAttackStart.UseVisualStyleBackColor = true;
            this.btnAttackStart.Click += new System.EventHandler(this.btnAttackStart_Click);
            // 
            // btnAttackStop
            // 
            this.btnAttackStop.Enabled = false;
            this.btnAttackStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAttackStop.Location = new System.Drawing.Point(128, 52);
            this.btnAttackStop.Name = "btnAttackStop";
            this.btnAttackStop.Size = new System.Drawing.Size(75, 23);
            this.btnAttackStop.TabIndex = 38;
            this.btnAttackStop.Text = "Stop";
            this.btnAttackStop.UseVisualStyleBackColor = true;
            this.btnAttackStop.Click += new System.EventHandler(this.btnAttackStop_Click);
            // 
            // optCountdownMobs
            // 
            this.optCountdownMobs.AutoSize = true;
            this.optCountdownMobs.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optCountdownMobs.Location = new System.Drawing.Point(3, 53);
            this.optCountdownMobs.Name = "optCountdownMobs";
            this.optCountdownMobs.Size = new System.Drawing.Size(76, 16);
            this.optCountdownMobs.TabIndex = 37;
            this.optCountdownMobs.Text = "Attack mobs";
            this.optCountdownMobs.UseVisualStyleBackColor = true;
            // 
            // optCountdownMulti
            // 
            this.optCountdownMulti.AutoSize = true;
            this.optCountdownMulti.Checked = true;
            this.optCountdownMulti.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optCountdownMulti.Location = new System.Drawing.Point(3, 36);
            this.optCountdownMulti.Name = "optCountdownMulti";
            this.optCountdownMulti.Size = new System.Drawing.Size(110, 16);
            this.optCountdownMulti.TabIndex = 36;
            this.optCountdownMulti.TabStop = true;
            this.optCountdownMulti.Text = "Attack multiple areas";
            this.optCountdownMulti.UseVisualStyleBackColor = true;
            // 
            // optCountdownSingle
            // 
            this.optCountdownSingle.AutoSize = true;
            this.optCountdownSingle.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optCountdownSingle.Location = new System.Drawing.Point(3, 19);
            this.optCountdownSingle.Name = "optCountdownSingle";
            this.optCountdownSingle.Size = new System.Drawing.Size(129, 16);
            this.optCountdownSingle.TabIndex = 35;
            this.optCountdownSingle.Text = "Attack within current area";
            this.optCountdownSingle.UseVisualStyleBackColor = true;
            // 
            // lnkStartCountdown
            // 
            this.lnkStartCountdown.AutoSize = true;
            this.lnkStartCountdown.Location = new System.Drawing.Point(92, 92);
            this.lnkStartCountdown.Name = "lnkStartCountdown";
            this.lnkStartCountdown.Size = new System.Drawing.Size(54, 13);
            this.lnkStartCountdown.TabIndex = 34;
            this.lnkStartCountdown.TabStop = true;
            this.lnkStartCountdown.Text = "Start timer";
            this.lnkStartCountdown.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkStartCountdown_LinkClicked);
            // 
            // lblTimeLeft
            // 
            this.lblTimeLeft.AutoSize = true;
            this.lblTimeLeft.Location = new System.Drawing.Point(5, 92);
            this.lblTimeLeft.Name = "lblTimeLeft";
            this.lblTimeLeft.Size = new System.Drawing.Size(73, 13);
            this.lblTimeLeft.TabIndex = 33;
            this.lblTimeLeft.Text = "Time left: N/A";
            // 
            // numCountdown
            // 
            this.numCountdown.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numCountdown.Location = new System.Drawing.Point(98, 109);
            this.numCountdown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numCountdown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCountdown.Name = "numCountdown";
            this.numCountdown.Size = new System.Drawing.Size(43, 18);
            this.numCountdown.TabIndex = 30;
            this.numCountdown.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.numCountdown.ValueChanged += new System.EventHandler(this.numTimer_ValueChanged);
            // 
            // lblTimer
            // 
            this.lblTimer.AutoSize = true;
            this.lblTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimer.Location = new System.Drawing.Point(0, 77);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new System.Drawing.Size(77, 13);
            this.lblTimer.TabIndex = 32;
            this.lblTimer.Text = "Cycle Timer:";
            // 
            // chkCountdownTimer
            // 
            this.chkCountdownTimer.AutoSize = true;
            this.chkCountdownTimer.Location = new System.Drawing.Point(16, 109);
            this.chkCountdownTimer.Name = "chkCountdownTimer";
            this.chkCountdownTimer.Size = new System.Drawing.Size(170, 17);
            this.chkCountdownTimer.TabIndex = 43;
            this.chkCountdownTimer.Text = "Attack every                minutes";
            this.chkCountdownTimer.UseVisualStyleBackColor = true;
            this.chkCountdownTimer.CheckedChanged += new System.EventHandler(this.chkCountdownTimer_CheckedChanged);
            // 
            // lblExpRage
            // 
            this.lblExpRage.AutoSize = true;
            this.lblExpRage.Location = new System.Drawing.Point(104, 28);
            this.lblExpRage.Name = "lblExpRage";
            this.lblExpRage.Size = new System.Drawing.Size(16, 13);
            this.lblExpRage.TabIndex = 31;
            this.lblExpRage.Text = "...";
            // 
            // CoreUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 485);
            this.Controls.Add(this.lblExpRage);
            this.Controls.Add(this.pnlAttack);
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.tabs);
            this.Controls.Add(this.lstAttacks);
            this.Controls.Add(this.ss);
            this.Controls.Add(this.mnuMain);
            this.Controls.Add(this.lstLog);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MainMenuStrip = this.mnuMain;
            this.MaximizeBox = false;
            this.Name = "CoreUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.ss.ResumeLayout(false);
            this.ss.PerformLayout();
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.tabAttack.ResumeLayout(false);
            this.grpSettings.ResumeLayout(false);
            this.grpSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numThreadDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRageLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLevelMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRageStop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLevel)).EndInit();
            this.tabs.ResumeLayout(false);
            this.tabFilters.ResumeLayout(false);
            this.tabFilters.PerformLayout();
            this.tabRooms.ResumeLayout(false);
            this.tabRooms.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPathfindId)).EndInit();
            this.tabMobs.ResumeLayout(false);
            this.tabMobs.PerformLayout();
            this.tabJoiner.ResumeLayout(false);
            this.tabJoiner.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRaidInterval)).EndInit();
            this.tabFormer.ResumeLayout(false);
            this.tabFormer.PerformLayout();
            this.tabTrainer.ResumeLayout(false);
            this.tabTrainer.PerformLayout();
            this.tabQuests.ResumeLayout(false);
            this.tabQuests.PerformLayout();
            this.tabChat.ResumeLayout(false);
            this.pnlRight.ResumeLayout(false);
            this.grpConnections.ResumeLayout(false);
            this.grpConnections.PerformLayout();
            this.pnlAttack.ResumeLayout(false);
            this.pnlAttack.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCountdown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip ss;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripStatusLabel lblMisc;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem changesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TabPage tabAttack;
        private System.Windows.Forms.GroupBox grpSettings;
        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.ListBox lstLog;
        private System.Windows.Forms.ComboBox cmbPause;
        private System.Windows.Forms.CheckBox chkAttackPause;
        private System.Windows.Forms.NumericUpDown numLevel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripMenuItem actionsToolStripMenuItem;
        private System.Windows.Forms.ListBox lstAttacks;
        private System.Windows.Forms.TabPage tabRooms;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton optPathfindChoose;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button btnPathfind;
        private System.Windows.Forms.TabPage tabTrainer;
        private System.Windows.Forms.Button btnTrain;
        private System.Windows.Forms.CheckBox chkTrainReturn;
        private System.Windows.Forms.Label lblTrain;
        private System.Windows.Forms.ToolStripMenuItem exportLogToolStripMenuItem;
        private System.Windows.Forms.NumericUpDown numPathfindId;
        private System.Windows.Forms.ToolStripProgressBar pgr;
        private System.Windows.Forms.CheckBox chkAutoTrain;
        private System.Windows.Forms.RadioButton optPathfindID;
        private System.Windows.Forms.ListView lvPathfind;

        internal System.Windows.Forms.ListView RoomsView
        {
            get { return lvPathfind; }
            set { lvPathfind = value; }
        }
        private System.Windows.Forms.TabPage tabFilters;
        private System.Windows.Forms.TextBox txtFilters;
        private System.Windows.Forms.CheckBox chkFilter;
        private System.Windows.Forms.Button btnFilterLoad;
        private System.Windows.Forms.Button btnFilterSave;
        private System.Windows.Forms.TabPage tabQuests;
        private System.Windows.Forms.RadioButton optQuestsAuto;
        private System.Windows.Forms.RadioButton optQuestsAlert;
        private System.Windows.Forms.RadioButton optQuestsNothing;
        private System.Windows.Forms.CheckBox chkVault;
        private System.Windows.Forms.NumericUpDown numRageStop;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown numLevelMin;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown numRageLimit;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.LinkLabel lnkUncheckRooms;
        private System.Windows.Forms.ToolStripMenuItem clearLogToolStripMenuItem;
        private System.Windows.Forms.TabPage tabMobs;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.TabPage tabChat;
        private System.Windows.Forms.ListView lvMobs;

        internal System.Windows.Forms.ListView MobsView
        {
            get { return lvMobs; }
            set { lvMobs = value; }
        }
        private System.Windows.Forms.LinkLabel lnkUncheckMobs;
        private System.Windows.Forms.TabPage tabJoiner;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ColumnHeader clmName;
        private System.Windows.Forms.ColumnHeader clmRoomID;
        private System.Windows.Forms.Button btnAdventuresGo;
        private System.Windows.Forms.CheckBox chkReturnToStart;
        private System.Windows.Forms.TabPage tabFormer;
        private System.Windows.Forms.CheckBox chkAutoJoin;
        private System.Windows.Forms.ListView lvAdventures;

        internal System.Windows.Forms.ListView AdventuresView
        {
            get { return lvAdventures; }
            set { lvAdventures = value; }
        }
        private System.Windows.Forms.ComboBox cmbFormer;
        private System.Windows.Forms.NumericUpDown numRaidInterval;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.CheckBox chkAutoForm;
        private ChatUI irc;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.GroupBox grpConnections;
        private System.Windows.Forms.CheckBox chkRemember;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel lnkAccountsInvert;
        private System.Windows.Forms.LinkLabel lnkAccountsCheckNone;
        private System.Windows.Forms.LinkLabel lnkAccountsCheckAll;
        internal System.Windows.Forms.ListView lvAccounts;
        private System.Windows.Forms.ColumnHeader clmCharName;
        private System.Windows.Forms.ColumnHeader clmStatus;
        private System.Windows.Forms.ColumnHeader clmInRoom;
        private System.Windows.Forms.ColumnHeader clmRooms;
        private System.Windows.Forms.ColumnHeader clmMobs;
        private System.Windows.Forms.ColumnHeader clmEXP;
        private System.Windows.Forms.Panel pnlAttack;
        private System.Windows.Forms.Button btnAttackStart;
        private System.Windows.Forms.Button btnAttackStop;
        private System.Windows.Forms.RadioButton optCountdownMobs;
        private System.Windows.Forms.RadioButton optCountdownMulti;
        private System.Windows.Forms.RadioButton optCountdownSingle;
        private System.Windows.Forms.LinkLabel lnkStartCountdown;
        private System.Windows.Forms.Label lblTimeLeft;
        private System.Windows.Forms.NumericUpDown numCountdown;
        private System.Windows.Forms.Label lblTimer;
        private System.Windows.Forms.CheckBox chkVariance;
        private System.Windows.Forms.NumericUpDown numThreadDelay;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numTimeout;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblExpRage;
        private System.Windows.Forms.LinkLabel lnkMobLoad;
        private System.Windows.Forms.LinkLabel lnkLoadRooms;
        private System.Windows.Forms.CheckBox chkHourTimer;
        private System.Windows.Forms.CheckBox chkCountdownTimer;
        private System.Windows.Forms.ToolStripMenuItem openInBrowserToolStripMenuItem;
    }
}