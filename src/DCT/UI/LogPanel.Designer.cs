namespace DCT.UI
{
    partial class LogPanel
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
            this.split = new System.Windows.Forms.SplitContainer();
            this.lstLog = new System.Windows.Forms.ListBox();
            this.lstAttacks = new System.Windows.Forms.ListBox();
            this.split.Panel1.SuspendLayout();
            this.split.Panel2.SuspendLayout();
            this.split.SuspendLayout();
            this.SuspendLayout();
            // 
            // split
            // 
            this.split.Dock = System.Windows.Forms.DockStyle.Fill;
            this.split.Location = new System.Drawing.Point(0, 0);
            this.split.Name = "split";
            this.split.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // split.Panel1
            // 
            this.split.Panel1.Controls.Add(this.lstLog);
            // 
            // split.Panel2
            // 
            this.split.Panel2.Controls.Add(this.lstAttacks);
            this.split.Size = new System.Drawing.Size(214, 178);
            this.split.SplitterDistance = 112;
            this.split.TabIndex = 0;
            // 
            // lstLog
            // 
            this.lstLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstLog.FormattingEnabled = true;
            this.lstLog.HorizontalScrollbar = true;
            this.lstLog.ItemHeight = 12;
            this.lstLog.Location = new System.Drawing.Point(0, 0);
            this.lstLog.Name = "lstLog";
            this.lstLog.Size = new System.Drawing.Size(214, 112);
            this.lstLog.TabIndex = 0;
            // 
            // lstAttacks
            // 
            this.lstAttacks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstAttacks.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstAttacks.FormattingEnabled = true;
            this.lstAttacks.HorizontalScrollbar = true;
            this.lstAttacks.ItemHeight = 12;
            this.lstAttacks.Location = new System.Drawing.Point(0, 0);
            this.lstAttacks.Name = "lstAttacks";
            this.lstAttacks.Size = new System.Drawing.Size(214, 52);
            this.lstAttacks.TabIndex = 0;
            // 
            // LogPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.split);
            this.Name = "LogPanel";
            this.Size = new System.Drawing.Size(214, 178);
            this.split.Panel1.ResumeLayout(false);
            this.split.Panel2.ResumeLayout(false);
            this.split.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer split;
        private System.Windows.Forms.ListBox lstLog;
        private System.Windows.Forms.ListBox lstAttacks;


    }
}
