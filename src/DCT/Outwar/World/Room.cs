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
        internal int Id { get; private set; }
        internal string Name { get; set; }
        internal bool Trained { get; private set; }
        // TODO this should not be here
        internal List<int> Links { get; private set; }
        internal List<Mob> Mobs { get; private set; }

        internal Room(Mover mover, int id)
        {
            Id = id;
            Mover = mover;

            Trained = false;
            Links = new List<int>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>0 on success, 1 on hash error, 2 on key, 3 on anything else</returns>
        internal int Load()
        {
            // create url
            string url = string.Format("ajax_changeroom.php?room={0}&lastroom={1}", Id, Mover.Location.Id);
            string src = Mover.Socket.Get(url);
            src = System.Text.RegularExpressions.Regex.Unescape(src);

            Parser p = new Parser(src);
            // TODO look how error messages changed
            if (src.Contains("Error #301"))
            {
                // hash error
                return 1;
            }
            if (src.Contains("you must be carrying") || src.Contains("cast on you to enter this room."))
            {
                // need a key
                return 2;
            }
            if (src.Contains("Rampid Gaming Login"))
            {
                // logged out
                return 4;
            }

            if (Id == 0)
            {
                // loading world.php; figure out where we are
                string tmp = Parser.Parse(src, "\"curRoom\":\"", "\"");
                int tmpid;
                if (!int.TryParse(tmp, out tmpid))
                {
                    return 3;
                }
                Id = tmpid;
            }
            Name = p.Parse("\"name\":\"", "\"");

            EnumRooms(src);

            if (Globals.AttackMode)
            {
                EnumMobs(src);
            }
            else if (CoreUI.Instance.Settings.AutoTrain || CoreUI.Instance.Settings.AutoQuest || CoreUI.Instance.Settings.AlertQuests)
            {
                EnumMobs(src);

                if (CoreUI.Instance.Settings.AutoTrain)
                    Trained = Train();
                if (CoreUI.Instance.Settings.AutoQuest || CoreUI.Instance.Settings.AlertQuests)
                    Quest();
            }

            return 0;
        }

        internal void EnumMobs(string src)
        {
            // TODO this should really just throw an exception
            if (string.IsNullOrEmpty(src))
            {
                Load();
            }
            Mobs = new List<Mob>();

            foreach (string s in Parser.MultiParse(src, "<div style=\"border-bottom:1px solid black;\"", "</div>"))
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
                        CoreUI.Instance.SpawnsPanel.Log(string.Format("{0} sighted {1} in room {2}", Mover.Account.Name, name, Id));
                        CoreUI.Instance.SpawnsPanel.Sighted(Id);
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

                if (string.IsNullOrEmpty(attackurl) && !quest && !trainer)
                {
                    continue;
                }

                Mobs.Add(new Mob(name, url, attackurl, quest, trainer, spawn, this));
            }
        }

        internal void EnumRooms(string src)
        {
            // TODO what about mysterious portal?
            int n, s, e, w;
            int.TryParse(Parser.Parse(src, "\"north\":\"", "\""), out n);
            int.TryParse(Parser.Parse(src, "\"south\":\"", "\""), out s);
            int.TryParse(Parser.Parse(src, "\"east\":\"", "\""), out e);
            int.TryParse(Parser.Parse(src, "\"west\":\"", "\""), out w);
            Links.Add(n);
            Links.Add(s);
            Links.Add(e);
            Links.Add(w);
        }
        
        /// <summary>
        /// Attack all the mobs in this room
        /// </summary>
        internal void Attack()
        {
            if (Mobs == null)
            {
                // TODO when does this happen?
                System.Windows.Forms.MessageBox.Show("Enum mobs has not occured before attack; report this error");
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