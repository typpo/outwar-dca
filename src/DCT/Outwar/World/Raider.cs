using System;
using System.Collections.Generic;
using DCT.Parsing;
using DCT.Protocols.Http;
using DCT.UI;

namespace DCT.Outwar.World
{
    internal class Raider
    {
        private Account mAccount;
        private OutwarHttpSocket mSocket;

        private SortedList<string, string> mRaids;
        internal SortedList<string, string> Raids
        {
            get { return mRaids; }
            set { mRaids = value; }
        }

        internal Raider(Account a)
        {
            mAccount = a;
            mSocket = a.Socket;
        }

        internal bool Form(string r)
        {
            RaidFormMob m = mAccount.Mover.Location.Raid;
            if (m == null || m.Name != r)
            {
                return false;
            }

            m.Form();
            return true;
        }

        internal bool Launch()
        {
            RaidFormMob m = mAccount.Mover.Location.Raid;
            if (m == null)
            {
                CoreUI.Instance.LogPanel.Log("E: Could not launch - can't find expected raid mob at current location.");
                return false;
            }

            m.Launch();
            return true;
        }

        internal bool Check(List<string> raidnames)
        {
            CoreUI.Instance.LogPanel.Log(mAccount.Name + " checking for raids...");

            Parser p = new Parser(mSocket.Get("currentraids.php"));

            mRaids = new SortedList<string, string>();

            foreach (string s in p.MultiParse("('joinraid.php?", "</font>"))
            {
                if (!s.Contains("raidid="))
                {
                    continue;
                }

                string url = s.Substring(0, s.IndexOf("'"));
                int i = s.IndexOf("yellow>") + 7;
                string name = s.Substring(i, s.Length - i);

                if (raidnames.Contains(name))
                {
                    mRaids.Add(url, name);
                }
            }

            bool found = mRaids.Count != 0;
            CoreUI.Instance.LogPanel.Log(found ? mRaids.Count + " raids to join" : "No raids to join");
            return found;
        }

        internal void Join()
        {
            if (mAccount.Mover.Location == null)
            {
                CoreUI.Instance.LogPanel.Log("E: " + mAccount.Name + " can't join raids if not authorized");
                return;
            }

            foreach (string s in mRaids.Values)
            {
                CoreUI.Instance.LogPanel.Log(mAccount.Name + " joining " + s + " raid...");

                string url = "joinraid.php?" + mRaids.Keys[mRaids.IndexOfValue(s)];
                Parser p = new Parser(mSocket.Get(url));
                string codeid = p.Parse("codeid\" value=\"", "\"");

                mSocket.Post(url, "codeid=" + codeid + "&submit=Launch!");
                mAccount.Socket.Status = "Joined " + s;
                CoreUI.Instance.LogPanel.Log(mAccount.Name + " joined " + s + " raid");
            }
        }
    }
}