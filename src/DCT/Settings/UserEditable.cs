using System;
using System.Xml.Serialization;

namespace DCT.Settings
{
    [XmlRoot("UserEditable")]
    public class UserEditable
    {
        [XmlArrayItem("Filter", typeof(string))]
        public string[] MobFilters { get; set; }
        [XmlElement("Fly")]
        public bool Fly { get; set; }
        [XmlElement("NotifyVisible")]
        public bool NotifyVisible { get; set; }
        [XmlElement("ClearLogs")]
        public bool ClearLogs { get; set; }
        [XmlElement("AutoTeleport")]
        public bool AutoTeleport { get; set; }
        [XmlElement("AlertQuests")]
        public bool AlertQuests { get; set; }
        [XmlElement("AutoTrain")]
        public bool AutoTrain { get; set; }
        [XmlElement("AutoQuest")]
        public bool AutoQuest { get; set; }
        [XmlElement("FilterMobs")]
        public bool FilterMobs { get; set; }
        [XmlElement("UseCountdownTimer")]
        public bool UseCountdownTimer { get; set; }
        [XmlElement("UseHourTimer")]
        public bool UseHourTimer { get; set; }
        [XmlElement("Variance")]
        public bool Variance { get; set; }
        [XmlElement("ReturnToStart")]
        public bool ReturnToStart { get; set; }
        [XmlElement("AttackMode")]
        public int AttackMode { get; set; }
        [XmlElement("Delay")]
        public int Delay { get; set; }
        [XmlIgnore()]
        public int MaxThreads { get; set; }
        [XmlElement("RageLimit")]
        public int RageLimit { get; set; }
        [XmlElement("StopAtRage")]
        public int StopAtRage { get; set; }
        [XmlElement("CycleInterval")]
        public int CycleInterval { get; set; }
        [XmlElement("LvlLimitMin")]
        public long LvlLimitMin { get; set; }
        [XmlElement("LvlLimit")]
        public long LvlLimit { get; set; }
        [XmlElement("Timeout")]
        public int Timeout { get; set; }
        [XmlElement("LastUsername")]
        public string LastUsername { get; set; }
        [XmlElement("LastPassword")]
        public string LastPassword { get; set; }

        public UserEditable()
        {
            MobFilters = new string[0];

            NotifyVisible = true;

            Fly = false;
            AutoTeleport = true;

            ClearLogs = true;
            AutoTrain = false;
            Variance = true;

            AlertQuests = false;
            AutoQuest = false;

            FilterMobs = false;

            UseCountdownTimer = true;
            UseHourTimer = false;

            AttackMode = 1;

            LvlLimitMin = 0;
            LvlLimit = 35;
            RageLimit = 30;
            StopAtRage = 0;
            Delay = 0;
            Timeout = 15000;
            MaxThreads = 5;
            CycleInterval = 40;

            LastUsername = LastPassword = string.Empty;
        }
    }
}