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

        private bool mTrainer;
        internal bool IsTrainer
        {
            get { return mTrainer; }
        }
        private bool mTalkable;
        internal bool IsTalkable
        {
            get { return mTalkable; }
        }
        private bool mQuit;

        private bool mAttacking;
        internal bool Attacking
        {
            get { return mAttacking; }
        }

        private bool FilterOK
        {
            get
            {
                if (!UserEditable.FilterMobs)
                {
                    return true;
                }

                foreach (string mob in UserEditable.MobFilters)
                {
                    if (mName.ToLower().Contains(mob.ToLower()))
                        return true;
                }
                return false;
            }
        }

        private int mExpGained;
        private long mRage, mLevel;

        private bool mSkipLoad;
        private string mAttackUrl;

        internal Mob(string name, string url, string attackurl, bool isQuest, bool isTrainer, Room room) : base(name, url, room)
        {
            mAttackUrl = attackurl;
            mTalkable = isQuest;
            mTrainer = isTrainer;
        }

        internal void Initialize()
        {
            if (mInitialized || mRoom.Mover.Account.Ret != mRoom.Mover.Account.Name)
            {
                return;
            }

            CoreUI.Instance.Log("Loading '" + mName + "'");

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

        private bool TestPreelimination()
        {
            if (!IsInRoom || !FilterOK)
            {
                return false;
            }

            MappedMob m = Pathfinder.Mobs.Find(PreeliminationPredicate);
            if (m != null)
            {
                mSkipLoad = true;

                if ((m.Rage > UserEditable.RageLimit && UserEditable.RageLimit != 0)
                    || (m.Level > UserEditable.LvlLimit && UserEditable.LvlLimit != 0)
                    || m.Level < UserEditable.LvlLimitMin
                    || m.Rage > mRoom.Mover.Account.Rage
                    || mRoom.Mover.Account.Rage - m.Rage < UserEditable.StopAtRage)
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
            if (mTalkable && (UserEditable.AutoQuest || UserEditable.AlertQuests))
            {
                string talk =
                    mRoom.Mover.Socket.Get("mob_talk.php?id=" + mId + "&userspawn=");
                CoreUI.Instance.Log("Checking " + mName + "'s talk page in room " + mRoom.Mover.Location.Id + "...");

                if (talk.Contains("acceptquest="))
                {
                    if (UserEditable.AlertQuests)
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

                    CoreUI.Instance.Log("Accepting " + mName + "'s quest in room " + mRoom.Mover.Location.Id + "...");
                    mRoom.Mover.Socket.Get("mob_talk.php?acceptquest="
                                           + Parser.Parse(talk, "mob_talk.php?acceptquest=", "\""));
                    CoreUI.Instance.Log("Quest accepted");
                }
                else
                {
                    CoreUI.Instance.Log("Already accepted " + mName + "'s task");
                }
            }
        }

        internal void Train()
        {
            if (mTrainer && UserEditable.AutoTrain)
            {
                if (mRoom.Mover.Account.NeedsLevel)
                {
                    CoreUI.Instance.Log("Leveling up " + mRoom.Mover.Account.Name + " automatically with bartender "
                                        + mName
                                        + "...");
                    Initialize();
                    mRoom.Mover.Socket.Get("mob_train.php?id=" + Parser.Parse(mLoadSrc, "mob_train.php?id=", "\""));
                }
            }
        }

        private void GeneralTestAttack()
        {
            // for mob attacking too
            if (mRage > mRoom.Mover.Account.Rage)
            {
                mQuit = true;
                CoreUI.Instance.Log("You don't have enough rage to attack " + mName + " (" + mRage + " > "
                                    + mRoom.Mover.Account.Rage + ")");
            }
            else if (mRoom.Mover.Account.Rage < UserEditable.StopAtRage)
            {
                mQuit = true;
                CoreUI.Instance.Log("Not enough rage to attack " + mName + " with " + mRage
                                    + " and stay above rage quota");
            }
            else if (mRoom.Mover.Account.Rage < 1)
            {
                mQuit = true;
                CoreUI.Instance.Log("Can't attack " + mName + ", you're out of rage");
                Globals.AttackOn = false; // TODO: stop it
            }
        }

        private void TestAttack()
        {
            // for not mob attacking
            if (mLevel > UserEditable.LvlLimit && UserEditable.LvlLimit != 0)
            {
                mQuit = true;
                CoreUI.Instance.Log(mName + "'s level is too high (" + mLevel + " > " + UserEditable.LvlLimit + ")");
            }
            else if (mLevel < UserEditable.LvlLimitMin)
            {
                mQuit = true;
                CoreUI.Instance.Log(mName + "'s level is too low (" + mLevel + " < " + UserEditable.LvlLimitMin
                                    + ")");
            }
            else if (mRage > UserEditable.RageLimit && UserEditable.RageLimit != 0)
            {
                mQuit = true;
                CoreUI.Instance.Log(mName + " requires too much rage (" + mRage + "), over the rage limit");
            }
        }


        internal bool Attack()
        {
            return Attack(true);
        }

        internal bool Attack(bool test)
        {
            if (mAttacked || (!FilterOK && test) || !IsInRoom
                || (!Globals.AttackOn || !Globals.AttackMode))
            {
                mQuit = true;
                return false;
            }

            Train();
            Talk();

            if (!TestPreelimination() && test)
            {
                CoreUI.Instance.Log(mName + " preeliminated - does not meet specifications.");
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

            mAttacking = true;

            CoreUI.Instance.Log("Attacking " + mName + " (" + mId + ") in rm. " + mRoom.Id);

            if (!mSkipLoad)
            {
                mAttackUrl = "newattack.php" + new Parser(mLoadSrc).Parse("newattack.php", "\"");
            }
            EvaluateOutcome(mRoom.Mover.Socket.Get(mAttackUrl));

            mAttacking = false;
        }

        private void AttackCallback(IAsyncResult ar)
        {
            MethodInvoker d = (MethodInvoker) ar.AsyncState;
            d.EndInvoke(ar);
        }

/*
        private string[] CreateRequestFromForm()
        {
            Parser mm;
            try
            {
                mm =
                    new Parser(
                        mLoadSrc.Substring(mLoadSrc.IndexOf("<form"),
                                           mLoadSrc.IndexOf("</form>") - mLoadSrc.IndexOf("<form")));
            }
            catch (ArgumentOutOfRangeException)
            {
                mFinished = true;
                return null;
            }
            string[] ret = { mm.Parse("action=\"", "\""), "" };

            Parser tm;
            bool amp = false;

            string[] tokens = mm.MultiParse("<input type=\"", ">");

            for (int i = 0; i < tokens.Length - 1; i++)
            {
                string token = tokens[i];

                if (token.Contains("type=\"image\""))
                    continue;

                tm = new Parser(token);

                if (amp)
                    ret[1] += "&";
                else
                    amp = true;

                ret[1] += tm.Parse("name=\"", "\"") + "=" + tm.Parse("value=\"", "\"");
            }

            return mRoom.Mover.Account.Ret == mRoom.Mover.Account.Name ? ret : null;
        }
*/

        private void EvaluateOutcome(string src)
        {
            // RESEND REQUEST
            if (src == "ERROR: Timeout"
                || src.Contains("operation has timed out")
                || mRoom.Mover.Account.Ret != mRoom.Mover.Account.Name)
            {
                CoreUI.Instance.Log("Attack on " + mName + " failed - timed out by server");
                SendAttack();
                return;
            }
            else if (src.Contains("an existing connection was forcibly closed by the remote host"))
            {
                CoreUI.Instance.Log("Attack on " + mName + " failed - connection forcibly closed by server");
                SendAttack();
                return;
            }
            else if (src.Contains("underlying connection was closed"))
            {
                CoreUI.Instance.Log("Attack on " + mName + " failed - underlying connection closed by server");
                SendAttack();
                return;
            }

            // ALL GOOD
            mAttacked = true;
            mRoom.Mover.MobsAttacked++;
            Globals.SecRight++;

            if (src.Contains("found a"))
            {
                string f = Parser.Parse(src, "found a ", "<br>");
                CoreUI.Instance.LogAttack(mRoom.Mover.Account.Name + " found a " + f);
            }
            if (src.Contains("has gained "))
            {
                if (int.TryParse(Parser.Parse(src, "has gained ", " experience!"), out mExpGained))
                {
                    Globals.ExpGained += mExpGained;
                    mRoom.Mover.ExpGained += mExpGained;
                    CoreUI.Instance.LogAttack(mRoom.Mover.Account.Name + " beat " + mName + ", gained " + mExpGained + " exp");
                }
            }
            else if (src.Contains("var battle_result"))
            {
                CoreUI.Instance.LogAttack(mRoom.Mover.Account.Name + " attacked " + mName);
            }
            //if (src.Contains("You've <font color=\"#CC0000\">KILLED</font>"))
            //{
            //    Parser mm = new Parser(src);
            //    string tmp;
            //    if ((tmp = mm.Parse("has gained ", " experience!")) != "ERROR")
            //    {
            //        if (!int.TryParse(tmp, out mExpGained))
            //            mExpGained = 0;
            //        Globals.ExpGained += mExpGained;
            //        mRoom.Mover.ExpGained += mExpGained;
            //    }

            //    CoreUI.Instance.LogAttack(mRoom.Mover.Account.Name + " beat " + mName + ", " + mId + " in room "
            //                              + mRoom.Id + " "
            //                              + (mExpGained > 0 ? (", gained exp: " + mExpGained) : ""));

            //    if (src.Contains("You found a"))
            //    {
            //        string tmpFound = mm.Parse("You found a ", "!");
            //        // Sometimes Outwar glitches
            //        if (!tmpFound.Contains("<"))
            //        {
            //            CoreUI.Instance.LogAttack(mName + " dropped " + tmpFound);
            //        }
            //    }
            //}
            //else if (src.Contains("KILLED"))
            //{
            //    CoreUI.Instance.LogAttack(mName + ", " + mId + " in " + mRoom.Id + " beat " + mRoom.Mover.Account.Name);
            //}
            else
            {
                string tmp;
                if (src.StartsWith("ERROR"))
                {
                    CoreUI.Instance.LogAttack("Attack E occurred in Connection: " + src);
                }
                else if ((tmp = Parser.Parse(src, "ERROR:</b></font> ", "<br>")) == "ERROR")
                {
                    CoreUI.Instance.LogAttack("Attack E: An unknown error occurred");
                }
                else
                {
                    CoreUI.Instance.LogAttack("Attack E (server-side): " + tmp);
                }
            }

            mQuit = true;
            CoreUI.Instance.UpdateDisplay();
        }
    }
}