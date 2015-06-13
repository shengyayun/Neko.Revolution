using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Library.Common;
using Library.Entity;

namespace Library.Core
{
    public partial class Neko
    {
        /// <summary>
        /// 教学
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private string TeachAction(string message)
        {
            //将称谓换掉
            message = message.Replace(Settings.NekoName, "$(name)");
            var groups = AnalyseRegex.Teach.Match(message).Groups;
            //问题
            var key = groups[1].Value;
            //答案
            var value = groups[2].Value;
            var item = new Lexicon { LxKey = key, LxValue = value, LxType = Convert.ToInt16(key.Contains("*") ? 1 : 0), Time = Convert.ToDateTime("1900-01-01 00:00:00.000") };
            if (item.LxType == 1 && item.LxKey.Replace("*", "").Trim().Length == 0)
            {
                //只有通配符
                return ConstString("MeetAllError");
            }
            if (RunTime.Database.Lexicon.Any(p => p.LxKey == item.LxKey && p.LxValue == item.LxValue))
            {
                //词库已存在
                return ConstString("LexiconExistsError");
            }
            RunTime.Database.Lexicon.InsertOnSubmit(item);
            RunTime.Database.SubmitChanges();
            //教学成功
            return ConstString("Success");
        }

        /// <summary>
        /// 正则教学
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public string RegexAction(string message)
        {
            //将称谓换掉
            message = message.Replace(Settings.NekoName, "$(name)");
            var groups = AnalyseRegex.Regex.Match(message).Groups;
            //问题
            var key = groups[1].Value;
            //答案
            var value = groups[2].Value;
            var item = new Lexicon { LxKey = key, LxValue = value, LxType = 2 };
            if (RunTime.Database.Lexicon.Any(p => p.LxKey == item.LxKey && p.LxValue == item.LxValue))
            {
                return ConstString("LexiconExistsError");
            }
            RunTime.Database.Lexicon.InsertOnSubmit(item);
            RunTime.Database.SubmitChanges();
            return ConstString("Success");
        }

        /// <summary>
        /// 设置回复率
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public string SetAction(string message)
        {
            var groups = AnalyseRegex.Set.Match(message).Groups;
            Settings.MasterRate = Convert.ToDouble(groups[1].Value) / 100;
            Settings.FriendRate = Convert.ToDouble(groups[2].Value) / 100;
            Settings.CustomRate = Convert.ToDouble(groups[3].Value) / 100;
            return "成功设置:Master回复率" + groups[1].Value + "% 好友回复率" + groups[2].Value + "% 路人回复率" + groups[3].Value + "%";
        }

        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public string DeleteAction(string message)
        {
            try
            {
                // ReSharper disable PossibleMultipleEnumeration
                var msg = message.Replace("$delete:", "").Replace(Settings.NekoName, "$(name)");
                var lexicons = RunTime.Database.Lexicon.ToList();
                //完全匹配的
                var matchItems = lexicons.Where(p => p.LxValue == msg);
                var matchItemsValueArray = matchItems.Select(p => p.LxValue);
                //blur匹配或者regex匹配,没有被完全匹配,但存在[?]这种匹配可能
                var otherItems = lexicons.Where(p => p.LxType != 0
                    && !matchItemsValueArray.Contains(p.LxValue)
                    && RunTime.HolderRegex.IsMatch(p.LxValue)
                    && BlurValue2Regex(p.LxValue).IsMatch(msg)
                    );
                var rmsg = matchItems.Count() + "条完全匹配的违规数据已经被清除";
                if (otherItems.Any())
                {
                    rmsg += "\r其他匹配的数据有:";
                    rmsg = otherItems.Aggregate(rmsg, (current, bi) => current + ("\r" + bi.LxValue));
                }
                foreach (var matchItem in matchItems)
                {
                    RunTime.Database.Lexicon.DeleteOnSubmit(matchItem);
                }
                RunTime.Database.SubmitChanges();
                return rmsg;
                // ReSharper restore PossibleMultipleEnumeration
            }
            catch (Exception e)
            {
                return "删除操作发生错误:" + e.Message;
            }
        }

