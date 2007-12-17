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
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.minimizeToTrayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.openInBrowserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openTyppousToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearLogsPeriodicallyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitLeftRight = new System.Windows.Forms.SplitContainer();
            this.splitTopBottom = new System.Windows.Forms.SplitContainer();
            this.splitLeftRight2 = new System.Windows.Forms.SplitContainer();
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabAttack = new System.Windows.Forms.TabPage();
            this.tabFilters = new System.Windows.Forms.TabPage();
            this.tabRooms = new System.Windows.Forms.TabPage();
            this.tabMobs = new System.Windows.Forms.TabPage();
            this.tabJoiner = new System.Windows.Forms.TabPage();
            this.tabTrainer = new System.Windows.Forms.TabPage();
            this.tabQuests = new System.Windows.Forms.TabPage();
            this.tabChat = new System.Windows.Forms.TabPage();
            this.mNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.mNotifyMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ss.SuspendLayout();
            this.mnuMain.SuspendLayout();
            this.splitLeftRight.Panel2.SuspendLayout();
            this.splitLeftRight.SuspendLayout();
            this.splitTopBottom.Panel1.SuspendLayout();
            this.splitTopBottom.Panel2.SuspendLayout();
            this.splitTopBottom.SuspendLayout();
            this.splitLeftRight2.SuspendLayout();
            this.tabs.SuspendLayout();
            this.mNotifyMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // ss
            // 
            this.ss.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.ss.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblMisc,
            this.pgr});
            this.ss.Location = new System.Drawing.Point(0, 463);
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
            this.preferencesToolStripMenuItem});
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
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(131, 6);
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
            this.minimizeToTrayToolStripMenuItem,
            this.toolStripMenuItem4,
            this.openInBrowserToolStripMenuItem,
            this.openTyppousToolStripMenuItem});
            this.actionsToolStripMenuItem.Name = "actionsToolStripMenuItem";
            this.actionsToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.actionsToolStripMenuItem.Text = "Actions";
            // 
            // minimizeToTrayToolStripMenuItem
            // 
            this.minimizeToTrayToolStripMenuItem.Name = "minimizeToTrayToolStripMenuItem";
            this.minimizeToTrayToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.minimizeToTrayToolStripMenuItem.Text = "Minimize to tray";
            this.minimizeToTrayToolStripMenuItem.Click += new System.EventHandler(this.minimizeToTrayToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(161, 6);
            // 
            // openInBrowserToolStripMenuItem
            // 
            this.openInBrowserToolStripMenuItem.Name = "openInBrowserToolStripMenuItem";
            this.openInBrowserToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.openInBrowserToolStripMenuItem.Text = "Open in browser";
            this.openInBrowserToolStripMenuItem.Click += new System.EventHandler(this.openInBrowserToolStripMenuItem_Click);
            // 
            // openTyppousToolStripMenuItem
            // 
            this.openTyppousToolStripMenuItem.Name = "openTyppousToolStripMenuItem";
            this.openTyppousToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.openTyppousToolStripMenuItem.Text = "Open typpo.us";
            this.openTyppousToolStripMenuItem.Click += new System.EventHandler(this.openTyppousToolStripMenuItem_Click);
            // 
            // preferencesToolStripMenuItem
            // 
            this.preferencesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearLogsPeriodicallyToolStripMenuItem});
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
            this.clearLogsPeriodicallyToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.clearLogsPeriodicallyToolStripMenuItem.Text = "Clear logs periodically";
            this.clearLogsPeriodicallyToolStripMenuItem.CheckedChanged += new System.EventHandler(this.clearLogsPeriodicallyToolStripMenuItem_CheckedChanged);
            // 
            // splitLeftRight
            // 
            this.splitLeftRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitLeftRight.Location = new System.Drawing.Point(0, 24);
            this.splitLeftRight.Name = "splitLeftRight";
            // 
            // splitLeftRight.Panel2
            // 
            this.splitLeftRight.Panel2.Controls.Add(this.splitTopBottom);
            this.splitLeftRight.Size = new System.Drawing.Size(664, 439);
            this.splitLeftRight.SplitterDistance = 229;
            this.splitLeftRight.TabIndex = 5;
            // 
            // splitTopBottom
            // 
            this.splitTopBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitTopBottom.Location = new System.Drawing.Point(0, 0);
            this.splitTopBottom.Name = "splitTopBottom";
            this.splitTopBottom.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitTopBottom.Panel1
            // 
            this.splitTopBottom.Panel1.Controls.Add(this.splitLeftRight2);
            // 
            // splitTopBottom.Panel2
            // 
            this.splitTopBottom.Panel2.Controls.Add(this.tabs);
            this.splitTopBottom.Size = new System.Drawing.Size(431, 439);
            this.splitTopBottom.SplitterDistance = 147;
            this.splitTopBottom.TabIndex = 0;
            // 
            // splitLeftRight2
            // 
            this.splitLeftRight2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitLeftRight2.Location = new System.Drawing.Point(0, 0);
            this.splitLeftRight2.Name = "splitLeftRight2";
            this.splitLeftRight2.Size = new System.Drawing.Size(431, 147);
            this.splitLeftRight2.SplitterDistance = 213;
            this.splitLeftRight2.TabIndex = 0;
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.tabAttack);
            this.tabs.Controls.Add(this.tabFilters);
            this.tabs.Controls.Add(this.tabRooms);
            this.tabs.Controls.Add(this.tabMobs);
            this.tabs.Controls.Add(this.tabJoiner);
            this.tabs.Controls.Add(this.tabTrainer);
            this.tabs.Controls.Add(this.tabQuests);
            this.tabs.Controls.Add(this.tabChat);
            this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabs.Location = new System.Drawing.Point(0, 0);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(431, 288);
            this.tabs.TabIndex = 6;
            // 
            // tabAttack
            // 
            this.tabAttack.Location = new System.Drawing.Point(4, 22);
            this.tabAttack.Name = "tabAttack";
            this.tabAttack.Padding = new System.Windows.Forms.Padding(3);
            this.tabAttack.Size = new System.Drawing.Size(423, 262);
            this.tabAttack.TabIndex = 0;
            this.tabAttack.Text = "Attack";
            this.tabAttack.UseVisualStyleBackColor = true;
            // 
            // tabFilters
            // 
            this.tabFilters.Location = new System.Drawing.Point(4, 22);
            this.tabFilters.Name = "tabFilters";
            this.tabFilters.Size = new System.Drawing.Size(423, 262);
            this.tabFilters.TabIndex = 7;
            this.tabFilters.Text = "Filters";
            this.tabFilters.UseVisualStyleBackColor = true;
            // 
            // tabRooms
            // 
            this.tabRooms.Location = new System.Drawing.Point(4, 22);
            this.tabRooms.Name = "tabRooms";
            this.tabRooms.Size = new System.Drawing.Size(423, 262);
            this.tabRooms.TabIndex = 5;
            this.tabRooms.Text = "Rooms";
            this.tabRooms.UseVisualStyleBackColor = true;
            // 
            // tabMobs
            // 
            this.tabMobs.Location = new System.Drawing.Point(4, 22);
            this.tabMobs.Name = "tabMobs";
            this.tabMobs.Size = new System.Drawing.Size(423, 262);
            this.tabMobs.TabIndex = 9;
            this.tabMobs.Text = "Mobs";
            this.tabMobs.UseVisualStyleBackColor = true;
            // 
            // tabJoiner
            // 
            this.tabJoiner.Location = new System.Drawing.Point(4, 22);
            this.tabJoiner.Name = "tabJoiner";
            this.tabJoiner.Size = new System.Drawing.Size(423, 262);
            this.tabJoiner.TabIndex = 11;
            this.tabJoiner.Text = "Raids";
            this.tabJoiner.UseVisualStyleBackColor = true;
            // 
            // tabTrainer
            // 
            this.tabTrainer.Location = new System.Drawing.Point(4, 22);
            this.tabTrainer.Name = "tabTrainer";
            this.tabTrainer.Size = new System.Drawing.Size(423, 262);
            this.tabTrainer.TabIndex = 6;
            this.tabTrainer.Text = "Trainer";
            this.tabTrainer.UseVisualStyleBackColor = true;
            // 
            // tabQuests
            // 
            this.tabQuests.Location = new System.Drawing.Point(4, 22);
            this.tabQuests.Name = "tabQuests";
            this.tabQuests.Size = new System.Drawing.Size(423, 262);
            this.tabQuests.TabIndex = 8;
            this.tabQuests.Text = "Quests";
            this.tabQuests.UseVisualStyleBackColor = true;
            // 
            // tabChat
            // 
            this.tabChat.Location = new System.Drawing.Point(4, 22);
            this.tabChat.Name = "tabChat";
            this.tabChat.Size = new System.Drawing.Size(423, 262);
            this.tabChat.TabIndex = 10;
            this.tabChat.Text = "Chat";
            this.tabChat.UseVisualStyleBackColor = true;
            // 
            // mNotifyIcon
            // 
            this.mNotifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.mNotifyIcon.BalloonTipTitle = "Current Stats";
            this.mNotifyIcon.ContextMenuStrip = this.mNotifyMenu;
            this.mNotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("mNotifyIcon.Icon")));
            this.mNotifyIcon.Text = "Typpo\'s DCT";
            this.mNotifyIcon.Visible = true;
            this.mNotifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mNotifyIcon_MouseClick);
            this.mNotifyIcon.DoubleClick += new System.EventHandler(this.mNotifyIcon_DoubleClick);
            // 
            // mNotifyMenu
            // 
            this.mNotifyMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.mNotifyMenu.Name = "mNotifyMenu";
            this.mNotifyMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.mNotifyMenu.Size = new System.Drawing.Size(112, 48);
            this.mNotifyMenu.Opening += new System.ComponentModel.CancelEventHandler(this.mNotifyMenu_Opening);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // CoreUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 485);
            this.Controls.Add(this.splitLeftRight);
            this.Controls.Add(this.ss);
            this.Controls.Add(this.mnuMain);
            this.KeyPreview = true;
            this.MainMenuStrip = this.mnuMain;
            this.Name = "CoreUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResizeBegin += new System.EventHandler(this.CoreUI_ResizeBegin);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CoreUI_FormClosing);
            this.ResizeEnd += new System.EventHandler(this.CoreUI_ResizeEnd);
            this.Load += new System.EventHandler(this.CoreUI_Load);
            this.ss.ResumeLayout(false);
            this.ss.PerformLayout();
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.splitLeftRight.Panel2.ResumeLayout(false);
            this.splitLeftRight.ResumeLayout(false);
            this.splitTopBottom.Panel1.ResumeLayout(false);
            this.splitTopBottom.Panel2.ResumeLayout(false);
            this.splitTopBottom.ResumeLayout(false);
            this.splitLeftRight2.ResumeLayout(false);
            this.tabs.ResumeLayout(false);
            this.mNotifyMenu.ResumeLayout(false);
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
        private System.Windows.Forms.SplitContainer splitLeftRight;
        private System.Windows.Forms.SplitContainer splitTopBottom;
        private System.Windows.Forms.SplitContainer splitLeftRight2;
        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tabAttack;
        private System.Windows.Forms.TabPage tabFilters;
        private System.Windows.Forms.TabPage tabRooms;
        private System.Windows.Forms.TabPage tabMobs;
        private System.Windows.Forms.TabPage tabJoiner;
        private System.Windows.Forms.TabPage tabTrainer;
        private System.Windows.Forms.TabPage tabQuests;
        private System.Windows.Forms.TabPage tabChat;
        private System.Windows.Forms.NotifyIcon mNotifyIcon;
        private System.Windows.Forms.ToolStripMenuItem minimizeToTrayToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ContextMenuStrip mNotifyMenu;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
    }
}