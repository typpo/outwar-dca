using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DCT.UI
{
    public partial class frmItemSettings : Form
    {
        private readonly CoreUI mUI;
        public frmItemSettings(CoreUI ui)
        {
            mUI = ui;
            InitializeComponent();
        }

        private void lnkHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Enter the name of the item you want to collect and the # of times you want it to drop.  Double-click to edit.",
                "Item Setings", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ListViewItem i = new ListViewItem(new[] { txtItem.Text, ((int)numItem.Value).ToString() });
            i.BackColor = Color.Linen;
            lv.Items.Add(i);
            lv.SelectedItems.Clear();
            lv.Items[lv.Items.Count - 1].Selected = true;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem i in lv.SelectedItems)
            {
                lv.Items.Remove(i);
            }
        }

        private void lv_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lv.SelectedIndices.Count < 1)
                return;

            txtItem.Text = lv.SelectedItems[0].SubItems[0].Text;
            numItem.Value = int.Parse(lv.SelectedItems[0].SubItems[1].Text);
        }

        private void txtItem_TextChanged(object sender, EventArgs e)
        {
            if (lv.SelectedItems.Count < 1)
            {
                return;
            }
            lv.SelectedItems[0].SubItems[0].Text = txtItem.Text;
        }

        private void numItem_ValueChanged(object sender, EventArgs e)
        {
            if (lv.SelectedItems.Count < 1)
            {
                return;
            }
            lv.SelectedItems[0].SubItems[1].Text = ((int)numItem.Value).ToString();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
