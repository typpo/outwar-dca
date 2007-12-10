using System;
using System.Windows.Forms;
using System.Collections.Generic;
using DCT.Settings;
using DCT.Threading;
using DCT.UI;
using DCT.Util;

namespace DCT.Outwar.World
{
    internal class RaidsEngine : IDisposable
    {
        private CountDownTimer mCountdownTimer;

        private AccountsEngine mAccounts;

        internal RaidsEngine(AccountsEngine e)
        {
            mAccounts = e;
        }

        internal void Process(string r)
        {
            // TODO: some sort of verification if in correct room; otherwise, move to room

            // form the raid
            Account mainAccount = CoreUI.Instance.Accounts.MainAccount;
            if (!mainAccount.Raider.Form(r))
            {
                return;
            }

            // join the raid
            SortedList<string, string> raids = mainAccount.Raider.Raids;
            foreach (Account a in CoreUI.Instance.Accounts.Accounts)
            {
                a.Raider.Raids = raids;
                CoreUI.Instance.Invoke((MethodInvoker)delegate
                {
                    a.Raider.Join();
                    CoreUI.Instance.Log(a.Name + " joined");
                });
            }

            // launch

            Wait();
        }

        private void Wait()
        {
            mCountdownTimer = new CountDownTimer(CoreUI.Instance.Settings.RaidInterval);
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