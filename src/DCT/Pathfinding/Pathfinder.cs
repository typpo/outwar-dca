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
        //private const string URL_ROOMS =
        //    "f5b57b6c048faa5bdc468e8caef8e0b3becc046212ae840dbadd4f30556b8a9337f2856bc94275ff7bee5d";
        //private const string KEY_ROOMS = "A6w_d0X_jQ";

        private const string URL_MOBS =
            "12cafa06a6d89aa2b3cce105d6febe6ea6c136ad6170168eda59f13ade129148c4faa510f56a95973980";
        private const string KEY_MOBS = "g8Z_k_3ok0";

        private const string URL_RAIDS =
            "52623494ddc5872f11e2e65ab83ee5479e30856af99ab338bd0be43fa25186d4f71378dbb9608b60ed9ac8";
        private const string KEY_RAIDS = "0QR3PT58t0";

        private static List<MappedRoom> mRooms;
        internal static List<MappedRoom> Rooms
        {
            get { return mRooms; }
        }

        private static List<MappedMob> mMobs;
        internal static List<MappedMob> Mobs
        {
            get { return mMobs; }
        }

        private static SortedList<string, int> mAdventures;
        internal static SortedList<string, int> Adventures
        {
            get { return mAdventures; }
        }

        private static List<List<int>> mAllPaths;

        internal static void BuildMap()
        {
            //f5b57b6c048faa5bdc468e8caef8e0b3becc046212ae840dbadd4f30556b8a9337f2856bc94275ff7bee5d
            //12cafa06a6d89aa2b3cce105d6febe6ea6c136ad6170168eda59f13ade129148c4faa510f56a95973980
            //52623494ddc5872f11e2e65ab83ee5479e30856af99ab338bd0be43fa25186d4f71378dbb9608b60ed9ac8
            //Console.WriteLine(Crypt.BinToHex(Crypt.Get("http://typpo.us/programs/dci/maps/roomq.php", KEY_ROOMS, false)));
            //Console.WriteLine(Crypt.BinToHex(Crypt.Get("http://typpo.us/programs/dci/maps/mobq.php", KEY_MOBS, false)));
            //Console.WriteLine(Crypt.BinToHex(Crypt.Get("http://typpo.us/programs/dci/maps/raidq.php", KEY_RAIDS, false)));

            Stack<int> stack = new Stack<int>(86);
            int bCrypt = 0;

            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x9C);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x51);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x50);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x0);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x3);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x2B);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0xD1);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0x0);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x53);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0xFE);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x5);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0xFE);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0xFB);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x5A);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x1);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x2C);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0xFF);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0xD);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0xFA);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0xCC);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0xD1);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x4);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x0);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0x6);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x58);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x59);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x5A);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x2C);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0xFF);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x0);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x5);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0x3);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x55);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x32);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x50);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x0);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x5);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x3);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x6);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x34);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x4);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0xC);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x5D);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x4);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x53);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x1);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0xFF);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0x4);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x2);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0xFC);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0x33);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0x0);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0xFE);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x3);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x51);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0xD1);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x52);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0x35);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x2D);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0xD2);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0xFF);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x4);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x2);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0xD5);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0x2D);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0xD3);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x2);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x2);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x57);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x7);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x2);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0xD3);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x54);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x0);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0x5);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x5E);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x4);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x4);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0x33);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x55);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x54);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0xD5);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x2);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0xD3);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x57);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0xCF);
            stack.Push(bCrypt);

            StringBuilder urlSb = new StringBuilder();
            while (stack.Count > 0)
            {
                urlSb.Append((char)stack.Pop());
            }

            stack = new Stack<int>(10);
            bCrypt = 0;

            bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0x51);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0x19);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0xB);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0xF9);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x28);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0x34);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x3B);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x28);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x41);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0xB);
            stack.Push(bCrypt);

            StringBuilder keySb = new StringBuilder();
            while (stack.Count > 0)
            {
                keySb.Append((char)stack.Pop());
            }

            string map = HttpSocket.DefaultInstance.Get(Crypt.Get(Crypt.HexToBin(urlSb.ToString()), keySb.ToString(), false));
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

        internal static string BadLinks()
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

        internal static List<int> CoverArea(int start)
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

        internal static List<int> GetSolution(int start, int finish, RoomHashRecord savedRooms)
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

        internal static int FindRoom(int find)
        {
            return new ArrayList(mRooms).BinarySearch(find);
        }

        internal static bool Exists(int id)
        {
            int i = FindRoom(id);
            return i > -1 && i < mRooms.Count;
        }
    }
}