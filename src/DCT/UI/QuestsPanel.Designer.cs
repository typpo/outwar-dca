namespace DCT.UI
{
    partial class QuestsPanel
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
            this.optQuestsNothing = new System.Windows.Forms.RadioButton();
            this.optQuestsAlert = new System.Windows.Forms.RadioButton();
            this.optQuestsAuto = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // optQuestsNothing
            // 
            this.optQuestsNothing.AutoSize = true;
            this.optQuestsNothing.Checked = true;
            this.optQuestsNothing.Location = new System.Drawing.Point(26, 66);
            this.optQuestsNothing.Name = "optQuestsNothing";
            this.optQuestsNothing.Size = new System.Drawing.Size(77, 17);
            this.optQuestsNothing.TabIndex = 5;
            this.optQuestsNothing.TabStop = true;
            this.optQuestsNothing.Text = "Do nothing";
            this.optQuestsNothing.UseVisualStyleBackColor = true;
            this.optQuestsNothing.CheckedChanged += new System.EventHandler(this.optQuestsNothing_CheckedChanged);
            // 
            // optQuestsAlert
            // 
            this.optQuestsAlert.AutoSize = true;
            this.optQuestsAlert.Location = new System.Drawing.Point(26, 43);
            this.optQuestsAlert.Name = "optQuestsAlert";
            this.optQuestsAlert.Size = new System.Drawing.Size(195, 17);
            this.optQuestsAlert.TabIndex = 4;
            this.optQuestsAlert.Text = "Ask me if I want to accept the quest";
            this.optQuestsAlert.UseVisualStyleBackColor = true;
            this.optQuestsAlert.CheckedChanged += new System.EventHandler(this.optQuestsAlert_CheckedChanged);
            // 
            // optQuestsAuto
            // 
            this.optQuestsAuto.AutoSize = true;
            this.optQuestsAuto.Location = new System.Drawing.Point(26, 20);
            this.optQuestsAuto.Name = "optQuestsAuto";
            this.optQuestsAuto.Size = new System.Drawing.Size(157, 17);
            this.optQuestsAuto.TabIndex = 3;
            this.optQuestsAuto.Text = "Accept quests automatically";
            this.optQuestsAuto.UseVisualStyleBackColor = true;
            this.optQuestsAuto.CheckedChanged += new System.EventHandler(this.optQuestsAuto_CheckedChanged);
            // 
            // QuestsTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.optQuestsNothing);
            this.Controls.Add(this.optQuestsAlert);
            this.Controls.Add(this.optQuestsAuto);
            this.Name = "QuestsTab";
            this.Size = new System.Drawing.Size(426, 229);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton optQuestsNothing;
        private System.Windows.Forms.RadioButton optQuestsAlert;
        private System.Windows.Forms.RadioButton optQuestsAuto;
    }
}
