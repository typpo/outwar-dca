namespace DCT.Security
{
    static internal class Version
    {
        internal const string Id = "3.1.";
        internal const string mini = "12";
        internal const string beta = "beta1";

        internal static string Full
        {
            get { return string.Format("{0}.{1}", Id, mini); }
        }

        internal static string Beta
        {
            get { return beta; }
        }
    }
}