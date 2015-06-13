using System;
using System.IO;
using System.Text;
using System.Web;
using Library.Entity;

namespace Library.Common
{
    public static class LogHelper
    {
        public static void Info(string info)
        {
            var name = DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            if (!Directory.Exists(RunTime.LogRootPath))
                Directory.CreateDirectory(RunTime.LogRootPath);
            var sw = new StreamWriter(RunTime.LogRootPath + "/" + name, true, Encoding.UTF8);
            sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "  " + info);
            sw.Close();
        }

        public static void Info(string format, params object[] args)
        {
            var info = string.Format(format, args);
            Info(info);
        }

        ///// <summary>
        ///// WebForm和WinForm通用的取当前根目录的方法 
        ///// </summary>
        //private static string BasePath
        //{
        //    get
        //    {
        //        System.Diagnostics.Process p = System.Diagnostics.Process.GetCurrentProcess();
        //        string processName = p.ProcessName.ToLower();
        //        if (processName == "aspnet_wp" || processName == "w3wp" || processName == "webdev.webserver" || processName == "iisexpress")
        //        {
        //            return HttpContext.Current != null ?
        //                HttpContext.Current.Server.MapPath("~/").TrimEnd(new[] { '\\' })
        //                : AppDomain.CurrentDomain.BaseDirectory.TrimEnd(new[] { '\\' });
        //            //当控件在定时器的触发程序中使用时就为空
        //        }
        //        return System.Windows.Forms.Application.StartupPath;

        //    }
        //}
    }
}
