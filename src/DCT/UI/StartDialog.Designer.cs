namespace DCT.UI
{
    partial class StartDialog
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
            this.txtMain = new System.Windows.Forms.TextBox();
            this.lblMain = new System.Windows.Forms.Label();
            this.lnkGo = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // txtMain
            // 
            this.txtMain.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMain.Location = new System.Drawing.Point(0, 0);
            this.txtMain.Multiline = true;
            this.txtMain.Name = "txtMain";
            this.txtMain.ReadOnly = true;
            this.txtMain.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMain.Size = new System.Drawing.Size(519, 315);
            this.txtMain.TabIndex = 0;
            // 
            // lblMain
            // 
            this.lblMain.AutoSize = true;
            this.lblMain.Location = new System.Drawing.Point(0, 318);
            this.lblMain.Name = "lblMain";
            this.lblMain.Size = new System.Drawing.Size(52, 13);
            this.lblMain.TabIndex = 1;
            this.lblMain.Text = "Starting...";
            // 
            // lnkGo
            // 
            this.lnkGo.AutoSize = true;
            this.lnkGo.Location = new System.Drawing.Point(496, 318);
            this.lnkGo.Name = "lnkGo";
            this.lnkGo.Size = new System.Drawing.Size(22, 13);
            this.lnkGo.TabIndex = 2;
            this.lnkGo.TabStop = true;
            this.lnkGo.Text = "OK";
            this.lnkGo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkGo_LinkClicked);
            // 
            // frmStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 333);
            this.Controls.Add(this.lnkGo);
            this.Controls.Add(this.lblMain);
            this.Controls.Add(this.txtMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmStart";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmStart_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtMain;
        private System.Windows.Forms.Label lblMain;
        private System.Windows.Forms.LinkLabel lnkGo;
    }
}