        /// <summary>
        /// 推送
        /// </summary>
        /// <param name="message"></param>
        /// <param name="sender"></param>
        /// <returns></returns>
        public string PushAction(string message, string sender)
        {
            message = message.Replace("$push:", "");
            foreach (var uin in RunTime.GroupUins)
            {
                var msgs = HandlePlaceHolder(message, sender);
                foreach (var msg in msgs)
                {
                    Thread.Sleep(1000);
                    RunTime.Net.SendGroupMsg(uin, msg);
                }
            }
            LogHelper.Info("{0} Pushed：{1}", sender, message);
            return "推送完成";
        }

        /// <summary>
        /// Level-C的Ext Xml设置
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public string ExtAction(string message)
        {
            LogHelper.Info(message);
            try
            {
                var groups = AnalyseRegex.Ext.Match(message).Groups;
                var name = groups[1].Value;
                var key = groups[2].Value;
                var value = groups[3].Value;
                ExtType type;
                if (Enum.TryParse(name, true, out type))
                {
                    ExtSettings.ModifySetting(key, value, type);
                    //更新内存
                    if (Settings.ExtSettings.First(p => p.Type == type).Settings.ContainsKey(key))
                    {
                        Settings.ExtSettings.First(p => p.Type == type).Settings[key] = value;
                    }
                    else
                    {
                        Settings.ExtSettings.First(p => p.Type == type).Settings.Add(key, value);
                    }
                    return "设置完成";
                }
                return "设置失败";
            }
            catch (Exception e)
            {
                LogHelper.Info("Level-C的Ext Xml设置错误：{0}", e.Message);
                return e.Message;
            }
        }

        /// <summary>
        /// 定时
        /// </summary>
        /// <returns></returns>
        public string SchemeAction(string sender)
        {
            var list = Settings.ExtSettings.First(p => p.Type == ExtType.Scheme).Settings;
            var sb = new StringBuilder();
            const string format = "$Ext[{0}]:{1}=>{2}$(r)";
            var orderedlist = list.OrderBy(p => p.Key);
            foreach (var line in orderedlist.Select(item => string.Format(format, ExtType.Scheme, item.Key, item.Value)))
            {
                sb.Append(line);
            }
            sb.Append("$(r)" + sender + "，你要好自为之：$(r)");
            sb.Append("添加：" + string.Format(format, ExtType.Scheme, "新的键", "新的值"));
            sb.Append("修改：" + string.Format(format, ExtType.Scheme, "旧的键", "新的值"));
            sb.Append("删除：不对外开放");
            return sb.ToString();
        }

        /// <summary>
        /// 测试
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public string TestAction(string message)
        {
            message = message.Replace("$test:", "");
            return message;
        }

