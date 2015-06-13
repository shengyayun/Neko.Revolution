using Library.Common;
using Library.Core;
using Library.Entity;
using Library.Manager;

namespace Library.Processor
{
    public class Core
    {
        public Core(string settingPath, string logPath)
        {
            //配置文件
            RunTime.SettingXmlPath = settingPath;
            //日志文件夹
            RunTime.LogRootPath = logPath;
            if (RunTime.Database.DatabaseExists()) return;
            LogHelper.Info("数据库不存在");
            //RunTime.Database.CreateDatabase();
        }

        /// <summary>
        /// 预登录
        /// </summary>
        /// <returns></returns>
        public byte[] PreLogin()
        {
            var mgr = new NetMgr();
            LogHelper.Info("开始预登录");
            var flag = mgr.CheckLogin();
            if (flag)
            {
                LogHelper.Info("不需要验证码");
                return null;
            }
            LogHelper.Info("需要验证码");
            var stream = mgr.FetchYzm();
            return stream;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="yzm">不需要输入验证码时为null或""</param>
        public string Login(string yzm)
        {
            if (!string.IsNullOrEmpty(yzm))
                RunTime.Yzm = yzm.ToUpper();
            var mgr = new NetMgr();
            string result;
            LogHelper.Info("进行登录");
            var flag = mgr.Login(out result);
            LogHelper.Info(result);
            if (flag)
            {
                var neko = new Neko();
                neko.Run();
            }
            return result;
        }
    }
}
