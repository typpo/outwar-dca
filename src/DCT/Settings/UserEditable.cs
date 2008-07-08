using System;
using System.Xml.Serialization;

namespace DCT.Settings
{
    [XmlRoot("UserEditable")]
    public class UserEditable
    {
        private string[] mMobFilters;
        [XmlArrayItem("Filter", typeof(string))]
        public string[] MobFilters
        {
            get { return mMobFilters; }
            set { mMobFilters = value; }
        }

        private bool mFly;
        [XmlElement("Fly")]
        public bool Fly
        {
            get { return mFly; }
            set { mFly = value; }
        }

        private bool mNotifyVisible;
        [XmlElement("NotifyVisible")]
        public bool NotifyVisible
        {
            get { return mNotifyVisible; }
            set { mNotifyVisible = value; }
        }

        private bool mClearLogs;
        [XmlElement("ClearLogs")]
        public bool ClearLogs
        {
            get { return mClearLogs; }
            set { mClearLogs = value; }
        }

        private bool mAutoTeleport;
        [XmlElement("AutoTeleport")]
        public bool AutoTeleport
        {
            get { return mAutoTeleport; }
            set { mAutoTeleport = value; }
        }

        private bool mAlertQuests;
        [XmlElement("AlertQuests")]
        public bool AlertQuests
        {
            get { return mAlertQuests; }
            set { mAlertQuests = value; }
        }
        private bool mAutoTrain;
        [XmlElement("AutoTrain")]
        public bool AutoTrain
        {
            get { return mAutoTrain; }
            set { mAutoTrain = value; }
        }
        private bool mAutoQuest;
        [XmlElement("AutoQuest")]
        public bool AutoQuest
        {
            get { return mAutoQuest; }
            set { mAutoQuest = value; }
        }
        private bool mFilterMobs;
        [XmlElement("FilterMobs")]
        public bool FilterMobs
        {
            get { return mFilterMobs; }
            set { mFilterMobs = value; }
        }
        private bool mUseCountdownTimer;
        [XmlElement("UseCountdownTimer")]
        public bool UseCountdownTimer
        {
            get { return mUseCountdownTimer; }
            set { mUseCountdownTimer = value; }
        }
        private bool mUseHourTimer;
        [XmlElement("UseHourTimer")]
        public bool UseHourTimer
        {
            get { return mUseHourTimer; }
            set { mUseHourTimer = value; }
        }
        private bool mVariance;
        [XmlElement("Variance")]
        public bool Variance
        {
            get { return mVariance; }
            set { mVariance = value; }
        }
        private bool mReturnToStart;
        [XmlElement("ReturnToStart")]
        public bool ReturnToStart
        {
            get { return mReturnToStart; }
            set { mReturnToStart = value; }
        }

        private int mAttackMode;
        [XmlElement("AttackMode")]
        public int AttackMode
        {
            get { return mAttackMode; }
            set { mAttackMode = value; }
        }

        private int mDelay;
        [XmlElement("Delay")]
        public int Delay
        {
            get { return mDelay; }
            set { mDelay = value; }
        }
        private int mMaxThreads;
        [XmlIgnore()]
        public int MaxThreads
        {
            get { return mMaxThreads; }
            set { mMaxThreads = value; }
        }
        private int mRageLimit;
        [XmlElement("RageLimit")]
        public int RageLimit
        {
            get { return mRageLimit; }
            set { mRageLimit = value; }
        }
        private int mStopAtRage;
        [XmlElement("StopAtRage")]
        public int StopAtRage
        {
            get { return mStopAtRage; }
            set { mStopAtRage = value; }
        }
        private int mCycleInterval;
        [XmlElement("CycleInterval")]
        public int CycleInterval
        {
            get { return mCycleInterval; }
            set { mCycleInterval = value; }
        }

        private long mLvlLimitMin;
        [XmlElement("LvlLimitMin")]
        public long LvlLimitMin
        {
            get { return mLvlLimitMin; }
            set { mLvlLimitMin = value; }
        }
        private long mLvlLimit;
        [XmlElement("LvlLimit")]
        public long LvlLimit
        {
            get { return mLvlLimit; }
            set { mLvlLimit = value; }
        }
        private int mTimeout;
        [XmlElement("Timeout")]
        public int Timeout
        {
            get { return mTimeout; }
            set { mTimeout = value; }
        }

        private string mLastUsername;
        [XmlElement("LastUsername")]
        public string LastUsername
        {
            get { return mLastUsername; }
            set { mLastUsername = value; }
        }

        private string mLastPassword;
        [XmlElement("LastPassword")]
        public string LastPassword
        {
            get { return mLastPassword; }
            set { mLastPassword = value; }
        }

        public UserEditable()
        {
            mMobFilters = new string[0];

            mNotifyVisible = true;

            mFly = false;
            mAutoTeleport = true;

            mClearLogs = true;
            mAutoTrain = false;
            mVariance = true;

            mAlertQuests = false;
            mAutoQuest = false;

            mFilterMobs = false;

            mUseCountdownTimer = true;
            mUseHourTimer = false;

            mAttackMode = 1;

            mLvlLimitMin = 0;
            mLvlLimit = 35;
            mRageLimit = 30;
            mStopAtRage = 0;
            mDelay = 0;
            mTimeout = 15000;
            mMaxThreads = 5;
            mCycleInterval = 40;

            mLastUsername = mLastPassword = string.Empty;
        }
    }
}