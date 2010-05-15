namespace DCT.UI
{
    partial class TalkPanel
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
            this.lvMobs = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.lbl = new System.Windows.Forms.Label();
            this.btnTalk = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblAccount = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lnkKeys = new System.Windows.Forms.LinkLabel();
            this.lnkPotions = new System.Windows.Forms.LinkLabel();
            this.lnkOrbs = new System.Windows.Forms.LinkLabel();
            this.lnkQuests = new System.Windows.Forms.LinkLabel();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvMobs
            // 
            this.lvMobs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lvMobs.FullRowSelect = true;
            this.lvMobs.GridLines = true;
            this.lvMobs.Location = new System.Drawing.Point(3, 57);
            this.lvMobs.MultiSelect = false;
            this.lvMobs.Name = "lvMobs";
            this.lvMobs.Size = new System.Drawing.Size(411, 234);
            this.lvMobs.TabIndex = 0;
            this.lvMobs.UseCompatibleStateImageBehavior = false;
            this.lvMobs.View = System.Windows.Forms.View.Details;
            this.lvMobs.SelectedIndexChanged += new System.EventHandler(this.lvMobs_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 150;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Id";
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.Location = new System.Drawing.Point(3, 41);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(148, 13);
            this.lbl.TabIndex = 1;
            this.lbl.Text = "You must login to use this tab.";
            // 
            // btnTalk
            // 
            this.btnTalk.AutoEllipsis = true;
            this.btnTalk.Location = new System.Drawing.Point(259, 3);
            this.btnTalk.Name = "btnTalk";
            this.btnTalk.Size = new System.Drawing.Size(155, 23);
            this.btnTalk.TabIndex = 2;
            this.btnTalk.Text = "Talk to...";
            this.btnTalk.UseVisualStyleBackColor = true;
            this.btnTalk.Click += new System.EventHandler(this.btnTalk_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(339, 31);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lblAccount
            // 
            this.lblAccount.AutoSize = true;
            this.lblAccount.Location = new System.Drawing.Point(3, 8);
            this.lblAccount.Name = "lblAccount";
            this.lblAccount.Size = new System.Drawing.Size(79, 13);
            this.lblAccount.TabIndex = 4;
            this.lblAccount.Text = "Character: N/A";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lnkKeys);
            this.groupBox1.Controls.Add(this.lnkPotions);
            this.groupBox1.Controls.Add(this.lnkOrbs);
            this.groupBox1.Controls.Add(this.lnkQuests);
            this.groupBox1.Location = new System.Drawing.Point(6, 198);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(408, 59);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Backpack";
            this.groupBox1.Visible = false;
            // 
            // lnkKeys
            // 
            this.lnkKeys.AutoSize = true;
            this.lnkKeys.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkKeys.Location = new System.Drawing.Point(264, 25);
            this.lnkKeys.Name = "lnkKeys";
            this.lnkKeys.Size = new System.Drawing.Size(38, 16);
            this.lnkKeys.TabIndex = 3;
            this.lnkKeys.TabStop = true;
            this.lnkKeys.Text = "Keys";
            this.lnkKeys.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkKeys_LinkClicked);
            // 
            // lnkPotions
            // 
            this.lnkPotions.AutoSize = true;
            this.lnkPotions.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkPotions.Location = new System.Drawing.Point(205, 25);
            this.lnkPotions.Name = "lnkPotions";
            this.lnkPotions.Size = new System.Drawing.Size(53, 16);
            this.lnkPotions.TabIndex = 2;
            this.lnkPotions.TabStop = true;
            this.lnkPotions.Text = "Potions";
            this.lnkPotions.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkPotions_LinkClicked);
            // 
            // lnkOrbs
            // 
            this.lnkOrbs.AutoSize = true;
            this.lnkOrbs.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkOrbs.Location = new System.Drawing.Point(162, 25);
            this.lnkOrbs.Name = "lnkOrbs";
            this.lnkOrbs.Size = new System.Drawing.Size(37, 16);
            this.lnkOrbs.TabIndex = 1;
            this.lnkOrbs.TabStop = true;
            this.lnkOrbs.Text = "Orbs";
            this.lnkOrbs.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkOrbs_LinkClicked);
            // 
            // lnkQuests
            // 
            this.lnkQuests.AutoSize = true;
            this.lnkQuests.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkQuests.Location = new System.Drawing.Point(106, 25);
            this.lnkQuests.Name = "lnkQuests";
            this.lnkQuests.Size = new System.Drawing.Size(50, 16);
            this.lnkQuests.TabIndex = 0;
            this.lnkQuests.TabStop = true;
            this.lnkQuests.Text = "Quests";
            this.lnkQuests.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkQuests_LinkClicked);
            // 
            // TalkPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblAccount);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnTalk);
            this.Controls.Add(this.lbl);
            this.Controls.Add(this.lvMobs);
            this.Name = "TalkPanel";
            this.Size = new System.Drawing.Size(417, 294);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvMobs;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label lbl;
        private System.Windows.Forms.Button btnTalk;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblAccount;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.LinkLabel lnkKeys;
        private System.Windows.Forms.LinkLabel lnkPotions;
        private System.Windows.Forms.LinkLabel lnkOrbs;
        private System.Windows.Forms.LinkLabel lnkQuests;
    }
}
