using System;
using System.Windows.Forms;
using DCT.Parsing;
using DCT.Pathfinding;
using DCT.Settings;
using DCT.UI;

namespace DCT.Outwar.World
{
    internal class Mob : Occupier
    {
        private bool mAttacked;
        private bool mInitialized;
        internal bool IsTrainer { get; private set; }
        internal bool IsTalkable { get; private set; }
        internal bool IsSpawn { get; private set; }
        internal bool Attacking { get; private set; }
        private bool mQuit;

        private bool FilterOK
        {
            get
            {
                if (!CoreUI.Instance.Settings.FilterMobs)
                {
                    return true;
                }


                // a mob should get through:
                // If there are only normal filters, then ONLY if it matches
                // If there are only ignore filters, then ONLY if it doesn't match
                // If there are normal and ignore filters, then ignore the ignore filters and let it through only if it matches

                bool ignorefilter = false;
                bool passedignore = true;
                bool normalfilter = false;
                bool passednormal = true;

                foreach (string str in CoreUI.Instance.Settings.MobFilters)
                {
                    if (str.StartsWith("!"))
                    {
                        ignorefilter = true;
                        if (mName.ToLower().Contains(str.Substring(1)))
                            passedignore = false;
                    }
                    else
                    {
                        normalfilter = true;
                        if (mName.ToLower().Contains(str))
                            return true;    // matches, so return true unconditionally
                        else
                            passednormal = false;
                    }
                }

                if (ignorefilter && normalfilter)
                {
                    return passednormal;
                }
                else if (ignorefilter)
                {
                    return passedignore;
                }
                else if (normalfilter)
                {
                    // this should never happen
                }

                return false;
            }
        }

        private int mExpGained;
        private long mRage, mLevel;

        private bool mSkipLoad;
        private string mAttackUrl;

        internal Mob(string name, string url, string attackurl, bool isQuest, bool isTrainer, bool isSpawn, Room room) : base(name, url, room)
        {
            mAttackUrl = attackurl;
            IsTalkable = isQuest;
            IsTrainer = isTrainer;
            IsSpawn = isSpawn;
        }

        internal void Initialize()
        {
            if (mInitialized || mRoom.Mover.Account.Ret != mRoom.Mover.Account.Name)
            {
                return;
            }

            CoreUI.Instance.LogPanel.Log("Loading '" + mName + "'");

            mLoadSrc = mRoom.Mover.Socket.Get(mURL);

            if (mQuit)
            {
                return;
            }

            // Parse level and rage
            Parser mm = new Parser(mLoadSrc);
            if (!long.TryParse(mm.Parse("(Level ", ")"), out mLevel)
                || !long.TryParse(mm.Parse("Attack!</a> (<b>", " rage"), out mRage))
            {
                mQuit = true;
                return;
            }

            mInitialized = true;
        }

        private bool TestRage(bool useRageLimit)
        {
            if (!IsInRoom)
            {
                return false;
            }

            MappedMob m = Pathfinder.Mobs.Find(PreeliminationPredicate);
            if (m != null)
            {
                mSkipLoad = true;

                if ((useRageLimit && CoreUI.Instance.Settings.RageLimit != 0 && m.Rage > CoreUI.Instance.Settings.RageLimit)
                    || m.Rage > mRoom.Mover.Account.Rage)
                {
                    return false;
                }
                else if (mRoom.Mover.Account.Rage < Math.Max(1, CoreUI.Instance.Settings.StopBelowRage))
                {
                    // TODO this all needs cleaning
                    // go to next account
                    CoreUI.Instance.LogPanel.Log(string.Format("Stopping attacks on {0}, reached rage limit", mRoom.Mover.Account.Name));
                    mQuit = true;
                    Globals.AttackOn = false;
                    return false;
                }
            }
            return true;
        }

