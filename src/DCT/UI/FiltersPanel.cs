using System;
using System.IO;
using System.Windows.Forms;

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

        private readonly CoreUI mUI;

        internal FiltersPanel(CoreUI ui)
        {
            mUI = ui;
            InitializeComponent();
        }

        private void chkFilter_CheckedChanged(object sender, EventArgs e)
        {
            txtFilters.Enabled = chkFilter.Checked;
            CoreUI.Instance.Settings.FilterMobs = chkFilter.Checked;

            UpdateTab();
        }

        internal void UpdateTab()
        {
            mUI.Tabs.TabPages[CoreUI.TABINDEX_FILTERS].Text = CoreUI.Instance.Settings.FilterMobs ? "Filters (*)" : "Filters";
        }

        private void txtFilters_TextChanged(object sender, EventArgs e)
        {
            string[] ss = txtFilters.Text.Split(new[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            // trim spaces
            for(int i = 0; i < ss.Length; i++)
            {
                ss[i] = ss[i].Trim().ToLower();
            }
            CoreUI.Instance.Settings.MobFilters = ss;
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
                myStream = saveFileDialog1.OpenFile();
                StreamWriter wText = new StreamWriter(myStream);

                wText.Write(FiltersText);

                wText.Flush();
                wText.Close();
                myStream.Close();
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
                    FiltersText += input + "\r\n";
                }

                myFile.Close();
            }

            mUI.Toggle(true);
        }
    }
}
