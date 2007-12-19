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
            switch (mSettings.AttackMode)
            {
                case 0:
                    type = AttackingType.Single;
                    break;
                case 1:
                    type = AttackingType.Multi;
                    break;
                default:
                    type = AttackingType.Mobs;
                    break;
            }

            List<Account> accounts = new List<Account>();
            if (mAccountsPanel.CheckedIndices.Count > 0)
            {
                foreach (int i in mAccountsPanel.CheckedIndices)
                {
                    accounts.Add(mAccountsPanel.Engine[i]);
                }
            }
            else if (mAccountsPanel.FocusedAccount != null)
            {
                accounts.Add(mAccountsPanel.Engine[mAccountsPanel.FocusedAccount.Index]);
            }
            else if(mAccountsPanel.Engine.Count > 0)
            {
                accounts.Add(mAccountsPanel.Engine[0]);
            }

            AttackHandler.Set(accounts, type);
        }

        internal void AttackArea()
        {
            SetupHandler();
            AttackHandler.BeginArea();
        }

        internal void DoAttackArea()
        {
            Globals.AttackOn = true;

            mAccountsPanel.Engine.MainAccount.Mover.CoverArea();

            if (!Globals.AttackMode)
            {
                mLogPanel.Log("Single-area coverage quit");
                mMainPanel.StopAttacking(true);
            }
        }

        internal void AttackAreas()
        {
            if (mRoomsPanel.CheckedRooms.Count < 1)
            {
                mLogPanel.Log("E: Choose at least 1 area to cover");
                return;
            }

            Dictionary<int, string> rooms = new Dictionary<int, string>();
            foreach (int i in mRoomsPanel.CheckedIndices)
            {
                rooms.Add(int.Parse(mRoomsPanel.Rooms[i].SubItems[1].Text), mRoomsPanel.Rooms[i].Text);
            }

            SetupHandler();
            AttackHandler.BeginMultiArea(rooms);
        }

        internal void DoAttackMultiAreas(Dictionary<int, string> rooms)
        {
            // TODO shouldn't need room name as string...

            Account mAccount = mAccountsPanel.Engine.MainAccount;
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

                mAccountsPanel.Engine.MainAccount.Mover.CoverArea();

                if (!Globals.AttackOn || !Globals.AttackMode)
                    goto quit;
            }
            return;
            quit:
            mLogPanel.Log("Multi-area coverage quit");
            mMainPanel.StopAttacking(true);
        }

        internal void AttackMobs()
        {
            if (mMobsPanel.CheckedIndices.Count < 1)
            {
                mLogPanel.Log("E: Choose at least 1 mob to attack");
                return;
            }

            Dictionary<int, int> mobs = new Dictionary<int, int>();
            foreach (int i in mMobsPanel.CheckedIndices)
            {
                int key = int.Parse(mMobsPanel.Mobs[i].SubItems[1].Text);
                if (!mobs.ContainsKey(key))
                {
                    mobs.Add(key, int.Parse(mMobsPanel.Mobs[i].SubItems[2].Text));
                }
            }

            SetupHandler();
            AttackHandler.BeginMobs(mobs);
        }

        internal void DoAttackMobs(Dictionary<int, int> mobs)
        {
            Account mAccount = mAccountsPanel.Engine.MainAccount;

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

                mAccountsPanel.Engine.MainAccount.Mover.Location.AttackMob(mb);
            }
            return;
            quit:
            mLogPanel.Log("Mob coverage quit");
            mMainPanel.StopAttacking(true);
        }

        #endregion

        #region Pathfinding

        internal void InvokePathfind(int room)
        {
            if (mAccountsPanel.CheckedIndices.Count < 1)
            {
                mLogPanel.Log("E: Check the accounts you want to move");
                return;
            }
            if (!Pathfinder.Exists(room))
            {
                mLogPanel.Log("E: Select a room that exists in the map database");
                return;
            }

            Toggle(false);

            foreach (int index in mAccountsPanel.CheckedIndices)
            {
                PathfindHandler d = new PathfindHandler(DoPathfind);
                d.BeginInvoke(index, room, new AsyncCallback(PathfindCallback), d);
            }
        }

        internal void InvokeAdventures(int room)
        {
            if (mAccountsPanel.CheckedIndices.Count < 1)
            {
                mLogPanel.Log("E: Check the accounts you want to move.");
            }
            else if (mRaidsPanel.FocusedRaid == null)
            {
                mLogPanel.Log("E: Choose an adventure to move to.");
            }

            Toggle(false);
            foreach (int index in mAccountsPanel.CheckedIndices)
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
            mAccountsPanel.Engine[accountIndex].Mover.RefreshRoom();
            mAccountsPanel.Engine[accountIndex].Mover.PathfindTo(room);
        }

        #endregion

        #region Training

        internal void InvokeTraining(bool returnToStart)
        {
            if (mAccountsPanel.CheckedIndices.Count < 1)
            {
                mLogPanel.Log("E: Check the accounts you want to move");
                return;
            }

            Toggle(false);

            foreach (int index in mAccountsPanel.CheckedIndices)
            {
                mAccountsPanel.Engine[index].Mover.RefreshRoom();
                mAccountsPanel.Engine[index].Mover.ReturnToStartHandler.SetOriginal();
                DCT.Threading.ThreadEngine.DefaultInstance.Enqueue(mAccountsPanel.Engine[index].Mover.Train);
            }

            // TODO this needs to go
            DCT.Threading.ThreadEngine.DefaultInstance.ProcessAll();

            if (returnToStart)
            {
                foreach (int index in mAccountsPanel.CheckedIndices)
                {
                    mAccountsPanel.Engine[index].Mover.ReturnToStartHandler.InvokeReturn();
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