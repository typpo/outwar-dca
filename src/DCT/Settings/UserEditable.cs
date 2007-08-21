namespace DCT.Settings
{
    internal class UserEditable
    {
        private static string[] mMobFilters;
        public static string[] MobFilters
        {
            get { return mMobFilters; }
            set { mMobFilters = value; }
        }

        private static bool mAutoJoin;
        public static bool AutoJoin
        {
            get { return mAutoJoin; }
            set { mAutoJoin = value; }
        }

        private static bool mUseVault;
        public static bool UseVault
        {
            get { return mUseVault; }
            set { mUseVault = value; }
        }
        private static bool mAttackPause;
        public static bool AttackPause
        {
            get { return mAttackPause; }
            set { mAttackPause = value; }
        }
        private static bool mAlertQuests;
        public static bool AlertQuests
        {
            get { return mAlertQuests; }
            set { mAlertQuests = value; }
        }
        private static bool mAutoTrain;
        public static bool AutoTrain
        {
            get { return mAutoTrain; }
            set { mAutoTrain = value; }
        }
        private static bool mAutoQuest;
        public static bool AutoQuest
        {
            get { return mAutoQuest; }
            set { mAutoQuest = value; }
        }
        private static bool mFilterMobs;
        public static bool FilterMobs
        {
            get { return mFilterMobs; }
            set { mFilterMobs = value; }
        }
        private static bool mUseCountdownTimer;
        public static bool UseCountdownTimer
        {
            get { return mUseCountdownTimer; }
            set { mUseCountdownTimer = value; }
        }
        private static bool mUseHourTimer;
        public static bool UseHourTimer
        {
            get { return mUseHourTimer; }
            set { mUseHourTimer = value; }
        }
        private static bool mVariance;
        public static bool Variance
        {
            get { return mVariance; }
            set { mVariance = value; }
        }
        private static bool mOptimize;
        public static bool Optimize
        {
            get { return mOptimize; }
            set { mOptimize = value; }
        }
        private static bool mReturnToStart;
        public static bool ReturnToStart
        {
            get { return mReturnToStart; }
            set { mReturnToStart = value; }
        }

        private static int mRaidInterval;
        public static int RaidInterval
        {
            get { return mRaidInterval; }
            set { mRaidInterval = value; }
        }

        private static int mPauseAt;
        public static int PauseAt
        {
            get { return mPauseAt; }
            set { mPauseAt = value; }
        }
        private static int mDelay;
        public static int Delay
        {
            get { return mDelay; }
            set { mDelay = value; }
        }
        private static int mMaxThreads;
        public static int MaxThreads
        {
            get { return mMaxThreads; }
            set { mMaxThreads = value; }
        }
        private static int mRageLimit;
        public static int RageLimit
        {
            get { return mRageLimit; }
            set { mRageLimit = value; }
        }
        private static int mStopAtRage;
        public static int StopAtRage
        {
            get { return mStopAtRage; }
            set { mStopAtRage = value; }
        }
        private static int mCycleInterval;
        public static int CycleInterval
        {
            get { return mCycleInterval; }
            set { mCycleInterval = value; }
        }

        private static long mLvlLimitMin;
        public static long LvlLimitMin
        {
            get { return mLvlLimitMin; }
            set { mLvlLimitMin = value; }
        }
        private static long mLvlLimit;
        public static long LvlLimit
        {
            get { return mLvlLimit; }
            set { mLvlLimit = value; }
        }
        private static long mTimeout;
        public static long Timeout
        {
            get { return mTimeout; }
            set { mTimeout = value; }
        }

        private static string mLastUsername;
        public static string LastUsername
        {
            get { return mLastUsername; }
            set { mLastUsername = value; }
        }

        private static string mLastPassword;
        public static string LastPassword
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
            mOptimize = true;

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