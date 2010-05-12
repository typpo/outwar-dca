using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DCT.Util;
using DCT.Pathfinding;

namespace DCT.UI
{
    public partial class SpawnsPanel : UserControl
    {
        private readonly CoreUI mUI;

        internal ListView.ListViewItemCollection Spawns
        {
            get { return lvSpawns.Items; }
        }

        internal ListView.CheckedListViewItemCollection CheckedSpawns
        {
            get { return lvSpawns.CheckedItems; }
        }

        internal bool AttackSpawns
        {
            get { return chkAttackSpawns.Checked; }
            set { chkAttackSpawns.Checked = value; }
        }

        internal bool IgnoreSpawnRage
        {
            get { return chkIgnoreRage.Checked; }
            set { chkIgnoreRage.Checked = value; }
        }

        internal bool SpawnsEnabled
        {
            get { return btnCamp.Enabled && btnGo.Enabled; }
            set{btnCamp.Enabled = btnGo.Enabled = value;}
        }

        public SpawnsPanel(CoreUI ui)
        {
            InitializeComponent();
            Log("Spawn log started");
            mUI = ui;
        }

        internal void BuildView()
        {
            lvSpawns.Items.Clear();
            foreach (MappedMob mb in Pathfinder.Spawns)
            {
                if (mb != null)
                {
                    ListViewItem tmp = new ListViewItem(new string[] { mb.Name, mb.Level.ToString(), mb.Room.ToString(), "0", "0" });
                    tmp.Name = mb.Name;
                    lvSpawns.Items.Add(tmp);
                }
            }
        }

        internal delegate void CountersHandler(int roomid);

        internal void Sighted(int roomid)
        {
            if (InvokeRequired)
            {
                Invoke(new CountersHandler(Sighted), roomid);
                return;
            }

            foreach (ListViewItem item in lvSpawns.Items)
            {
                if (int.Parse(item.SubItems[2].Text) == roomid)
                {
                    item.SubItems[3].Text = ((int.Parse(item.SubItems[3].Text) + 1)).ToString();
                    item.BackColor = Color.Red;
                    return;
                }
            }
        }

        internal void Attacked(int roomid)
        {
            if (InvokeRequired)
            {
                Invoke(new CountersHandler(Attacked), roomid);
                return;
            }

            foreach (ListViewItem item in lvSpawns.Items)
            {
                if (int.Parse(item.SubItems[2].Text) == roomid)
                {
                    item.SubItems[4].Text = ((int.Parse(item.SubItems[4].Text) + 1)).ToString();
                    item.BackColor = Color.Green;
                    return;
                }
            }
        }

        internal delegate void LogHandler(string txt);

        internal void Log(string txt)
        {
            if (InvokeRequired)
            {
                Invoke(new LogHandler(Log), txt);
                return;
            }

            lstLog.Items.Insert(0, string.Format("[{0}] {1}", DateTime.Now.ToString("T"), txt));
            if (mUI != null && mUI.SelectedTabIndex != CoreUI.TABINDEX_SPAWNS)
            {
                // initialized and tab not focused, so flag as updated
                mUI.Tabs.TabPages[CoreUI.TABINDEX_SPAWNS].Text = "Spawns (*)";
            }
        }

        internal void Export()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" *** SPAWN LOG\r\n");
            foreach (string text in lstLog.Items)
                sb.AppendLine(text);

