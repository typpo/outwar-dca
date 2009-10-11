namespace DCT.UI
{
    partial class MainPanel
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
            this.pnlAttack = new System.Windows.Forms.Panel();
            this.chkHourTimer = new System.Windows.Forms.CheckBox();
            this.numCountdown = new System.Windows.Forms.NumericUpDown();
            this.chkCountdownTimer = new System.Windows.Forms.CheckBox();
            this.pnl = new System.Windows.Forms.Panel();
            this.lblExpRage = new System.Windows.Forms.Label();
            this.pnlAttack.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCountdown)).BeginInit();
            this.pnl.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlAttack
            // 
            this.pnlAttack.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAttack.Controls.Add(this.chkHourTimer);
            this.pnlAttack.Controls.Add(this.numCountdown);
            this.pnlAttack.Controls.Add(this.chkCountdownTimer);
            this.pnlAttack.Location = new System.Drawing.Point(3, 0);
            this.pnlAttack.Name = "pnlAttack";
            this.pnlAttack.Size = new System.Drawing.Size(208, 144);
            this.pnlAttack.TabIndex = 32;
            // 
            // chkHourTimer
            // 
            this.chkHourTimer.AutoSize = true;
            this.chkHourTimer.Location = new System.Drawing.Point(17, 33);
            this.chkHourTimer.Name = "chkHourTimer";
            this.chkHourTimer.Size = new System.Drawing.Size(162, 17);
            this.chkHourTimer.TabIndex = 42;
            this.chkHourTimer.Text = "Attack after the hour change";
            this.chkHourTimer.UseVisualStyleBackColor = true;
            this.chkHourTimer.CheckedChanged += new System.EventHandler(this.chkHourTimer_CheckedChanged);
            // 
            // numCountdown
            // 
            this.numCountdown.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numCountdown.Location = new System.Drawing.Point(99, 15);
            this.numCountdown.Maximum = new decimal(new int[] {
            100000,
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
            this.numCountdown.ValueChanged += new System.EventHandler(this.numCountdown_ValueChanged);
            // 
            // chkCountdownTimer
            // 
            this.chkCountdownTimer.AutoSize = true;
            this.chkCountdownTimer.Location = new System.Drawing.Point(17, 15);
            this.chkCountdownTimer.Name = "chkCountdownTimer";
            this.chkCountdownTimer.Size = new System.Drawing.Size(170, 17);
            this.chkCountdownTimer.TabIndex = 43;
            this.chkCountdownTimer.Text = "Attack every                minutes";
            this.chkCountdownTimer.UseVisualStyleBackColor = true;
            this.chkCountdownTimer.CheckedChanged += new System.EventHandler(this.chkCountdownTimer_CheckedChanged);
            // 
            // pnl
            // 
            this.pnl.Controls.Add(this.lblExpRage);
            this.pnl.Controls.Add(this.pnlAttack);
            this.pnl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl.Location = new System.Drawing.Point(0, 0);
            this.pnl.Name = "pnl";
            this.pnl.Size = new System.Drawing.Size(216, 166);
            this.pnl.TabIndex = 34;
            // 
            // lblExpRage
            // 
            this.lblExpRage.AutoSize = true;
            this.lblExpRage.Location = new System.Drawing.Point(99, 147);
            this.lblExpRage.Name = "lblExpRage";
            this.lblExpRage.Size = new System.Drawing.Size(16, 13);
            this.lblExpRage.TabIndex = 35;
            this.lblExpRage.Text = "...";
            // 
            // MainPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnl);
            this.Name = "MainPanel";
            this.Size = new System.Drawing.Size(216, 166);
            this.pnlAttack.ResumeLayout(false);
            this.pnlAttack.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCountdown)).EndInit();
            this.pnl.ResumeLayout(false);
            this.pnl.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlAttack;
        private System.Windows.Forms.CheckBox chkHourTimer;
        private System.Windows.Forms.NumericUpDown numCountdown;
        private System.Windows.Forms.CheckBox chkCountdownTimer;
        private System.Windows.Forms.Panel pnl;
        private System.Windows.Forms.Label lblExpRage;
    }
}
