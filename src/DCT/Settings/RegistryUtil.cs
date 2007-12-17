using Microsoft.Win32;

namespace DCT.Settings
{
    static internal class RegistryUtil
    {
        private const string KEY = "SOFTWARE\\Typpo's Software";

        internal static void Save()
        {
            try
            {
                RegistryKey k = Registry.LocalMachine.OpenSubKey(KEY, true);
                if (k == null)
                {
                    k = Registry.LocalMachine.CreateSubKey(KEY);
                }
                k.SetValue("DCTexpg", (Globals.ExpGainedTotal + Globals.ExpGained), RegistryValueKind.String);
            }
            catch (System.UnauthorizedAccessException)
            {

            }
        }

        internal static void Load()
        {
            try
            {
                RegistryKey myKey = Registry.LocalMachine.OpenSubKey(KEY, false);
                if (myKey == null)
                    return;

                object tmp = myKey.GetValue("DCTexpg");
                if (tmp == null)
                    return;
                Globals.ExpGainedTotal = long.Parse((string)tmp);
            }
            catch (System.UnauthorizedAccessException)
            {

            }
        }
    }
}