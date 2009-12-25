using System;
using System.Collections.Generic;

namespace DCT.Pathfinding
{
    internal class MappedRoom : IComparable
    {
        internal int Id { get; private set; }
        internal string Name { get; set; }  // no private set because spider needs it
        internal List<int> Neighbors { get; private set; }
        internal List<MappedRoom> MappedNeighbors { get; private set; }

        internal MappedRoom Pi { get; set; }
        internal int D { get; set; }
        internal int State { get; set; }    // 1=white, 2=gray, 3=black

        internal MappedRoom(int id, string name, List<int> nbrs)
        {
            Id = id;
            Name = name;
            Neighbors = nbrs;
            MappedNeighbors = new List<MappedRoom>();
            Pi = null;
            D = int.MaxValue;
            State = 1;
        }

        public override string ToString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendFormat("{0},", Id);
            foreach (int nbr in Neighbors)
                sb.AppendFormat("{0},", nbr);
            sb.AppendFormat("{0}", Name);
            return sb.ToString();
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