using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using Library.Common;
using Library.Entity;
using Library.Interface;
using MSScriptControl;

namespace Library.Components
{
    public class NetDao : INet
    {
        public Userfriends2SuccessResult LoadFriends()
        {
            var hashCode = Hash(Settings.QQ, RunTime.Ptwebqq);
            var item = new Userfriends2Params { vfwebqq = RunTime.Vfwebqq, hash = hashCode };
            var jss = new JavaScriptSerializer();
            var data = "r=" + HttpUtility.UrlEncode(jss.Serialize(item));
            var str = HttpHelper.Post("http://s.web2.qq.com/api/get_user_friends2", data,
                "s.web2.qq.com", "", "http://s.web2.qq.com/proxy.html?v=20130916001&callback=1&id=1");
            var result = jss.Deserialize<Userfriends2SuccessResult>(str);
            return result;
        }

        public Groups2SuccessResult LoadGroups()
        {
            var hashCode = Hash(Settings.QQ, RunTime.Ptwebqq);
            var jss = new JavaScriptSerializer();
            var item = new Groups2Params { vfwebqq = RunTime.Vfwebqq, hash = hashCode };
            var data = "r=" + HttpUtility.UrlEncode(jss.Serialize(item));
            var str = HttpHelper.Post("http://s.web2.qq.com/api/get_group_name_list_mask2",
            data, "s.web2.qq.com", "", "http://s.web2.qq.com/proxy.html?v=20130916001&callback=1&id=1");
            return jss.Deserialize<Groups2SuccessResult>(str);
        }

        public GroupInfoExt2 LoadGroupDetail(string gcode)
        {
            var url = String.Format("http://s.web2.qq.com/api/get_group_info_ext2?gcode={0}&vfwebqq={1}&t={2}"
                    , gcode, RunTime.Vfwebqq, HtmlHelper.FetchUnixTime(DateTime.Now));
            var str = HttpHelper.Get(url, "s.web2.qq.com", "", "http://s.web2.qq.com/proxy.html?v=20130916001&callback=1&id=1");
            return new JavaScriptSerializer().Deserialize<GroupInfoExt2>(str);
        }

        public FriendUin Uin2Qq(string uin)
        {
            var url = String.Format("http://s.web2.qq.com/api/get_friend_uin2?tuin={0}&type=1&vfwebqq={1}&t={2}"
                    , uin, RunTime.Vfwebqq, HtmlHelper.FetchUnixTime(DateTime.Now));
            var str = HttpHelper.Get(url, "s.web2.qq.com", "", "http://s.web2.qq.com/proxy.html?v=20130916001&callback=1&id=1");
            return new JavaScriptSerializer().Deserialize<FriendUin>(str);
        }

