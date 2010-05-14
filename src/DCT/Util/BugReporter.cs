using System.Web;
using DCT.Protocols.Http;

namespace DCT.Util
{
    class BugReporter
    {
        private const string URL = "http://typpo.us/submit.php";

        internal static readonly string IDENTIFIER = Randomizer.RandomString(10);

        internal bool ReportBug(string msg, string email)
        {
            HttpSocket s = new HttpSocket();

            // send data
            string resp = s.Post(URL, string.Format("message={0}&email={1}&identifier={2}&hide=1", HttpUtility.UrlEncode(msg), HttpUtility.UrlEncode(email), IDENTIFIER));
            if (resp.Contains("Input successful."))
                return true;
            return false;
        }
    }
}
