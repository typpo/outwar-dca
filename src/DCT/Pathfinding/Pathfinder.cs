using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DCT.Outwar.World;
using DCT.Protocols.Http;
using DCT.Security;
using DCT.Settings;
using DCT.Threading;
using DCT.Util;

namespace DCT.Pathfinding
{
    internal static class Pathfinder
    {
        // q
        private const string URL_ROOMS =
            "f5b57b6c048faa5bdc468e8caef8e0b3becc046212ae840dbadd4f30556b8a9337f2856bc94275ff7bee5d";
        private const string KEY_ROOMS = "A6w_d0X_jQ";

        private const string URL_MOBS =
            "12cafa06a6d89aa2b3cce105d6febe6ea6c136ad6170168eda59f13ade129148c4faa510f56a95973980";
        private const string KEY_MOBS = "g8Z_k_3ok0";

        private const string URL_RAIDS =
            "52623494ddc5872f11e2e65ab83ee5479e30856af99ab338bd0be43fa25186d4f71378dbb9608b60ed9ac8";
        private const string KEY_RAIDS = "0QR3PT58t0";

        private static List<MappedRoom> mRooms;
        public static List<MappedRoom> Rooms
        {
            get { return mRooms; }
        }

        private static List<MappedMob> mMobs;
        public static List<MappedMob> Mobs
        {
            get { return mMobs; }
        }

        private static SortedList<string, int> mAdventures;
        public static SortedList<string, int> Adventures
        {
            get { return mAdventures; }
        }

        private static List<List<int>> mAllPaths;

        public static void BuildMap()
        {
            //f5b57b6c048faa5bdc468e8caef8e0b3becc046212ae840dbadd4f30556b8a9337f2856bc94275ff7bee5d
            //12cafa06a6d89aa2b3cce105d6febe6ea6c136ad6170168eda59f13ade129148c4faa510f56a95973980
            //52623494ddc5872f11e2e65ab83ee5479e30856af99ab338bd0be43fa25186d4f71378dbb9608b60ed9ac8
            //Console.WriteLine(Crypt.BinToHex(Crypt.Get("http://typpo.us/programs/dci/maps/roomq.php", KEY_ROOMS, false)));
            //Console.WriteLine(Crypt.BinToHex(Crypt.Get("http://typpo.us/programs/dci/maps/mobq.php", KEY_MOBS, false)));
            //Console.WriteLine(Crypt.BinToHex(Crypt.Get("http://typpo.us/programs/dci/maps/raidq.php", KEY_RAIDS, false)));

            string map = HttpSocket.DefaultInstance.Get(Crypt.Get(Crypt.HexToBin(URL_ROOMS), KEY_ROOMS, false));
            map = Crypt.Get(Crypt.HexToBin(map), HttpSocket.DefaultInstance.UserAgent, false);

            mRooms = new List<MappedRoom>();

            foreach (string token in map.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (string.IsNullOrEmpty(token.Trim()) || token.StartsWith("#"))
                    continue;
                mRooms.Add(new MappedRoom(token));
            }

            // ------------------

            map = HttpSocket.DefaultInstance.Get(Crypt.Get(Crypt.HexToBin(URL_MOBS), KEY_MOBS, false));

            mMobs = new List<MappedMob>();
            foreach (string token in map.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (string.IsNullOrEmpty(token.Trim()) || token.StartsWith("#"))
                    continue;
                mMobs.Add(new MappedMob(token));
            }

            // -----------------

            mAdventures = new SortedList<string, int>();
            map = HttpSocket.DefaultInstance.Get(Crypt.Get(Crypt.HexToBin(URL_RAIDS), KEY_RAIDS, false));
            foreach (string token in map.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (string.IsNullOrEmpty(token.Trim()) || token.StartsWith("#"))
                    continue;
                int i = token.IndexOf(";");
                string name = token.Substring(0, i);
                int id = int.Parse(token.Substring(i + 1));
                mAdventures.Add(name, id);
            }
        }

