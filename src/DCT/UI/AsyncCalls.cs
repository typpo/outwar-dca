using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DCT.Outwar;
using DCT.Outwar.World;
using DCT.Pathfinding;
using DCT.Settings;

namespace DCT.UI
{
    partial class CoreUI
    {
        #region Attacking

        private void SetupHandler()
        {
            AttackingType type;
            if (optCountdownSingle.Checked)
            {
                type = AttackingType.Single;
            }
            else if (optCountdownMulti.Checked)
            {
                type = AttackingType.Multi;
            }
            else
            {
                type = AttackingType.Mobs;
            }
            List<Account> accounts = new List<Account>();
            if (lvAccounts.CheckedIndices.Count > 0)
            {
                foreach (int i in lvAccounts.CheckedIndices)
                {
                    accounts.Add(mAccounts[i]);
                }
            }
            else if (lvAccounts.FocusedItem != null)
            {
                accounts.Add(mAccounts[lvAccounts.FocusedItem.Index]);
            }
            else if(mAccounts.Count > 0)
            {
                accounts.Add(mAccounts[0]);
            }

            AttackHandler.Set(accounts, type);
        }

        private void AttackArea()
        {
            if (mAccounts.Count < 1)
            {
                LogAttack("E: You must enter an account first");
                return;
            }

            SetupHandler();
            AttackHandler.BeginArea();
        }

        internal void DoAttackArea()
        {
            Globals.AttackOn = true;

            mAccounts.MainAccount.Mover.CoverArea();

            if (!Globals.AttackMode)
            {
                Log("Single-area coverage quit");
                StopAttacking(true);
            }
        }

        private void AttackAreas()
        {
            if (mAccounts.Count < 1)
            {
                LogAttack("E: You must enter an account first");
                return;
            }
            else if (lvPathfind.CheckedIndices.Count < 1)
            {
                Log("E: Choose at least 1 area to cover");
                return;
            }

            Dictionary<int, string> rooms = new Dictionary<int, string>();
            foreach (int i in lvPathfind.CheckedIndices)
            {
                rooms.Add(int.Parse(lvPathfind.Items[i].SubItems[1].Text), lvPathfind.Items[i].Text);
            }

            SetupHandler();
            AttackHandler.BeginMultiArea(rooms);
        }

        internal void DoAttackMultiAreas(Dictionary<int, string> rooms)
        {
            // TODO shouldn't need room name as string...

            Account mAccount = mAccounts.MainAccount;
            List<string> done = new List<string>();
            foreach (int rm in rooms.Keys)
            {
                if (!Globals.AttackMode || mAccount.Mover.Location == null)
                    goto quit;

                if (done.Contains(rooms[rm]))
                    continue;
                else
                    done.Add(rooms[rm]);

                Globals.AttackOn = false;

                mAccount.Mover.PathfindTo(rm);

                if (!Globals.AttackMode)
                    goto quit;

                Globals.AttackOn = true;

                mAccounts.MainAccount.Mover.CoverArea();

                if (!Globals.AttackOn || !Globals.AttackMode)
                    goto quit;
            }
            return;
            quit:
            Log("Multi-area coverage quit");
            StopAttacking(true);
        }

        private void AttackMobs()
        {
            if (mAccounts.Count < 1)
            {
                LogAttack("E: You must enter an account first");
                return;
            }
            else if (lvMobs.CheckedIndices.Count < 1)
            {
                Log("E: Choose at least 1 mob to attack");
                return;
            }

            Dictionary<int, int> mobs = new Dictionary<int, int>();
            foreach (int i in lvMobs.CheckedIndices)
            {
                mobs.Add(int.Parse(lvMobs.Items[i].SubItems[1].Text), int.Parse(lvMobs.Items[i].SubItems[2].Text));
            }

            SetupHandler();
            AttackHandler.BeginMobs(mobs);
        }

