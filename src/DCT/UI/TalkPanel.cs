using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DCT.Outwar;
using DCT.Outwar.World;

namespace DCT.UI
{
    public partial class TalkPanel : UserControl
    {
        internal bool TalkEnabled
        {
            get { return btnRefresh.Enabled && btnTalk.Enabled; }
            set { btnRefresh.Enabled = btnTalk.Enabled = value; }
        }

        private readonly CoreUI mUI;

        internal TalkPanel(CoreUI ui)
        {
            mUI = ui;
            InitializeComponent();
        }

        private void btnTalk_Click(object sender, EventArgs e)
        {
            Account a = mUI.AccountsPanel.Engine.MainAccount;
            if (a == null)
            {
                lbl.Text = "You must login to use this tab.";
                return;
            }

            if (lvMobs.SelectedItems.Count < 1)
            {
                MessageBox.Show("You must select a mob to talk to.", "Mob Talk", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            TalkEnabled = false;

            Room r = a.Mover.Location;
            long id = long.Parse(lvMobs.SelectedItems[0].SubItems[1].Text);
            foreach (Mob mb in r.Mobs)
            {
                if (mb.Id == id)
                {
                    new Questing.frmMobTalk(a, id, mUI.AccountsPanel.Engine.RgSessId).ShowDialog();
                    TalkEnabled = true;
                    return;
                }
            }

            TalkEnabled = true;
            MessageBox.Show("There's been an error - can no longer find mob to talk to.", "Mob Talk", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            TalkEnabled = false;
            RefreshMobs();
            TalkEnabled = true;
        }

        internal void RefreshMobs()
        {
            if (mUI.AccountsPanel.Engine.MainAccount == null)
                return;

            lvMobs.Items.Clear();

            // populate with talkable mobs in current room
            Account a = mUI.AccountsPanel.Engine.MainAccount;
            if (a == null)
            {
                lbl.Text = "You must login to use this tab.";
                return;
            }

            lblAccount.Text = "Account: " + a.Name;

            Room r = a.Mover.Location;
            if (r == null)
            {
                lbl.Text = "Room not loaded.";
                return;
            }

            int n = 0;
            if (r.Mobs != null)
            {
                foreach (Mob mb in r.Mobs)
                {
                    if (mb.IsTalkable)
                    {
                        n++;
                        lvMobs.Items.Add(new ListViewItem(new string[] { mb.Name, mb.Id.ToString() }));
                    }
                }
            }

            lbl.Text = n + " talkable mobs in room " + r.Name + "(" + r.Id + ")";
            btnTalk.Enabled = n != 0;
        }

        private void lvMobs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvMobs.SelectedItems.Count < 1)
            {
                btnTalk.Text = "Go to selection";
                return;
            }
            string txt = lvMobs.SelectedItems[0].SubItems[0].Text;
            if (txt.StartsWith("A "))
                txt = txt.Substring(2);
            btnTalk.Text = string.Format("Talk to {0}", txt);
        }

        private void lnkQuests_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // http://sigil.outwar.com/backpack.php?quest=1
        }

        private void lnkOrbs_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // http://sigil.outwar.com/backpack.php?orb=1
        }

        private void lnkPotions_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // http://sigil.outwar.com/backpack.php?potion=1
        }

        private void lnkKeys_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // http://sigil.outwar.com/backpack.php?key=1
        }
    }
}
