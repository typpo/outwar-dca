using DCT.Outwar.World;
using DCT.Parsing;
using DCT.Protocols.Http;

namespace DCT.Outwar
{
    internal class Account
    {
        internal OutwarHttpSocket Socket { get; private set; }
        internal Mover Mover { get; private set; }
        internal ServerName Server { get; private set; }
        internal string Name { get; private set; }

        private int mRage;
        internal int Rage
        {
            get { return mRage; }
        }

        private long mGold;
        internal long Gold
        {
            get { return mGold; }
        }

        private int mLevel;
        internal int Level
        {
            get { return mLevel; }
        }
        internal long Id { get; private set; }

        private long mExp;
        internal long Exp
        {
            get { return mExp; }
        }
        internal bool NeedsLevel { get; private set; }

        //internal List<DropHandler> DropHandlers { get; private set; }

        internal Account(OutwarHttpSocket socket, string name, int id, ServerName server)
        {
            Socket = socket;
            Socket.Account = this;
            Name = name;
            Id = id;
            Server = server;

            mRage = -1;
            mLevel = -1;
            mExp = -1;
            mGold = -1;
            NeedsLevel = false;

            Socket = socket;
            Mover = new Mover(this, Socket);

            //DropHandlers = new List<DropHandler>();
        }

        internal void GetStats(string source)
        {
            Parser mm = new Parser(source);

            int.TryParse(mm.Parse("LEVEL:</b> ", "</div>").Trim(), out mLevel);
            int.TryParse(mm.Parse("RAGE</span>:</b> ", "</div>").Trim(), out mRage);

            long.TryParse(mm.Parse("EXP:</b> ", "</div>").Trim().Replace(",", ""), out mExp);

            long.TryParse(mm.Parse("Gold:</b></td><td>", "</td>").Trim(), out mGold);

            NeedsLevel = source.Contains("LEVEL!");
        }
    }
}