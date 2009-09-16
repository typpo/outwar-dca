namespace DCT.UI
{
    partial class RoomsPanel
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
            this.lnkSaveRooms = new System.Windows.Forms.LinkLabel();
            this.lnkLoadRooms = new System.Windows.Forms.LinkLabel();
            this.btnPathfind = new System.Windows.Forms.Button();
            this.numPathfindId = new System.Windows.Forms.NumericUpDown();
            this.lvPathfind = new System.Windows.Forms.ListView();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.label6 = new System.Windows.Forms.Label();
            this.lnkUncheckRooms = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.numPathfindId)).BeginInit();
            this.SuspendLayout();
            // 
            // lnkSaveRooms
            // 
            this.lnkSaveRooms.AutoSize = true;
            this.lnkSaveRooms.Location = new System.Drawing.Point(163, 249);
            this.lnkSaveRooms.Name = "lnkSaveRooms";
            this.lnkSaveRooms.Size = new System.Drawing.Size(83, 13);
            this.lnkSaveRooms.TabIndex = 16;
            this.lnkSaveRooms.TabStop = true;
            this.lnkSaveRooms.Text = "Export rooms list";
            this.lnkSaveRooms.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSaveRooms_LinkClicked);
            // 
            // lnkLoadRooms
            // 
            this.lnkLoadRooms.AutoSize = true;
            this.lnkLoadRooms.Location = new System.Drawing.Point(75, 249);
            this.lnkLoadRooms.Name = "lnkLoadRooms";
            this.lnkLoadRooms.Size = new System.Drawing.Size(82, 13);
            this.lnkLoadRooms.TabIndex = 15;
            this.lnkLoadRooms.TabStop = true;
            this.lnkLoadRooms.Text = "Import rooms list";
            this.lnkLoadRooms.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLoadRooms_LinkClicked);
            // 
            // btnPathfind
            // 
            this.btnPathfind.Location = new System.Drawing.Point(388, 3);
            this.btnPathfind.Name = "btnPathfind";
            this.btnPathfind.Size = new System.Drawing.Size(31, 23);
            this.btnPathfind.TabIndex = 12;
            this.btnPathfind.Text = "Go";
            this.btnPathfind.UseVisualStyleBackColor = true;
            this.btnPathfind.Click += new System.EventHandler(this.btnPathfind_Click);
            // 
            // numPathfindId
            // 
            this.numPathfindId.Location = new System.Drawing.Point(328, 6);
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
            this.numPathfindId.TabIndex = 10;
            this.numPathfindId.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numPathfindId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numPathfindId_KeyDown);
            // 
            // lvPathfind
            // 
            this.lvPathfind.CheckBoxes = true;
            this.lvPathfind.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader1});
            this.lvPathfind.FullRowSelect = true;
            this.lvPathfind.GridLines = true;
            this.lvPathfind.Location = new System.Drawing.Point(7, 32);
            this.lvPathfind.MultiSelect = false;
            this.lvPathfind.Name = "lvPathfind";
            this.lvPathfind.Size = new System.Drawing.Size(412, 214);
            this.lvPathfind.TabIndex = 13;
            this.lvPathfind.UseCompatibleStateImageBehavior = false;
            this.lvPathfind.View = System.Windows.Forms.View.Details;
            this.lvPathfind.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvPathfind_ColumnClick);
            this.lvPathfind.Click += new System.EventHandler(this.lvPathfind_Click);
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
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(243, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Move to room #:";
            // 
            // lnkUncheckRooms
            // 
            this.lnkUncheckRooms.AutoSize = true;
            this.lnkUncheckRooms.Location = new System.Drawing.Point(4, 249);
            this.lnkUncheckRooms.Name = "lnkUncheckRooms";
            this.lnkUncheckRooms.Size = new System.Drawing.Size(65, 13);
            this.lnkUncheckRooms.TabIndex = 14;
            this.lnkUncheckRooms.TabStop = true;
            this.lnkUncheckRooms.Text = "Uncheck All";
            this.lnkUncheckRooms.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkUncheckRooms_LinkClicked);
            // 
            // RoomsPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lnkSaveRooms);
            this.Controls.Add(this.lnkLoadRooms);
            this.Controls.Add(this.btnPathfind);
            this.Controls.Add(this.numPathfindId);
            this.Controls.Add(this.lvPathfind);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lnkUncheckRooms);
            this.Name = "RoomsPanel";
            this.Size = new System.Drawing.Size(426, 262);
            ((System.ComponentModel.ISupportInitialize)(this.numPathfindId)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel lnkSaveRooms;
        private System.Windows.Forms.LinkLabel lnkLoadRooms;
        private System.Windows.Forms.Button btnPathfind;
        private System.Windows.Forms.NumericUpDown numPathfindId;
        private System.Windows.Forms.ListView lvPathfind;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.LinkLabel lnkUncheckRooms;
    }
}
