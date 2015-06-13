using System;
using System.Globalization;
using System.Linq;
using Library.Common;
using Library.Entity;

namespace Library.Core
{
    public partial class Neko
    {
        /// <summary>
        /// 处理有关群的信息
        /// </summary>
        public bool LoadGroups()
        {
            try
            {
                var groups = RunTime.Net.LoadGroups();
                var result = groups.result.gmarklist.Select(p => new GroupSender { uin = p.uin, type = GroupSenderType.Master }).ToList();
                var uins = groups.result.gnamelist.Select(p => p.gid).ToList();
                foreach (var group in groups.result.gnamelist)
                {
                    var r = RunTime.Net.LoadGroupDetail(group.code);
                    if (r != null && r.retcode == "0")
                    {
                        var minfos = r.result.minfo;
                        var gid = group.gid.ToString(CultureInfo.InvariantCulture);
                        foreach (var minfo in minfos)
                        {
                            RunTime.NickNames.Add(new NameItem { group = gid, uin = minfo.uin, name = minfo.nick });
                        }
                        if (r.result.cards != null)
                        {
                            foreach (var card in r.result.cards)
                            {
                                var item = RunTime.NickNames.FirstOrDefault(p => p.group == gid && p.uin == card.muin);
                                if (item != null)
                                    item.name = card.card;
                            }
                        }
                    }
                }
                RunTime.GroupSenders = result;
                RunTime.GroupUins = uins;
                return true;
            }
            catch (Exception e)
            {
                LogHelper.Info("处理有关群的信息失败：{0}", e.Message);
                return false;
            }
        }

        /// <summary>
        /// 处理有关好友的信息
        /// </summary>
        public bool LoadFriends()
        {
            try
            {
                var friends = RunTime.Net.LoadFriends();
                var masterCategory = friends.result.categories.FirstOrDefault(p => p.name == "Masters");
                if (masterCategory == null)
                {
                    LogHelper.Info("Masters组读取失败");
                }
                var result = friends.result.friends.Select(sender =>
                {
                    //私聊中用的昵称
                    var firstOrDefault = friends.result.info.FirstOrDefault(p => p.uin == sender.uin);
                    RunTime.NickNames.Add(new NameItem
                    {
                        name = firstOrDefault == null ? "大姐姐" : firstOrDefault.nick,
                        uin = sender.uin,
                        group = ""
                    });
                    return new Sender
                    {
                        uin = sender.uin,
                        qq = "",
                        type = (masterCategory != null && sender.categories == masterCategory.index ? SenderType.Master : SenderType.Friend)
                    };
                }).ToList();
                for (var i = 0; i < result.Count(); i++)
                {
                    //取得qq号
                    var item = RunTime.Net.Uin2Qq(result[i].uin);
                    if (item != null && item.retcode == "0")
                    {
                        result[i].qq = item.result.account;
                    }
                }
                //好友的备注名
                if (friends.result.marknames != null)
                {
                    foreach (var a in friends.result.marknames)
                    {
                        RunTime.FriendMasks.Add(a.uin, a.markname);
                    }
                }
                RunTime.Senders = result;
                return true;
            }
            catch (Exception e)
            {
                LogHelper.Info("处理有关好友的信息失败：{0}", e.Message);
                return false;
            }
        }
    }
}
