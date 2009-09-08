using DCT.Outwar.World;
using DCT.Parsing;
using DCT.Protocols.Http;

namespace DCT.Outwar
{
    internal class Account
    {
        internal string Ret { get; set; }
        internal OutwarHttpSocket Socket { get; private set; }
        internal Mover Mover { get; private set; }
        internal ServerName Server { get; private set; }
        internal string Name { get; private set; }

        private int mRage;
        internal int Rage
        {
            get { return mRage; }
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


        internal Account(OutwarHttpSocket socket, string name, int id, ServerName server)
        {
            Socket = socket;
            Socket.Account = this;
            Name = name;
            Id = id;
            Server = server;

            mRage = 0;
            mLevel = 0;
            mExp = 0;
            NeedsLevel = false;

            Socket = socket;
            Mover = new Mover(this, Socket);
        }

        internal void GetStats(string source)
        {
            Parser mm = new Parser(source);

            int.TryParse(mm.Parse("LEVEL:</b> ", "</div>").Trim(), out mLevel);
            int.TryParse(mm.Parse("RAGE</span>:</b> ", "</div>").Trim(), out mRage);

            long.TryParse(mm.Parse("EXP:</b> ", "</div>").Trim().Replace(",", ""), out mExp);

            if (source.Contains("LEVEL!"))
                NeedsLevel = true;
            else
                NeedsLevel = false;
        }
    }
}