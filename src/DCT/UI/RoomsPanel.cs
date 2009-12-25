using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DCT.Util;
using DCT.Pathfinding;

namespace DCT.UI
{
    public partial class RoomsPanel : UserControl
    {
        internal ListView.ListViewItemCollection Rooms
        {
            get { return lvPathfind.Items; }
        }

        internal ListView.CheckedListViewItemCollection CheckedRooms
        {
            get { return lvPathfind.CheckedItems; }
        }

        internal ListView.CheckedIndexCollection CheckedIndices
        {
            get { return lvPathfind.CheckedIndices; }
        }

        internal int PathfindId
        {
            get { return (int)numPathfindId.Value; }
        }

        internal bool PathfindEnabled
        {
            get { return btnPathfind.Enabled; }
            set { btnPathfind.Enabled = value; }
        }

        private readonly CoreUI mUI;

        internal RoomsPanel(CoreUI ui)
        {
            mUI = ui;
            InitializeComponent();
        }

        internal void BuildView()
        {
            lvPathfind.Items.Clear();
            foreach (MappedRoom rm in Pathfinder.Rooms)
            {
                if (rm != null)
                {
                    ListViewItem tmp = new ListViewItem(new string[] { rm.Name, rm.Id.ToString() });
                    tmp.Name = rm.Name;
                    lvPathfind.Items.Add(tmp);
                }
            }
        }

        private int mSortColumn;
        private void lvPathfind_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine whether the column is the same as the last column clicked.
            if (e.Column != mSortColumn)
            {
                // Set the sort column to the new column.
                mSortColumn = e.Column;
                // Set the sort order to ascending by default.
                lvPathfind.Sorting = SortOrder.Ascending;
            }
            else
            {
                // Determine what the last sort order was and change it.
                if (lvPathfind.Sorting == SortOrder.Ascending)
                    lvPathfind.Sorting = SortOrder.Descending;
                else
                    lvPathfind.Sorting = SortOrder.Ascending;
            }


            // Set the ListViewItemSorter property to a new ListViewItemComparer
            // object.
            lvPathfind.ListViewItemSorter = new RoomsViewItemComparer(e.Column,
                                                                           lvPathfind.Sorting);

            // Call the sort method to manually sort.
            lvPathfind.Sort();
        }

        private void btnPathfind_Click(object sender, EventArgs e)
        {
            mUI.InvokePathfind((int)numPathfindId.Value);
        }

        private void lnkUncheckRooms_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ClearChecks();
        }

        internal void ClearChecks()
        {
            foreach (ListViewItem item in CheckedRooms)
            {
                item.Checked = false;
            }
        }

        internal void CheckRoom(int index)
        {
            lvPathfind.Items[index].Checked = true;
        }

        private void lnkLoadRooms_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string s = FileIO.LoadFileToString("Import Rooms");
            if (s == null)
            {
                return;
            }
            int i = 0;
            foreach (string l in s.Split(new char[] { ',', '\n', '\r', ';', '\t' }))
            {
                i += SelectRoomsByName(l);
            }
            MessageBox.Show("Selected " + i + " rooms.", "Loaded Rooms", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void lnkSaveRooms_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            List<string> rs = new List<string>();
            foreach (ListViewItem m in CheckedRooms)
            {
                if (!rs.Contains(m.Text))
                {
                    rs.Add(m.Text);
                }
            }
            StringBuilder sb = new StringBuilder();
            foreach (string s in rs)
            {
                sb.Append(s).Append("\n");
            }
            FileIO.SaveFileFromString("Export Rooms List", "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
                "DCT rooms.txt", sb.ToString());
        }

        private int SelectRoomsByName(string name)
        {
            int i = 0;
            foreach (ListViewItem item in Rooms.Find(name, false))
            {
                item.Checked = true;
                i++;
            }
            return i;
        }

        private void numPathfindId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                btnPathfind_Click(null, null);
            }
        }

        private void lvPathfind_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvPathfind.SelectedItems.Count < 1)
                return;
            numPathfindId.Value = int.Parse(lvPathfind.SelectedItems[0].SubItems[1].Text);
        }

        private void lnkRoomsSelect_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int i = 0;
            string input = InputBox.Prompt("Room Selection", "Check all rooms with the following names (separated by commas):");
            if (string.IsNullOrEmpty(input))
                return;
            foreach (string s in input.Split(new char[] { ',' }))
            {
                i += SelectRoomsByName(s.Trim());
            }
            MessageBox.Show("Selected " + i + " rooms.", "Select Rooms", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