        public SendMsgSuccessResult SendGroupMsg(long gid, string message)
        {
            var jss = new JavaScriptSerializer();
            message = message.Replace(@"\", @"\\\\").Replace("\r", @"\\r");
            var data = "{\"group_uin\":" + gid + ",\"content\":\"[\\\"" + message + "\\\"," +
                       "[\\\"font\\\",{\\\"name\\\":\\\"黑体\\\",\\\"size\\\":10,\\\"style\\\":[120,0,0],\\\"color\\\":\\\"780000\\\"}]]\"," +
                       "\"face\":519,\"clientid\":" + RunTime.ClientId + ",\"msg_id\":" + new Random().Next(10000000) + ",\"psessionid\":\"" +
                       RunTime.Psessionid + "\"}";
            data = "r=" + HttpUtility.UrlEncode(data);
            var str = HttpHelper.Post("http://d.web2.qq.com/channel/send_qun_msg2",
                data, "d.web2.qq.com", "", "http://d.web2.qq.com/proxy.html?v=20130916001&callback=1&id=2");
            return jss.Deserialize<SendMsgSuccessResult>(str);
        }

        public SendMsgSuccessResult SendMessage(string uin, string message)
        {
            var jss = new JavaScriptSerializer();
            message = message.Replace(@"\", @"\\\\").Replace("\r", @"\\r");
            var data = "{\"to\":" + uin + ",\"face\":0,\"content\":\"[\\\"" + message + "\\\"," +
                       "[\\\"font\\\",{\\\"name\\\":\\\"黑体\\\",\\\"size\\\":\\\"10\\\",\\\"style\\\":[120,0,0],\\\"color\\\":\\\"780000\\\"}]]\"," +
                       "\"msg_id\":" + new Random().Next(10000000) + ",\"clientid\":\"" + RunTime.ClientId + "\"," +
                       "\"psessionid\":\"" + RunTime.Psessionid + "\"}";
            data = "r=" + HttpUtility.UrlEncode(data);
            var str = HttpHelper.Post("http://d.web2.qq.com/channel/send_buddy_msg2", data,
                "d.web2.qq.com", "", "http://d.web2.qq.com/proxy.html?v=20130916001&callback=1&id=2");
            return jss.Deserialize<SendMsgSuccessResult>(str);
        }

        public string Hash(string qq, string ptwebqq)
        {
            const string url = "http://0.web.qstatic.com/webqqpic/pubapps/0/50/eqq.all.js";
            //获取hash 函数代码所在JS文件
            var jsContent = HttpHelper.Get(url);
            var index = jsContent.IndexOf("P=function(", StringComparison.Ordinal);
            var end = jsContent.IndexOf(",b=function(b){c.out", StringComparison.Ordinal) - 1;
            var hashJs = jsContent.Substring(index, end - index + 1);
            var js = new ScriptControlClass { Language = "javascript" };//使用ScriptControlClass  
            js.Reset();
            js.Eval(hashJs);
            return js.Run("P", new object[] { qq, ptwebqq }).ToString();
        }

        public Poll2SuccessResult Poll2(Poll2Params source)
        {
            var jss = new JavaScriptSerializer();
            var data = "r=" + HttpUtility.UrlEncode(jss.Serialize(source));
            var json = HttpHelper.Post("http://d.web2.qq.com/channel/poll2", data, "d.web2.qq.com", "", "http://d.web2.qq.com/proxy.html?v=20130916001&callback=1&id=2");
            var ser = new JavaScriptSerializer();
            return ser.Deserialize<Poll2SuccessResult>(json);
        }

        public bool CheckLogin()
        {
            const string url = "http://w.qq.com/login.html";
            var html = HttpHelper.Get(url);
            //  var iframeUrl = HtmlHelper.GetTagContent(html, "iframe", "src");
            var iframeUrl = Regex.Match(html, @"(https://ui\.ptlogin2\.qq\.com/cgi-bin/.*)&f_qr", RegexOptions.IgnoreCase)
                .Groups[1].Value;
            var loginHtml = HttpHelper.Get(iframeUrl);

            RunTime.Appid = HtmlHelper.GetUrlParam(iframeUrl, "appid");
            RunTime.LoginSig = HtmlHelper.GetJsParams(loginHtml, "var g_login_sig=encodeURIComponent(\"");
            RunTime.PltVersion = HtmlHelper.GetJsParams(loginHtml, "var g_pt_version=encodeURIComponent(\"");
            RunTime.Daid = HtmlHelper.GetJsParams(loginHtml, "var g_daid=encodeURIComponent(\"");
            var checkUrl =
                "https://ssl.ptlogin2.qq.com/check?uin={0}&appid={1}&js_ver=10082&js_type=0&login_sig={2}&u1=http%3A%2F%2Fw.qq.com%2Fproxy.html&r=" +
                new Random().NextDouble();
            checkUrl = string.Format(checkUrl, Settings.QQ, RunTime.Appid, RunTime.LoginSig);
            var htmlContent = HttpHelper.Get(checkUrl);
            var array =
                htmlContent.Replace("ptui_checkVC(", "").Replace("'", "").Replace(")", "").Split(",".ToCharArray());
            RunTime.Yzm = array[1];
            RunTime.Uin = array[2];
            //第一个0表示不需要验证码，1表示需要验证码
            return array[0] == "0";
        }

        public byte[] FetchYzm()
        {
            HttpHelper.CookieContainers.Add(new Cookie("chkuin", Settings.QQ, "/", ".ptlogin2.qq.com"));
            HttpHelper.ModifyCookie(new Cookie("confirmuin", Settings.QQ, "/", ".ptlogin2.qq.com"));
            var url = "https://ssl.captcha.qq.com/getimage?aid="
                      + RunTime.Appid + "&r=" + new Random().NextDouble() + "&uin=" + Settings.QQ;
            var stream = HttpHelper.GetResponseImage(url, "ssl.captcha.qq.com", "", "https://ui.ptlogin2.qq.com/cgi-bin/login?daid=164&target=self&style=16&mibao_css=m_webqq&appid=501004106&enable_qlogin=0&no_verifyimg=1&s_url=http%3A%2F%2Fw.qq.com%2Fproxy.html&f_url=loginerroralert&strong_login=1&login_state=10&t=20131024001");
            return stream;
        }

        public bool Login(out string msg)
        {
            var url = "https://ssl.ptlogin2.qq.com/login?u=" + Settings.QQ + "&p=" + MD5Helper.Md5(Settings.Pwd, RunTime.Uin, RunTime.Yzm)
                + "&verifycode=" + RunTime.Yzm + "&webqq_type=10&remember_uin=1&login2qq=1&aid=" + RunTime.Appid +
                      "&u1=http%3A%2F%2Fw.qq.com%2Fproxy.html%3Flogin2qq%3D1%26webqq_type%3D10&h=1&ptredirect=0&ptlang=2052&daid=" +
                      RunTime.Daid + "&from_ui=1&pttype=1&dumy=&fp=loginerroralert&action=0-42-29540&mibao_css=m_webqq&t=3&g=1&js_type=0&js_ver=" +
                      RunTime.PltVersion + "&login_sig=" + RunTime.LoginSig;
            var html = HttpHelper.Get(url);
            var array = html.Replace("ptuiCB(", "")
                            .Replace("'", "")
                            .Replace(")", "")
                            .Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if (!array[4].Contains("登录成功！"))
            {
                RunTime.ClientId = 0;
                RunTime.Ptwebqq = null;
                msg = array[3];
                return false;
            }
            var redirectUrl = array[2];
            HttpHelper.Get(redirectUrl);
            url = "http://d.web2.qq.com/channel/login2";
            RunTime.ClientId = new Random().Next(10000000, 100000000);
            //var data = "{\"status\":\"online\",\"ptwebqq\":\"" + HttpHelper.GetCookie(url, "ptwebqq") + "\",\"clientid\":" + clientid + ",\"psessionid\":\"\"}";
            RunTime.Ptwebqq = HttpHelper.GetCookie(url, "ptwebqq");
            var l2P = new Login2Params
            {
                clientid = RunTime.ClientId,
                online = "",
                psessionid = "",
                ptwebqq = RunTime.Ptwebqq,
                status = ""
            };
            var jss = new JavaScriptSerializer();
            var data = "r=" + HttpUtility.UrlEncode(jss.Serialize(l2P));
            var result = HttpHelper.Post(url, data, "d.web2.qq.com", "", "http://d.web2.qq.com/proxy.html?v=20130916001&callback=1&id=2");
            var lsr = jss.Deserialize<Login2SuccessResult>(result);
            if (lsr == null)
            {
                msg = "无法连接服务器";
            }
            else
            {
                msg = "登录成功";
                RunTime.Psessionid = lsr.result.psessionid;
                RunTime.Vfwebqq = lsr.result.vfwebqq;
            }
            return true;
        }


    }
}
