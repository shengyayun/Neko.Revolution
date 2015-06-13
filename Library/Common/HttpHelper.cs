using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;

namespace Library.Common
{
    public class HttpHelper
    {
        public static CookieContainer CookieContainers = new CookieContainer();

        public static string Get(string url)
        {
            return GetResponse(url, "get", "", "", "", "");
        }

        public static string Get(string url, string host, string orign, string refer)
        {
            return GetResponse(url, "get", "", host, orign, refer);
        }

        public static string Post(string url, string data, string host, string orign, string refer)
        {
            return GetResponse(url, "post", data, host, orign, refer);
        }

        public static string GetResponse(string url, string method, string data, string host, string orign, string refer)
        {
            HttpWebResponse res;
            var myEncoding = Encoding.UTF8;
            Stream st;
            StreamReader sr;
            try
            {
                var req = (HttpWebRequest)WebRequest.Create(url);
                req.KeepAlive = true;
                req.Method = method.ToUpper();
                req.AllowAutoRedirect = true;
                req.CookieContainer = CookieContainers;
                req.ContentType = "application/x-www-form-urlencoded";
                req.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; InfoPath.2; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022; .NET4.0C; .NET4.0E)";
                req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                req.Timeout = 20000;
                req.Referer = refer;
                if (!string.IsNullOrEmpty(host))
                {
                    req.Host = host;
                }
                req.Headers.Add("X-Requested-With", "XMLHttpRequest");
                if (!string.IsNullOrEmpty(orign))
                {
                    req.Headers.Add("Origin", orign);
                }
                if (method.ToUpper() == "POST" && data != null)
                {
                    var encoding = new ASCIIEncoding();
                    var postBytes = encoding.GetBytes(data);
                    req.ContentLength = postBytes.Length;
                    st = req.GetRequestStream();
                    st.Write(postBytes, 0, postBytes.Length);
                    st.Close();
                }
                ServicePointManager.ServerCertificateValidationCallback += (se, cert, chain, sslerror) => true;
                res = (HttpWebResponse)req.GetResponse();
                st = res.GetResponseStream();
                if (st == null) return "";
                if (res.ContentEncoding.ToLower().Contains("gzip"))
                {
                    st = new GZipStream(st, CompressionMode.Decompress);
                }
                sr = new StreamReader(st, myEncoding);
                if (res.Cookies.Count > 0)
                {
                    CookieContainers.Add(res.Cookies);
                }
                if (req.CookieContainer != null)
                {
                    var host1 = req.Address.Host;
                    if (host1 == "d.web2.qq.com")
                    {
                        var cookieList = GetAllCookies();
                        foreach (var cookie in cookieList)
                        {
                            cookie.Domain = "d.web2.qq.com";
                            CookieContainers.Add(cookie);
                        }
                    }
                }
                var str = sr.ReadToEnd();
                return str;

            }
            catch (WebException ex)
            {
                var error = string.Empty;
                if (ex.Response != null)
                {
                    res = (HttpWebResponse)ex.Response;
                    st = res.GetResponseStream();
                    sr = new StreamReader(st, myEncoding);
                    error = sr.ReadToEnd();
                }
                return error;
            }

        }

        public static List<Cookie> GetAllCookies()
        {
            var cc = CookieContainers;
            var lstCookies = new List<Cookie>();

            var table = (Hashtable)cc.GetType().InvokeMember("m_domainTable", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.Instance, null, cc, new object[] { });

            foreach (var lstCookieCol in from object pathList in table.Values
                                         select (SortedList)pathList.GetType()
                                             .InvokeMember("m_list", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.Instance, null, pathList, new object[] { }))
            {
                lstCookies.AddRange(from CookieCollection colCookies in lstCookieCol.Values from Cookie c in colCookies select c);
            }

            return lstCookies;
        }

        public static string GetCookie(string url, string name)
        {
            var cookieStr = string.Empty;
            foreach (var cookie in CookieContainers.GetCookies(new Uri(url)).Cast<Cookie>().Where(cookie => cookie.Name == name))
            {
                cookieStr = cookie.Value;
                break;
            }
            return cookieStr;
        }

        /// <summary>
        /// 获得图片流
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="host">host</param>
        /// <param name="orign">orign</param>
        /// <param name="refer">refer</param>
        /// <returns></returns>
        public static byte[] GetResponseImage(string url, string host, string orign, string refer)
        {
            try
            {
                var req = (HttpWebRequest)WebRequest.Create(url);
                req.KeepAlive = true;
                req.Method = "GET";
                req.AllowAutoRedirect = true;
                req.CookieContainer = CookieContainers;

                req.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; rv:30.0) Gecko/20100101 Firefox/30.0";
                req.Accept = "image/png,image/*;q=0.8,*/*;q=0.5";
                req.Timeout = 50000;
                if (!string.IsNullOrEmpty(refer))
                {
                    req.Referer = refer;
                }
                if (!string.IsNullOrEmpty(orign))
                {
                    req.Headers.Add("Origin", orign);
                }
                if (!string.IsNullOrEmpty(host))
                {
                    req.Host = host;
                }
                //   ServicePointManager.ServerCertificateValidationCallback += (se, cert, chain, sslerror) => true;
                var res = (HttpWebResponse)req.GetResponse();
                if (res.Cookies.Count > 0)
                {
                    CookieContainers.Add(res.Cookies);
                }

                var resst = res.GetResponseStream();
                return ConvertStreamToByteBuffer(resst);
            }
            catch
            {
                return null;
            }
        }

        private static byte[] ConvertStreamToByteBuffer(Stream theStream)
        {
            int b1;
            var tempStream = new MemoryStream();
            while ((b1 = theStream.ReadByte()) != -1)
            {
                tempStream.WriteByte(((byte)b1));
            }
            return tempStream.ToArray();
        }

        /// <summary>
        /// 遍历CookieContainer
        /// </summary>
        /// <param name="cc"></param>
        /// <returns></returns>
        public static List<Cookie> GetAllCookies(CookieContainer cc)
        {
            var table = (Hashtable)cc.GetType().InvokeMember("m_domainTable",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField |
                System.Reflection.BindingFlags.Instance, null, cc, new object[] { });

            return (from object pathList in table.Values
                    select (SortedList)pathList.GetType()
                        .InvokeMember("m_list", System.Reflection.BindingFlags.NonPublic |
                        System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.Instance,
                        null, pathList, new object[] { }) into lstCookieCol
                    from CookieCollection colCookies
                        in lstCookieCol.Values
                    from Cookie c in colCookies
                    select c).ToList();
        }

        public static void ModifyCookie(Cookie cookie)
        {
            var cookies = GetAllCookies();
            for (var i = 0; i < cookies.Count(); i++)
            {
                if (cookies[i].Name == cookie.Name)
                {
                    cookies[i] = cookie;
                    break;
                }
            }
            CookieContainers = new CookieContainer();
            foreach (var cookie1 in cookies)
            {
                CookieContainers.Add(cookie1);
            }
        }
    }
}

