using System;
using System.Collections.Generic;

namespace DCT.Pathfinding
{
    internal class MappedRoom : IComparable
    {
        private int mId;
        internal int Id
        {
            get { return mId; }
        }

        private string mName;
        internal string Name
        {
            get { return mName; }
        }

        private List<int> mNeighbors;
        internal List<int> Neighbors
        {
            get { return mNeighbors; }
        }

        internal MappedRoom(string token)
        {
            mNeighbors = new List<int>();

            string[] tmp = token.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries);
            mId = int.Parse(tmp[0]);
            mName = tmp[tmp.Length - 1];
            for (int i = 1; i < tmp.Length - 1; i++)
            {
                mNeighbors.Add(int.Parse(tmp[i]));
            }
        }

        public int CompareTo(object other)
        {
            if (other.GetType() != typeof (int))
            {
                throw new Exception("Invalid room compare type: " + other.GetType());
            }
            int id = (int) other;
            return mId.CompareTo(id);
        }
    }
}