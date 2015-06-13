using System.Linq;
using Library.Entity;

namespace Library.Core
{
    public partial class Neko
    {
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="uin"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        private Sender FetchPersonalSender(string uin, string group)
        {
            var sender = RunTime.Senders.FirstOrDefault(p => p.uin == uin);
            var name = RunTime.NickNames.FirstOrDefault(p => p.group == group && p.uin == uin);
            if (sender == null)
            {
                sender = new Sender { uin = uin, type = SenderType.Custom };
            }
            sender.name = name == null ? "大姐姐" : name.name;
            if (RunTime.FriendMasks.ContainsKey(uin))
            {
                sender.name = RunTime.FriendMasks[sender.uin];
            }
            return sender;
        }

       /// <summary>
       /// 获取群信息
       /// </summary>
       /// <param name="uin"></param>
       /// <returns></returns>
        private GroupSender FetchGroupSender(long uin)
        {
            var groupsender = RunTime.GroupSenders.FirstOrDefault(p => p.uin == uin);
            return groupsender ?? new GroupSender { uin = uin, type = GroupSenderType.Custom };
        }
    }
}
