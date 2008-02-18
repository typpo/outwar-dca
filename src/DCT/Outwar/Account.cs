using DCT.Outwar.World;
using DCT.Parsing;
using DCT.Protocols.Http;

namespace DCT.Outwar
{
    internal class Account
    {
        private string mRet;
        internal string Ret
        {
            get { return mRet; }
            set { mRet = value; }
        }

        private OutwarHttpSocket mSocket;
        internal OutwarHttpSocket Socket
        {
            get { return mSocket; }
        }

        private Mover mMover;
        internal Mover Mover
        {
            get { return mMover; }
        }

        private ServerName mServer;
        internal ServerName Server
        {
            get { return mServer; }
        }

        private string mName;
        internal string Name
        {
            get { return mName; }
        }

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

        private long mId;
        internal long Id
        {
            get { return mId; }
        }

        private long mExp;
        internal long Exp
        {
            get { return mExp; }
        }

        private bool mNeedsLevel;
        internal bool NeedsLevel
        {
            get { return mNeedsLevel; }
        }


        internal Account(OutwarHttpSocket socket, string name, int id, ServerName server)
        {
            mSocket = socket;
            mSocket.Account = this;
            mName = name;
            mId = id;
            mServer = server;

            mRage = 0;
            mLevel = 0;
            mExp = 0;
            mNeedsLevel = false;

            mSocket = socket;
            mMover = new Mover(this, mSocket);
        }

        internal void GetStats(string source)
        {
            Parser mm = new Parser(source);

            int.TryParse(mm.Parse("LEVEL:</b> ", "</div>").Trim(), out mLevel);
            int.TryParse(mm.Parse("RAGE</span>:</b> ", "</div>").Trim(), out mRage);

            long.TryParse(mm.Parse("EXP:</b> ", "</div>").Trim().Replace(",", ""), out mExp);

            if (source.Contains("LEVEL!"))
                mNeedsLevel = true;
            else
                mNeedsLevel = false;
        }
    }
}