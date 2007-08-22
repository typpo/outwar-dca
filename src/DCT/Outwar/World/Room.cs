using System.Collections.Generic;
using DCT.Parsing;
using DCT.Settings;
using DCT.Threading;
using DCT.UI;
using DCT.Util;

namespace DCT.Outwar.World
{
    internal class Room
    {
        private Mover mMover;
        internal Mover Mover
        {
            get { return mMover; }
        }
        internal int Id
        {
            get { return mId; }
        }
        internal string Name
        {
            get { return mName; }
            set { mName = value; }
        }
        internal bool Trained
        {
            get { return mTrained; }
        }
        private int mId;
        private string mUrl;
        private string mName;
        // TODO this should not be here
        private bool mTrained;
        private string mSource;
        private List<string> mLinks;
        private List<Mob> mMobs;

        internal Room(Mover mover, string url)
        {
            if (url != "world.php")
            {
                string tmp = Parser.Parse(url, "id=", "&");
                if (tmp != Parser.ERR_CONST)
                {
                    int.TryParse(tmp, out mId);
                }
                else if (url == "world.php?room=1")
                {
                    mId = 1;
                }
            }
            mUrl = url;
            mMover = mover;

            mTrained = false;
            mLinks = new List<string>();
        }

        internal bool Load()
        {
            // load source from url
            mSource = mMover.Socket.Get(mUrl);

            Parser p = new Parser(mSource);
            if (mSource.Contains("Error #301"))
            {
                return false;
            }
            else
            {
                if (mId == 0)
                {
                    string tmp = Parser.Parse(mSource, "dcmap/room_", ".");
                    if (!int.TryParse(tmp, out mId))
                    {
                        return false;
                    }
                }
                mName = p.Parse("'font-size:9pt;color:black'><b>- ", " -");
            }

            EnumRooms();

            if (Globals.AttackMode)
            {
                EnumMobs();
                EnumItems();
            }
            else if (UserEditable.AutoTrain || UserEditable.AutoQuest || UserEditable.AlertQuests)
            {
                EnumMobs();

                if (UserEditable.AutoTrain)
                    mTrained = Train();
                if (UserEditable.AutoQuest || UserEditable.AlertQuests)
                    Quest();
            }

            return true;
        }

        /// <summary>
        /// Returns the URL for a connecting room, given an id#
        /// </summary>
        /// <param name="id">Room id to get connecting link to</param>
        /// <returns>A URL; null if no such link exists</returns>
        internal string this[int id]
        {
            get
            {
                foreach (string url in mLinks)
                {
                    if (url.Contains("room=" + id + "&"))
                    {
                        return url;
                    }
                }

                return null;
            }
        }

        internal void EnumItems()
        {
            //if (SettingsContainer.AttackOn || SettingsContainer.AttackMode)
            //{
            //    mAccount.ItemManager.numItems(mSource);

            //    ThreadEngine.DefaultInstance.DoNonBlocking(mAccount.ItemManager.manage);
            //}
        }

        internal void EnumMobs()
        {
            // TODO this should really just throw an exception
            if (string.IsNullOrEmpty(mSource))
            {
                Load();
            }
            mMobs = new List<Mob>();

            foreach (string s in Parser.MultiParse(mSource, "<div style=\"border-bottom:1px solid black;\"", "</div>"))
            {
                Parser p = new Parser(s);

                string url = "mob.php?" + p.Parse("mob.php?", "\"");
                string name = Parser.Parse(Parser.CutLeading(s, url + "\">"), "\">", " [");
                string attackurl = string.Empty;
                bool trainer = false;
                bool quest = false;

                if (s.Contains("mobattack.php"))
                {
                    attackurl = "mobattack.php" + p.Parse("mobattack.php", "\"");
                }
                if (s.Contains("Talk to"))
                {
                    quest = true;
                }
                if (s.Contains("dc_trainer.gif"))
                {
                    trainer = true;
                }

                if (s.Contains("rareicon.jpg"))
                {   
                    // spawn mob
                    name = Parser.Parse(name, "\"#00FF00\">*", " (");
                }

                if (s.Contains("raidz") || (attackurl == string.Empty && !quest && !trainer))
                {
                    continue;
                }

                Mob mb = new Mob(name, url, attackurl, quest, trainer, this);
                mMobs.Add(mb);
            }
        }

        internal void EnumRooms()
        {
            // TODO just parse all world links...
            string[] tokens = Parser.MultiParse(mSource, "<a href=\"", "\"");
            for (int i = 0; i < tokens.Length; i++)
            {
                string url = tokens[i].Replace("\">", "").Replace(" ", "").Replace("/", "").ToLower().Trim();
                if (url.StartsWith("world.php") && !mLinks.Contains(url))
                {
                    mLinks.Add(url);
                }
            }
        }

        internal void Attack()
        {
            if (mMobs == null)
            {
                EnumMobs();
            }

            CoreUI.Instance.UpdateProgressbar(0, 0);
            for (int i = 0; i < mMobs.Count; i++)
            {
                Mob mob = mMobs[i];
                if (!Globals.AttackOn || !Globals.AttackMode)
                {
                    return;
                }
                mMover.Socket.Status = "Attacking " + i + "/" + mMobs.Count;

                if (mob.Attack() && UserEditable.Delay != 0)
                {
                    int delay = UserEditable.Delay
                                +
                                (UserEditable.Variance
                                     ? (UserEditable.Delay/100)*Randomizer.Random.Next(51)*Randomizer.RandomPosNeg()
                                     : 0);

                    CoreUI.Instance.Log("Waiting for delay: " + delay + " ms");
                    ThreadEngine.Sleep(delay);

                    CoreUI.Instance.UpdateProgressbar(i, mMobs.Count);
                }
            }

            CoreUI.Instance.Log("Waiting for Outwar to respond...");
            for (int i = 0; i < mMobs.Count; i++)
            {
                Mob mob = mMobs[i];
                while (mob.Attacking)
                {
                    ThreadEngine.Sleep(10);
                }
                CoreUI.Instance.UpdateProgressbar(i, mMobs.Count);
            }
        }

        internal void AttackMob(int id)
        {
            foreach (Mob mb in mMobs)
            {
                if (mb.Id == id)
                {
                    mb.Attack(false);
                }
            }
        }

        internal bool Train()
        {
            mMover.Socket.Status = "Looking up trainers";
            foreach (Mob mb in mMobs)
            {
                if (mb.IsTrainer)
                {
                    mb.Train();
                    mMover.Socket.Status = "Trained";
                    return true;
                }
            }
            mMover.Socket.Status = "Idle";
            return false;
        }

        internal void Quest()
        {
            mMover.Socket.Status = "Looking up quests";
            foreach (Mob mb in mMobs)
            {
                if (mb.IsTalkable)
                {
                    mb.Talk();
                }
            }
            mMover.Socket.Status = "Idle";
        }
    }
}