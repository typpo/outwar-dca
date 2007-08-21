using DCT.Parsing;

namespace DCT.Outwar.World
{
    public abstract class Occupier
    {
        protected string mName;
        protected string mURL;
        protected bool IsInRoom
        {
            get { return (mRoom.Mover.Location != null && mRoom.Id == mRoom.Mover.Location.Id); }
        }
        protected Room mRoom;
        protected string mLoadSrc;
        protected long mId;
        public long Id
        {
            get { return mId; }
        }

        public Occupier(string name, string url, Room room)
        {
            mName = name;
            mURL = url;
            mRoom = room;

            if (mURL.Contains("ONMOUSEOVER") || mRoom.Mover.Account.Ret != mRoom.Mover.Account.Name)
            {
                // spawn mob
                mURL = mURL.Substring(0, mURL.IndexOf("\""));
            }

            if (!long.TryParse(new Parser(mURL).Parse("id=", "&h"), out mId))
            {
                mId = 0;
            }
        }
    }
}