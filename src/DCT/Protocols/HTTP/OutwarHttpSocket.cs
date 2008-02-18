using System;
using DCT.Outwar;
using DCT.Parsing;

namespace DCT.Protocols.Http
{
    internal class OutwarHttpSocket : HttpSocket
    {
        private Account mAccount;
        internal Account Account
        {
            get { return mAccount; }
            set { mAccount = value; }
        }

        protected override string Request(string url, string write)
        {
            if (mAccount == null)
            {
                throw new Exception("Attempted HTTP request before account was assigned.");
            }

            string ret = base.Request("http://" + mAccount.Server + ".outwar.com/" + url, write);
            ret = Parser.RemoveRange(ret, "<!--", "-->");
            if (Settings.Globals.AttackMode && ret.Contains(".outwar.com"))
            {
                mAccount.GetStats(ret);
            }
            return ret;
        }
    }
}