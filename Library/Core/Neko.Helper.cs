using System;
using System.Linq;
using System.Text.RegularExpressions;
using Library.Entity;

namespace Library.Core
{
    public partial class Neko
    {
        /// <summary>
        /// 判断收到的message的类型
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private MessageType Analyse(string message)
        {
            if (AnalyseRegex.Teach.IsMatch(message))
            {
                //普通教学
                return MessageType.Teach;
            }
            if (AnalyseRegex.Regex.IsMatch(message))
            {
                //正则教学
                return MessageType.Regex;
            }
            if (AnalyseRegex.Delete.IsMatch(message))
            {
                //删除
                return MessageType.Delete;
            }
            if (AnalyseRegex.Set.IsMatch(message))
            {
                //设置回复率
                return MessageType.Set;
            }
            if (AnalyseRegex.Push.IsMatch(message))
            {
                //推送
                return MessageType.Push;
            }
            if (AnalyseRegex.Test.IsMatch(message))
            {
                //测试
                return MessageType.Test;
            }
            if (AnalyseRegex.Scheme.IsMatch(message))
            {
                //设置Xml
                return MessageType.Scheme;
            }
            if (AnalyseRegex.Ext.IsMatch(message))
            {
                //设置Xml
                return MessageType.Ext;
            }

            return MessageType.Chat;
        }

        #region 检查权限
        /// <summary>
        /// 检查是否有命令权限
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        public bool CheckPermission(Sender sender)
        {
            return (sender.type == SenderType.Master);
        }

        /// <summary>
        /// 检查是否有命令权限
        /// </summary>
        /// <param name="groupSender"></param>
        /// <param name="sender"></param>
        /// <returns></returns>
        public bool CheckPermission(Sender sender, GroupSender groupSender)
        {
            return (sender.type == SenderType.Master) || (groupSender.type == GroupSenderType.Master);
        }
        #endregion

        #region 关注程度
        /// <summary>
        /// 加关注(最高5点关注)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="degree"></param>
        public void AddAttention(Sender sender, int degree)
        {
            for (var i = 0; i < degree; i++)
            {
                if (RunTime.Attention.Count(p => p == sender.uin) == 5) break;
                RunTime.Attention.Add(sender.uin);
            }
        }

        /// <summary>
        /// 失去关注
        /// </summary>
        /// <param name="sender"></param>
        public void LoseAttention(Sender sender)
        {
            RunTime.Attention.Remove(sender.uin);
        }

        /// <summary>
        /// 是否随机插话
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        public bool RandomAttention(Sender sender)
        {
            double rate = 0d;
            switch (sender.type)
            {
                case SenderType.Master:
                    rate = Settings.MasterRate;
                    break;
                case SenderType.Friend:
                    rate = Settings.FriendRate;
                    break;
                case SenderType.Custom:
                    rate = Settings.CustomRate;
                    break;
            }
            if (RunTime.Attention.Any(p => p == sender.uin))
            {
                rate += 0.5;
            }
            return new Random().NextDouble() < rate;
        }

        #endregion

        public static string ConstString(string name)
        {
            return Settings.ExtSettings.First(p => p.Type == ExtType.String).Settings[name];
        }

        public Regex BlurValue2Regex(string value)
        {
            var regexStr = RunTime.HolderRegex.Replace(value, "&temp;");
            regexStr = EscapeRegex(regexStr);
            return new Regex("^" + regexStr.Replace("&temp;", "(.*)") + "$");
        }

        public Regex BlurKey2Regex(string key)
        {
            var regexStr = key.Replace("*", "&temp;");
            regexStr = EscapeRegex(regexStr);
            return new Regex("^" + regexStr.Replace("&temp;", "(.*)") + "$", RegexOptions.IgnoreCase);
        }

        public string EscapeRegex(string regexStr)
        {
            regexStr = regexStr.Replace(@"\", @"\\");
            regexStr = regexStr.Replace("$", @"\$");
            regexStr = regexStr.Replace("(", @"\(");
            regexStr = regexStr.Replace(")", @"\)");
            regexStr = regexStr.Replace("[", @"\[");
            regexStr = regexStr.Replace("]", @"\]");
            regexStr = regexStr.Replace("{", @"\{");
            regexStr = regexStr.Replace("}", @"\}");
            regexStr = regexStr.Replace("*", @"\*");
            regexStr = regexStr.Replace("+", @"\+");
            regexStr = regexStr.Replace("?", @"\?");
            regexStr = regexStr.Replace("^", @"\^");
            regexStr = regexStr.Replace("|", @"\|");
            regexStr = regexStr.Replace(".", @"\.");
            return regexStr;
        }
    }
}
