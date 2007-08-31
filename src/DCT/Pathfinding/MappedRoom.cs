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

        internal MappedRoom(int id)
        {
            mId = id;
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
            if (other.GetType() == typeof(MappedRoom))
            {
                return mId.CompareTo(((MappedRoom)other).mId);
            }
            else
            {
                throw new Exception("Invalid room compare type: " + other.GetType());
            }
        }
    }
}