namespace DCT.Security
{
    static internal class Version
    {
        internal const string Id = "3.1.";
        internal const string mini = "8f";
        internal static string Full
        {
            get { return string.Format("{0}.{1}", Id, mini); }
        }
    }
}