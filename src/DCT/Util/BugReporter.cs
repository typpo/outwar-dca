using System.Web;
using DCT.Protocols.Http;
using System.Threading;

namespace DCT.Util
{
    class BugReporter
    {
        private const string URL = "http://typpo.us/submit.php";

        internal bool ReportBug(string msg, string email)
        {
            HttpSocket s = new HttpSocket();

            // send data
            string resp = s.Post(URL, string.Format("message={0}&email={1}", HttpUtility.UrlEncode(msg), HttpUtility.UrlEncode(email)));
            if (resp.Contains("Input successful."))
                return true;
            return false;
        }
    }
}