        /// <summary>
        /// 聊天
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public string ChatAction(string message)
        {
            //喊了我 把前缀拿掉
            if (message.IndexOf(Settings.NekoName + " ", StringComparison.Ordinal) == 0)
            {
                message = message.Substring(Settings.NekoName.Length).Trim();
            }
            //回答与权重
            var candidates = new Dictionary<string, int>();
            var lexicons = RunTime.Database.Lexicon.ToList();
            #region 正则匹配
            var source = lexicons.Where(p => p.LxType == 2).ToList();
            var regexResults = (from lexicon in source
                                let regex = new Regex(lexicon.LxKey.Replace("$(name)", Settings.NekoName), RegexOptions.IgnoreCase)
                                where regex.IsMatch(message)
                                select lexicon).ToList();
            //正则匹配成功
            if (regexResults.Any())
            {
                var randomResult = regexResults.OrderBy(p => p.Time).First();
                randomResult.Time = DateTime.Now;
                RunTime.Database.SubmitChanges();
                var answer = randomResult.LxValue;
                var regex = new Regex(randomResult.LxKey.Replace("$(name)", Settings.NekoName), RegexOptions.IgnoreCase);
                var groups = regex.Match(message).Groups;
                //替换答案中的[1]、[2]等
                for (var i = 0; i < groups.Count; i++)
                {
                    answer = answer.Replace("[" + i + "]", groups[i].Value);
                }
                // return answer;
                candidates.Add(answer, 60);
            }
            #endregion
            #region Levenshtein匹配
            //Levenshtein匹配
            source = lexicons.Where(p => p.LxType == 0).ToList();
            var customResults = new List<Lexicon>();
            //最小相似度
            var rate = 0.7m;
            foreach (var lexicon in source)
            {
                var str1 = lexicon.LxKey.Replace("$(name)", Settings.NekoName).ToUpper();
                var str2 = message.ToUpper();
                var value = LevenshteinDistance.Instance
                    .LevenshteinDistancePercent(str1, str2);
                if (value >= rate)
                {
                    if (value != rate)
                    {
                        customResults.Clear();
                        rate = value;
                    }
                    customResults.Add(lexicon);
                }
            }
            //Levenshtein匹配成功
            if (customResults.Any())
            {
                var randomResult = customResults.OrderBy(p => p.Time).First();
                randomResult.Time = DateTime.Now;
                RunTime.Database.SubmitChanges();
                // return randomResult.LxValue;
                if (!candidates.ContainsKey(randomResult.LxValue))
                    candidates.Add(randomResult.LxValue, 60);
            }
            #endregion
            #region 模糊匹配
            source = lexicons.Where(p => p.LxType == 1).ToList();
            var blurResults = new List<Lexicon>();
            var blurMaxLength = 0;
            foreach (var lexicon in source)
            {
                var regex = BlurKey2Regex(lexicon.LxKey.Replace("$(name)", Settings.NekoName));
                if (regex.IsMatch(message))
                {
                    //这个匹配的问题除去通配符后的长度
                    //多个模糊匹配的时候，我让除去通配符后句子更长的更为优先
                    var length = lexicon.LxKey.Replace("*", "").Length;
                    if (length >= blurMaxLength)
                    {
                        //如果更长  清空其他 
                        if (length != blurMaxLength)
                        {
                            //出现更长的了
                            blurResults.Clear();
                            blurMaxLength = length;
                        }
                        blurResults.Add(lexicon);
                    }
                }
            }
            //模糊匹配成功
            if (blurResults.Any())
            {
                //相似度很低 但是模糊查找成功了
                var randomResult = blurResults.OrderBy(p => p.Time).First();
                randomResult.Time = DateTime.Now;
                RunTime.Database.SubmitChanges();
                var answer = randomResult.LxValue;
                var groups = BlurKey2Regex(randomResult.LxKey.Replace("$(name)", Settings.NekoName)).Match(message).Groups;
                //取得问题中的通配符数量
                var count = randomResult.LxKey.Split(new[] { "*" }, StringSplitOptions.None).Count();
                //替换答案中的[1]、[2]等
                for (var i = 0; i < count; i++)
                {
                    answer = answer.Replace("[" + i + "]", groups[i].Value);
                }
                // return answer;
                if (!candidates.ContainsKey(answer))
                    candidates.Add(answer, 40);
            }
            #endregion
            //使用foreach避免"集合已修改；可能无法执行枚举操作"的错误
            var keys = candidates.Keys.ToList();
            foreach (var key in keys)
            {
                //随机权重
                candidates[key] += new Random().Next(100);
            }
            var result = candidates.Any()
                ? candidates.First(p => p.Value == candidates.Values.Max()).Key
                : null;
            if (result == null && message.IndexOf(Settings.NekoName, StringComparison.Ordinal) == 0)
            {
                //部分人的特殊口语习惯
                result = ChatAction(message.Substring(Settings.NekoName.Length - 1));
            }
            return result;
        }
    }
}