        public static string BadLinks()
        {
            StringBuilder sb = new StringBuilder();

            foreach (MappedRoom rm in mRooms)
            {
                foreach (int nbr in rm.Neighbors)
                {
                    if (FindRoom(nbr) == -1)
                    {
                        sb.Append("Room " + rm.Id + "/" + rm.Name + " invalid link to: " + nbr);
                    }
                }
            }

            foreach (MappedMob mb in mMobs)
            {
                if (mb != null && FindRoom(mb.Room) == -1)
                {
                    sb.Append("Mob " + mb.Id + "/" + mb.Name + " invalid placement in: " + mb.Room);
                }
            }

            return sb.ToString();
        }

        public static List<int> CoverArea(int start)
        {
            List<int> ret = new List<int>();

            MappedRoom startRoom = mRooms[FindRoom(start)];
            ret.Add(start);

            foreach (MappedRoom rm in mRooms)
            {
                if (rm != null && rm.Name == startRoom.Name)
                    ret.Insert(Randomizer.Random.Next(ret.Count), rm.Id);
            }

            return ret;
        }

        public static List<int> GetSolution(int start, int finish, RoomHashRecord savedRooms)
        {
            mAllPaths = new List<List<int>>();

            PathfindHandler
                room1forward = new PathfindHandler(GetPath),
                forward = new PathfindHandler(GetPath);

            if (UserEditable.Optimize)
            {
                room1forward.BeginInvoke(1, finish, new AsyncCallback(PathfindCallback), room1forward);
            }
            else
            {
                mAllPaths.Add(null);
            }

            forward.BeginInvoke(start, finish, new AsyncCallback(PathfindCallback), forward);

            while (mAllPaths.Count < 2)
            {
                ThreadEngine.Sleep(10);
            }

            try
            {
                room1forward.EndInvoke(null);
                forward.EndInvoke(null);
            }
            catch
            {
            }

            List<int> bestPath = mAllPaths[0];
            for (int i = 0; i < mAllPaths.Count; i++)
            {
                List<int> tmpPath = mAllPaths[i];
                if (tmpPath == null)
                {
                    continue;
                }
                if (bestPath == null || (tmpPath.Count < bestPath.Count && tmpPath.Count != 0))
                {
                    bestPath = tmpPath;
                }
            }
            return savedRooms.Optimize(bestPath);
        }

        private delegate List<int> PathfindHandler(int from, int to);
        private static List<int> GetPath(int start, int finish)
        {
            List<int> roomList = new List<int>();

            if (start == finish)
            {
                return roomList;
            }
            else if (finish == 1)
            {
                roomList.Add(1);
                return roomList;
            }

            Queue<List<int>> paths = new Queue<List<int>>();
            List<int> tmpList;

            roomList.Add(start);
            paths.Enqueue(roomList);

            do
            {
                roomList = paths.Dequeue();
                int rm = roomList[roomList.Count - 1];

                foreach (int nbr in GetNeighbors(rm))
                {
                    if (!roomList.Contains(nbr))
                    {
                        tmpList = new List<int>(roomList);

                        tmpList.Add(nbr);

                        if (nbr == finish)
                        {
                            return tmpList;
                        }
                        else if (Shortest(tmpList.Count, nbr, paths))
                        {
                            paths.Enqueue(tmpList);
                        }
                    }
                }
            }
            while (paths.Count > 0);

            return null;
        }

        private static List<int> GetNeighbors(int id)
        {
            int tmp = FindRoom(id);
            if (tmp != -1 && Math.Abs(tmp) < mRooms.Count)
            {
                return mRooms[tmp].Neighbors;
            }
            else
            {
                return new List<int>();
            }
        }

        private static bool Shortest(int count, int endid, IEnumerable<List<int>> paths)
        {
            foreach (List<int> path in paths)
            {
                if (path[path.Count - 1] == endid && path.Count <= count)
                    return false;
            }

            return true;
        }

        private static void PathfindCallback(IAsyncResult ar)
        {
            PathfindHandler d = (PathfindHandler)ar.AsyncState;
            List<int> ret = d.EndInvoke(ar);
            mAllPaths.Add(ret);
        }

        public static int FindRoom(int find)
        {
            return new ArrayList(mRooms).BinarySearch(find);
        }

        public static bool Exists(int id)
        {
            int i = FindRoom(id);
            return i > -1 && i < mRooms.Count;
        }
    }
}