using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Library.Common
{
    public class HtmlHelper
    {
        #region 获取指定ID的标签内容
        /// <summary>
        /// 获取指定ID的标签内容
        /// </summary>
        /// <param name="html">HTML源码</param>
        /// <param name="id">标签ID</param>
        /// <returns></returns>
        public static string GetElementById(string html, string id)
        {
            string pattern = @"<([a-z]+)(?:(?!id)[^<>])*id=([""']?){0}\2[^>]*>(?>(?<o><\1[^>]*>)|(?<-o></\1>)|(?:(?!</?\1).))*(?(o)(?!))</\1>";
            pattern = string.Format(pattern, Regex.Escape(id));
            Match match = Regex.Match(html, pattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);
            return match.Success ? match.Value : "";
        }
        #endregion
        #region 通过class属性获取对应标签集合
        /// <summary>
        /// 通过class属性获取对应标签集合
        /// </summary>
        /// <param name="html">HTML源码</param>
        /// <param name="className">class值</param>
        /// <returns></returns>
        public static string[] GetElementsByClass(string html, string className)
        {
            return GetElements(html, "", className);
        }
        #endregion
        #region 通过标签名获取标签集合
        /// <summary>
        /// 通过标签名获取标签集合
        /// </summary>
        /// <param name="html">HTML源码</param>
        /// <param name="tagName">标签名(如div)</param>
        /// <returns></returns>
        public static string[] GetElementsByTagName(string html, string tagName)
        {
            return GetElements(html, tagName, "");
        }
        #endregion
        #region 通过同时指定标签名+class值获取标签集合
        /// <summary>
        /// 通过同时指定标签名+class值获取标签集合
        /// </summary>
        /// <param name="html">HTML源码</param>
        /// <param name="tagName">标签名</param>
        /// <param name="className">class值</param>
        /// <returns></returns>
        public static string[] GetElementsByTagAndClass(string html, string tagName, string className)
        {
            return GetElements(html, tagName, className);
        }
        #endregion
        #region 通过同时指定标签名+class值获取标签集合（内部方法）
        /// <summary>
        /// 通过同时指定标签名+class值获取标签集合（内部方法）
        /// </summary>
        /// <param name="html"></param>
        /// <param name="tagName"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        private static string[] GetElements(string html, string tagName, string className)
        {
            string pattern = "";
            if (tagName != "" && className != "")
            {
                pattern = @"<({0})(?:(?!class)[^<>])*class=([""']?){1}\2[^>]*>(?>(?<o><\1[^>]*>)|(?<-o></\1>)|(?:(?!</?\1).))*(?(o)(?!))</\1>";
                pattern = string.Format(pattern, Regex.Escape(tagName), Regex.Escape(className));
            }
            else if (tagName != "")
            {
                pattern = @"<({0})(?:[^<>])*>(?>(?<o><\1[^>]*>)|(?<-o></\1>)|(?:(?!</?\1).))*(?(o)(?!))</\1>";
                pattern = string.Format(pattern, Regex.Escape(tagName));
            }
            else if (className != "")
            {
                pattern = @"<([a-z]+)(?:(?!class)[^<>])*class=([""']?){0}\2[^>]*>(?>(?<o><\1[^>]*>)|(?<-o></\1>)|(?:(?!</?\1).))*(?(o)(?!))</\1>";
                pattern = string.Format(pattern, Regex.Escape(className));
            }
            if (pattern == "")
            {
                return new string[] { };
            }
            var list = new List<string>();
            var reg = new Regex(pattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);
            var match = reg.Match(html);
            while (match.Success)
            {
                list.Add(match.Value);
                match = reg.Match(html, match.Index + match.Length);
            }
            return list.ToArray();
        }
        /// <summary>
        /// 根据正则获取内容
        /// </summary>
        /// <param name="text">内容</param>
        /// <param name="pat">正则</param>
        public static List<string> GetListByHtml(string text, string pat)
        {
            var list = new List<string>();
            var r = new Regex(pat, RegexOptions.IgnoreCase);
            var m = r.Match(text);
            //int matchCount = 0;
            while (m.Success)
            {
                list.Add(m.Value);
                m = m.NextMatch();
            }
            return list;
        }
        /// <summary>
        /// 根据正则获取内容
        /// </summary>
        /// <param name="text">内容</param>
        /// <param name="pat">正则</param>
        public static string GetResultByHtml(string text, string pat)
        {
            var result = string.Empty;
            var r = new Regex(pat, RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.Singleline);
            var m = r.Match(text);
            //int matchCount = 0;
            while (m.Success)
            {
                result = m.Value;
                break;
            }
            return result;
        }
        /// <summary>
        /// 去除html标签
        /// </summary>
        /// <param name="htmlstring"></param>
        /// <returns></returns>
        public static string NoHtml(string htmlstring)
        {
            //删除脚本
            htmlstring = Regex.Replace(htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML
            htmlstring = Regex.Replace(htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            // Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            htmlstring = htmlstring.Replace("<", "");
            htmlstring = htmlstring.Replace(">", "");
            htmlstring = htmlstring.Trim();
            return htmlstring;
        }
        /// <summary>
        /// 取得HTML中所有图片的 URL。
        /// </summary>
        /// <param name="sHtmlText">HTML代码</param>
        /// <returns>图片的URL列表</returns>
        public static string[] GetHtmlImageUrlList(string sHtmlText)
        {
            // 定义正则表达式用来匹配 img 标签
            var regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);
            // 搜索匹配的字符串
            var matches = regImg.Matches(sHtmlText);
            var i = 0;
            var sUrlList = new string[matches.Count];
            // 取得匹配项列表
            foreach (Match match in matches)
                sUrlList[i++] = match.Groups["imgUrl"].Value;
            return sUrlList;
        }
        /// <summary>
        /// 获取img的alt标签
        /// </summary>
        /// <param name="strHtml"></param>
        /// <returns></returns>
        public static string[] GetImagAltList(string strHtml)
        {
            // 定义正则表达式用来匹配 img 标签
            var regImg = new Regex(@"<img\b[^<>]*?\balt[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgAlt>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);
            // 搜索匹配的字符串
            var matches = regImg.Matches(strHtml);
            var i = 0;
            var sUrlList = new string[matches.Count];
            // 取得匹配项列表
            foreach (Match match in matches)
                sUrlList[i++] = match.Groups["imgAlt"].Value;
            return sUrlList;
        }
        /// <summary>
        /// 获取字符中指定标签的值
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="tag">标签</param>
        /// <returns>值</returns>
        public static string GetTagContent(string str, string tag)
        {
            var tmpStr = string.Format("<{0}[^>]*?>(?<Text>[^<]*)</{1}>", tag, tag); //获取<tag>之间内容
            var titleMatch = Regex.Match(str, tmpStr, RegexOptions.IgnoreCase);
            var result = titleMatch.Groups["Text"].Value;
            return result;
        }
        /// <summary>
        /// 获取字符中指定标签的值
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="tag">标签</param>
        /// <param name="attrib">属性名</param>
        /// <returns>属性</returns>
        public static string GetTagContent(string str, string tag, string attrib)
        {
            var tmpStr = string.Format("<{0}[^>]*?{1}=(['\"\"]?)(?<url>[^'\"\"\\s>]+)\\1[^>]*>", tag, attrib); //获取<tag>之间内容
            var titleMatch = Regex.Match(str, tmpStr, RegexOptions.IgnoreCase);
            var result = titleMatch.Groups["url"].Value;
            return result;
        }
        #endregion

        public static string GetJsParams(string html, string temp)
        {
            var gLoginSindex = html.IndexOf(temp, StringComparison.Ordinal);
            var tempResult = html.Substring(gLoginSindex + temp.Length);
            var result = tempResult.Substring(0, tempResult.IndexOf("\"", StringComparison.Ordinal));
            return result;
        }

        #region 根据参数名称，获取url的参数值
        /// <summary>
        /// 根据参数名称，获取url的参数值
        /// </summary>
        /// <param name="url"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetUrlParam(string url, string name)
        {
            var value = string.Empty;
            var uri = new Uri(url);
            var array = uri.Query.Replace("?", "").Split("&".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in array.Select(str => str.Split("=".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
                .Where(item => item[0] == name))
            {
                value = item[1];
                break;
            }
            return value;
        }
        #endregion

        /// <summary>
        /// 取得当前的unix时间
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static long FetchUnixTime(DateTime time)
        {
            var dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return Convert.ToInt64(Math.Floor((time - dtStart).TotalMilliseconds));
        }
    }
}
