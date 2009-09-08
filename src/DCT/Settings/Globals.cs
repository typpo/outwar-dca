namespace DCT.Settings
{
    internal class Globals
    {
        internal static bool AttackOn { get; set; }
        internal static bool AttackMode { get; set; }
        internal static bool Terminate { get; set; }
        internal static long ExpGained { get; set; }
        internal static long ExpGainedTotal { get; set; }

        static Globals()
        {
            AttackOn = false;
            AttackMode = false;
            Terminate = false;

            ExpGained = 0;
            ExpGainedTotal = 0;
        }
    }
}