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
        internal Mover Mover { get; private set; }
        private int mId;
        internal int Id
        {
            get { return mId; }
        }
        internal string Name { get; set; }
        internal bool Trained { get; private set; }
        internal string Url { get; private set; }
        // TODO this should not be here
        private string mSource;
        internal SortedList<int, string> Links { get; private set; }
        internal List<Mob> Mobs { get; private set; }

        internal Room(Mover mover, string url)
        {
            if (url != "world.php")
            {
                string tmp = Parser.Parse(url, "id=", "&");
                if (tmp != Parser.ERR_CONST)
                {
                    int.TryParse(tmp, out mId);
                }
            }
            Url = url;
            Mover = mover;

            Trained = false;
            Links = new SortedList<int, string>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>0 on success, 1 on hash error, 2 on key, 3 on anything else</returns>
        internal int Load()
        {
            // load source from url
            mSource = Mover.Socket.Get(Url);

            Parser p = new Parser(mSource);
            if (mSource.Contains("Error #301"))
            {
                // hash error
                return 1;
            }
            else if (mSource.Contains("you must be carrying") || mSource.Contains("cast on you to enter this room."))
            {
                // need a key
                return 2;
            }
            else if (mSource.Contains("Rampid Gaming Login"))
            {
                // logged out
                return 4;
            }
            else
            {
                if (mId == 0)
                {
                    string tmp = Parser.Parse(mSource, "&lastroom=", "'");
                    if (!int.TryParse(tmp, out mId))
                    {
                        return 3;
                    }
                }
                Name = p.Parse("'font-size:9pt;color:black'><b>- ", " -");
            }

            EnumRooms();

            if (Globals.AttackMode)
            {
                EnumMobs();
            }
            else if (CoreUI.Instance.Settings.AutoTrain || CoreUI.Instance.Settings.AutoQuest || CoreUI.Instance.Settings.AlertQuests)
            {
                EnumMobs();

                if (CoreUI.Instance.Settings.AutoTrain)
                    Trained = Train();
                if (CoreUI.Instance.Settings.AutoQuest || CoreUI.Instance.Settings.AlertQuests)
                    Quest();
            }

            return 0;
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
                if (Links.ContainsKey(id))
                    return Links[id];
                return null;
            }
        }

        internal void EnumMobs()
        {
            // TODO this should really just throw an exception
            if (string.IsNullOrEmpty(mSource))
            {
                Load();
            }
            Mobs = new List<Mob>();

            foreach (string s in Parser.MultiParse(mSource, "<div style=\"border-bottom:1px solid black;\"", "</div>"))
            {
                Parser p = new Parser(s);

                string url = "mob.php?" + p.Parse("mob.php?", "\"");
                string name;
                string attackurl = string.Empty;
                bool trainer = false;
                bool quest = false;
                bool spawn = false;

                if (s.Contains("Spawned by"))
                {
                    name = string.Format("*{0}*", p.Parse("\">*", " ["));
                    if (name.Contains("<"))
                    {
                        // TODO this is a bandaid fix re: a bug with the parser.  It will pick up html from killed spawn mobs
                        continue;
                    }
                    spawn = true;
                    // log spawn sighting, but don't attack it if we shouldn't
                    if (Globals.AttackOn)
                    {
                        CoreUI.Instance.SpawnsPanel.Log(string.Format("{0} sighted {1} in room {2}", Mover.Account.Name, name, mId));
                        CoreUI.Instance.SpawnsPanel.Sighted(mId);
                        if (!CoreUI.Instance.Settings.AttackSpawns)
                            continue;
                    }
                }
                else
                {
                    name = Parser.Parse(Parser.CutLeading(s, url + "\">"), "\">", " [");
                }

                if (s.Contains("newattack.php"))
                {
                    attackurl = "newattack.php" + p.Parse("newattack.php", "\"");
                }
                if (s.Contains("Talk to"))
                {
                    quest = true;
                }
                if (s.Contains("dc_trainer.gif"))
                {
                    trainer = true;
                }

                if (s.Contains("raidz") && s.Contains("Form new raid"))
                {
                    // create raidformmob
                    //string formurl = "formraid.php" + Parser.Parse(s, "formraid.php", "\"");
                    //mRaid = new RaidFormMob(name, url, formurl, this);
                    // TODO: logic could be nicer
                    continue;
                }
                //else
                //{
                //    mRaid = null;
                //}

                if (string.IsNullOrEmpty(attackurl) && !quest && !trainer)
                {
                    continue;
                }

                Mobs.Add(new Mob(name, url, attackurl, quest, trainer, spawn, this));
            }
        }

        internal void EnumRooms()
        {
            // TODO just parse all world links...
            string[] tokens = Parser.MultiParse(mSource, "<a href=\"", "\"");
            string url;
            int id;
            for (int i = 0; i < tokens.Length; i++)
            {
                url = tokens[i].Replace("\">", "").Replace(" ", "").Replace("/", "").ToLower().Trim();
                if (url.StartsWith("world.php"))
                {
                    if (!int.TryParse(Parser.Parse(url, "room=", "&"), out id))
                        continue;
                    if (!Links.ContainsKey(id))
                        Links.Add(id, url);
                }
            }
        }
        
        /// <summary>
        /// Attack all the mobs in this room
        /// </summary>
        internal void Attack()
        {
            if (Mobs == null)
            {
                EnumMobs();
            }
            if (Mobs.Count < 1)
            {
                return;
            }

            for (int i = 0; i < Mobs.Count; i++)
            {
                Mob mob = Mobs[i];
                if (!Globals.AttackOn || !Globals.AttackMode)
                {
                    return;
                }

                if (mob.Attack() && CoreUI.Instance.Settings.Delay != 0)
                {
                    int delay = CoreUI.Instance.Settings.Delay
                                +
                                (CoreUI.Instance.Settings.Variance
                                     ? (CoreUI.Instance.Settings.Delay/100)*Randomizer.Random.Next(51)*Randomizer.RandomPosNeg()
                                     : 0);

                    CoreUI.Instance.LogPanel.Log("Waiting for delay: " + delay + " ms");
                    ThreadEngine.Sleep(delay);
                }
            }
            // TODO should be done with callback
            CoreUI.Instance.LogPanel.Log("Waiting for Outwar to respond...");
            for (int i = 0; i < Mobs.Count; i++)
            {
                Mob mob = Mobs[i];
                while (mob.Attacking)
                {
                    ThreadEngine.Sleep(10);
                }
            }
        }

        internal void AttackMob(int id)
        {
            if (Mobs == null || Mobs.Count < 1)
            {
                return;
            }

            // TODO mMobs should be in a dictionary by id
            foreach (Mob mb in Mobs)
            {
                if (mb.Id == id)
                {
                    AttackMob(mb);
                    return;
                }
            }
        }

        internal void AttackMob(string name)
        {
            foreach (Mob mb in Mobs)
            {
                if (mb.Name == name)
                {
                    AttackMob(mb);
                    return;
                }
            }
        }

        internal void AttackSpawns()
        {
            foreach (Mob mb in Mobs)
            {
                if (mb.IsSpawn)
                {
                    AttackMob(mb);
                    return;
                }
            }
        }

        private static void AttackMob(Mob mb)
        {
            if (mb.Attack(false) && CoreUI.Instance.Settings.Delay != 0)
            {
                int delay = CoreUI.Instance.Settings.Delay
                    +
                    (CoreUI.Instance.Settings.Variance
                         ? (CoreUI.Instance.Settings.Delay / 100) * Randomizer.Random.Next(51) * Randomizer.RandomPosNeg()
                         : 0);

                CoreUI.Instance.LogPanel.Log("Waiting for delay: " + delay + " ms");
                ThreadEngine.Sleep(delay);
            }
        }

        internal bool Train()
        {
            if (Mobs == null || Mobs.Count < 1)
            {
                return false;
            }

            foreach (Mob mb in Mobs)
            {
                if (mb.IsTrainer)
                {
                    mb.Train();
                    return true;
                }
            }
            return false;
        }

        internal void Quest()
        {
            foreach (Mob mb in Mobs)
            {
                if (mb.IsTalkable)
                {
                    mb.Talk();
                }
            }
        }
    }
}