using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DCT.Pathfinding;

namespace DCT.UI
{
    public partial class RaidsPanel : UserControl
    {
        internal ListView.ListViewItemCollection Raids
        {
            get { return lvAdventures.Items; }
        }

        internal ListView.CheckedListViewItemCollection CheckedRaids
        {
            get { return lvAdventures.CheckedItems; }
        }

        internal ListView.CheckedIndexCollection CheckedIndices
        {
            get { return lvAdventures.CheckedIndices; }
        }

        internal ListViewItem FocusedRaid
        {
            get { return lvAdventures.FocusedItem; }
        }

        internal bool MoveEnabled
        {
            get { return btnAdventuresGo.Enabled; }
            set { btnAdventuresGo.Enabled = value; }
        }

        private readonly CoreUI mUI;

        internal RaidsPanel(CoreUI ui)
        {
            mUI = ui;
            InitializeComponent();
        }

        internal void BuildView()
        {
            lvAdventures.Items.Clear();
            SortedList<string, int> l = Pathfinder.Adventures;
            for (int i = 0; i < l.Count; i++)
            {
                ListViewItem tmp = new ListViewItem(
                        new string[]
                            {
                                l.Keys[i], l.Values[i].ToString()
                            });
                tmp.Name = l.Keys[i];
                lvAdventures.Items.Add(tmp);
            }
        }

        private void btnAdventuresGo_Click(object sender, EventArgs e)
        {
            if (mUI.AccountsPanel.CheckedIndices.Count < 1)
            {
                mUI.LogPanel.Log("E: Check the accounts you want to move.");
                return;
            }
            if (FocusedRaid == null)
            {
                mUI.LogPanel.Log("E: Choose an adventure to move to.");
                return;
            }
            mUI.InvokeBulkMove(int.Parse(FocusedRaid.SubItems[1].Text));
        }
    }
}
