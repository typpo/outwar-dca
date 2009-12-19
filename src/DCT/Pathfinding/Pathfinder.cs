using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
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
        private const string MUTEX_NAME = "DCT_PATHFINDER_MUTEX";

        internal static List<MappedRoom> Rooms { get; private set; }
        internal static List<MappedMob> Mobs { get; private set; }
        internal static SortedList<string, int> Adventures { get; private set; }
        internal static List<MappedMob> Spawns { get; private set; }

        private static List<List<int>> mAllPaths;

        internal static void BuildMap(object update)
        {
            BuildMap((bool)update);
        }

        internal static void BuildMap(bool update)
        {
            using (System.Threading.Mutex mutex = new System.Threading.Mutex(false, MUTEX_NAME))
            {
                if (!mutex.WaitOne(0, true))
                {
                    System.Windows.Forms.MessageBox.Show("Can't build multiple maps at once.", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    System.Windows.Forms.Application.Exit();
                    return;
                }
                else
                {
                    DoBuildMap(update);
                }
            }
        }

        private static void DoBuildMap(bool update)
        {
            Rooms = new List<MappedRoom>();
            Mobs = new List<MappedMob>();
            Spawns = new List<MappedMob>();
            Adventures = new SortedList<string, int>();

            string map;
            List<int> nbrs;
            string name;
            int id;
            string[] tmp;
            //*
            for (int i = 0; i < 2 && Rooms.Count < 1; i++)
            {
                if (!File.Exists("rooms.dat") || update)
                {
                    Download("rooms");
                }
                if ((map = ReadDecrypt("rooms")) == null)
                    continue;

                foreach (string token in map.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (string.IsNullOrEmpty(token.Trim()) || token.StartsWith("#"))
                        continue;

                    tmp = token.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    if (!int.TryParse(tmp[0], out id))
                    {
                        continue;
                    }
                    name = tmp[tmp.Length - 1];
                    nbrs = new List<int>();
                    for (int j = 1; j < tmp.Length - 1; j++)
                    {
                        nbrs.Add(int.Parse(tmp[j]));
                    }
                    if (nbrs.Count < 1)
                        continue;
                    Rooms.Add(new MappedRoom(id, name, nbrs));
                }
            }
            Rooms.Sort();
            //*/

            // ------------------

            MappedMob mm;
            string[] parts;
            for (int i = 0; i < 2 && Mobs.Count < 1; i++)
            {
                if (!File.Exists("mobs.dat") || update)
                {
                    Download("mobs");
                }
                if ((map = ReadDecrypt("mobs")) == null)
                    continue;
                foreach (string token in map.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (string.IsNullOrEmpty(token.Trim()) || token.StartsWith("#"))
                        continue;
                    parts = token.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length != 5)
                    {
                        // not good input
                        continue;
                    }
                    mm = new MappedMob(parts[0], long.Parse(parts[1]), int.Parse(parts[2]), long.Parse(parts[3]), long.Parse(parts[4]));
                    Mobs.Add(mm);
                }
                i++;
            }
            Mobs.Sort();

            // -----------------

            for (int i = 0; i < 2 && Adventures.Count < 1; i++)
            {
                if (!File.Exists("raids.dat") || update)
                {
                    Download("raids");
                }
                if ((map = ReadDecrypt("raids")) == null)
                    continue;

                foreach (string token in map.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (string.IsNullOrEmpty(token.Trim()) || token.StartsWith("#"))
                        continue;
                    int j = token.IndexOf(";");
                    name = token.Substring(0, j);
                    id = int.Parse(token.Substring(j + 1));
                    Adventures.Add(name, id);
                }
                i++;
            }

            // ------------------
            // Spawns

            for (int i = 0; i < 2 && Spawns.Count < 1; i++)
            {
                if (!File.Exists("spawns.dat") || update)
                {
                    Download("spawns");
                }
                if ((map = ReadDecrypt("spawns")) == null)
                    continue;
                foreach (string token in map.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (string.IsNullOrEmpty(token.Trim()) || token.StartsWith("#"))
                        continue;
                    parts = token.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length != 3)
                    {
                        // not good input
                        continue;
                    }
                    mm = new MappedMob(parts[0], -1, int.Parse(parts[2]), long.Parse(parts[1]), -1);
                    Spawns.Add(mm);
                }
                i++;
            }
            Spawns.Sort();
        }

        internal static void Download(string maptype)
        {
            WebClient client = new WebClient();
            client.Headers.Add("User-Agent", HttpSocket.DefaultInstance.UserAgent);
            try
            {
                client.DownloadFile("http://typpo.us/maps/" + maptype + ".php", maptype + ".dat");
                CoreUI.Instance.Settings.LastMapUpdate = DateTime.Now.ToUniversalTime();
            }
            catch (WebException)
            {
                // 
            }
        }

        internal static string ReadDecrypt(string maptype)
        {
            if (!File.Exists(maptype + ".dat"))
                return null;

            StreamReader sr = new StreamReader(maptype + ".dat");
            string text;
            try
            {
                text = sr.ReadToEnd();
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                sr.Close();
            }
            return Crypt.Get(Crypt.HexToBin(text), HttpSocket.DefaultInstance.UserAgent, false);
        }

        internal static string BadLinks()
        {
            StringBuilder sb = new StringBuilder();

            foreach (MappedRoom rm in Rooms)
            {
                foreach (int nbr in rm.Neighbors)
                {
                    if (FindRoom(nbr) == -1)
                    {
                        sb.Append("Room " + rm.Id + "/" + rm.Name + " invalid link to: " + nbr);
                    }
                }
            }

            foreach (MappedMob mb in Mobs)
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
            List<int> idList = new List<int>();

            int idx = FindRoom(start);
            MappedRoom startRoom;
            if (idx > -1 && idx < Rooms.Count)
            {
                startRoom = Rooms[idx];
            }
            else
            {
                return null;
            }
            idList.Add(start);

            foreach (MappedRoom rm in Rooms)
            {
                if (rm != null && rm.Name.Equals(startRoom.Name))
                {
                    idList.Add(rm.Id);
                }
            }

            List<int> ret = new List<int>();
            ret = GetSolution(start, idList[0]);
            if (ret == null)
            {
                return null;
            }

            for(int i = 1; i < idList.Count; i++)
            {
                List<int> tmp = GetPath(idList[i - 1], idList[i]);
                //if(CoreUI.Instance.Settings.Fly)
                //    tmp = savedRooms.Optimize(tmp);
                if (tmp != null)
                {
                    ret.AddRange(tmp);
                }
            }

            return ret;
        }

        internal static void Benchmark(object n)
        {
            long total = 0;
            int j = (int)n;
            Console.WriteLine("Running...");
            for (int i = 0; i < j; i++)
            {
                int a = 0, b = 0;
                while (!Exists(a = Randomizer.Random.Next(1, Rooms.Count)));
                while (!Exists(a = Randomizer.Random.Next(1, Rooms.Count)));

                DateTime startTime = DateTime.Now;
                GetPath(a, b);
                DateTime stopTime = DateTime.Now;
                TimeSpan duration = stopTime - startTime;
                total += duration.Milliseconds;
            }
            Console.WriteLine(string.Format("Ran {0} tests, average {1}", j, total / j));
        }

        internal static List<int> GetSolution(int start, int finish)
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
            if (bestPath != null && bestPath.Count > 0)
                bestPath.RemoveAt(0);

            //if (CoreUI.Instance.Settings.Fly)
            //    return savedRooms.Optimize(bestPath);
            //else
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

            // Best First Search
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
            if (tmp > -1)
            {
                return Rooms[tmp].Neighbors;
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

        /// <summary>
        /// Returns the index of a room with a given ID#
        /// </summary>
        /// <param name="find"></param>
        /// <returns></returns>
        internal static int FindRoom(int find)
        {
            return Rooms.BinarySearch(new MappedRoom(find, null, null));
        }

        internal static bool Exists(int id)
        {
            return FindRoom(id) > -1;
        }

        internal static void ExportRooms()
        {
            StringBuilder sb = new StringBuilder();
            foreach(MappedRoom rm in Rooms)
                sb.AppendFormat("{0}\n", rm.ToString());

            FileIO.SaveFileFromString("Export Rooms", "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
                                      "DCT Rooms " + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second, sb.ToString());
        }

        internal static void ExportMobs()
        {
            StringBuilder sb = new StringBuilder();
            foreach (MappedMob mb in Mobs)
                sb.AppendFormat("{0}\n", mb.ToString());
 
            FileIO.SaveFileFromString("Export Mobs", "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
                                      "DCT Mobs " + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second, sb.ToString());
        }
    }
}