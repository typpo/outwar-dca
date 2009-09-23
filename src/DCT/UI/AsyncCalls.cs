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

        private void SetUpHandler()
        {
            AttackingType type;
            switch (Settings.AttackMode)
            {
                case 0:
                    type = AttackingType.Single;
                    break;
                case 1:
                    type = AttackingType.Multi;
                    break;
                case 2:
                    type = AttackingType.Mobs;
                    break;
                default:
                    type = AttackingType.Rooms;
                    break;
            }

            List<Account> accounts = new List<Account>();
            if (AccountsPanel.CheckedIndices.Count > 0)
            {
                foreach (int i in AccountsPanel.CheckedIndices)
                {
                    accounts.Add(AccountsPanel.Engine[i]);
                }
            }
            else if (AccountsPanel.FocusedAccount != null)
            {
                accounts.Add(AccountsPanel.Engine[AccountsPanel.FocusedAccount.Index]);
            }
            else if(AccountsPanel.Engine.Count > 0)
            {
                accounts.Add(AccountsPanel.Engine[0]);
            }

            AttackHandler.Set(accounts, type);
        }

        internal void AttackArea()
        {
            SetUpHandler();
            AttackHandler.BeginArea();
        }

        internal void DoAttackArea()
        {
            Globals.AttackOn = true;

            AccountsPanel.Engine.MainAccount.Mover.CoverArea();

            if (!Globals.AttackMode)
            {
                LogPanel.Log("Single-area coverage quit");
                MainPanel.StopAttacking(true);
            }
        }

        internal void AttackAreas()
        {
            if (RoomsPanel.CheckedRooms.Count < 1)
            {
                LogPanel.Log("E: Choose at least 1 area to cover");
                return;
            }

            Dictionary<int, string> rooms = new Dictionary<int, string>();
            foreach (int i in RoomsPanel.CheckedIndices)
            {
                rooms.Add(int.Parse(RoomsPanel.Rooms[i].SubItems[1].Text), RoomsPanel.Rooms[i].Text);
            }

            SetUpHandler();
            AttackHandler.BeginMultiArea(rooms);
        }

        internal void DoAttackMultiAreas(Dictionary<int, string> rooms)
        {
            // TODO shouldn't need room name as string...

            Account mAccount = AccountsPanel.Engine.MainAccount;
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

                AccountsPanel.Engine.MainAccount.Mover.CoverArea();

                if (!Globals.AttackOn || !Globals.AttackMode)
                    goto quit;
            }
            return;
            quit:
            LogPanel.Log("Multi-area coverage quit");
            MainPanel.StopAttacking(true);
        }

        internal void AttackMobs()
        {
            if (MobsPanel.CheckedIndices.Count < 1)
            {
                LogPanel.Log("E: Choose at least 1 mob to attack");
                return;
            }

            /*
            foreach (int i in MobsPanel.CheckedIndices)
            {
                int key = int.Parse(MobsPanel.Mobs[i].SubItems[1].Text);
                if (!mobs.ContainsKey(key))
                {
                    mobs.Add(key, int.Parse(MobsPanel.Mobs[i].SubItems[2].Text));
                }
            }
            */
            
            // sort by value - ie., sort by room number
            List<KeyValuePair<int, int>> sortinglist =
                    new List<KeyValuePair<int, int>>();
            foreach (int i in MobsPanel.CheckedIndices)
            {
                int key = int.Parse(MobsPanel.Mobs[i].SubItems[1].Text);
                int val = int.Parse(MobsPanel.Mobs[i].SubItems[2].Text);
                KeyValuePair<int,int> kvp = new KeyValuePair<int,int>(key, val);
                if (!sortinglist.Contains(kvp))
                {
                    sortinglist.Add(kvp);
                }
            }
            sortinglist.Sort(
              delegate(
                KeyValuePair<int, int> first,
                KeyValuePair<int, int> second)
              {
                  return second.Value.CompareTo(first.Value);
              }
              );
            // Clear the dictionary and repopulate it from the List
            Dictionary<int, int> mobs = new Dictionary<int, int>();
            foreach (KeyValuePair<int, int> kvp in sortinglist)
            {
                if (!mobs.ContainsKey(kvp.Key))
                {
                    mobs.Add(kvp.Key, kvp.Value);
                }
            }

            SetUpHandler();
            AttackHandler.BeginMobs(mobs);
        }

        internal void DoAttackMobs(Dictionary<int, int> mobs)
        {
            Account mAccount = AccountsPanel.Engine.MainAccount;

            foreach (int mb in mobs.Keys)
            {
                if (!Globals.AttackMode || mAccount.Mover.Location == null)
                    goto quit;

                Globals.AttackOn = false;

                mAccount.Mover.PathfindTo(mobs[mb]);

                if (!Globals.AttackMode)
                    goto quit;

                Globals.AttackOn = true;

                AccountsPanel.Engine.MainAccount.Mover.Location.AttackMob(mb);
                if (!Globals.AttackOn)
                {
                    return;
                }
            }
            return;
            quit:
            LogPanel.Log("Mob coverage quit");
            MainPanel.StopAttacking(true);
        }

        internal void AttackRooms()
        {
            List<int> rooms = new List<int>();
            foreach (int i in RoomsPanel.CheckedIndices)
            {
                rooms.Add(int.Parse(RoomsPanel.Rooms[i].SubItems[1].Text));
            }
            rooms.Sort();

            SetUpHandler();
            AttackHandler.BeginRooms(rooms);
        }

        internal void DoAttackRooms(List<int> rooms)
        {
            Account mAccount = AccountsPanel.Engine.MainAccount;

            foreach (int rm in rooms)
            {
                if (!Globals.AttackMode || mAccount.Mover.Location == null)
                    goto quit;

                Globals.AttackOn = false;

                mAccount.Mover.PathfindTo(rm);

                if (!Globals.AttackMode)
                    goto quit;

                Globals.AttackOn = true;

                AccountsPanel.Engine.MainAccount.Mover.Location.Attack();
                if (!Globals.AttackOn)
                {
                    return;
                }
            }
            return;
        quit:
            LogPanel.Log("Rooms coverage quit");
            MainPanel.StopAttacking(true);
        }

        #endregion

        #region Pathfinding

        internal void InvokePathfind(int room)
        {
            if (AccountsPanel.CheckedIndices.Count < 1)
            {
                LogPanel.Log("E: Check the accounts you want to move");
                return;
            }
            if (!Pathfinder.Exists(room))
            {
                LogPanel.Log("E: Select a room that exists in the map database.  If you are moving to a mob, its room information may be invalid.");
                return;
            }

            InvokeBulkMove(room);
        }

        internal void InvokeAdventures(int room)
        {
            if (AccountsPanel.CheckedIndices.Count < 1)
            {
                LogPanel.Log("E: Check the accounts you want to move.");
                return;
            }
            else if (RaidsPanel.FocusedRaid == null)
            {
                LogPanel.Log("E: Choose an adventure to move to.");
                return;
            }

            InvokeBulkMove(room);
        }

        //private int mBulkCounter;
        private void InvokeBulkMove(int room)
        {
            //if (mAccountsPanel.CheckedIndices.Count > 1)
            //{
            //    string tmp;
            //    int n;
            //    do
            //    {
            //        tmp = Util.InputBox.Prompt("Bulk Move", "Move up to how many accounts at once?", "5");
            //    }
            //    while (int.TryParse(tmp, out n));
            //    mBulkCounter = 0;
            //}
            // TODO stagger
            Toggle(false);
            foreach (int index in AccountsPanel.CheckedIndices)
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

            //mBulkCounter++;
        }

        private delegate void PathfindHandler(int accountIndex, int room);

        private void DoPathfind(int accountIndex, int room)
        {
            AccountsPanel.Engine[accountIndex].Mover.RefreshRoom();
            AccountsPanel.Engine[accountIndex].Mover.PathfindTo(room);
        }

        #endregion

        #region Training

        internal void InvokeTraining(bool returnToStart)
        {
            if (AccountsPanel.CheckedIndices.Count < 1)
            {
                LogPanel.Log("E: Check the accounts you want to move");
                return;
            }

            Toggle(false);

            foreach (int index in AccountsPanel.CheckedIndices)
            {
                AccountsPanel.Engine[index].Mover.RefreshRoom();
                AccountsPanel.Engine[index].Mover.ReturnToStartHandler.SetOriginal();
                DCT.Threading.ThreadEngine.DefaultInstance.Enqueue(AccountsPanel.Engine[index].Mover.Train);
            }

            // TODO this needs to go
            DCT.Threading.ThreadEngine.DefaultInstance.ProcessAll();

            if (returnToStart)
            {
                foreach (int index in AccountsPanel.CheckedIndices)
                {
                    AccountsPanel.Engine[index].Mover.ReturnToStartHandler.InvokeReturn();
                    // TODO not necessary to thread this - it already is threaded
                    //InvokeReturn(index);
                }
            }

            Toggle(true);
        }

        private void InvokeReturn(int index)
        {
            //MethodInvoker d = new MethodInvoker(mAccountsPanel.Engine[index].Mover.ReturnToStartHandler.Return);
            //d.BeginInvoke(new AsyncCallback(TrainingReturnCallback), d);
        }

        private void TrainingReturnCallback(IAsyncResult ar)
        {
            Toggle(true);
        }

        #endregion
    }
}