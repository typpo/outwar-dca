using System;
using System.Collections;
using System.Threading;
using DCT.Parsing;
using DCT.Protocols.Http;
using DCT.Settings;
using DCT.UI;

namespace DCT.Outwar
{
    public class ItemManager
    {
        #region DECLARATIONS

        public int qBackpack, numBackpack;
        private HttpSocket Connection;
        private Account mAccount;
        private bool tested, OK, vaultFull;
        private Stack iBackpack;
        private ArrayList unmoveables;

        #endregion

        #region CONSTRUCTOR

        public ItemManager(Account pOW, HttpSocket pConnection)
        {
            qBackpack = 0;
            numBackpack = 0;
            Connection = pConnection;
            mAccount = pOW;

            tested = false;
            OK = true;
            vaultFull = false;
            iBackpack = new Stack();
            unmoveables = new ArrayList();
        }

        #endregion

        #region GENERAL

        public void initQuota()
        {
            try
            {
                numItems(Connection.Get("profile.php"));
            }
            catch
            {
                qBackpack = 0;
                CoreUI.Instance.Log("E: Backpack quota could not be found");
            }
        }

        public void numItems(string source)
        {
            try
            {
                qBackpack = int.Parse(new Parser(source).Parse("BACKPACK (", ")"));
            }
            catch
            {
            }

            iBackpack.Clear();
            foreach (string str in new Parser(source).MultiParse("itemlink.php?id=", "&owner="))
            {
                int nothing;
                if (int.TryParse(str, out nothing))
                    iBackpack.Push(str);
            }

            numBackpack = iBackpack.Count;
        }

        public void manage()
        {
            if (numBackpack >= qBackpack && qBackpack != 0) // pack is full
            {
                if (UserEditable.AttackPause)
                {
                    if (UserEditable.PauseAt == 0)
                    {
                        CoreUI.Instance.Log("Stopping, your backpack is full");
                        CoreUI.Instance.StopAttacking(false);
                    }
                    else if (UserEditable.PauseAt == 2 && vaultFull)
                    {
                        CoreUI.Instance.Log("Stopping, your vault and backpack are full");
                        CoreUI.Instance.StopAttacking(false);
                    }
                }

                if (UserEditable.UseVault)
                {
                    moveToVault(numBackpack);
                }
            }
            else if (numBackpack > 0) // at least 1 item in pack
            {
                if (UserEditable.AttackPause && vaultFull && UserEditable.PauseAt == 1)
                {
                    CoreUI.Instance.Log("E: Stopping, your vault is full");
                    CoreUI.Instance.StopAttacking(false);
                }
                else if (UserEditable.UseVault)
                {
                    CoreUI.Instance.Log("Moving items to vault");
                    moveToVault(numBackpack);
                }
            }
        }

        #endregion

        #region VAULT

        private bool testVault(string src)
        {
            if (!tested)
            {
                OK = !src.Contains("answer the following");
                tested = true;

                if (!OK)
                    CoreUI.Instance.Log(
                        "E: You can't turn on an option that affects your vault without removing security word protection");
            }

            return OK;
        }

        public void moveToVault(int num)
        {
            if (OK)
            {
                if (testVault(Connection.Get("vault.php")))
                {
                    for (int i = 0; i < num && iBackpack.Count > 0; i++)
                    {
                        ThreadPool.QueueUserWorkItem(new WaitCallback(moveToVault), Convert.ToString(iBackpack.Pop()));
                        Thread.Sleep(75);
                    }
                }
            }
        }

        public void moveToVault(object id)
        {
            if (unmoveables.Contains(id))
                return;

            CoreUI.Instance.Log("Moving item " + Convert.ToString(id) + " of " + mAccount.Name + " to vault");

            Parser mm;
            int tmp;
            string src = "";
            do
            {
                try
                {
                    src = Connection.Post("vault.php?depid=" + Convert.ToString(id) + "&deposit=Deposit",
                                          "depid=" + Convert.ToString(id) + "&deposit=Deposit");
                }
                catch (Exception)
                {
                }

                mm = new Parser(src);

                try
                {
                    if ((tmp = int.Parse(mm.Parse("Storing <b>", " "))) !=
                        int.Parse(mm.Parse("<b>" + tmp + " / ", "</b>")))
                    {
                        vaultFull = true;
                    }
                }
                catch
                {
                }

                if (!src.Contains("added to your vault"))
                    unmoveables.Add(id);
                //if (Manip.Appears(src, "for trade"))
                //    unmoveables.Add(id);
            }
            while (!src.Contains("added to your vault")
                   && !vaultFull
                   && !unmoveables.Contains(id));
        }

        #endregion

        #region SELL

        public void sell(int num)
        {
            foreach (string str in
                new Parser(Connection.Get("specialtysell.php")).MultiParse("sell\" value=\"", "\""))
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(sell), str);
            }
        }

        public void sell(object id)
        {
            // TODO: ?
            /*
            Connection.Post(
                OW.URL("specialtysell.php"),
                "sell=" + Convert.ToInt64(id) + "&answer=" + OW.secretWord + "&submit=Sell");*/
        }

        #endregion
    }
}