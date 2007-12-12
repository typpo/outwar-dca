namespace DCT.UI
{
    partial class FiltersPanel
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
            this.btnFilterLoad = new System.Windows.Forms.Button();
            this.btnFilterSave = new System.Windows.Forms.Button();
            this.txtFilters = new System.Windows.Forms.TextBox();
            this.chkFilter = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnFilterLoad
            // 
            this.btnFilterLoad.Location = new System.Drawing.Point(216, 230);
            this.btnFilterLoad.Name = "btnFilterLoad";
            this.btnFilterLoad.Size = new System.Drawing.Size(75, 23);
            this.btnFilterLoad.TabIndex = 7;
            this.btnFilterLoad.Text = "Load Filters";
            this.btnFilterLoad.UseVisualStyleBackColor = true;
            this.btnFilterLoad.Click += new System.EventHandler(this.btnFilterLoad_Click);
            // 
            // btnFilterSave
            // 
            this.btnFilterSave.Location = new System.Drawing.Point(135, 230);
            this.btnFilterSave.Name = "btnFilterSave";
            this.btnFilterSave.Size = new System.Drawing.Size(75, 23);
            this.btnFilterSave.TabIndex = 6;
            this.btnFilterSave.Text = "Save Filters";
            this.btnFilterSave.UseVisualStyleBackColor = true;
            this.btnFilterSave.Click += new System.EventHandler(this.btnFilterSave_Click);
            // 
            // txtFilters
            // 
            this.txtFilters.Enabled = false;
            this.txtFilters.Location = new System.Drawing.Point(76, 36);
            this.txtFilters.Multiline = true;
            this.txtFilters.Name = "txtFilters";
            this.txtFilters.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtFilters.Size = new System.Drawing.Size(278, 188);
            this.txtFilters.TabIndex = 5;
            this.txtFilters.TextChanged += new System.EventHandler(this.txtFilters_TextChanged);
            // 
            // chkFilter
            // 
            this.chkFilter.AutoSize = true;
            this.chkFilter.Location = new System.Drawing.Point(30, 13);
            this.chkFilter.Name = "chkFilter";
            this.chkFilter.Size = new System.Drawing.Size(355, 17);
            this.chkFilter.TabIndex = 4;
            this.chkFilter.Text = "Only attack mobs with these words in their names (separated by lines):";
            this.chkFilter.UseVisualStyleBackColor = true;
            this.chkFilter.CheckedChanged += new System.EventHandler(this.chkFilter_CheckedChanged);
            // 
            // FiltersPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnFilterLoad);
            this.Controls.Add(this.btnFilterSave);
            this.Controls.Add(this.txtFilters);
            this.Controls.Add(this.chkFilter);
            this.Name = "FiltersPanel";
            this.Size = new System.Drawing.Size(426, 256);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFilterLoad;
        private System.Windows.Forms.Button btnFilterSave;
        private System.Windows.Forms.TextBox txtFilters;
        private System.Windows.Forms.CheckBox chkFilter;
    }
}
