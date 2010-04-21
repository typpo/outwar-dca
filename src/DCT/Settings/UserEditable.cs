using System;
using System.Xml.Serialization;

namespace DCT.Settings
{
    [XmlRoot("UserEditable")]
    public class UserEditable
    {
        [XmlElement("LastMapUpdate")]
        public DateTime LastMapUpdate { get; set; }

        [XmlArrayItem("Filter", typeof(string))]
        public string[] MobFilters { get; set; }
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
        public DCT.Outwar.World.AttackingType AttackMode { get; set; }
        [XmlElement("Delay")]
        public int Delay { get; set; }
        [XmlIgnore()]
        public int MaxThreads { get; set; }
        [XmlElement("RageLimit")]
        public int RageLimit { get; set; }
        [XmlElement("StopAtRage")]
        public int StopBelowRage { get; set; }
        [XmlElement("CycleInterval")]
        public int CycleInterval { get; set; }
        [XmlElement("LvlLimitMin")]
        public long LvlLimitMin { get; set; }
        [XmlElement("LvlLimit")]
        public long LvlLimit { get; set; }
        [XmlElement("Timeout")]
        public int Timeout { get; set; }
        [XmlElement("AttackSpawns")]
        public bool AttackSpawns { get; set; }
        [XmlElement("IgnoreSpawnRage")]
        public bool IgnoreSpawnRage { get; set; }
        [XmlElement("LastUsername")]
        public string LastUsername { get; set; }
        [XmlElement("LastPassword")]
        public string LastPassword { get; set; }


        [XmlElement("LoginThrough")]
        public int Server { get; set; }

        public enum StopAfterType
        {
            Runs,
            Minutes
        }

        [XmlElement("StopAfter")]
        public bool StopAfter { get; set; }
        [XmlElement("StopAfterVal")]
        public int StopAfterVal { get; set; }
        [XmlElement("StopAfterMode")]
        public StopAfterType StopAfterMode { get; set; }

        public UserEditable()
        {
            LastMapUpdate = DateTime.MinValue;

            MobFilters = new string[0];

            NotifyVisible = true;

            AutoTeleport = true;

            ClearLogs = true;
            AutoTrain = false;
            Variance = true;

            AlertQuests = false;
            AutoQuest = false;

            FilterMobs = false;

            UseCountdownTimer = true;
            UseHourTimer = false;

            AttackMode = DCT.Outwar.World.AttackingType.Mobs;

            LvlLimitMin = 0;
            LvlLimit = 65;
            RageLimit = 75;
            StopBelowRage = 0;
            Delay = 0;
            Timeout = 15000;
            MaxThreads = 5;
            CycleInterval = 40;

            AttackSpawns = false;
            IgnoreSpawnRage = true;

            LastUsername = LastPassword = string.Empty;

            StopAfter = false;
            StopAfterVal = 1;
            StopAfterMode = StopAfterType.Runs;

            Server = 1;
        }
    }
}