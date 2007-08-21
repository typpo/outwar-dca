using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DCT.Settings;
using DCT.UI;

namespace DCT.Outwar.World
{
    internal static class AttackHandler
    {
        private static List<Account> mAccounts;
        private static AttackingType mType;

        private static Dictionary<int, string> mRooms;
        private static Dictionary<int, int> mMobs;

        static AttackHandler()
        {
            mRooms = new Dictionary<int, string>();
            mMobs = new Dictionary<int, int>();
        }

        public static void Set(List<Account> accounts, AttackingType type)
        {
            mAccounts = accounts;
            mType = type;
        }

        public static void BeginArea()
        {
            StartRun();
        }

        public static void BeginMultiArea(Dictionary<int, string> rooms)
        {
            mRooms = rooms;
            StartRun();
        }

        public static void BeginMobs(Dictionary<int, int> mobs)
        {
            mMobs = mobs;
            StartRun();
        }

        /// <summary>
        /// Passes account processing to a ThreadPool thread
        /// </summary>
        private static void StartRun()
        {
            CoreUI.Instance.ToggleAttack(true);

            MethodInvoker d = new MethodInvoker(Run);
            d.BeginInvoke(new AsyncCallback(RunCallback), d);
        }

        /// <summary>
        /// Attacks with accounts
        /// </summary>
        private static void Run()
        {
            lock (mAccounts)
            {
                foreach (Account a in mAccounts)
                {
                    a.Mover.RefreshRoom();
                    a.Mover.ReturnToStartHandler.SetOriginal();

                    CoreUI.Instance.Accounts.SetMain(a);
                    switch (mType)
                    {
                        case AttackingType.Single:
                            CoreUI.Instance.DoAttackArea();
                            break;
                        case AttackingType.Multi:
                            CoreUI.Instance.DoAttackMultiAreas(mRooms);
                            break;
                        case AttackingType.Mobs:
                            CoreUI.Instance.DoAttackMobs(mMobs);
                            break;
                    }
                    CoreUI.Instance.Log(a.Name + " attack coverage complete");
                    if (!Globals.AttackMode || a.Ret != a.Name)
                    {
                        return;
                    }
                }
            }
        }

        private static void RunCallback(IAsyncResult ar)
        {
            MethodInvoker d = (MethodInvoker)ar.AsyncState;
            d.EndInvoke(ar);

            CoreUI.Instance.ToggleAttack(false);
            Globals.AttackOn = false;

            if (UserEditable.ReturnToStart)
            {
                foreach (Account a in mAccounts)
                {
                    a.Mover.ReturnToStartHandler.Return();
                }
            }

            if (UserEditable.UseCountdownTimer || UserEditable.UseHourTimer)
            {
                CoreUI.Instance.Countdown(mType);
            }

            mRooms.Clear();
            mMobs.Clear();
        }
    }
}