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
        public static bool AttackOn
        {
            get { return mAttackOn; }
            set { mAttackOn = value; }
        }
        public static bool AttackMode
        {
            get { return mAttackMode; }
            set { mAttackMode = value; }
        }
        public static bool Terminate
        {
            get { return mTerminate; }
            set { mTerminate = value; }
        }
        public static long ExpGained
        {
            get { return mExpGained; }
            set { mExpGained = value; }
        }
        public static long ExpGainedTotal
        {
            get { return mExpGainedTotal; }
            set { mExpGainedTotal = value; }
        }
        public static long SecRight
        {
            get { return mSecRight; }
            set { mSecRight = value; }
        }
        public static long SecWrong
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