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

        private static Dictionary<int, string> mAreas = new Dictionary<int, string>();
        private static Dictionary<int, int> mMobs = new Dictionary<int, int>();
        private static List<int> mRooms = new List<int>();

        internal static void Set(List<Account> accounts, AttackingType type)
        {
            mAccounts = accounts;
            mType = type;
        }

        internal static void BeginArea()
        {
            StartRun();
        }

        internal static void BeginMultiArea(Dictionary<int, string> rooms)
        {
            mAreas = rooms;
            StartRun();
        }

        internal static void BeginMobs(Dictionary<int, int> mobs)
        {
            mMobs = mobs;
            StartRun();
        }

        internal static void BeginRooms(List<int> rooms)
        {
            mRooms = rooms;
            StartRun();
        }

        /// <summary>
        /// Passes account processing to a ThreadPool thread
        /// </summary>
        private static void StartRun()
        {
            if (mAccounts.Count < 1)
            {
                CoreUI.Instance.LogPanel.LogAttack("E: You must enter an account first");
                return;
            }
            else if (CoreUI.Instance.Settings.AttackMode != 2 && CoreUI.Instance.Settings.FilterMobs && CoreUI.Instance.Settings.MobFilters.Length < 1)
            {
                CoreUI.Instance.LogPanel.LogAttack("E: You have filters enabled but you haven't set them.  Nothing will be attacked with these settings - turn filtering off.");
                return;
            }

            CoreUI.Instance.ToggleAttack(true);

            MethodInvoker d = new MethodInvoker(Run);
            d.BeginInvoke(new AsyncCallback(RunCallback), d);
        }

        /// <summary>
        /// Attacks with accounts
        /// </summary>
        private static void Run()
        {
            // save settings
            RegistryUtil.Save();
            IniWriter.Save();
            ConfigSerializer.WriteFile("config.xml", CoreUI.Instance.Settings);

            lock (mAccounts)
            {
                foreach (Account a in mAccounts)
                {
                    CoreUI.Instance.LogPanel.Log("Refreshing " + a.Name + "'s position...");
                    a.Mover.RefreshRoom();

                    // no point in moving if we don't have rage
                    if (a.Mover.Account.Rage < Math.Max(1, CoreUI.Instance.Settings.StopBelowRage))
                    {
                        // go to next account
                        CoreUI.Instance.LogPanel.Log(string.Format("Not attacking on {0}, reached rage limit", a.Name));
                        continue;
                    }

                    a.Mover.ReturnToStartHandler.SetOriginal();

                    CoreUI.Instance.AccountsPanel.Engine.SetMain(a);
                    switch (mType)
                    {
                        case AttackingType.Single:
                            CoreUI.Instance.DoAttackArea();
                            break;
                        case AttackingType.Multi:
                            CoreUI.Instance.DoAttackMultiAreas(mAreas);
                            break;
                        case AttackingType.Mobs:
                            CoreUI.Instance.DoAttackMobs(mMobs);
                            break;
                        case AttackingType.Rooms:
                            CoreUI.Instance.DoAttackRooms(mRooms);
                            break;
                    }
                    CoreUI.Instance.LogPanel.Log(a.Name + " attack coverage complete");

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

            if (CoreUI.Instance.Settings.ReturnToStart)
            {
                foreach (Account a in mAccounts)
                {
                    a.Mover.ReturnToStartHandler.InvokeReturn();
                }
            }

            if (CoreUI.Instance.Settings.UseCountdownTimer || CoreUI.Instance.Settings.UseHourTimer)
            {
                CoreUI.Instance.MainPanel.Countdown(mType);
            }

            mAreas.Clear();
            mMobs.Clear();
        }
    }
}