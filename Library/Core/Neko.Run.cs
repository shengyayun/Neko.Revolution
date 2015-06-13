using System.Threading;
using Library.Common;
using Library.Entity;

namespace Library.Core
{
    public partial class Neko
    {
        /// <summary>
        /// 登录成功后开始运行
        /// </summary>
        public void Run()
        {
            if (!LoadFriends()) { LogHelper.Info("Neko启动失败"); return; }
            LogHelper.Info("好友信息载入完成");
            if (!LoadGroups()) { LogHelper.Info("Neko启动失败"); return; }
            LogHelper.Info("群组信息载入完成");
            //开始心跳
            RunTime.Poll2Thread = new Thread(Poll2Request) { IsBackground = true };
            RunTime.Poll2Thread.Start();
            LogHelper.Info("心跳线程启动完成");
            //开始回复
            RunTime.ReplyThread = new Thread(Poll2Response) { IsBackground = true };
            RunTime.ReplyThread.Start();
            LogHelper.Info("行为线程启动完成");
            //开始定时
            RunTime.SchemeThread = new Thread(SchemeResponse) { IsBackground = true };
            RunTime.SchemeThread.Start();
            LogHelper.Info("定时线程启动完成");
            LogHelper.Info("Neko启动完成");
        }
    }
}
