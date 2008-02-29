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
using DCT.UI;

namespace DCT.Pathfinding
{
    internal static class Pathfinder
    {
        // q
        //private const string URL_ROOMS =
        //    "f5b57b6c048faa5bdc468e8caef8e0b3becc046212ae840dbadd4f30556b8a9337f2856bc94275ff7bee5d";
        //private const string KEY_ROOMS = "A6w_d0X_jQ";

        private const string URL_MOBS =
            "12cafa06a6d89aa2b3cce105d6efb42fb2dd2ae47c631cc7c20da361de1b934c98b8a90fe434d6883383744a9285";
        private const string KEY_MOBS = "g8Z_k_3ok0";

        private const string URL_RAIDS =
            "52623494ddc5872f11e2e65ab82fef068a2c9923e489b971a55fb664a25884d0ab516bcaa32b882ff496cb9a5d4c35";
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
            /*
            f5b57b6c048faa5bdc468e8caee9eaf2aad0182b0fbd8e44a2891d6b556288976bb09674d50076be64eb5ed78cb7ce
            12cafa06a6d89aa2b3cce105d6efb42fb2dd2ae47c631cc7c20da361de1b934c98b8a90fe434d6883383744a9285
            52623494ddc5872f11e2e65ab82fef068a2c9923e489b971a55fb664a25884d0ab516bcaa32b882ff496cb9a5d4c35
             */
            //Console.WriteLine(Crypt.BinToHex(Crypt.Get("http://typpo.dyndns.org:7012/dct/maps/rooms.php", KEY_ROOMS, false)));
            //Console.WriteLine(Crypt.BinToHex(Crypt.Get("http://typpo.dyndns.org:7012/dct/maps/mobs.php", KEY_MOBS, false)));
            //Console.WriteLine(Crypt.BinToHex(Crypt.Get("http://typpo.dyndns.org:7012/dct/maps/raids.php", KEY_RAIDS, false)));

