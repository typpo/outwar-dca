using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DCT.Util;
using DCT.Settings;

namespace DCT.UI.Feedback
{
    public partial class frmBugReporter : Form
    {
        private readonly CoreUI mUI;
        public frmBugReporter(CoreUI ui)
        {
            mUI = ui;
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txt.Text.Length < 5)
            {
                MessageBox.Show("Enter more details about your problem.", "Can't submit", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            BugReporter r = new BugReporter();
            btnSubmit.Enabled = false;
            bool result = r.ReportBug(string.Format("v.{0}\n{1}\n\n{2}", Security.Version.Full, txt.Text, mUI.CollectData()), txtEmail.Text);
            if (!result)
                MessageBox.Show("An error occured; try submitting content manually to http://typpo.us/submit.php", "Not submitted", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                this.Close();
            btnSubmit.Enabled = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
