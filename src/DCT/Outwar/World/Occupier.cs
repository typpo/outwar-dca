using DCT.Parsing;

namespace DCT.Outwar.World
{
    internal abstract class Occupier
    {
        protected string mName;
        internal string Name
        {
            get { return mName; }
        }
        protected string mURL;
        protected bool IsInRoom
        {
            get { return (mRoom.Mover.Location != null && mRoom.Id == mRoom.Mover.Location.Id); }
        }
        protected Room mRoom;
        protected string mLoadSrc;
        protected long mId;
        internal long Id
        {
            get { return mId; }
        }

        internal Occupier(string name, string url, Room room)
        {
            mName = name;
            mURL = url;
            mRoom = room;

            if (mURL.Contains("ONMOUSEOVER"))
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