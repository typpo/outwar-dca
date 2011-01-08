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
                return Accounts[mMainIndex];
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

        internal int Login(string server, string user, string pass)
        {
            HttpSocket.DefaultInstance.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.0; en-US; rv:0.9.4) Gecko/20011019 Netscape6/6.2";
            HttpSocket.DefaultInstance.Cookie = null;
            //string loginpage = HttpSocket.DefaultInstance.Get("http://sigil.outwar.com");
            //if(loginpage.Contains("tempSec"))
            //{
            //    string tmp = Parser.Parse(loginpage, "Enter ", " here");
            //    tmp = Parser.RemoveRange(tmp, "<", ">").Replace("\"", "").Replace("'", "");
            //    toPost += "&tempSec=" + tmp;
            //}
            string toPost = "login_username=" + user + "&login_password=" + pass;
            HttpSocket.DefaultInstance.Post(string.Format("http://{0}.outwar.com/myaccount.php", server), toPost);

            int ret = AddCharacters();

            HttpSocket.DefaultInstance.UserAgent = "Typpo DCAA Client";
            return ret;
        }

        internal int Login(string server, string rgsessid)
        {

            HttpSocket.DefaultInstance.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.0; en-US; rv:0.9.4) Gecko/20011019 Netscape6/6.2";
            HttpSocket.DefaultInstance.Cookie = null;

            HttpSocket.DefaultInstance.Get(string.Format("http://www.outwar.com/myaccount.php?rg_sess_id={0}&serverid={1}&suid={2}",
                    RgSessId,
                    Server.NameToId(MainAccount.Server),
                    MainAccount.Id));

            int ret = AddCharacters();

            HttpSocket.DefaultInstance.UserAgent = "Typpo DCAA Client";
            return ret;
        }

        private int AddCharacters()
        {
            int ret = 0;
            for (int i = 1; i <= Server.NUM_SERVERS; i++)
            {
                string s = string.Format("http://{0}.outwar.com/accounts.php?ac_serverid={1}",
                                         Server.IdToName(i), i);
                string svrlist = HttpSocket.DefaultInstance.Get(s);
                ret += AddAccountsFromSource(svrlist);
            }

            if (HttpSocket.DefaultInstance.HasCookie)
            {
                RgSessId = HttpSocket.DefaultInstance.Cookie.Substring(11, HttpSocket.DefaultInstance.Cookie.IndexOf(";") - 11);
            }
            return ret;
        }

        private int AddAccountsFromSource(string src)
        {
            // No accounts
            if (!src.Contains("PLAY!"))
            {
                return 0;
            }

            Parser m1 = new Parser(src);
            int serverid;
            if (!int.TryParse(m1.Parse("serverid=", "\""), out serverid))
            {
                return 0;
            }

            int ret = 0;
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
                ret++;
                Last.Socket.Cookie = HttpSocket.DefaultInstance.Cookie + " ow_userid=" + id
                                     + "; ow_serverid=" + serverid;
            }
            return ret;
        }
    }
}