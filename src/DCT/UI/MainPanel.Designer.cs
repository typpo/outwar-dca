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
            this.cmbStopAfter = new System.Windows.Forms.ComboBox();
            this.numStopAfter = new System.Windows.Forms.NumericUpDown();
            this.chkStopAfter = new System.Windows.Forms.CheckBox();
            this.numCountdown = new System.Windows.Forms.NumericUpDown();
            this.pnl = new System.Windows.Forms.Panel();
            this.lblExpRage = new System.Windows.Forms.Label();
            this.chkCountdownTimer = new System.Windows.Forms.CheckBox();
            this.chkHourTimer = new System.Windows.Forms.CheckBox();
            this.pnlAttack.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numStopAfter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCountdown)).BeginInit();
            this.pnl.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlAttack
            // 
            this.pnlAttack.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAttack.Controls.Add(this.chkHourTimer);
            this.pnlAttack.Controls.Add(this.cmbStopAfter);
            this.pnlAttack.Controls.Add(this.numStopAfter);
            this.pnlAttack.Controls.Add(this.chkStopAfter);
            this.pnlAttack.Controls.Add(this.numCountdown);
            this.pnlAttack.Controls.Add(this.chkCountdownTimer);
            this.pnlAttack.Location = new System.Drawing.Point(3, 3);
            this.pnlAttack.Name = "pnlAttack";
            this.pnlAttack.Size = new System.Drawing.Size(208, 106);
            this.pnlAttack.TabIndex = 32;
            // 
            // cmbStopAfter
            // 
            this.cmbStopAfter.Enabled = false;
            this.cmbStopAfter.FormattingEnabled = true;
            this.cmbStopAfter.Items.AddRange(new object[] {
            "runs",
            "minutes"});
            this.cmbStopAfter.Location = new System.Drawing.Point(126, 67);
            this.cmbStopAfter.Name = "cmbStopAfter";
            this.cmbStopAfter.Size = new System.Drawing.Size(77, 21);
            this.cmbStopAfter.TabIndex = 46;
            this.cmbStopAfter.Text = "runs";
            this.cmbStopAfter.SelectedIndexChanged += new System.EventHandler(this.cmbStopAfter_SelectedIndexChanged);
            // 
            // numStopAfter
            // 
            this.numStopAfter.Enabled = false;
            this.numStopAfter.Location = new System.Drawing.Point(72, 67);
            this.numStopAfter.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numStopAfter.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numStopAfter.Name = "numStopAfter";
            this.numStopAfter.Size = new System.Drawing.Size(50, 20);
            this.numStopAfter.TabIndex = 45;
            this.numStopAfter.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // chkStopAfter
            // 
            this.chkStopAfter.AutoSize = true;
            this.chkStopAfter.Location = new System.Drawing.Point(4, 69);
            this.chkStopAfter.Name = "chkStopAfter";
            this.chkStopAfter.Size = new System.Drawing.Size(72, 17);
            this.chkStopAfter.TabIndex = 44;
            this.chkStopAfter.Text = "Stop after";
            this.chkStopAfter.UseVisualStyleBackColor = true;
            this.chkStopAfter.CheckedChanged += new System.EventHandler(this.chkStopAfter_CheckedChanged);
            // 
            // numCountdown
            // 
            this.numCountdown.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numCountdown.Location = new System.Drawing.Point(86, 13);
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
            // pnl
            // 
            this.pnl.Controls.Add(this.lblExpRage);
            this.pnl.Controls.Add(this.pnlAttack);
            this.pnl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl.Location = new System.Drawing.Point(0, 0);
            this.pnl.Name = "pnl";
            this.pnl.Size = new System.Drawing.Size(216, 133);
            this.pnl.TabIndex = 34;
            // 
            // lblExpRage
            // 
            this.lblExpRage.AutoSize = true;
            this.lblExpRage.Location = new System.Drawing.Point(99, 114);
            this.lblExpRage.Name = "lblExpRage";
            this.lblExpRage.Size = new System.Drawing.Size(16, 13);
            this.lblExpRage.TabIndex = 35;
            this.lblExpRage.Text = "...";
            // 
            // chkCountdownTimer
            // 
            this.chkCountdownTimer.AutoSize = true;
            this.chkCountdownTimer.Location = new System.Drawing.Point(4, 14);
            this.chkCountdownTimer.Name = "chkCountdownTimer";
            this.chkCountdownTimer.Size = new System.Drawing.Size(170, 17);
            this.chkCountdownTimer.TabIndex = 49;
            this.chkCountdownTimer.Text = "Attack every                minutes";
            this.chkCountdownTimer.UseVisualStyleBackColor = true;
            this.chkCountdownTimer.CheckedChanged += new System.EventHandler(this.chkCountdownTimer_CheckedChanged);
            // 
            // chkHourTimer
            // 
            this.chkHourTimer.AutoSize = true;
            this.chkHourTimer.Location = new System.Drawing.Point(4, 37);
            this.chkHourTimer.Name = "chkHourTimer";
            this.chkHourTimer.Size = new System.Drawing.Size(162, 17);
            this.chkHourTimer.TabIndex = 50;
            this.chkHourTimer.Text = "Attack after the hour change";
            this.chkHourTimer.UseVisualStyleBackColor = true;
            this.chkHourTimer.CheckedChanged += new System.EventHandler(this.chkHourTimer_CheckedChanged);
            // 
            // MainPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnl);
            this.Name = "MainPanel";
            this.Size = new System.Drawing.Size(216, 133);
            this.pnlAttack.ResumeLayout(false);
            this.pnlAttack.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numStopAfter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCountdown)).EndInit();
            this.pnl.ResumeLayout(false);
            this.pnl.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlAttack;
        private System.Windows.Forms.NumericUpDown numCountdown;
        private System.Windows.Forms.Panel pnl;
        private System.Windows.Forms.Label lblExpRage;
        private System.Windows.Forms.CheckBox chkStopAfter;
        private System.Windows.Forms.NumericUpDown numStopAfter;
        private System.Windows.Forms.ComboBox cmbStopAfter;
        private System.Windows.Forms.CheckBox chkHourTimer;
        private System.Windows.Forms.CheckBox chkCountdownTimer;
    }
}
