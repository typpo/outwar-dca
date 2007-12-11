using System;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Text;
using DCT.Util;
using DCT.UI;

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
                    return mIP;
                }
                else
                    return mIP;
            }
        }

        private string mStatus;
        internal string Status
        {
            get { return mStatus; }
            set { mStatus = value; }
        }

        private bool mRedirect;
        internal bool Redirect
        {
            get { return mRedirect; }
            set { mRedirect = value; }
        }

        private bool mKeepAlive;
        internal bool KeepAlive
        {
            get { return mKeepAlive; }
            set { mKeepAlive = value; }
        }

        private int mTimeout;
        internal int Timeout
        {
            get { return mTimeout; }
            set { mTimeout = value; }
        }

        private string mUserAgent;
        internal string UserAgent
        {
            get { return mUserAgent; }
            set { mUserAgent = value; }
        }

        private string mCookie;
        internal string Cookie
        {
            get { return mCookie; }
            set { mCookie = value; }
        }
        internal bool HasCookie
        {
            get { return !string.IsNullOrEmpty(mCookie); }
        }

        private static readonly string[] USER_AGENTS = {
                                           "Lynx/2.8.4rel.1 libwww-FM/2.14 SSL-MM/1.4.1 OpenSSL/0.9.6b",
                                           "Mozilla/5.0 (Windows; U; Windows NT 5.0; en-US; rv:0.9.4) Gecko/20011019 Netscape6/6.2"
                                           ,
                                           "Mozilla/5.0 (Windows; U; Windows NT 5.0; en-US; rv:0.9.9) Gecko/20020311",
                                           "Mozilla/4.0 (compatible; MSIE 5.0; Windows ME) Opera 6.01 [en]",
                                           "Mozilla/5.0 (compatible; Konqueror/3.1; Linux 2.4.22-4GB-athlon; X11; i686)"
                                           ,
                                           "Opera/7.0 (Windows 2000; U) [en]",
                                           "Opera/7.02 Bork-edition (Windows NT 5.0; U) [en]",
                                           "Mozilla/4.0 (compatible; MSIE 5.0; AOL 5.0; Windows 98; DigExt; VNIE4 3.1.814)"
                                           ,
                                           "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; InfoPath.1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)"
                                       };

        //private static readonly RequestCachePolicy CACHE_POLICY =
        //    new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);

        internal HttpSocket()
        {
            mTimeout = (int)CoreUI.Instance.Settings.Timeout;

            mRedirect = true;
            mKeepAlive = false;
            mCookie = string.Empty;
            mUserAgent = USER_AGENTS[Randomizer.Random.Next(USER_AGENTS.Length)];
            Status = "Created";
        }

        internal string Get(string url)
        {
            return Get(url, string.Empty);
        }

        internal string Get(string url, string referer)
        {
            return Request(url, string.Empty, referer);
        }

        internal string Post(string url, string postData)
        {
            return Post(url, postData, string.Empty);
        }

        internal string Post(string url, string postData, string referer)
        {
            return Request(url, postData, referer);
        }

        protected virtual string Request(string url, string write, string referer)
        {
            Stream sw = null;
            StreamReader sr = null;
            WebResponse response = null;
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    bool post = !string.IsNullOrEmpty(write);

                    HttpWebRequest request = GenerateRequest(url);

                    if (!string.IsNullOrEmpty(referer))
                        request.Referer = referer;

                    if (post)
                    {
                        request.Method = "POST";
                        request.ContentLength = write.Length;
                        sw = request.GetRequestStream();
                        UTF8Encoding encoding = new UTF8Encoding();
                        byte[] bytes = encoding.GetBytes(write);
                        sw.Write(bytes, 0, bytes.Length);
                        sw.Flush();
                        sw.Close();
                    }

                    response = request.GetResponse();
                    sr = new StreamReader(response.GetResponseStream());

                    string ret = sr.ReadToEnd();
                    sr.Close();
                    response.Close();

                    if (string.IsNullOrEmpty(mCookie))
                    {
                        StringBuilder full = new StringBuilder();
                        string[] cookieA = response.Headers.GetValues("Set-Cookie");

                        if (cookieA != null)
                        {
                            for (int j = 0; j < cookieA.Length; j++)
                                full.Append(cookieA[j].Substring(0, cookieA[j].IndexOf(";") + 1));
                        }

                        mCookie = full.ToString();
                    }

                    return ret;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return string.Empty;

            /*
            StreamWriter sw = null;
            StreamReader sr = null;
            WebResponse response = null;
            string ret;

            try
            {
                bool post = write != string.Empty;

                HttpWebRequest request = GenerateRequest(url);

                if (referer != null && referer != "")
                    request.Referer = referer;

                if (post)
                {
                    request.Method = "POST";
                    request.ContentLength = write.Length;
                    sw = new StreamWriter(request.GetRequestStream());
                    sw.Write(write);
                }

                response = request.GetResponse();
                sr = new StreamReader(response.GetResponseStream());

                ret = sr.ReadToEnd();
                response.Close();

                if (String.IsNullOrEmpty(mCookie))
                {
                    StringBuilder full = new StringBuilder();
                    string[] cookieA = response.Headers.GetValues("Set-Cookie");

                    if (cookieA != null)
                    {
                        for (int j = 0; j < cookieA.Length; j++)
                            full.Append(cookieA[j].Substring(0, cookieA[j].IndexOf(";") + 1));
                    }

                    mCookie = full.ToString();
                }
            }
            finally
            {
                if (sw != null)
                {
                    sw.Flush();
                    sw.Close();
                }
                if (sr != null)
                {
                    sr.Close();
                }
                if (response != null)
                {
                    response.Close();
                }
            }

            return ret;
            */
        }

        private HttpWebRequest GenerateRequest(string url)
        {
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
            //request.CachePolicy = CACHE_POLICY;
            request.Timeout = (int)CoreUI.Instance.Settings.Timeout;//mTimeout;

            if (!string.IsNullOrEmpty(mCookie))
            {
                request.Headers.Add("Cookie: " + mCookie);
            }

            request.UserAgent = mUserAgent;
            request.ContentType = "application/x-www-form-urlencoded";
            request.KeepAlive = mKeepAlive;
            request.AllowAutoRedirect = mRedirect;

            return request;
        }
    }
}