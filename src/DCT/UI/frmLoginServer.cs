
using System.Windows.Forms;
using DCT.Outwar;

namespace DCT.UI
{
    public partial class frmLoginServer : Form
    {
        private CoreUI mUI;
        public frmLoginServer(CoreUI ui)
        {
            mUI = ui;
            InitializeComponent();

            foreach(string s in Server.NamesList)
            {
                lst.Items.Add(s);
            }

            lst.SelectedIndex = ui.Settings.Server - 1;
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            if (lst.SelectedIndex >= 0)
            {
                mUI.Settings.Server = lst.SelectedIndex + 1;
                mUI.LogPanel.Log("Server login settings saved.");
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }


    }
}
