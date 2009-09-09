using System.Collections.Generic;
using DCT.Parsing;
using DCT.Protocols.Http;

namespace DCT.Outwar
{
    internal class AccountsEngine
    {
        internal string RgSessId { get; private set; }
        internal List<Account> Accounts { get; private set; }

        private int mMainIndex;

        internal Account MainAccount
        {
            get
            {
                if (Accounts.Count < 1)
                {
                    return null;
                }
                else
                {
                    return Accounts[mMainIndex];
                }
            }
        }

        internal AccountsEngine()
        {
            Accounts = new List<Account>();
            mMainIndex = 0;
        }

        internal Account this[int i]
        {
            get { return Accounts[i]; }
        }

        internal int Count
        {
            get { return Accounts.Count; }
        }

        internal void SetMain(Account a)
        {
            mMainIndex = Accounts.IndexOf(a);
        }

        internal void SetMain(int index)
        {
            mMainIndex = index;
        }

        internal Account Last
        {
            get { return Accounts[Accounts.Count - 1]; }
        }

        internal Account Add(string name, int id, ServerName server)
        {
            OutwarHttpSocket socket = new OutwarHttpSocket();
            Account a = new Account(socket, name, id, server);
            Accounts.Add(a);
            return a;
        }

        internal void Remove(Account a)
        {
            Accounts.Remove(a);
        }

        internal void Remove(int index)
        {
            Accounts.RemoveAt(index);
        }

        internal void Login(string user, string pass)
        {
            HttpSocket.DefaultInstance.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.0; en-US; rv:0.9.4) Gecko/20011019 Netscape6/6.2";
            HttpSocket.DefaultInstance.Cookie = null;
            string toPost = "login_username=" + user + "&login_password=" + pass;
            //string loginpage = HttpSocket.DefaultInstance.Get("http://sigil.outwar.com");
            //if(loginpage.Contains("tempSec"))
            //{
            //    string tmp = Parser.Parse(loginpage, "Enter ", " here");
            //    tmp = Parser.RemoveRange(tmp, "<", ">").Replace("\"", "").Replace("'", "");
            //    toPost += "&tempSec=" + tmp;
            //}
            HttpSocket.DefaultInstance.Post("http://sigil.outwar.com/myaccount.php", toPost);

            for (int i = 1; i <= Server.NUM_SERVERS; i++)
            {
                string svrlist = HttpSocket.DefaultInstance.Get("http://outwar.com/accounts.php?ac_serverid=" + i);                
                AddAccountsFromSource(svrlist);
            }

            if (HttpSocket.DefaultInstance.HasCookie)
            {
                RgSessId = HttpSocket.DefaultInstance.Cookie.Substring(11, HttpSocket.DefaultInstance.Cookie.IndexOf(";") - 11);
            }
            HttpSocket.DefaultInstance.UserAgent = "Typpo DCAA Client";
        }

        private void AddAccountsFromSource(string src)
        {
            // No accounts
            if (!src.Contains("PLAY!"))
            {
                return;
            }

            Parser m1 = new Parser(src);
            int serverid;
            if (!int.TryParse(m1.Parse("serverid=", "\""), out serverid))
            {
                return;
            }
            foreach (
                string t in
                    m1.MultiParse("<font color=\"#FFFF00\" face=\"Verdana, Arial, Helvetica, sans-serif\" size=\"1\">",
                                  "PLAY!"))
            {
                Parser p = new Parser(t);
                string name = p.Parse("<b>", "</b>");
                int id;
                if (!int.TryParse(p.Parse("suid=", "&"), out id))
                {
                    continue;
                }

                Add(name, id, Server.IdToName(serverid));
                Last.Socket.Cookie = HttpSocket.DefaultInstance.Cookie + " ow_userid=" + id
                                     + "; ow_serverid=" + serverid;
            }
        }
    }
}