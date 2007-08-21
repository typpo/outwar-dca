namespace DCT.Settings
{
    internal class Globals
    {
        private static bool mAttackOn;
        private static bool mAttackMode;
        private static bool mTerminate;
        private static long mExpGained;
        private static long mExpGainedTotal;
        private static long mSecRight;
        private static long mSecWrong;
        internal static bool AttackOn
        {
            get { return mAttackOn; }
            set { mAttackOn = value; }
        }
        internal static bool AttackMode
        {
            get { return mAttackMode; }
            set { mAttackMode = value; }
        }
        internal static bool Terminate
        {
            get { return mTerminate; }
            set { mTerminate = value; }
        }
        internal static long ExpGained
        {
            get { return mExpGained; }
            set { mExpGained = value; }
        }
        internal static long ExpGainedTotal
        {
            get { return mExpGainedTotal; }
            set { mExpGainedTotal = value; }
        }
        internal static long SecRight
        {
            get { return mSecRight; }
            set { mSecRight = value; }
        }
        internal static long SecWrong
        {
            get { return mSecWrong; }
            set { mSecWrong = value; }
        }

        static Globals()
        {
            mAttackOn = false;
            mAttackMode = false;
            mTerminate = false;

            mSecRight = 0;
            mSecWrong = 0;

            mExpGained = 0;
            mExpGainedTotal = 0;
        }
    }
}