            Stack<int> stack = new Stack<int>(94);
            int bCrypt = 0;

            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x65);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x6);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x2C);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0x2B);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0x1);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0xD5);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0xFF);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0x2D);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0xFF);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0xD0);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0xD3);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x7);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x51);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x2);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x53);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x7);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x2C);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x1);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0xF9);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0x0);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0x5);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0xD1);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0xD0);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0x3);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x1);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0x3);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0xF7);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0xCE);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x0);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x2C);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x1);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0xE);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x1);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x0);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0xA);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0xFC);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x1);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0x0);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x57);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0xD4);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0xD2);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x33);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x8);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0xFF);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0xFA);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0xD1);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x2D);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x0);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0xCF);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x5D);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x5C);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x2);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0xFC);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x36);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0x32);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0xD0);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0xFA);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x9);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0xFF);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0xCC);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x5);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x0);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0xD1);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0xCC);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0xFB);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0x4);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0xD4);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0xD4);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x0);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x4);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0x2);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0xD5);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x5D);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x2D);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x2);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0xFE);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0xD1);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0xFF);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x6);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x57);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0x2C);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x0);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0xFB);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x5E);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x4);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0xFC);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0x33);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x55);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0x2C);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x2B);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x2);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0x2D);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x2D);
            stack.Push(bCrypt);
            bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x53);
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

            string map;
            int i = 0;
            mRooms = new List<MappedRoom>();
            while (mRooms.Count < 1 && i < 2)
            {
                map = HttpSocket.DefaultInstance.Get(Crypt.Get(Crypt.HexToBin(urlSb.ToString()), keySb.ToString(), false));
                map = Crypt.Get(Crypt.HexToBin(map), HttpSocket.DefaultInstance.UserAgent, false);

                foreach (string token in map.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (string.IsNullOrEmpty(token.Trim()) || token.StartsWith("#"))
                        continue;
                    mRooms.Add(new MappedRoom(token));
                }
                i++;
            }
            mRooms.Sort();

            // ------------------

            i = 0;
            mMobs = new List<MappedMob>();
            while (mMobs.Count < 1 && i < 2)
            {
                map = HttpSocket.DefaultInstance.Get(Crypt.Get(Crypt.HexToBin(URL_MOBS), KEY_MOBS, false));
                map = Crypt.Get(Crypt.HexToBin(map), HttpSocket.DefaultInstance.UserAgent, false);

                foreach (string token in map.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (string.IsNullOrEmpty(token.Trim()) || token.StartsWith("#"))
                        continue;
                    mMobs.Add(new MappedMob(token));
                }
                i++;
            }
            mMobs.Sort();

            // -----------------
            
            i = 0;
            mAdventures = new SortedList<string, int>();
            while (mAdventures.Count < 1 && i < 2)
            {
                map = HttpSocket.DefaultInstance.Get(Crypt.Get(Crypt.HexToBin(URL_RAIDS), KEY_RAIDS, false));
                foreach (string token in map.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (string.IsNullOrEmpty(token.Trim()) || token.StartsWith("#"))
                        continue;
                    int j = token.IndexOf(";");
                    string name = token.Substring(0, j);
                    int id = int.Parse(token.Substring(j + 1));
                    mAdventures.Add(name, id);
                }
                i++;
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

        internal static List<int> CoverArea(int start, RoomHashRecord savedRooms)
        {
            List<int> idList = new List<int>();

            int idx = FindRoom(start);
            MappedRoom startRoom;
            if (idx > -1 && idx < mRooms.Count)
            {
                startRoom = mRooms[idx];
            }
            else
            {
                return null;
            }
            idList.Add(start);

            foreach (MappedRoom rm in mRooms)
            {
                if (rm != null && rm.Name.Equals(startRoom.Name))
                {
                    // TODO: reactivate?
                    //idList.Insert(Randomizer.Random.Next(idList.Count), rm.Id);
                    idList.Add(rm.Id);
                }
            }

            List<int> ret = new List<int>();
            ret = GetSolution(start, idList[0], savedRooms);
            if (ret == null)
            {
                return null;
            }

            for(int i = 1; i < idList.Count; i++)
            {
                List<int> tmp = GetPath(idList[i - 1], idList[i]);
                if(CoreUI.Instance.Settings.Fly)
                    tmp = savedRooms.Optimize(tmp);
                if (tmp != null)
                {
                    ret.AddRange(tmp);
                }
            }

            return ret;
        }

        internal static List<int> GetSolution(int start, int finish, RoomHashRecord savedRooms)
        {
            mAllPaths = new List<List<int>>();

            mAllPaths.Add(GetPath(start, finish));

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

            // remove starting room id
            if(bestPath.Count > 0)
                bestPath.RemoveAt(0);

            if (CoreUI.Instance.Settings.Fly)
                return savedRooms.Optimize(bestPath);
            else
                return bestPath;
        }

        private static Hashtable mShortest;
        private delegate List<int> PathfindHandler(int from, int to);
        private static List<int> GetPath(int start, int finish)
        {
            List<int> roomList = new List<int>();

            if (start == finish)
            {
                return roomList;
            }

            Queue<List<int>> paths = new Queue<List<int>>();

            roomList.Add(start);
            paths.Enqueue(roomList);

            mShortest = new Hashtable();

            // BFS search
            do
            {
                roomList = paths.Dequeue();
                int rm = roomList[roomList.Count - 1];

                foreach (int nbr in GetNeighbors(rm))
                {
                    if (roomList.Contains(nbr))
                    {
                        continue;
                    }

                    List<int> tmpList = new List<int>(roomList);
                    tmpList.Add(nbr);

                    if (nbr == finish)
                    {
                        return tmpList;
                    }
                    else if(Shortest(tmpList))
                    {
                        paths.Enqueue(tmpList);
                    }
                }
            }
            while (paths.Count > 0);

            return null;
        }

        private static bool Shortest(List<int> path)
        {
            int last = path[path.Count - 1];
            if (!mShortest.Contains(last))
            {
                mShortest.Add(last, path.Count);
                return true;
            }
            if ((int)mShortest[last] > path.Count)
            {
                mShortest[last] = path.Count;
                return true;
            }
            return false;
        }

        private static List<int> GetNeighbors(int id)
        {
            int tmp = FindRoom(id);
            if (tmp > -1 && tmp < mRooms.Count)
            {
                return mRooms[tmp].Neighbors;
            }
            else
            {
                return new List<int>();
            }
        }

        private static void PathfindCallback(IAsyncResult ar)
        {
            PathfindHandler d = (PathfindHandler)ar.AsyncState;
            List<int> ret = d.EndInvoke(ar);
            mAllPaths.Add(ret);
        }

        internal static int FindRoom(int find)
        {
            return mRooms.BinarySearch(new MappedRoom(find));
        }

        internal static bool Exists(int id)
        {
            return FindRoom(id) > -1;
        }
    }
}