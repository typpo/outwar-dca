using System.Collections.Generic;

namespace DCT.Outwar.World
{
    internal class RoomHashRecord
    {
        private Dictionary<int, string> mRooms;

        internal int Count
        {
            get { return mRooms.Count; }
        }

        internal void Clear()
        {
            mRooms.Clear();
        }

        private Account mAccount;

        internal RoomHashRecord(Account a)
        {
            mAccount = a;
            mRooms = new Dictionary<int, string>();
        }

        internal void Save(int id, string url)
        {
            if (!mRooms.ContainsKey(id))
            {
                mRooms.Add(id, url);
            }
        }

        internal string GetRoom(int id)
        {
            if (mRooms.ContainsKey(id) && mAccount.Ret == mAccount.Name)
            {
                foreach (int i in mRooms.Keys)
                {
                    if (i == id)
                    {
                        return mRooms[i];
                    }
                }
            }
            return null;
        }

        internal List<int> Optimize(List<int> nodes)
        {
            if (nodes == null || mAccount.Ret != mAccount.Name)
            {
                return null;
            }

            List<int> ret = new List<int>();
            for (int i = nodes.Count; i > 0; i--)
            {
                int rm = nodes[i - 1];
                ret.Insert(0, rm);
                if (mRooms.ContainsKey(rm))
                {
                    break;
                }
            }
            return ret;
        }
    }
}