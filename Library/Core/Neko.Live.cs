using System;
using System.Linq;
using System.Threading;
using Library.Common;
using Library.Entity;

namespace Library.Core
{
    public partial class Neko
    {
        private static readonly Poll2Params P2P = new Poll2Params
        {
            key = "0",
            clientid = RunTime.ClientId,
            psessionid = RunTime.Psessionid,
            ptwebqq = RunTime.Ptwebqq
        };
        /// <summary>
        /// locker
        /// </summary>
        public static readonly object LockHelper = new object();

        /// <summary>
        /// 心跳
        /// </summary>
        public void Poll2Request()
        {
            try
            {
                while (true)
                {
                    var result = RunTime.Net.Poll2(P2P);
                    if (result == null) continue;
                    lock (LockHelper)
                    {
                        RunTime.Poll2Results.Enqueue(result);
                    }
                }
            }
            catch (Exception e)
            {
                LogHelper.Info("心跳进程发生错误：{0}", e.Message);
            }
        }

        /// <summary>
        /// 处理心跳结果队列
        /// </summary>
        public void Poll2Response()
        {
            while (RunTime.Poll2Thread.IsAlive)
            {
                try
                {
                    lock (LockHelper)
                    {
                        if (!RunTime.Poll2Results.Any()) continue;
                        var result = RunTime.Poll2Results.Dequeue();
                        switch (result.retcode)
                        {
                            //无最新消息
                            case "102":
                                break;
                            case "103":
                            case "121":
                                //连接不成功，需要重新登录
                                LogHelper.Info("连接失败，需要重新登录：{0}", result.retcode);
                                RunTime.SchemeThread.Abort();
                                RunTime.ReplyThread.Abort();
                                RunTime.SchemeThread.Abort();
                                break;
                            //有信息
                            case "0":
                                HandlePoll2Result(result);
                                break;
                            //其他情况
                            //default:
                            //    LogHelper.Info("未知状态码：{0}", result.retcode);
                            //    break;
                        }
                    }
                }
                catch (Exception e)
                {
                    LogHelper.Info("行为进程发生错误：{0}", e.Message);
                }
            }
        }

        /// <summary>
        /// 定时事件
        /// </summary>
        public void SchemeResponse()
        {
            try
            {
                while (true)
                {
                    var schemes = Settings.ExtSettings.First(p => p.Type == ExtType.Scheme).Settings;
                    foreach (var scheme in from scheme in schemes
                                           let time = Convert.ToDateTime(scheme.Key)
                                           where time > DateTime.Now &&
                                           time.AddMinutes(-1) < DateTime.Now
                                           select scheme)
                    {
                        PushAction(scheme.Value, Settings.NekoName);
                        Thread.Sleep(500);
                    }
                    Thread.Sleep(60000);
                }
            }
            catch (Exception e)
            {
                LogHelper.Info("定时事件进程发生错误：{0}", e.Message);
                RunTime.SchemeThread.Abort();
            }
        }
    }
}
