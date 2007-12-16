using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DCT.UI
{
    public partial class FiltersPanel : UserControl
    {
        internal string FiltersText
        {
            get { return txtFilters.Text; }
            set { txtFilters.Text = value; }
        }

        internal bool FiltersEnabled
        {
            get { return chkFilter.Checked; }
            set { chkFilter.Checked = value; }
        }

        private CoreUI mUI;

        internal FiltersPanel(CoreUI ui)
        {
            mUI = ui;
            InitializeComponent();
        }

        private void chkFilter_CheckedChanged(object sender, EventArgs e)
        {
            txtFilters.Enabled = chkFilter.Checked;
            CoreUI.Instance.Settings.FilterMobs = chkFilter.Checked;
        }

        private void txtFilters_TextChanged(object sender, EventArgs e)
        {
            CoreUI.Instance.Settings.MobFilters =
                txtFilters.Text.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

        private void btnFilterSave_Click(object sender, EventArgs e)
        {
            mUI.Toggle(false);

            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "All files (*.*)|*.*";
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {
                    StreamWriter wText = new StreamWriter(myStream);

                    wText.Write(FiltersText);

                    wText.Flush();
                    wText.Close();
                    myStream.Close();
                }
            }

            mUI.Toggle(true);
        }

        private void btnFilterLoad_Click(object sender, EventArgs e)
        {
            mUI.Toggle(false);

            OpenFileDialog DlgOpen = new OpenFileDialog();

            DlgOpen.Filter = "Any File|*.*";
            DlgOpen.Title = "Select a User Account File";

            if (DlgOpen.ShowDialog() == DialogResult.OK)
            {
                StreamReader myFile = new StreamReader(DlgOpen.FileName);

                string input;
                while ((input = myFile.ReadLine()) != null)
                {
                    FiltersText = input + "\r\n";
                }

                myFile.Close();
            }

            mUI.Toggle(true);
        }
    }
}
