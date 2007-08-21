using DCT.Outwar.World;
using DCT.Parsing;
using DCT.Protocols.Http;

namespace DCT.Outwar
{
    public class Account
    {
        private string mRet;
        public string Ret
        {
            get { return mRet; }
            set { mRet = value; }
        }

        private OutwarHttpSocket mSocket;
        public OutwarHttpSocket Socket
        {
            get { return mSocket; }
        }

        private Mover mMover;
        public Mover Mover
        {
            get { return mMover; }
        }

        private Raider mRaider;
        public Raider Raider
        {
            get { return mRaider; }
        }

        private ItemManager mItemManager;
        public ItemManager ItemManager
        {
            get { return mItemManager; }
        }

        private ServerName mServer;
        public ServerName Server
        {
            get { return mServer; }
        }

        private string mName;
        public string Name
        {
            get { return mName; }
        }

        private int mRage;
        public int Rage
        {
            get { return mRage; }
        }

        private int mLevel;
        public int Level
        {
            get { return mLevel; }
        }

        private long mId;
        public long Id
        {
            get { return mId; }
        }

        private long mExp;
        public long Exp
        {
            get { return mExp; }
        }

        private bool mNeedsLevel;
        public bool NeedsLevel
        {
            get { return mNeedsLevel; }
        }


        public Account(OutwarHttpSocket socket, string name, int id, ServerName server)
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
            mItemManager = new ItemManager(this, mSocket);
            mRaider = new Raider(this);
        }

        public void GetStats(string source)
        {
            Parser mm = new Parser(source);

            if (source.Contains("name=\"owtime\">"))
            {
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
}