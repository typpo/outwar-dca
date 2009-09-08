using System;
using System.Collections.Generic;

namespace DCT.Pathfinding
{
    internal class MappedRoom : IComparable
    {
        internal int Id { get; private set; }
        internal string Name { get; private set; }
        internal List<int> Neighbors { get; private set; }

        internal MappedRoom(int id)
        {
            Id = id;
        }

        internal MappedRoom(string token)
        {
            Neighbors = new List<int>();

            string[] tmp = token.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries);
            Id = int.Parse(tmp[0]);
            Name = tmp[tmp.Length - 1];
            for (int i = 1; i < tmp.Length - 1; i++)
            {
                Neighbors.Add(int.Parse(tmp[i]));
            }
        }

        public int CompareTo(object other)
        {
            if (other.GetType() == typeof(MappedRoom))
            {
                return Id.CompareTo(((MappedRoom)other).Id);
            }
            throw new Exception("Invalid room compare type: " + other.GetType());
        }
    }
}