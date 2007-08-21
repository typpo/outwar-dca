using System;

namespace DCT.Pathfinding
{
    internal class MappedMob : IComparable
    {
        private string mName;
        internal string Name
        {
            get { return mName; }
        }

        private int mRoom;
        internal int Room
        {
            get { return mRoom; }
        }

        private long mId;
        internal long Id
        {
            get { return mId; }
        }

        private long mLevel;
        internal long Level
        {
            get { return mLevel; }
        }

        private long mRage;
        internal long Rage
        {
            get { return mRage; }
        }

        internal MappedMob(string token)
        {
            string[] parts = token.Split(new char[] {';'});

            mName = parts[0];
            mId = int.Parse(parts[1]);
            mRoom = int.Parse(parts[2]);
            mLevel = long.Parse(parts[3]);
            mRage = long.Parse(parts[4]);
        }

        public int CompareTo(object other)
        {
            if(other.GetType() != typeof(MappedMob))
            {
                throw new Exception("Invalid mob compare type: " + other.GetType());
            }
            MappedMob mb = (MappedMob) other;
            return mName.CompareTo(mb.Name);
        }
    }
}