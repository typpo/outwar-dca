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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainPanel));
            this.pnlAttack = new System.Windows.Forms.Panel();
            this.chkHourTimer = new System.Windows.Forms.CheckBox();
            this.optCountdownMobs = new System.Windows.Forms.RadioButton();
            this.optCountdownMulti = new System.Windows.Forms.RadioButton();
            this.optCountdownSingle = new System.Windows.Forms.RadioButton();
            this.numCountdown = new System.Windows.Forms.NumericUpDown();
            this.chkCountdownTimer = new System.Windows.Forms.CheckBox();
            this.pnl = new System.Windows.Forms.Panel();
            this.lblExpRage = new System.Windows.Forms.Label();
            this.strip = new System.Windows.Forms.ToolStrip();
            this.btnStart = new System.Windows.Forms.ToolStripButton();
            this.btnStop = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnStartTimer = new System.Windows.Forms.ToolStripButton();
            this.lblTimeLeft = new System.Windows.Forms.ToolStripLabel();
            this.pnlAttack.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCountdown)).BeginInit();
            this.pnl.SuspendLayout();
            this.strip.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlAttack
            // 
            this.pnlAttack.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAttack.Controls.Add(this.chkHourTimer);
            this.pnlAttack.Controls.Add(this.optCountdownMobs);
            this.pnlAttack.Controls.Add(this.optCountdownMulti);
            this.pnlAttack.Controls.Add(this.optCountdownSingle);
            this.pnlAttack.Controls.Add(this.numCountdown);
            this.pnlAttack.Controls.Add(this.chkCountdownTimer);
            this.pnlAttack.Location = new System.Drawing.Point(3, 28);
            this.pnlAttack.Name = "pnlAttack";
            this.pnlAttack.Size = new System.Drawing.Size(208, 96);
            this.pnlAttack.TabIndex = 32;
            // 
            // chkHourTimer
            // 
            this.chkHourTimer.AutoSize = true;
            this.chkHourTimer.Location = new System.Drawing.Point(16, 77);
            this.chkHourTimer.Name = "chkHourTimer";
            this.chkHourTimer.Size = new System.Drawing.Size(162, 17);
            this.chkHourTimer.TabIndex = 42;
            this.chkHourTimer.Text = "Attack after the hour change";
            this.chkHourTimer.UseVisualStyleBackColor = true;
            this.chkHourTimer.CheckedChanged += new System.EventHandler(this.chkHourTimer_CheckedChanged);
            // 
            // optCountdownMobs
            // 
            this.optCountdownMobs.AutoSize = true;
            this.optCountdownMobs.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optCountdownMobs.Location = new System.Drawing.Point(3, 37);
            this.optCountdownMobs.Name = "optCountdownMobs";
            this.optCountdownMobs.Size = new System.Drawing.Size(76, 16);
            this.optCountdownMobs.TabIndex = 37;
            this.optCountdownMobs.Text = "Attack mobs";
            this.optCountdownMobs.UseVisualStyleBackColor = true;
            this.optCountdownMobs.CheckedChanged += new System.EventHandler(this.optCountdownMobs_CheckedChanged);
            // 
            // optCountdownMulti
            // 
            this.optCountdownMulti.AutoSize = true;
            this.optCountdownMulti.Checked = true;
            this.optCountdownMulti.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optCountdownMulti.Location = new System.Drawing.Point(3, 20);
            this.optCountdownMulti.Name = "optCountdownMulti";
            this.optCountdownMulti.Size = new System.Drawing.Size(110, 16);
            this.optCountdownMulti.TabIndex = 36;
            this.optCountdownMulti.TabStop = true;
            this.optCountdownMulti.Text = "Attack multiple areas";
            this.optCountdownMulti.UseVisualStyleBackColor = true;
            this.optCountdownMulti.CheckedChanged += new System.EventHandler(this.optCountdownMulti_CheckedChanged);
            // 
            // optCountdownSingle
            // 
            this.optCountdownSingle.AutoSize = true;
            this.optCountdownSingle.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optCountdownSingle.Location = new System.Drawing.Point(3, 3);
            this.optCountdownSingle.Name = "optCountdownSingle";
            this.optCountdownSingle.Size = new System.Drawing.Size(129, 16);
            this.optCountdownSingle.TabIndex = 35;
            this.optCountdownSingle.Text = "Attack within current area";
            this.optCountdownSingle.UseVisualStyleBackColor = true;
            this.optCountdownSingle.CheckedChanged += new System.EventHandler(this.optCountdownSingle_CheckedChanged);
            // 
            // numCountdown
            // 
            this.numCountdown.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numCountdown.Location = new System.Drawing.Point(98, 59);
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
            this.chkCountdownTimer.Location = new System.Drawing.Point(16, 59);
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
            this.pnl.Controls.Add(this.strip);
            this.pnl.Controls.Add(this.pnlAttack);
            this.pnl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl.Location = new System.Drawing.Point(0, 0);
            this.pnl.Name = "pnl";
            this.pnl.Size = new System.Drawing.Size(216, 144);
            this.pnl.TabIndex = 34;
            // 
            // lblExpRage
            // 
            this.lblExpRage.AutoSize = true;
            this.lblExpRage.Location = new System.Drawing.Point(101, 127);
            this.lblExpRage.Name = "lblExpRage";
            this.lblExpRage.Size = new System.Drawing.Size(16, 13);
            this.lblExpRage.TabIndex = 35;
            this.lblExpRage.Text = "...";
            // 
            // strip
            // 
            this.strip.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.strip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.strip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnStart,
            this.btnStop,
            this.toolStripSeparator1,
            this.btnStartTimer,
            this.lblTimeLeft});
            this.strip.Location = new System.Drawing.Point(0, 0);
            this.strip.Name = "strip";
            this.strip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.strip.Size = new System.Drawing.Size(216, 25);
            this.strip.TabIndex = 34;
            this.strip.Text = "toolStrip1";
            // 
            // btnStart
            // 
            this.btnStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnStart.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.Image = ((System.Drawing.Image)(resources.GetObject("btnStart.Image")));
            this.btnStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(40, 22);
            this.btnStart.Text = "Start";
            this.btnStart.Click += new System.EventHandler(this.btnAttackStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnStop.Enabled = false;
            this.btnStop.Image = ((System.Drawing.Image)(resources.GetObject("btnStop.Image")));
            this.btnStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(28, 22);
            this.btnStop.Text = "Stop";
            this.btnStop.Click += new System.EventHandler(this.btnAttackStop_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnStartTimer
            // 
            this.btnStartTimer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnStartTimer.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartTimer.Image = ((System.Drawing.Image)(resources.GetObject("btnStartTimer.Image")));
            this.btnStartTimer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStartTimer.Name = "btnStartTimer";
            this.btnStartTimer.Size = new System.Drawing.Size(53, 22);
            this.btnStartTimer.Text = "Start timer";
            this.btnStartTimer.Click += new System.EventHandler(this.btnStartTimer_Click);
            // 
            // lblTimeLeft
            // 
            this.lblTimeLeft.Name = "lblTimeLeft";
            this.lblTimeLeft.Size = new System.Drawing.Size(65, 22);
            this.lblTimeLeft.Text = "Time left: N/A";
            // 
            // MainPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnl);
            this.Name = "MainPanel";
            this.Size = new System.Drawing.Size(216, 144);
            this.pnlAttack.ResumeLayout(false);
            this.pnlAttack.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCountdown)).EndInit();
            this.pnl.ResumeLayout(false);
            this.pnl.PerformLayout();
            this.strip.ResumeLayout(false);
            this.strip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlAttack;
        private System.Windows.Forms.CheckBox chkHourTimer;
        private System.Windows.Forms.RadioButton optCountdownMobs;
        private System.Windows.Forms.RadioButton optCountdownMulti;
        private System.Windows.Forms.RadioButton optCountdownSingle;
        private System.Windows.Forms.NumericUpDown numCountdown;
        private System.Windows.Forms.CheckBox chkCountdownTimer;
        private System.Windows.Forms.Panel pnl;
        private System.Windows.Forms.ToolStrip strip;
        private System.Windows.Forms.ToolStripButton btnStart;
        private System.Windows.Forms.ToolStripButton btnStop;
        private System.Windows.Forms.Label lblExpRage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel lblTimeLeft;
        private System.Windows.Forms.ToolStripButton btnStartTimer;
    }
}