        private bool TestLevel()
        {
            if (!IsInRoom)
            {
                return false;
            }

            MappedMob m = Pathfinder.Mobs.Find(PreeliminationPredicate);
            if (m != null)
            {
                mSkipLoad = true;

                if ((m.Level > CoreUI.Instance.Settings.LvlLimit && CoreUI.Instance.Settings.LvlLimit != 0)
                       || m.Level < CoreUI.Instance.Settings.LvlLimitMin)
                {
                    return false;
                }
            }
            return true;
        }


        private bool PreeliminationPredicate(MappedMob m)
        {
            return m.Name.Equals(mName);
        }

        internal void Talk()
        {
            string talk =
                mRoom.Mover.Socket.Get("mob_talk.php?id=" + mId + "&userspawn=");
            CoreUI.Instance.LogPanel.Log("Checking " + mName + "'s talk page in room " + mRoom.Mover.Location.Id + "...");

            if (talk.Contains("acceptquest="))
            {
                if (CoreUI.Instance.Settings.AlertQuests)
                {
                    if (
                        MessageBox.Show(
                            "Accept quest from " + mName + " in room " + mRoom.Mover.Location.Id + "?\n\n\""
                            +
                            talk.Substring(talk.IndexOf("<p>") + 3,
                                           talk.IndexOf("</p>") - talk.IndexOf("<p>") - 3).Trim() + "\"",
                            "Quest Mob", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
                        != DialogResult.Yes)
                    {
                        return;
                    }
                }

                CoreUI.Instance.LogPanel.Log("Accepting " + mName + "'s quest in room " + mRoom.Mover.Location.Id + "...");
                mRoom.Mover.Socket.Get("mob_talk.php?acceptquest="
                                       + Parser.Parse(talk, "mob_talk.php?acceptquest=", "\""));
                CoreUI.Instance.LogPanel.Log("Quest accepted");
            }
            else
            {
                CoreUI.Instance.LogPanel.Log("Already accepted " + mName + "'s task");
            }
        }

        internal void Train()
        {
            if (mRoom.Mover.Account.NeedsLevel)
            {
                CoreUI.Instance.LogPanel.Log("Leveling up " + mRoom.Mover.Account.Name + " automatically with bartender "
                                    + mName
                                    + "...");
                Initialize();
                mRoom.Mover.Socket.Get("mob_train.php?id=" + Parser.Parse(mLoadSrc, "mob_train.php?id=", "\""));
            }
        }

        private void GeneralTestAttack()
        {
            // for mob attacking too
            if (mRage > mRoom.Mover.Account.Rage)
            {
                mQuit = true;
                CoreUI.Instance.LogPanel.Log("You don't have enough rage to attack " + mName + " (" + mRage + " > "
                                    + mRoom.Mover.Account.Rage + ")");
            }
            else if (mRoom.Mover.Account.Rage < Math.Max(1, CoreUI.Instance.Settings.StopBelowRage))
            {
                // go to next account
                CoreUI.Instance.LogPanel.Log(string.Format("Stopping attacks on {0}, reached rage limit", mRoom.Mover.Account.Name));
                mQuit = true;
                Globals.AttackOn = false;

            }
                /*
            else if (mRoom.Mover.Account.Rage < CoreUI.Instance.Settings.StopBelowRage)
            {
                mQuit = true;
                CoreUI.Instance.LogPanel.Log("Not enough rage to attack " + mName + " with " + mRage
                                    + " and stay above rage quota");
            }
            else if (mRoom.Mover.Account.Rage < 1)
            {
                mQuit = true;
                CoreUI.Instance.LogPanel.Log("Can't attack " + mName + ", you're out of rage");
                Globals.AttackOn = false; // TODO: stop it
            }
                 * */
        }

        private void TestAttack()
        {
            // for not mob attacking
            if (mLevel > CoreUI.Instance.Settings.LvlLimit && CoreUI.Instance.Settings.LvlLimit != 0)
            {
                mQuit = true;
                CoreUI.Instance.LogPanel.Log(mName + "'s level is too high (" + mLevel + " > " + CoreUI.Instance.Settings.LvlLimit + ")");
            }
            else if (mLevel < CoreUI.Instance.Settings.LvlLimitMin)
            {
                mQuit = true;
                CoreUI.Instance.LogPanel.Log(mName + "'s level is too low (" + mLevel + " < " + CoreUI.Instance.Settings.LvlLimitMin
                                    + ")");
            }
            else if (mRage > CoreUI.Instance.Settings.RageLimit && CoreUI.Instance.Settings.RageLimit != 0 && !(IsSpawn && CoreUI.Instance.Settings.IgnoreSpawnRage))
            {
                mQuit = true;
                CoreUI.Instance.LogPanel.Log(mName + " requires too much rage (" + mRage + "), over the rage limit");
            }
        }


        internal bool Attack()
        {
            return Attack(true);
        }

        /// <summary>
        /// Attempts to start attack thread
        /// </summary>
        /// <param name="test">True if we should test mob stats for preelimination</param>
        /// <returns>True if thread started; False otherwise</returns>
        internal bool Attack(bool test)
        {
            if (mAttacked || (!FilterOK && test) || !IsInRoom
                || (!Globals.AttackOn || !Globals.AttackMode))
            {
                mQuit = true;
                return false;
            }
            if (IsTrainer && CoreUI.Instance.Settings.AutoTrain)
            {
                Train();
            }
            if (IsTalkable && (CoreUI.Instance.Settings.AutoQuest || CoreUI.Instance.Settings.AlertQuests))
            {
                Talk();
            }

            if (
                (test && (!TestLevel() || !FilterOK || !TestRage(true)))
                || (!test && !TestRage(false))
                )
            {
                CoreUI.Instance.LogPanel.Log(mName + " does not meet specifications.");
                mQuit = true;
                return false;
            }

            if (mSkipLoad)
            {
                SendAttack();
                return true;
            }

            Initialize();
            if (mQuit)
            {
                return false;
            }

            GeneralTestAttack();
            if (test)
            {
                TestAttack();
            }

            if (mQuit)
            {
                return false;
            }

            // Launch attack thread
            MethodInvoker d = new MethodInvoker(SendAttack);
            d.BeginInvoke(new AsyncCallback(AttackCallback), d);

            return true;
        }

        private void SendAttack()
        {
            if (mQuit || !IsInRoom || !(Globals.AttackOn || Globals.AttackMode) || mRoom.Mover.Account.Ret != mRoom.Mover.Account.Name)
            {
                return;
            }

            Attacking = true;

            CoreUI.Instance.LogPanel.Log("Attacking " + mName + " (" + mId + ") in rm. " + mRoom.Id);

            if (!mSkipLoad)
            {
                mAttackUrl = "newattack.php" + new Parser(mLoadSrc).Parse("newattack.php", "\"");
            }
            EvaluateOutcome(mRoom.Mover.Socket.Get(mAttackUrl));

            Attacking = false;
        }

        private void AttackCallback(IAsyncResult ar)
        {
            MethodInvoker d = (MethodInvoker) ar.AsyncState;
            d.EndInvoke(ar);
        }

        //private string[] CreateRequestFromForm()
        //{
        //    Parser mm;
        //    try
        //    {
        //        mm =
        //            new Parser(
        //                mLoadSrc.Substring(mLoadSrc.IndexOf("<form"),
        //                                   mLoadSrc.IndexOf("</form>") - mLoadSrc.IndexOf("<form")));
        //    }
        //    catch (ArgumentOutOfRangeException)
        //    {
        //        mFinished = true;
        //        return null;
        //    }
        //    string[] ret = { mm.Parse("action=\"", "\""), "" };

        //    Parser tm;
        //    bool amp = false;

        //    string[] tokens = mm.MultiParse("<input type=\"", ">");

        //    for (int i = 0; i < tokens.Length - 1; i++)
        //    {
        //        string token = tokens[i];

        //        if (token.Contains("type=\"image\""))
        //            continue;

        //        tm = new Parser(token);

        //        if (amp)
        //            ret[1] += "&";
        //        else
        //            amp = true;

        //        ret[1] += tm.Parse("name=\"", "\"") + "=" + tm.Parse("value=\"", "\"");
        //    }

        //    return mRoom.Mover.Account.Ret == mRoom.Mover.Account.Name ? ret : null;
        //}

        private void EvaluateOutcome(string src)
        {
            // RESEND REQUEST
            if (src == "ERROR: Timeout"
                || src.Contains("operation has timed out")
                || mRoom.Mover.Account.Ret != mRoom.Mover.Account.Name)
            {
                CoreUI.Instance.LogPanel.Log("Attack on " + mName + " failed - timed out by server");
                SendAttack();
                return;
            }
            else if (src.Contains("an existing connection was forcibly closed by the remote host"))
            {
                CoreUI.Instance.LogPanel.Log("Attack on " + mName + " failed - connection forcibly closed by server");
                SendAttack();
                return;
            }
            else if (src.Contains("underlying connection was closed"))
            {
                CoreUI.Instance.LogPanel.Log("Attack on " + mName + " failed - underlying connection closed by server");
                SendAttack();
                return;
            }

            // ALL GOOD

            // bookkeeping
            mAttacked = true;
            mRoom.Mover.MobsAttacked++;

            // spawn handling and logging
            if (src.Contains("steps out of the shadows"))
            {
                CoreUI.Instance.SpawnsPanel.Log(string.Format("Spawned a mob in room {0}", mRoom.Id));

                if (CoreUI.Instance.Settings.AttackSpawns)
                {
                    // attack the spawn mob
                    CoreUI.Instance.SpawnsPanel.Log(string.Format("Attacking spawns in room {0}", mRoom.Id));
                    if (mRoom.Load() != 0)
                    {
                        CoreUI.Instance.SpawnsPanel.Log(string.Format("Room {0} reload failed", mRoom.Id));
                    }
                    else
                    {
                        mRoom.AttackSpawns();
                    }
                }
            }
            if (IsSpawn)
            {
                CoreUI.Instance.SpawnsPanel.Log(string.Format("Attacked {0}", Name));
            }

            // other outcome handling
            if (src.Contains("found a"))
            {
                string f = Parser.Parse(src, "found a ", "<br>");
                CoreUI.Instance.LogPanel.LogAttack(mRoom.Mover.Account.Name + (f.Length < 30 ? " found a " + f : " found an item"));
            }
            if (src.Contains("has gained "))
            {
                if (int.TryParse(Parser.Parse(src, "has gained ", " experience!"), out mExpGained))
                {
                    Globals.ExpGained += mExpGained;
                    mRoom.Mover.ExpGained += mExpGained;
                    CoreUI.Instance.LogPanel.LogAttack(string.Format("{0} beat {1}, gained {2} exp", mRoom.Mover.Account.Name, mName, mExpGained));
                }
            }
            else if (src.Contains("has weakened"))
            {
                CoreUI.Instance.LogPanel.LogAttack(string.Format("{0} lost to {1}", mRoom.Mover.Account.Name, mName));
            }
            else if (src.Contains("var battle_result"))
            {
                CoreUI.Instance.LogPanel.LogAttack(string.Format("{0} beat {1}", mRoom.Mover.Account.Name, mName));
            }
            else
            {
                string tmp;
                if (src.StartsWith("ERROR"))
                {
                    CoreUI.Instance.LogPanel.LogAttack("Attack E occurred in Connection: " + src);
                }
                else if ((tmp = Parser.Parse(src, "ERROR:</b></font> ", "<br>")) == "ERROR")
                {
                    CoreUI.Instance.LogPanel.LogAttack("Attack E: An unknown error occurred");
                }
                else
                {
                    CoreUI.Instance.LogPanel.LogAttack("Attack E (server-side): " + tmp);
                }
            }

            mQuit = true;
            CoreUI.Instance.UpdateDisplay();
        }
    }
}