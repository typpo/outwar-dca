using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
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

        private CoreUI mUI;

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
            int room = int.Parse(lvAdventures.FocusedItem.SubItems[1].Text);

            mUI.InvokeAdventures(room);
        }
    }
}
