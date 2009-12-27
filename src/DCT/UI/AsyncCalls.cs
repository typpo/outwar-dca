using System;
using System.Collections.Generic;
using System.Threading;
using DCT.Outwar;
using DCT.Outwar.World;
using DCT.Pathfinding;
using DCT.Settings;
using DCT.Threading;

namespace DCT.UI
{
    partial class CoreUI
    {
        #region Attacking

        private void SetUpHandler()
        {
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

            AttackHandler.Set(accounts, Settings.AttackMode);
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
                StopAttacking(true);
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
            StopAttacking(true);
        }

        internal void AttackMobs()
        {
            if (MobsPanel.CheckedIndices.Count < 1)
            {
                LogPanel.Log("E: Choose at least 1 mob to attack");
                return;
            }
            
            // sort by value - ie., sort by room number
            List<AttackHandler.MobArg> mobs = new List<AttackHandler.MobArg>();
            foreach (int i in MobsPanel.CheckedIndices)
            {
                int id = int.Parse(MobsPanel.Mobs[i].SubItems[1].Text);
                int room = int.Parse(MobsPanel.Mobs[i].SubItems[2].Text);
                string name = MobsPanel.Mobs[i].SubItems[0].Text;
                AttackHandler.MobArg arg = new AttackHandler.MobArg(id, room, name);
                if (!mobs.Contains(arg))
                {
                    mobs.Add(arg);
                }
            }
            mobs.Sort(
              delegate(
                AttackHandler.MobArg first,
                AttackHandler.MobArg second)
              {
                  return second.RoomId.CompareTo(first.RoomId);
              }
              );

            SetUpHandler();
            AttackHandler.BeginMobs(mobs);
        }

        internal void DoAttackMobs(List<AttackHandler.MobArg> mobs)
        {
            Account mAccount = AccountsPanel.Engine.MainAccount;

            foreach (AttackHandler.MobArg arg in mobs)
            {
                if (!Globals.AttackMode || mAccount.Mover.Location == null)
                    goto quit;

                Globals.AttackOn = false;

                mAccount.Mover.PathfindTo(arg.RoomId);

                if (!Globals.AttackMode)
                    goto quit;

                Globals.AttackOn = true;

                if (arg.Id < 0)
                {
                    // userspawn
                    AccountsPanel.Engine.MainAccount.Mover.Location.AttackMob(arg.Name);
                }
                else
                {
                    AccountsPanel.Engine.MainAccount.Mover.Location.AttackMob(arg.Id);
                }
                if (!Globals.AttackOn)
                {
                    return;
                }
            }
            return;
            quit:
            LogPanel.Log("Mob coverage quit");
            StopAttacking(true);
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
            StopAttacking(true);
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

        private class BulkMoveArg
        {
            internal int AcccountIndex {get;private set;}
            internal int Room { get; private set; }
            internal ManualResetEvent Handle {get; private set;}
            public BulkMoveArg(int accountIndex, int room, ManualResetEvent handle)
            {
                AcccountIndex = accountIndex;
                Room = room;
                Handle = handle;
            }
        }

        internal void InvokeBulkMove(int room)
        {
            Toggle(false);

            int[] indices = new int[AccountsPanel.CheckedIndices.Count];
            for (int i = 0; i < indices.Length; i++)
            {
                indices[i] = AccountsPanel.CheckedIndices[i];
            }

            BulkMoveHandler d = DoBulkMove;
            d.BeginInvoke(indices, room, BulkMoveCallback, d);
        }

        private void BulkMoveCallback(IAsyncResult ar)
        {
            BulkMoveHandler d = (BulkMoveHandler)ar.AsyncState;
            d.EndInvoke(ar);

            Toggle(true);
        }

        private delegate void BulkMoveHandler(int[] indices, int room);
        private void DoBulkMove(int[] indices, int room)
        {
            ManualResetEvent[] doneEvents = new ManualResetEvent[indices.Length];
            int i = 0;
            foreach (int index in indices)
            {
                doneEvents[i] = new ManualResetEvent(false);
                ThreadPool.QueueUserWorkItem(DoBulkMoveCallback, new BulkMoveArg(index, room, doneEvents[i]));
                i++;

                //DoPathfind(index, room);
            }

            WaitHandle.WaitAll(doneEvents);
        }

        // callback for threadpool
        private void DoBulkMoveCallback(object context)
        {
            BulkMoveArg a = (BulkMoveArg)context;
            DoPathfind(a.AcccountIndex, a.Room);
            a.Handle.Set();
        }

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
                ThreadEngine.DefaultInstance.Enqueue(AccountsPanel.Engine[index].Mover.Train);
            }

            // TODO this needs to go - convert to ThreadPool
            ThreadEngine.DefaultInstance.ProcessAll();

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

        #endregion
    }
}