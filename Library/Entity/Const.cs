using System.Text.RegularExpressions;

namespace Library.Entity
{
    public enum ExtType
    {
        Scheme,
        String
    }

    public enum LexiconType
    {
        Custom,
        Blur,
        Regex
    }

    public enum SenderType
    {
        Master,
        Friend,
        Custom
    }

    public enum GroupSenderType
    {
        Master,
        Custom
    }

    public enum PollType
    {
        // ReSharper disable InconsistentNaming
        message,
        kick_message,//被迫下线
        group_message,//表示群消息
        group_web_message,
        sys_g_msg,      
        shake_message,
        discu_message,
        sess_message,
        buddies_status_change,//好友上下线
        buddylist_change
        // ReSharper restore InconsistentNaming
    }

    public enum MessageType
    {
        Chat,
        Teach,
        Delete,
        Set,
        Regex,
        Push,
        Test,       
        Ext,
        Scheme
    }

    public static class AnalyseRegex
    {
        public static Regex Teach = new Regex(@"\$teach:([\s\S]+?)=>([\s\S]+?)$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        public static Regex Delete = new Regex(@"\$delete:([\s\S]+?)$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        public static Regex Set = new Regex(@"\$set:([\d]+) ([\d]+) ([\d]+)$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        public static Regex Regex = new Regex(@"\$regex:([\s\S]+?)=>([\s\S]+?)$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        public static Regex Push = new Regex(@"\$push:([\s\S]+?)$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        public static Regex Test = new Regex(@"\$test:([\s\S]+?)$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        public static Regex Ext = new Regex(@"\$ext\[([\s\S]+?)\]:([\s\S]+?)=>([\s\S]+?)$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        public static Regex Scheme = new Regex(@"^\$scheme$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    }
}
