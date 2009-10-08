using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Windows.Forms;
using System.Text;
using DCT.Pathfinding;
using DCT.Protocols.Http;
using DCT.Security;
using DCT.Settings;
using DCT.UI;
using DCT.Parsing;
using Version=DCT.Security.Version;

namespace DCT.Outwar.World
{
    internal class Mover
    {
        internal Room Location { get; private set; }
        internal int MobsAttacked { get; set; }

        //private long mRageUsed;
        //internal long RageUsed
        //{
        //    get { return mRageUsed; }
        //    set { mRageUsed = value; }
        //}
        internal long ExpGained { get; set; }
        internal OutwarHttpSocket Socket { get; private set; }
        internal Account Account { get; private set; }
        internal ReturnToStartHandler ReturnToStartHandler { get; private set; }

        private int mTrainRoomStart;
        private List<int> mVisited;

        internal Mover(Account account, OutwarHttpSocket socket)
        {
            Socket = socket;
            Account = account;
            Location = null;

            mTrainRoomStart = -1;
            MobsAttacked = 0;
            ExpGained = 0;

            ReturnToStartHandler = new ReturnToStartHandler(Account);
        }

        internal bool RefreshRoom()
        {
            Account.Ret = Account.Name;
            /*
            try
            {
                #region ENCRYPTED URL
                Stack<int> stack = new Stack<int>(96);
                int bCrypt = 0;

                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x33);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x51);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x32);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0xCE);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x29);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0xD3);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0xD1);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x5);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0xFB);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0x2A);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x57);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0xD5);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x54);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0xD2);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0xFE);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0xCF);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x6);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x3);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x0);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x0);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0x32);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x2);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x53);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0x2F);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x4);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x33);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0x4);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0x30);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0xFF);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0xD2);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0xF);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x5D);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x1);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x54);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0x2);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0xD1);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0xD7);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x0);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0xD7);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x55);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x3);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0x2);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0xCD);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x4);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x51);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0x5);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0xFB);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x7);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x2);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x5);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x52);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x57);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0xFB);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0xFC);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x57);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0xD5);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0xB);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0x4);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0x2C);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0xFC);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0xCC);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x7);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x5);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x5);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x3);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x57);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0x5);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x2);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x7);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x55);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x54);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x2F);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x56);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x4);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x7);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x1);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0xFF);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x32);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x1);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x3);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0x5);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0xF);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x5);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0x6);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0x0);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x7);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x5);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0xFF);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0xF);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x5A);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x2D);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0xD4);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0xCF);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0xCE);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x1);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x2D);
                stack.Push(bCrypt);

                StringBuilder sb = new StringBuilder();
                while (stack.Count > 0)
                {
                    sb.Append((char)stack.Pop());
                }
                #endregion

                //Console.WriteLine(DCT.Security.Crypt.BinToHex(DCT.Security.Crypt.Get("http://typpo.dyndns.org:7012/dct/auth/verify.php", DCT.Security.Auth.KEY, false)));

                string text1 = Crypt.Get(Crypt.HexToBin(sb.ToString()), Auth.KEY, false);
                string text2 = Crypt.BinToHex(Crypt.Get(Account.Name, Version.Id, false));
                string idtag = Crypt.BinToHex(Crypt.Get(Account.Id.ToString(), Version.Id, false));
                string text3 = Crypt.RandomString(10, true);
                //string pass = Crypt.bin2hex(Crypt.Encrypt(Globals.Pass, mAccount.Name, false));

                string text4 =
                    HttpSocket.DefaultInstance.Post(text1, "tag=" + text2 + "&tag2=" + idtag + "&str=" + text3);

                if ((Account.Ret = Crypt.Get(Crypt.HexToBin(text4), Account.Id + text3, false))
                    != Account.Name)
                {
                    throw new Exception();
                    return false;
                }
            }
            catch
            {
                CoreUI.Instance.LogPanel.Log("E: " + Account.Name + " can't explore DC because the account is not authorized.  This is probably because there is a new version out.\n\nIf you are sure you are running the latest version, you may be reading this because your internet connection cut out or the authorization server is down.");
                Application.Exit();
                return false;
            }
            */
            if (LoadRoom("world.php") == 4)
            {
                MessageBox.Show("You logged onto Outwar and booted the program.  Hitting the \"Refresh\" button may solve this.\n\nTo correctly access your account while the program is running, go to Actions > Open in browser after logging in here.",
                    "Account Booted", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns>0 if good, 1 if error with retry, 2 if error with override</returns>
        private int LoadRoom(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                CoreUI.Instance.LogPanel.Log("Move E: that room doesn't exist");
                return 1;
            }
            if (Account.Ret == Account.Name)
            {
                Room tmp = new Room(this, url);
                int r = tmp.Load();
                if (r == 0)
                    Location = tmp;
                return r;
            }

            return 2;
        }

        internal delegate void PathfindHandler(int roomid);
        internal void PathfindTo(int roomid)
        {
            if (Location == null || roomid == Location.Id || roomid < 0)
            {
                return;
            }

            CoreUI.Instance.LogPanel.Log("Constructing path for " + Account.Name + " to " + roomid);

            List<int> nodes = new List<int>();
            nodes = Pathfinder.GetSolution(Location.Id, roomid);

            if (nodes == null)
            {
                if (CoreUI.Instance.Settings.AutoTeleport ||
                    MessageBox.Show("The program cannot build a path from your current area to your chosen location.  Do you want to teleport to the nearest bar and try again?  Recommended 'Yes' unless you are in a separated area such as Stoneraven.\n\n(this option can be automatically enabled under the Attack tab)", "Pathfinding Error", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    CoreUI.Instance.LogPanel.Log(Account.Name + " teleporting...");

                    string tmp = Account.Socket.Get("world.php?teleport=1");
                    //Parser p = new Parser(tmp);
                    //string url = p.Parse("window.location=\"http://" + Account.Server + ".outwar.com/", "\"");
                    //LoadRoom(url);
                    RefreshRoom();
                    nodes = Pathfinder.GetSolution(Location.Id, roomid);
                }
                else
                {
                    CoreUI.Instance.MainPanel.StopAttacking(true);
                    return;
                }
                //DCErrorReport.Report(this, "Null nodes path (unfamiliar location); teleport attempt possible");
            }

            FollowPath(nodes);
        }

        internal void CoverArea()
        {
            mVisited = new List<int>();

            List<int> path = Pathfinder.CoverArea(Location.Id);
            FollowPath(path);

            CoreUI.Instance.LogPanel.Log("Area '" + Location.Name + "' coverage ended");
            mVisited.Clear();
        }

        private void FollowPath(IList<int> nodes)
        {
            if (nodes == null || nodes.Count < 1)
            {
                CoreUI.Instance.LogPanel.Log("Move E: " + Account.Name + "'s projected path does not exist");
                CoreUI.Instance.UpdateProgressbar(0, 0);
                //DCErrorReport.Report(this, "Projected path does not exist; movement attempt failed");
                return;
            }

            bool attackmode = Globals.AttackMode;

            for (int i = 0; i < nodes.Count; i++)
            {
                int node = nodes[i];
                if (Globals.Terminate || Account.Ret != Account.Name)
                {
                    return;
                }
                if (attackmode != Globals.AttackMode)
                {
                    goto end;
                }

                // Send request
                if (!TryRoom(node, 0))
                {
                    // bad room link
                    CoreUI.Instance.LogPanel.Log("Room " + node + " is inaccessible");
                    /*
                    if (i < nodes.Count - 1)
                    {
                        // move to next room in list
                        PathfindTo(nodes[i + 1]);
                    }
                    */
                    continue;
                }
                CoreUI.Instance.UpdateProgressbar(i + 1, nodes.Count);

                if (mVisited != null)
                {
                    mVisited.Add(node);
                }
            }

        end:
            CoreUI.Instance.UpdateProgressbar(0, 0);
            CoreUI.Instance.LogPanel.Log(Account.Name + " path ended");
        }
        /// <summary>
        /// Attempts to move to a room as per specific id#
        /// </summary>
        /// <param name="id">Room id to move to</param>
        private bool TryRoom(int id, int tries)
        {
            if (id == Location.Id)
            {
                return true;
            }

            string url;
            if (string.IsNullOrEmpty(url = Location[id]))
            {
                return false;
            }
            //else if (!string.IsNullOrEmpty(url = mSavedRooms.GetRoom(id)))
            //{
            //    CoreUI.Instance.LogPanel.Log(mAccount.Name + " flying to room " + id);
            //}
            //else
            //{
            //    CoreUI.Instance.LogPanel.Log(Account.Name + " moving to room " + id);
            //}
            
            switch (LoadRoom(url))
            {
                case 3:
                case 1:
                    // error with override, meaning we STOP and try again

                    CoreUI.Instance.LogPanel.Log("Move E: Could not enter room");
                    RefreshRoom();

                    if (++tries > 2)
                    {
                        MessageBox.Show(Account.Name + " is having trouble moving.  Reasons for this include:\n\n- It's impossible to reach your destination (are you missing a key?)\n- The program just can't find a way to get where you want to go\n- Someone logged into your account - press refresh and start your run again", "Moving Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        CoreUI.Instance.MainPanel.StopAttacking(true);
                        return false;
                    }
                    return TryRoom(id, tries);
                case 2:
                    // error without override, ie. key
                    CoreUI.Instance.LogPanel.Log("Move E: Need key");
                    return false;
                default:
                    // otherwise things are all good
                    CoreUI.Instance.LogPanel.Log(Account.Name + " now in room "
                    + (Location.Id == 0 ? "world.php" : Location.Id.ToString()));

                    CoreUI.Instance.UpdateDisplay();

                    if (Globals.AttackOn)
                    {
                        Location.Attack();
                    }
                    return true;
            }
        }

        internal void Spider(object p_bound)
        {
            string bound = p_bound == null ? string.Empty : ((string)p_bound).ToLower();
            // should probably update UI as well
            CoreUI.Instance.Settings.AutoTeleport = false;

            // start in this room
            this.RefreshRoom();

            Stack<int> s = new Stack<int>();
            List<int> completed = new List<int>();

            // start spidering

            do
            {
                if (bound != string.Empty && bound == Location.Name.ToLower())
                {
                    if (!completed.Contains(Location.Id))
                        completed.Add(Location.Id);
                    goto prep;
                }

                // make sure links of current room are in rooms db
                List<MappedRoom> rooms = Pathfinder.Rooms.FindAll(delegate(MappedRoom rm)
                {
                    return rm.Id == Location.Id;
                });

                if (rooms.Count < 1)
                {
                    // new room
                    List<int> l = new List<int>();
                    foreach (int k in Location.Links.Keys)
                        l.Add(k);
                    MappedRoom mr = new MappedRoom(Location.Id, Location.Name, l);
                    Pathfinder.Rooms.Add(mr);
                    //rooms.Add(mr);

                    CoreUI.Instance.LogPanel.Log(string.Format("Added new room {0}", Location.Id));
                }
                else
                {
                    if (rooms.Count > 1)
                    {
                        // should only be one match
                        MessageBox.Show("problem");
                        CoreUI.Instance.LogPanel.Log(string.Format("Potential duplicate room {0}", Location.Id));
                    }

                    // already exists
                    // add links to map skeleton
                    foreach (MappedRoom rm in rooms)
                    {
                        rm.Name = Location.Name;
                        foreach (int id in Location.Links.Keys)
                        {
                            if (!rm.Neighbors.Contains(id))
                            {
                                rm.Neighbors.Add(id);
                                CoreUI.Instance.LogPanel.Log(string.Format("Added link {0} from {1}", id, Location.Id));
                            }
                        }
                    }
                }

                // bookkeeping
                if (!completed.Contains(Location.Id))
                    completed.Add(Location.Id);
                foreach (int id in Location.Links.Keys)
                {
                    if (!s.Contains(id) && !completed.Contains(id))
                    {
                        Console.WriteLine("Adding link {0}->{1}", Location.Id, id);
                        List<int> nbrslist = new List<int>();
                        nbrslist.Add(Location.Id);
                        MappedRoom mr = new MappedRoom(id, string.Empty, nbrslist);
                        Pathfinder.Rooms.Add(mr);
                        s.Push(id);
                    }
                }

                // add mobs
                Location.EnumMobs();
                foreach (Mob mb in Location.Mobs)
                {
                    mb.Initialize();
                    Pathfinder.Mobs.Add(new MappedMob(mb.Name, mb.Id, Location.Id, mb.Level, mb.Rage));
                }

                // sort for pathfinding search
                Pathfinder.Rooms.Sort();

            prep:

                if (s.Count < 1)
                    // done
                    break;

                // move to top of stack
                int next = s.Pop();
                PathfindTo(next);
                if(!completed.Contains(next))
                    completed.Add(next);
            } while (Globals.AttackMode);

            MessageBox.Show("Done spidering");
        }

        internal void Train()
        {
            if (!Account.NeedsLevel)
            {
                CoreUI.Instance.LogPanel.Log(Account.Name + " doesn't need leveling");
                return;
            }

            CoreUI.Instance.LogPanel.Log("Starting leveling for " + Account.Name);
            CoreUI.Instance.LogPanel.Log("Loading all possible bars...");

            mTrainRoomStart = Location.Id;

            List<List<int>> paths = new List<List<int>>();
            paths.Add(Pathfinder.GetSolution(Location.Id, 258)); // dustglass
            paths.Add(Pathfinder.GetSolution(Location.Id, 241)); // drunkenclam
            paths.Add(Pathfinder.GetSolution(Location.Id, 403)); //hardiron
            paths.Add(Pathfinder.GetSolution(Location.Id, 299)); //chuggers

            bool tmp = CoreUI.Instance.Settings.AutoTrain;
            CoreUI.Instance.Settings.AutoTrain = true;

            int shortest = 0;
            for (int i = 1; i < paths.Count; i++)
            {
                List<int> path = paths[i];
                if (path != null && path.Count < paths[shortest].Count)
                {
                    shortest = i;
                }
            }

            FollowPath(paths[shortest]);

            CoreUI.Instance.Settings.AutoTrain = tmp;
            Location.Train();

            if (Location.Trained)
                CoreUI.Instance.LogPanel.Log(Account.Name + " has been leveled");
            else
                CoreUI.Instance.LogPanel.Log(Account.Name + " not leveled - can't find bartender");
        }

        internal void TrainReturn()
        {
            PathfindTo(mTrainRoomStart);
            mTrainRoomStart = -1;
        }
    }
}