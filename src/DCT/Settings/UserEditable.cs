namespace DCT.Settings
{
    internal class UserEditable
    {
        private static string[] mMobFilters;
        internal static string[] MobFilters
        {
            get { return mMobFilters; }
            set { mMobFilters = value; }
        }

        private static bool mAutoJoin;
        internal static bool AutoJoin
        {
            get { return mAutoJoin; }
            set { mAutoJoin = value; }
        }

        private static bool mUseVault;
        internal static bool UseVault
        {
            get { return mUseVault; }
            set { mUseVault = value; }
        }
        private static bool mAttackPause;
        internal static bool AttackPause
        {
            get { return mAttackPause; }
            set { mAttackPause = value; }
        }
        private static bool mAlertQuests;
        internal static bool AlertQuests
        {
            get { return mAlertQuests; }
            set { mAlertQuests = value; }
        }
        private static bool mAutoTrain;
        internal static bool AutoTrain
        {
            get { return mAutoTrain; }
            set { mAutoTrain = value; }
        }
        private static bool mAutoQuest;
        internal static bool AutoQuest
        {
            get { return mAutoQuest; }
            set { mAutoQuest = value; }
        }
        private static bool mFilterMobs;
        internal static bool FilterMobs
        {
            get { return mFilterMobs; }
            set { mFilterMobs = value; }
        }
        private static bool mUseCountdownTimer;
        internal static bool UseCountdownTimer
        {
            get { return mUseCountdownTimer; }
            set { mUseCountdownTimer = value; }
        }
        private static bool mUseHourTimer;
        internal static bool UseHourTimer
        {
            get { return mUseHourTimer; }
            set { mUseHourTimer = value; }
        }
        private static bool mVariance;
        internal static bool Variance
        {
            get { return mVariance; }
            set { mVariance = value; }
        }
        private static bool mReturnToStart;
        internal static bool ReturnToStart
        {
            get { return mReturnToStart; }
            set { mReturnToStart = value; }
        }

        private static int mRaidInterval;
        internal static int RaidInterval
        {
            get { return mRaidInterval; }
            set { mRaidInterval = value; }
        }

        private static int mPauseAt;
        internal static int PauseAt
        {
            get { return mPauseAt; }
            set { mPauseAt = value; }
        }
        private static int mDelay;
        internal static int Delay
        {
            get { return mDelay; }
            set { mDelay = value; }
        }
        private static int mMaxThreads;
        internal static int MaxThreads
        {
            get { return mMaxThreads; }
            set { mMaxThreads = value; }
        }
        private static int mRageLimit;
        internal static int RageLimit
        {
            get { return mRageLimit; }
            set { mRageLimit = value; }
        }
        private static int mStopAtRage;
        internal static int StopAtRage
        {
            get { return mStopAtRage; }
            set { mStopAtRage = value; }
        }
        private static int mCycleInterval;
        internal static int CycleInterval
        {
            get { return mCycleInterval; }
            set { mCycleInterval = value; }
        }

        private static long mLvlLimitMin;
        internal static long LvlLimitMin
        {
            get { return mLvlLimitMin; }
            set { mLvlLimitMin = value; }
        }
        private static long mLvlLimit;
        internal static long LvlLimit
        {
            get { return mLvlLimit; }
            set { mLvlLimit = value; }
        }
        private static long mTimeout;
        internal static long Timeout
        {
            get { return mTimeout; }
            set { mTimeout = value; }
        }

        private static string mLastUsername;
        internal static string LastUsername
        {
            get { return mLastUsername; }
            set { mLastUsername = value; }
        }

        private static string mLastPassword;
        internal static string LastPassword
        {
            get { return mLastPassword; }
            set { mLastPassword = value; }
        }

        static UserEditable()
        {
            mMobFilters = new string[0];

            mAutoJoin = false;
            mUseVault = false;
            mAutoTrain = true;
            mVariance = true;

            mAlertQuests = false;
            mAutoQuest = false;

            mFilterMobs = false;

            mUseCountdownTimer = true;
            mUseHourTimer = false;

            mRaidInterval = 1;

            mPauseAt = 2;
            mLvlLimitMin = 0;
            mLvlLimit = 35;
            mRageLimit = 30;
            mStopAtRage = 0;
            mDelay = 0;
            mTimeout = 8000;
            mMaxThreads = 5;
            mCycleInterval = 40;

            mLastUsername = mLastPassword = string.Empty;
        }
    }
}