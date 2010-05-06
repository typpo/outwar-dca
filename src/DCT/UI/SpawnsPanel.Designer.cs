namespace DCT.UI
{
    partial class SpawnsPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.btnGo = new System.Windows.Forms.Button();
            this.lnkAdd = new System.Windows.Forms.LinkLabel();
            this.linkUncheckAll = new System.Windows.Forms.LinkLabel();
            this.lnkCheckAll = new System.Windows.Forms.LinkLabel();
            this.lnkCampSelected = new System.Windows.Forms.LinkLabel();
            this.btnCamp = new System.Windows.Forms.Button();
            this.chkIgnoreRage = new System.Windows.Forms.CheckBox();
            this.chkAttackSpawns = new System.Windows.Forms.CheckBox();
            this.lvSpawns = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.lstLog = new System.Windows.Forms.ListBox();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lstLog);
            this.splitContainer1.Size = new System.Drawing.Size(417, 294);
            this.splitContainer1.SplitterDistance = 215;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.btnGo);
            this.splitContainer2.Panel1.Controls.Add(this.lnkAdd);
            this.splitContainer2.Panel1.Controls.Add(this.linkUncheckAll);
            this.splitContainer2.Panel1.Controls.Add(this.lnkCheckAll);
            this.splitContainer2.Panel1.Controls.Add(this.lnkCampSelected);
            this.splitContainer2.Panel1.Controls.Add(this.btnCamp);
            this.splitContainer2.Panel1.Controls.Add(this.chkIgnoreRage);
            this.splitContainer2.Panel1.Controls.Add(this.chkAttackSpawns);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.lvSpawns);
            this.splitContainer2.Size = new System.Drawing.Size(417, 215);
            this.splitContainer2.SplitterDistance = 70;
            this.splitContainer2.TabIndex = 4;
            // 
            // btnGo
            // 
            this.btnGo.AutoEllipsis = true;
            this.btnGo.Location = new System.Drawing.Point(291, 4);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(110, 23);
            this.btnGo.TabIndex = 9;
            this.btnGo.Text = "Go to selection";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // lnkAdd
            // 
            this.lnkAdd.AutoSize = true;
            this.lnkAdd.Location = new System.Drawing.Point(128, 46);
            this.lnkAdd.Name = "lnkAdd";
            this.lnkAdd.Size = new System.Drawing.Size(35, 13);
            this.lnkAdd.TabIndex = 8;
            this.lnkAdd.TabStop = true;
            this.lnkAdd.Text = "Add...";
            this.lnkAdd.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkAdd_LinkClicked);
            // 
            // linkUncheckAll
            // 
            this.linkUncheckAll.AutoSize = true;
            this.linkUncheckAll.Location = new System.Drawing.Point(57, 46);
            this.linkUncheckAll.Name = "linkUncheckAll";
            this.linkUncheckAll.Size = new System.Drawing.Size(65, 13);
            this.linkUncheckAll.TabIndex = 7;
            this.linkUncheckAll.TabStop = true;
            this.linkUncheckAll.Text = "Uncheck All";
            this.linkUncheckAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkUncheckAll_LinkClicked);
            // 
            // lnkCheckAll
            // 
            this.lnkCheckAll.AutoSize = true;
            this.lnkCheckAll.Location = new System.Drawing.Point(-1, 46);
            this.lnkCheckAll.Name = "lnkCheckAll";
            this.lnkCheckAll.Size = new System.Drawing.Size(52, 13);
            this.lnkCheckAll.TabIndex = 6;
            this.lnkCheckAll.TabStop = true;
            this.lnkCheckAll.Text = "Check All";
            this.lnkCheckAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkCheckAll_LinkClicked);
            // 
            // lnkCampSelected
            // 
            this.lnkCampSelected.AutoSize = true;
            this.lnkCampSelected.Location = new System.Drawing.Point(393, 40);
            this.lnkCampSelected.Name = "lnkCampSelected";
            this.lnkCampSelected.Size = new System.Drawing.Size(13, 13);
            this.lnkCampSelected.TabIndex = 5;
            this.lnkCampSelected.TabStop = true;
            this.lnkCampSelected.Text = "?";
            this.lnkCampSelected.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkCampSelected_LinkClicked);
            // 
            // btnCamp
            // 
            this.btnCamp.Location = new System.Drawing.Point(291, 33);
            this.btnCamp.Name = "btnCamp";
            this.btnCamp.Size = new System.Drawing.Size(96, 26);
            this.btnCamp.TabIndex = 4;
            this.btnCamp.Text = "Camp selected";
            this.btnCamp.UseVisualStyleBackColor = true;
            this.btnCamp.Click += new System.EventHandler(this.btnCamp_Click);
            // 
            // chkIgnoreRage
            // 
            this.chkIgnoreRage.AutoSize = true;
            this.chkIgnoreRage.Checked = true;
            this.chkIgnoreRage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIgnoreRage.Enabled = false;
            this.chkIgnoreRage.Location = new System.Drawing.Point(3, 26);
            this.chkIgnoreRage.Name = "chkIgnoreRage";
            this.chkIgnoreRage.Size = new System.Drawing.Size(177, 17);
            this.chkIgnoreRage.TabIndex = 3;
            this.chkIgnoreRage.Text = "Ignore rage limit for spawn mobs";
            this.chkIgnoreRage.UseVisualStyleBackColor = true;
            this.chkIgnoreRage.CheckedChanged += new System.EventHandler(this.chkIgnoreRage_CheckedChanged);
            // 
            // chkAttackSpawns
            // 
            this.chkAttackSpawns.AutoSize = true;
            this.chkAttackSpawns.Location = new System.Drawing.Point(3, 3);
            this.chkAttackSpawns.Name = "chkAttackSpawns";
            this.chkAttackSpawns.Size = new System.Drawing.Size(119, 17);
            this.chkAttackSpawns.TabIndex = 2;
            this.chkAttackSpawns.Text = "Attack spawn mobs";
            this.chkAttackSpawns.UseVisualStyleBackColor = true;
            this.chkAttackSpawns.CheckedChanged += new System.EventHandler(this.chkAttackSpawns_CheckedChanged);
            // 
            // lvSpawns
            // 
            this.lvSpawns.CheckBoxes = true;
            this.lvSpawns.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader3,
            this.columnHeader2,
            this.columnHeader4,
            this.columnHeader5});
            this.lvSpawns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvSpawns.FullRowSelect = true;
            this.lvSpawns.GridLines = true;
            this.lvSpawns.Location = new System.Drawing.Point(0, 0);
            this.lvSpawns.Name = "lvSpawns";
            this.lvSpawns.Size = new System.Drawing.Size(417, 141);
            this.lvSpawns.TabIndex = 0;
            this.lvSpawns.UseCompatibleStateImageBehavior = false;
            this.lvSpawns.View = System.Windows.Forms.View.Details;
            this.lvSpawns.SelectedIndexChanged += new System.EventHandler(this.lvSpawns_SelectedIndexChanged);
            this.lvSpawns.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvSpawns_ColumnClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 175;
            // 
            // columnHeader3
            // 
            this.columnHeader3.DisplayIndex = 2;
            this.columnHeader3.Text = "Room";
            this.columnHeader3.Width = 50;
            // 
            // columnHeader2
            // 
            this.columnHeader2.DisplayIndex = 1;
            this.columnHeader2.Text = "Level";
            this.columnHeader2.Width = 50;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Sightings";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Attacks";
            // 
            // lstLog
            // 
            this.lstLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstLog.FormattingEnabled = true;
            this.lstLog.Location = new System.Drawing.Point(0, 0);
            this.lstLog.Name = "lstLog";
            this.lstLog.Size = new System.Drawing.Size(417, 69);
            this.lstLog.TabIndex = 0;
            // 
            // SpawnsPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "SpawnsPanel";
            this.Size = new System.Drawing.Size(417, 294);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.CheckBox chkIgnoreRage;
        private System.Windows.Forms.CheckBox chkAttackSpawns;
        private System.Windows.Forms.ListBox lstLog;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ListView lvSpawns;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Button btnCamp;
        private System.Windows.Forms.LinkLabel lnkCampSelected;
        private System.Windows.Forms.LinkLabel lnkCheckAll;
        private System.Windows.Forms.LinkLabel lnkAdd;
        private System.Windows.Forms.LinkLabel linkUncheckAll;
        private System.Windows.Forms.Button btnGo;

    }
}
