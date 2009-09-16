namespace DCT.UI
{
    partial class MobsPanel
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
            this.lnkMobSave = new System.Windows.Forms.LinkLabel();
            this.lnkMobsSelect = new System.Windows.Forms.LinkLabel();
            this.btnMobRage = new System.Windows.Forms.Button();
            this.lblMobRage = new System.Windows.Forms.Label();
            this.btnPotionMobsSelect = new System.Windows.Forms.Button();
            this.cmbPotionMobs = new System.Windows.Forms.ComboBox();
            this.lnkMobLoad = new System.Windows.Forms.LinkLabel();
            this.lnkUncheckMobs = new System.Windows.Forms.LinkLabel();
            this.lvMobs = new System.Windows.Forms.ListView();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.btnMobGo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lnkMobSave
            // 
            this.lnkMobSave.AutoSize = true;
            this.lnkMobSave.Location = new System.Drawing.Point(158, 226);
            this.lnkMobSave.Name = "lnkMobSave";
            this.lnkMobSave.Size = new System.Drawing.Size(80, 13);
            this.lnkMobSave.TabIndex = 17;
            this.lnkMobSave.TabStop = true;
            this.lnkMobSave.Text = "Export mobs list";
            this.lnkMobSave.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkMobSave_LinkClicked);
            // 
            // lnkMobsSelect
            // 
            this.lnkMobsSelect.AutoSize = true;
            this.lnkMobsSelect.Location = new System.Drawing.Point(2, 243);
            this.lnkMobsSelect.Name = "lnkMobsSelect";
            this.lnkMobsSelect.Size = new System.Drawing.Size(117, 13);
            this.lnkMobsSelect.TabIndex = 16;
            this.lnkMobsSelect.TabStop = true;
            this.lnkMobsSelect.Text = "Select mobs by name...";
            this.lnkMobsSelect.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkMobsSelect_LinkClicked);
            // 
            // btnMobRage
            // 
            this.btnMobRage.Location = new System.Drawing.Point(131, 2);
            this.btnMobRage.Name = "btnMobRage";
            this.btnMobRage.Size = new System.Drawing.Size(75, 23);
            this.btnMobRage.TabIndex = 15;
            this.btnMobRage.Text = "Recalculate";
            this.btnMobRage.UseVisualStyleBackColor = true;
            this.btnMobRage.Click += new System.EventHandler(this.btnMobRage_Click);
            // 
            // lblMobRage
            // 
            this.lblMobRage.AutoSize = true;
            this.lblMobRage.Location = new System.Drawing.Point(7, 7);
            this.lblMobRage.Name = "lblMobRage";
            this.lblMobRage.Size = new System.Drawing.Size(81, 13);
            this.lblMobRage.TabIndex = 14;
            this.lblMobRage.Text = "Rage per run: ?";
            // 
            // btnPotionMobsSelect
            // 
            this.btnPotionMobsSelect.Location = new System.Drawing.Point(365, 229);
            this.btnPotionMobsSelect.Name = "btnPotionMobsSelect";
            this.btnPotionMobsSelect.Size = new System.Drawing.Size(54, 22);
            this.btnPotionMobsSelect.TabIndex = 13;
            this.btnPotionMobsSelect.Text = "Select";
            this.btnPotionMobsSelect.UseVisualStyleBackColor = true;
            this.btnPotionMobsSelect.Click += new System.EventHandler(this.btnPotionMobsSelect_Click);
            // 
            // cmbPotionMobs
            // 
            this.cmbPotionMobs.Items.AddRange(new object[] {
            "Kinetic",
            "Fire",
            "Holy",
            "Shadow",
            "Arcane"});
            this.cmbPotionMobs.Location = new System.Drawing.Point(244, 229);
            this.cmbPotionMobs.Name = "cmbPotionMobs";
            this.cmbPotionMobs.Size = new System.Drawing.Size(115, 21);
            this.cmbPotionMobs.TabIndex = 12;
            this.cmbPotionMobs.Text = "Choose a potion...";
            // 
            // lnkMobLoad
            // 
            this.lnkMobLoad.AutoSize = true;
            this.lnkMobLoad.Location = new System.Drawing.Point(73, 226);
            this.lnkMobLoad.Name = "lnkMobLoad";
            this.lnkMobLoad.Size = new System.Drawing.Size(79, 13);
            this.lnkMobLoad.TabIndex = 11;
            this.lnkMobLoad.TabStop = true;
            this.lnkMobLoad.Text = "Import mobs list";
            this.lnkMobLoad.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkMobLoad_LinkClicked);
            // 
            // lnkUncheckMobs
            // 
            this.lnkUncheckMobs.AutoSize = true;
            this.lnkUncheckMobs.Location = new System.Drawing.Point(2, 226);
            this.lnkUncheckMobs.Name = "lnkUncheckMobs";
            this.lnkUncheckMobs.Size = new System.Drawing.Size(65, 13);
            this.lnkUncheckMobs.TabIndex = 10;
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
            this.lvMobs.Location = new System.Drawing.Point(5, 29);
            this.lvMobs.Name = "lvMobs";
            this.lvMobs.Size = new System.Drawing.Size(414, 194);
            this.lvMobs.TabIndex = 9;
            this.lvMobs.UseCompatibleStateImageBehavior = false;
            this.lvMobs.View = System.Windows.Forms.View.Details;
            this.lvMobs.SelectedIndexChanged += new System.EventHandler(this.lvMobs_SelectedIndexChanged);
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
            // btnMobGo
            // 
            this.btnMobGo.AutoEllipsis = true;
            this.btnMobGo.Location = new System.Drawing.Point(309, 3);
            this.btnMobGo.Name = "btnMobGo";
            this.btnMobGo.Size = new System.Drawing.Size(110, 23);
            this.btnMobGo.TabIndex = 18;
            this.btnMobGo.Text = "Go to selection";
            this.btnMobGo.UseVisualStyleBackColor = true;
            this.btnMobGo.Click += new System.EventHandler(this.btnMobGo_Click);
            // 
            // MobsPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnMobGo);
            this.Controls.Add(this.lnkMobSave);
            this.Controls.Add(this.lnkMobsSelect);
            this.Controls.Add(this.btnMobRage);
            this.Controls.Add(this.lblMobRage);
            this.Controls.Add(this.btnPotionMobsSelect);
            this.Controls.Add(this.cmbPotionMobs);
            this.Controls.Add(this.lnkMobLoad);
            this.Controls.Add(this.lnkUncheckMobs);
            this.Controls.Add(this.lvMobs);
            this.Name = "MobsPanel";
            this.Size = new System.Drawing.Size(426, 256);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel lnkMobSave;
        private System.Windows.Forms.LinkLabel lnkMobsSelect;
        private System.Windows.Forms.Button btnMobRage;
        private System.Windows.Forms.Label lblMobRage;
        private System.Windows.Forms.Button btnPotionMobsSelect;
        private System.Windows.Forms.ComboBox cmbPotionMobs;
        private System.Windows.Forms.LinkLabel lnkMobLoad;
        private System.Windows.Forms.LinkLabel lnkUncheckMobs;
        private System.Windows.Forms.ListView lvMobs;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.Button btnMobGo;
    }
}
