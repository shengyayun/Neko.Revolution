using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using Library.Manager;

namespace Library.Entity
{
    public static class RunTime
    {
        #region 运行时参数
        public static string Appid { get; set; }

        public static string LoginSig { get; set; }

        public static string PltVersion { get; set; }

        public static string Daid { get; set; }

        public static string Yzm { get; set; }

        public static string Uin { get; set; }

        public static int ClientId { get; set; }

        public static string Ptwebqq { get; set; }

        public static string Vfwebqq { get; set; }

        public static string Psessionid { get; set; }
        #endregion

        #region 运行时用户信息
        public static NetMgr Net = new NetMgr();

        /// <summary>
        /// 配置文件的地址
        /// </summary>
        public static string SettingXmlPath;

        /// <summary>
        /// 日志根目录
        /// </summary>
        public static string LogRootPath = "";
        /// <summary>
        /// 我的好友
        /// </summary>
        public static List<Sender> Senders { get; set; }

        /// <summary>
        /// 我的群号（用于广播）
        /// </summary>
        public static List<long> GroupUins { get; set; }

        /// <summary>
        /// 我的群
        /// </summary>
        public static List<GroupSender> GroupSenders { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public static List<NameItem> NickNames = new List<NameItem>();

        /// <summary>
        /// 匹配[1]之类
        /// </summary>
        public static Regex HolderRegex = new Regex(@"\[\d\]", RegexOptions.Compiled);

        /// <summary>
        /// 好友备注名
        /// </summary>
        public static Dictionary<string, string> FriendMasks = new Dictionary<string, string>();
        #endregion

        /// <summary>
        /// 心跳结果队列
        /// </summary>
        public static Queue<Poll2SuccessResult> Poll2Results = new Queue<Poll2SuccessResult>();

        /// <summary>
        /// 聊天注意力（针对个人）
        /// </summary>
        public static List<string> Attention = new List<string>();

        /// <summary>
        /// 数据库
        /// </summary>
        public static DatabaseContext Database = new DatabaseContext();

        /// <summary>
        /// 心跳线程
        /// </summary>
        public static Thread Poll2Thread;

        /// <summary>
        /// 行为线程
        /// </summary>
        public static Thread ReplyThread;

        /// <summary>
        /// 定时线程
        /// </summary>
        public static Thread SchemeThread;
    }
}
