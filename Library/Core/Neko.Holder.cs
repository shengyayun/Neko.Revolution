using System;
using System.Collections.Generic;
using Library.Entity;

namespace Library.Core
{
    public partial class Neko
    {
        /// <summary>
        /// 处理回答中的占位符
        /// </summary>
        /// <param name="message"></param>
        /// <param name="sender"></param>
        /// <returns></returns>
        public IEnumerable<string> HandlePlaceHolder(string message, string sender)
        {
            var result = message;
            //普通占位符
            result = ReplaceCustomHolder(result, sender);
            //扩展占位符
            result = ReplaceExtHolder(result);
            return result.Split(new[] { "$(n)" }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// 普通占位符
        /// </summary>
        /// <param name="message"></param>
        /// <param name="sender"></param>
        /// <returns></returns>
        public string ReplaceCustomHolder(string message,string sender)
        {
            message = message.Replace("$(name)", Settings.NekoName);
            message = message.Replace("$(sender)", sender);
            message = message.Replace("$(r)", "\r");
            return message;
        }

        /// <summary>
        /// 扩展占位符
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public string ReplaceExtHolder(string message)
        {
            message = message.Replace("$(time)", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
            return message;
        }
    }
}
