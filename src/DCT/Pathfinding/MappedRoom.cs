using System;
using System.Collections.Generic;

namespace DCT.Pathfinding
{
    internal class MappedRoom : IComparable
    {
        internal int Id { get; set; }
        internal string Name { get; set; }
        internal List<int> Neighbors { get; set; }

        internal MappedRoom(int id, string name, List<int> nbrs)
        {
            Id = id;
            Name = name;
            Neighbors = nbrs;
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