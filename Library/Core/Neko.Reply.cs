using System;
using System.Threading;
using Library.Entity;

namespace Library.Core
{
    public partial class Neko
    {
        public void Reply(string message, Sender sender)
        {
            var type = Analyse(message);
            //判断是否有权限    
            var flag = CheckPermission(sender);
            var result = "";
            switch (type)
            {
                case MessageType.Teach:
                    result = flag ? TeachAction(message) : ConstString("NoThanks");
                    break;
                case MessageType.Regex:
                    result = flag ? RegexAction(message) : ConstString("NoThanks");
                    break;
                case MessageType.Delete:
                    result = flag ? DeleteAction(message) : ConstString("NoThanks");
                    break;
                case MessageType.Set:
                    result = flag ? SetAction(message) : ConstString("NoThanks");
                    break;
                case MessageType.Test:
                    result = flag ? TestAction(message) : ConstString("NoThanks");
                    break;
                case MessageType.Scheme:
                    result = flag ? SchemeAction(sender.name) : ConstString("NoThanks");
                    break;
                case MessageType.Ext:
                    result = flag ? ExtAction(message) : ConstString("NoThanks");
                    break;
                case MessageType.Push:
                    result = flag ? PushAction(message, sender.name) : ConstString("NoThanks");
                    break;
                case MessageType.Chat:
                    result = ChatAction(message) ?? ConstString("Require");
                    break;
            }
            //处理占位符
            var msgs = HandlePlaceHolder(result, sender.name);
            foreach (var msg in msgs)
            {
                Thread.Sleep(500);
                RunTime.Net.SendMessage(sender.uin, msg);
            }
        }

        public void Reply(string message, Sender sender, GroupSender groupSender)
        {
            //群聊不处理50字以上
            if (message.Length > 50) return;
            var type = Analyse(message);
            //判断是否有权限    
            var flag = CheckPermission(sender);
            var result = "";
            switch (type)
            {
                case MessageType.Teach:
                    result = flag ? TeachAction(message) : ConstString("NoThanks");
                    AddAttention(sender, 2);
                    break;
                case MessageType.Regex:
                    result = flag ? RegexAction(message) : ConstString("NoThanks");
                    AddAttention(sender, 2);
                    break;
                case MessageType.Delete:
                    result = flag ? DeleteAction(message) : ConstString("NoThanks");
                    AddAttention(sender, 2);
                    break;
                case MessageType.Set:
                    result = flag ? SetAction(message) : ConstString("NoThanks");
                    AddAttention(sender, 2);
                    break;
                case MessageType.Test:
                    result = flag ? TestAction(message) : ConstString("NoThanks");
                    AddAttention(sender, 2);
                    break;
                case MessageType.Scheme:
                    result = flag ? SchemeAction(sender.name) : ConstString("NoThanks");
                    AddAttention(sender, 2);
                    break;
                case MessageType.Ext:
                    result = flag ? ExtAction(message) : ConstString("NoThanks");
                    AddAttention(sender, 2);
                    break;
                case MessageType.Push:
                    result = flag ? PushAction(message, sender.name) : ConstString("NoThanks");
                    AddAttention(sender, 2);
                    break;
                case MessageType.Chat:
                    flag = message.Contains(Settings.NekoName) || RandomAttention(sender);
                    LoseAttention(sender);
                    if (flag)
                    {
                        result = ChatAction(message);
                        AddAttention(sender, 2);
                        if (groupSender.type == GroupSenderType.Master && String.IsNullOrEmpty(result))
                        {
                            result = ConstString("Require");
                        }
                    }
                    break;
            }
            //在没有权限的群里 而且词库没有相关信息 不做回答
            if (string.IsNullOrEmpty(result)) return;
            //处理占位符
            var msgs = HandlePlaceHolder(result, sender.name);
            foreach (var msg in msgs)
            {
                Thread.Sleep(500);
                RunTime.Net.SendGroupMsg(groupSender.uin, msg);
            }
        }
    }
}
