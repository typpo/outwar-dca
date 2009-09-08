using System;
using DCT.Outwar;
using DCT.Parsing;

namespace DCT.Protocols.Http
{
    internal class OutwarHttpSocket : HttpSocket
    {
        internal Account Account { get; set; }

        protected override string Request(string url, string write)
        {
            if (Account == null)
            {
                throw new Exception("Attempted HTTP request before account was assigned.");
            }

            string ret = base.Request("http://" + Account.Server + ".outwar.com/" + url, write);
            ret = Parser.RemoveRange(ret, "<!--", "-->");
            if (Settings.Globals.AttackMode && ret.Contains(".outwar.com"))
            {
                Account.GetStats(ret);
            }
            return ret;
        }
    }
}