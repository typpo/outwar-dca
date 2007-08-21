using System;

using prjDCI.Parsing;
using prjDCI.HTTP;

namespace prjDCI.Outwar
{
    public class BankManager
    {
        private HttpSocket Connection;
        private Account OW;

        public BankManager(Account pOW, HttpSocket pConnection)
        {
            OW = pOW;
            Connection = pConnection;
        }

        public bool deposit()
        {
            Connection.Get(OW.URL("bank.php?deposit=1&amount=" +
                new Parser(Connection.Get(OW.URL("bank.php"))).Parse("?deposit=1&amount=", "\"")));
            return true;
        }

        public bool deposit(int amount)
        {
            string src = Connection.Get(OW.URL("bank.php?deposit=1&amount=" + amount));

            if (Parser.Appears(src, "that much money"))
                return false;
            return true;
        }

        public bool withdraw(int amount)
        {
            string src = Connection.Get(OW.URL("bank.php?withdraw=1&amount=" + amount));

            if (Parser.Appears(src, "that much money"))
                return false;
            return true;
        }
    }
}