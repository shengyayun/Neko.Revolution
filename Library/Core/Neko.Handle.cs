using System;
using System.Globalization;
using Library.Common;
using Library.Entity;

namespace Library.Core
{
    public partial class Neko
    {
        private void HandlePoll2Result(Poll2SuccessResult p2Sr)
        {
            var results = p2Sr.result;
            foreach (var result in results)
            {
                switch (result.poll_type)
                {
                    case PollType.buddylist_change:
                        lock (LockHelper)
                        {
                            LoadFriends();
                        }
                        LogHelper.Info("好友信息重新载入");
                        break;
                    case PollType.message:
                        {
                            var message = ResolveContent(result.value["content"]);
                            if (string.IsNullOrEmpty(message)) continue;
                            Reply(message, FetchPersonalSender(result.value["from_uin"].ToString(), ""));
                        }
                        break;
                    case PollType.group_message:
                        {
                            var message = ResolveContent(result.value["content"]);
                            if (string.IsNullOrEmpty(message)) continue;
                            long fromUin = Convert.ToInt64(result.value["from_uin"]);
                            var sendUin = result.value["send_uin"].ToString();
                            Reply(message, FetchPersonalSender(sendUin, fromUin.ToString(CultureInfo.InvariantCulture)), FetchGroupSender(fromUin));
                        }
                        break;
                }
            }
        }

        private static string ResolveContent(dynamic content)
        {
            var result = "";
            foreach (var item in content)
            {
                result += item is string ? item.ToString() : "";
            }
            return result.Trim();
        }
    }
}
