using System.Collections.Generic;

namespace DCT.Outwar.World
{
    public class RoomHashRecord
    {
        private Dictionary<int, string> mRooms;

        public int Count
        {
            get { return mRooms.Count; }
        }

        public void Clear()
        {
            mRooms.Clear();
        }

        private Account mAccount;

        public RoomHashRecord(Account a)
        {
            mAccount = a;
            mRooms = new Dictionary<int, string>();
        }

        public void Save(int id, string url)
        {
            if (!mRooms.ContainsKey(id))
            {
                mRooms.Add(id, url);
            }
        }

        public string GetRoom(int id)
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

        public List<int> Optimize(List<int> nodes)
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