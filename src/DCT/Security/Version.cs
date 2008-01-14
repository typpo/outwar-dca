namespace DCT.Security
{
    static internal class Version
    {
        internal const string Id = "pi";
        internal const string mini = "3";
        internal static string Full
        {
            get { return string.Format("{0}.{1}", Id, mini); }
        }
    }
}