        internal void DoAttackMobs(Dictionary<int, int> mobs)
        {
            Account mAccount = mAccounts.MainAccount;

            foreach (int mb in mobs.Keys)
            {
                if (!Globals.AttackMode || mAccount.Mover.Location == null)
                    goto quit;

                Globals.AttackOn = false;

                mAccount.Mover.PathfindTo(mobs[mb]);

                if (!Globals.AttackMode)
                    goto quit;

                Globals.AttackOn = true;

                if (!Globals.AttackOn || !Globals.AttackMode)
                    goto quit;

                mAccounts.MainAccount.Mover.Location.AttackMob(mb);
            }
            return;
            quit:
            Log("Mob coverage quit");
            StopAttacking(true);
        }

        #endregion

        #region Pathfinding

        private void InvokePathfind(int room)
        {
            if (!Pathfinder.Exists(room))
            {
                Log("E: Select a room that exists in the map database");
                return;
            }

            foreach (int index in lvAccounts.CheckedIndices)
            {
                PathfindHandler d = new PathfindHandler(DoPathfind);
                d.BeginInvoke(index, room, new AsyncCallback(PathfindCallback), d);
            }
        }

        private void InvokeAdventures(int room)
        {
            foreach (int index in lvAccounts.CheckedIndices)
            {
                PathfindHandler d = new PathfindHandler(DoPathfind);
                d.BeginInvoke(index, room, new AsyncCallback(PathfindCallback), d);
            }
        }

        private void PathfindCallback(IAsyncResult ar)
        {
            PathfindHandler d = (PathfindHandler) ar.AsyncState;
            d.EndInvoke(ar);
            Toggle(true);
        }

        private delegate void PathfindHandler(int accountIndex, int room);

        private void DoPathfind(int accountIndex, int room)
        {
            mAccounts[accountIndex].Mover.RefreshRoom();
            mAccounts[accountIndex].Mover.PathfindTo(room);
        }

        #endregion

        #region Login

        private void Login()
        {
            if (Pathfinder.Rooms.Count == 0)
            {
                Log("E: Use the correct version.");
                return;
            }

            txtUsername.Enabled = false;
            txtPassword.Enabled = false;
            chkRemember.Enabled = false;
            btnLogin.Enabled = false;
            btnLogout.Enabled = false;

            Log("Logging in...");

            LoginHandler d = new LoginHandler(DoLogin);
            d.BeginInvoke(new AsyncCallback(LoginCallback), d);
        }

        private delegate void LoginCallbackHandler(IAsyncResult ar);

        private void LoginCallback(IAsyncResult ar)
        {
            if (InvokeRequired)
            {
                Invoke(new LoginCallbackHandler(LoginCallback), ar);
                return;
            }

            LoginHandler d = (LoginHandler) ar.AsyncState;
            int n = d.EndInvoke(ar);
            if (n < 1)
            {
                txtUsername.Text = string.Empty;
                txtPassword.Text = string.Empty;
                Log(
                    "E: Outwar rejected your login.  Make sure you're putting in the correct Rampid Gaming Account information.");
            }
            else
            {
                for (int i = mAccounts.Count - n; i < mAccounts.Count; i++)
                {
                    Account a = mAccounts.Accounts[i];
                    ListViewItem item = new ListViewItem(new string[] {a.Name, "Loaded", "-", "0", "0", "0"});
                    lvAccounts.Groups[Server.NameToId(a.Server) - 1].Items.Add(item);
                    lvAccounts.Items.Add(item);
                }
                Log("Loaded " + n + " characters.");
            }

            UserEditable.LastUsername = txtUsername.Text;
            UserEditable.LastPassword = txtPassword.Text;

            txtUsername.Enabled = true;
            txtPassword.Enabled = true;
            chkRemember.Enabled = true;
            btnLogout.Enabled = true;
            btnRefresh.Enabled = true;
        }

        private delegate int LoginHandler();

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Number of accounts added</returns>
        private int DoLogin()
        {
            int orig = mAccounts.Count;
            mAccounts.Login(txtUsername.Text, txtPassword.Text, chkRemember.Checked);

            return mAccounts.Count - orig;
        }

        #endregion

        #region Training

        private void InvokeReturn(int index)
        {
            MethodInvoker d = new MethodInvoker(mAccounts[index].Mover.ReturnToStartHandler.Return);
            d.BeginInvoke(new AsyncCallback(TrainingReturnCallback), d);
        }

        private void TrainingReturnCallback(IAsyncResult ar)
        {
            Toggle(true);
        }

        #endregion
    }
}