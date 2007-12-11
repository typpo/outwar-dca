namespace DCT.Settings
{
    internal class Globals
    {
        private static bool mAttackOn;
        private static bool mAttackMode;
        private static bool mTerminate;
        private static long mExpGained;
        private static long mExpGainedTotal;
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

        static Globals()
        {
            mAttackOn = false;
            mAttackMode = false;
            mTerminate = false;

            mExpGained = 0;
            mExpGainedTotal = 0;
        }
    }
}