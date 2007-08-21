using System;
using System.Collections.Generic;
using DCT.Settings;
using DCT.Threading;
using DCT.UI;
using DCT.Util;

namespace DCT.Outwar.World
{
    public class RaidsEngine : IDisposable
    {
        private CountDownTimer mCountdownTimer;

        private AccountsEngine mAccounts;

        public RaidsEngine(AccountsEngine e)
        {
            mAccounts = e;
        }

        public void Process(List<string> r)
        {
            // form the raid...
            Account mainAccount = CoreUI.Instance.Accounts.MainAccount;
            if (!mainAccount.Raider.Check(r))
            {
                return;
            }

            // join the raid
            SortedList<string, string> raids = mainAccount.Raider.Raids;
            foreach (Account a in CoreUI.Instance.Accounts.Accounts)
            {
                a.Raider.Raids = raids;
                ThreadEngine.DefaultInstance.Enqueue(a.Raider.Join);
            }

            ThreadEngine.DefaultInstance.ProcessAll();
            Wait();
        }

        private void Wait()
        {
            mCountdownTimer = new CountDownTimer(UserEditable.RaidInterval*60);
            mCountdownTimer.Interval = 1000;
            mCountdownTimer.Stopped += new EventHandler(mCountdownTimer_Stopped);

            mCountdownTimer.Start();
        }

        private void mCountdownTimer_Stopped(object sender, EventArgs e)
        {
            CoreUI.Instance.ProcessRaidsThreaded();
        }

        public void Dispose()
        {
            mCountdownTimer.Dispose();
            this.Dispose();
        }
    }
}