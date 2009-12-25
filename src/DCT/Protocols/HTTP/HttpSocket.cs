using System;
using System.IO;
using System.Net;
using System.Text;
using DCT.UI;
using DCT.Util;

namespace DCT.Protocols.Http
{
    internal class HttpSocket
    {
        private static HttpSocket mDefaultInstance;
        internal static HttpSocket DefaultInstance
        {
            get
            {
                if (mDefaultInstance == null)
                {
                    mDefaultInstance = new HttpSocket();
                    mDefaultInstance.Redirect = false;
                    mDefaultInstance.UserAgent = "Typpo DCAA Client";
                }
                return mDefaultInstance;
            }
        }

        private static string mIP;
        internal static string IP
        {
            get
            {
                if (mIP == null)
                {
                    mIP = DefaultInstance.Get("http://typpo.dyndns.org:7012/auth/ip.php");
                }
                return mIP;
            }
        }
        internal bool Redirect { get; set; }
        internal int Timeout { get; set; }
        internal string UserAgent { get; set; }
        internal string Cookie { get; set; }
        internal bool HasCookie
        {
            get { return !string.IsNullOrEmpty(Cookie); }
        }

        private static readonly string[] USER_AGENTS = {
                                           "Mozilla/5.0 (Windows; U; Windows NT 5.0; en-US; rv:0.9.4) Gecko/20011019 Netscape6/6.2"
                                           ,
                                           "Mozilla/5.0 (Windows; U; Windows NT 5.0; en-US; rv:0.9.9) Gecko/20020311",
                                           "Mozilla/4.0 (compatible; MSIE 5.0; Windows ME) Opera 6.01 [en]",
                                           "Mozilla/5.0 (compatible; Konqueror/3.1; Linux 2.4.22-4GB-athlon; X11; i686)"
                                           ,
                                           "Opera/7.0 (Windows 2000; U) [en]",
                                           "Mozilla/4.0 (compatible; MSIE 5.0; AOL 5.0; Windows 98; DigExt; VNIE4 3.1.814)"
                                           ,
                                           "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; InfoPath.1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)"
                                       };

        //private static readonly RequestCachePolicy CACHE_POLICY =
        //    new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);

        internal HttpSocket()
        {
            Timeout = CoreUI.Instance.Settings.Timeout;

            Redirect = true;
            Cookie = string.Empty;
            UserAgent = USER_AGENTS[Randomizer.Random.Next(USER_AGENTS.Length)];
        }
        internal string Get(string url)
        {
            return Request(url, null);
        }

        internal string Post(string url, string postData)
        {
            return Request(url, postData);
        }

        protected virtual string Request(string url, string write)
        {
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    HttpWebRequest request = GenerateRequest(url);

                    if (write != null)
                    {
                        request.Method = "POST";
                        request.ContentLength = write.Length;
                        StreamWriter sw = new StreamWriter(request.GetRequestStream());
                        sw.Write(write);
                        sw.Flush();
                        sw.Close();
                    }

                    WebResponse response = request.GetResponse();
                    StreamReader sr = new StreamReader(response.GetResponseStream());

                    string ret = sr.ReadToEnd();
                    sr.Close();
                    response.Close();

                    if (string.IsNullOrEmpty(Cookie))
                    {
                        StringBuilder full = new StringBuilder();
                        string[] cookieA = response.Headers.GetValues("Set-Cookie");

                        if (cookieA != null)
                        {
                            for (int j = 0; j < cookieA.Length; j++)
                                full.Append(cookieA[j].Substring(0, cookieA[j].IndexOf(";") + 1));
                        }

                        Cookie = full.ToString();
                    }

                    return ret;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return string.Empty;
        }

        private HttpWebRequest GenerateRequest(string url)
        {
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
            //request.CachePolicy = CACHE_POLICY;
            request.Timeout = CoreUI.Instance.Settings.Timeout;//mTimeout;

            if (!string.IsNullOrEmpty(Cookie))
            {
                request.Headers.Add("Cookie: " + Cookie);
            }

            request.UserAgent = UserAgent;
            request.ContentType = "application/x-www-form-urlencoded";
            request.AllowAutoRedirect = Redirect;

            return request;
        }
    }
}