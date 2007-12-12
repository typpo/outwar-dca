namespace DCT.UI
{
    partial class TrainPanel
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
            this.chkAutoTrain = new System.Windows.Forms.CheckBox();
            this.chkTrainReturn = new System.Windows.Forms.CheckBox();
            this.lblTrain = new System.Windows.Forms.Label();
            this.btnTrain = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // chkAutoTrain
            // 
            this.chkAutoTrain.AutoSize = true;
            this.chkAutoTrain.Checked = true;
            this.chkAutoTrain.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoTrain.Location = new System.Drawing.Point(22, 21);
            this.chkAutoTrain.Name = "chkAutoTrain";
            this.chkAutoTrain.Size = new System.Drawing.Size(362, 17);
            this.chkAutoTrain.TabIndex = 3;
            this.chkAutoTrain.Text = "Automatically train accounts that need leveling in rooms with bartenders";
            this.chkAutoTrain.UseVisualStyleBackColor = true;
            this.chkAutoTrain.CheckedChanged += new System.EventHandler(this.chkAutoTrain_CheckedChanged);
            // 
            // chkTrainReturn
            // 
            this.chkTrainReturn.AutoSize = true;
            this.chkTrainReturn.Location = new System.Drawing.Point(59, 79);
            this.chkTrainReturn.Name = "chkTrainReturn";
            this.chkTrainReturn.Size = new System.Drawing.Size(132, 17);
            this.chkTrainReturn.TabIndex = 5;
            this.chkTrainReturn.Text = "Move back afterwards";
            this.chkTrainReturn.UseVisualStyleBackColor = true;
            // 
            // lblTrain
            // 
            this.lblTrain.AutoSize = true;
            this.lblTrain.Location = new System.Drawing.Point(19, 63);
            this.lblTrain.Name = "lblTrain";
            this.lblTrain.Size = new System.Drawing.Size(344, 13);
            this.lblTrain.TabIndex = 4;
            this.lblTrain.Text = "The checked accounts will be moved to the nearest trainer and trained.";
            // 
            // btnTrain
            // 
            this.btnTrain.Location = new System.Drawing.Point(59, 102);
            this.btnTrain.Name = "btnTrain";
            this.btnTrain.Size = new System.Drawing.Size(66, 23);
            this.btnTrain.TabIndex = 6;
            this.btnTrain.Text = "Go";
            this.btnTrain.UseVisualStyleBackColor = true;
            this.btnTrain.Click += new System.EventHandler(this.btnTrain_Click);
            // 
            // TrainTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkAutoTrain);
            this.Controls.Add(this.chkTrainReturn);
            this.Controls.Add(this.lblTrain);
            this.Controls.Add(this.btnTrain);
            this.Name = "TrainTab";
            this.Size = new System.Drawing.Size(426, 229);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkAutoTrain;
        private System.Windows.Forms.CheckBox chkTrainReturn;
        private System.Windows.Forms.Label lblTrain;
        private System.Windows.Forms.Button btnTrain;
    }
}
