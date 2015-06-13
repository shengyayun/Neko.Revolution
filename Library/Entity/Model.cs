using System.Collections.Generic;
// ReSharper disable InconsistentNaming
namespace Library.Entity
{
    #region 登录
    /// <summary>
    /// 用户登录时提交的参数
    /// </summary>
    public class Login2Params
    {
        public string status { get; set; }
        public string online { get; set; }
        public string ptwebqq { get; set; }
        public long clientid { get; set; }
        public string psessionid { get; set; }
    }
    /// <summary>
    /// 提交login2登录
    /// </summary>
    public class Login2SuccessResult
    {
        public string retcode { get; set; }
        public Login2Result result { get; set; }
    }

    public class Login2Result
    {
        public string uin { get; set; }
        public string cip { get; set; }
        public string index { get; set; }
        public string port { get; set; }
        public string status { get; set; }
        public string vfwebqq { get; set; }
        public string psessionid { get; set; }
        public string user_state { get; set; }
        public string f { get; set; }
    }
    #endregion

    #region 心跳
    public class Poll2Params
    {
        public string ptwebqq { get; set; }
        public long clientid { get; set; }
        public string psessionid { get; set; }
        public string key { get; set; }
    }
    /*
    返回103、121，代表连接不成功，需要重新登录；
    返回102，代表连接正常，此时服务器暂无信息；
    返回0，代表服务器有信息传递过来：包括群信、群成员给你的发信，QQ好友给你的发信。
     */
    public class Poll2SuccessResult
    {
        public string retcode { get; set; }

        public List<Poll2Result> result { get; set; }

    }

    public class Poll2Result
    {
        public PollType poll_type { get; set; }

        public dynamic value { get; set; }
    }

    #endregion

    #region 取得好友
    public class Userfriends2Params
    {
        public string vfwebqq { get; set; }
        public string hash { get; set; }
    }

    public class Userfriends2SuccessResult
    {
        public string retcode { get; set; }
        public Userfriends2Result result { get; set; }
    }

    public class Userfriends2Result
    {
        public List<Userfriends2Friends> friends { get; set; }
        public List<Userfriends2Marknames> marknames { get; set; }
        public List<Userfriends2Categories> categories { get; set; }
        public List<Userfriends2Vipinfo> vipinfo { get; set; }
        public List<Userfriends2Info> info { get; set; }
    }

    public class Userfriends2Friends
    {
        public string flag { get; set; }
        public string uin { get; set; }
        public string categories { get; set; }
    }

    public class Userfriends2Marknames
    {
        public string uin { get; set; }
        public string markname { get; set; }
        public string type { get; set; }
    }

    public class Userfriends2Categories
    {
        public string index { get; set; }
        public string sort { get; set; }
        public string name { get; set; }
    }

    public class Userfriends2Vipinfo
    {
        public string vip_level { get; set; }
        public string u { get; set; }
        public string is_vip { get; set; }
    }

    public class Userfriends2Info
    {
        public string face { get; set; }
        public string flag { get; set; }
        public string nick { get; set; }
        public string uin { get; set; }
    }
    #endregion

    #region 取得群

    public class Groups2Params
    {
        public string vfwebqq { get; set; }
        public string hash { get; set; }
    }

    public class Groups2SuccessResult
    {
        public string retcode { get; set; }
        public Group2Result result { get; set; }
    }

    public class Group2Result
    {
        public List<dynamic> gmasklist { get; set; }
        public List<Group2Gnamelist> gnamelist { get; set; }
        public List<Group2Gmarklist> gmarklist { get; set; }
    }

    public class Group2Gnamelist
    {
        public string flag { get; set; }
        public string name { get; set; }
        public long gid { get; set; }
        public string code { get; set; }
    }

    public class Group2Gmarklist
    {
        public long uin { get; set; }
        public string markname { get; set; }
    }

    public class GroupInfoParams
    {
        public string gcode { get; set; }
        public long t { get; set; }
        public string vfwebqq { get; set; }
    }

    public class GroupInfoItem
    {
        public long gid { get; set; }
        public Dictionary<string, string> members { get; set; }
    }

    #endregion

    #region 发送消息

    public class SendMsgSuccessResult
    {
        public long retcode { get; set; }
        public string result { get; set; }
    }

    #endregion

    #region 好友

    public class Sender
    {
        public string uin { get; set; }
        public string qq { get; set; }
        public string name { get; set; }
        public SenderType type { get; set; }
    }

    public class GroupSender
    {
        public long uin { get; set; }
        public GroupSenderType type { get; set; }
    }

    public class NameItem
    {
        public string uin { get; set; }
        public string name { get; set; }
        public string group { get; set; }
    }

    #endregion

    #region 群成员
    public class GroupInfoExt2
    {
        public string retcode { get; set; }
        public GroupInfoExt2Result result { get; set; }
    }

    public class GroupInfoExt2Result
    {
        public dynamic stats { get; set; }
        public List<Minfo> minfo { get; set; }
        public dynamic ginfo { get; set; }
        //可能没有cards
        public List<Card> cards { get; set; }
        public dynamic vipinfo { get; set; }
    }

    public class Minfo
    {
        public string nick { get; set; }
        public string province { get; set; }
        public string gender { get; set; }
        public string uin { get; set; }
        public string country { get; set; }
        public string city { get; set; }
    }

    public class Card
    {
        public string muin { get; set; }
        public string card { get; set; }
    }

    #endregion

    #region qq号取得

    public class FriendUin
    {
        public string retcode { get; set; }
        public FriendUinResult result { get; set; }
    }

    public class FriendUinResult
    {
        public string uiuin { get; set; }
        public string account { get; set; }

        public string uin { get; set; }

    }
    #endregion
}
// ReSharper restore InconsistentNaming