            FileIO.SaveFileFromString("Export Log", "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
                                      "DCT SpawnLog " + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second, sb.ToString());
        }

        internal void Clear()
        {
            lstLog.Items.Clear();
        }

        private void chkAttackSpawns_CheckedChanged(object sender, EventArgs e)
        {
            chkIgnoreRage.Enabled = mUI.Settings.AttackSpawns = chkAttackSpawns.Checked;
        }

        private void chkIgnoreRage_CheckedChanged(object sender, EventArgs e)
        {
            mUI.Settings.IgnoreSpawnRage = chkIgnoreRage.Checked;
        }

        private void btnCamp_Click(object sender, EventArgs e)
        {
            if (CheckedSpawns.Count < 1)
            {
                MessageBox.Show("You need to select at least one spawn.", "Select a Spawn", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // check appropriate rooms; warn if overwriting

            if (mUI.RoomsPanel.CheckedRooms.Count > 0 && 
                MessageBox.Show("This will erase your current settings and selected rooms.  Continue?", "Rooms Erase", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
            {
                return;
            }

            mUI.RoomsPanel.ClearChecks();
            foreach (ListViewItem lvi in CheckedSpawns)
            {
                // note that this assumes the rooms listview is exactly the rooms list
                int i = Pathfinder.FindRoom(int.Parse(lvi.SubItems[2].Text));
                mUI.RoomsPanel.CheckRoom(i);
            }

            mUI.Settings.FilterMobs = false;
            mUI.Settings.IgnoreSpawnRage = true;
            mUI.Settings.AttackSpawns = true;
            mUI.Settings.LvlLimitMin = 0;
            mUI.Settings.LvlLimit = 80;
            mUI.Settings.RageLimit = 150;

            mUI.Settings.AttackMode = Outwar.World.AttackingType.Rooms;

            mUI.SyncSettings();

            mUI.StartAttacking();
        }

        private void lnkCampSelected_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("This option will configure your settings to continually farm mobs in selected spawn rooms and attack any spawns that are sighted.",
                "Camp Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void lnkCheckAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (ListViewItem item in Spawns)
            {
                item.Checked = true;
            }
        }
            
        private void linkUncheckAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (ListViewItem item in CheckedSpawns)
            {
                item.Checked = false;
            }
        }

        private void lnkAdd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string name = InputBox.Prompt("Add Spawn", "Enter spawn's name:");
            if (string.IsNullOrEmpty(name))
                return;
            string id = InputBox.Prompt("Add Spawn", "Enter spawn's room id# (this is important):");
            if (string.IsNullOrEmpty(id))
                return;
            int roomid;
            if (!int.TryParse(id, out roomid) || Pathfinder.FindRoom(roomid) < 0)
            {
                MessageBox.Show("Invalid room id#", "Add Spawn", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Pathfinder.Spawns.Add(new MappedMob(name, 0, roomid, 0, 0));
            Pathfinder.Spawns.Sort();
            BuildView();
        }

        private int mSortColumn;
        private void lvSpawns_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine whether the column is the same as the last column clicked.
            if (e.Column != mSortColumn)
            {
                // Set the sort column to the new column.
                mSortColumn = e.Column;
                // Set the sort order to ascending by default.
                lvSpawns.Sorting = SortOrder.Ascending;
            }
            else
            {
                // Determine what the last sort order was and change it.
                if (lvSpawns.Sorting == SortOrder.Ascending)
                    lvSpawns.Sorting = SortOrder.Descending;
                else
                    lvSpawns.Sorting = SortOrder.Ascending;
            }


            // Set the ListViewItemSorter property to a new ListViewItemComparer
            // object.
            lvSpawns.ListViewItemSorter = new SpawnsViewItemComparer(e.Column,
                                                                           lvSpawns.Sorting);

            // Call the sort method to manually sort.
            lvSpawns.Sort();
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            if (lvSpawns.SelectedItems.Count < 1)
            {
                mUI.LogPanel.Log("E: No mob selected.");
                return;
            }
            ListViewItem item = lvSpawns.SelectedItems[0];
            if (lvSpawns.SelectedItems.Count > 1)
            {
                mUI.LogPanel.Log(string.Format("Moving to {0} in room {1}", item.SubItems[0].Text, item.SubItems[2].Text));
            }
            mUI.InvokePathfind(int.Parse(item.SubItems[2].Text));
        }

        private void lvSpawns_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvSpawns.SelectedItems.Count < 1)
            {
                btnGo.Text = "Go to selection";
                return;
            }
            string txt = lvSpawns.SelectedItems[0].SubItems[0].Text;
            if (txt.StartsWith("A "))
                txt = txt.Substring(2);
            btnGo.Text = string.Format("Go to {0}", txt);
        }
    }
}