using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Library.Common;
using Library.Entity;

namespace Service.Neko
{
    public class XmlHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            LogHelper.Info("In Handler");
            context.Response.ContentType = "text/plain";
            var name = Path.GetFileNameWithoutExtension(context.Request.FilePath);
            ExtType type;
            var result = "Hello World";
            if (Enum.TryParse(name, true, out type))
            {
                name = type.ToString();
                var list = Settings.ExtSettings.First(p => p.Type == type).Settings;
                var sb = new StringBuilder();
                const string format = "$Ext[{0}]:{1}=>{2}\r\n";
                var orderedlist = list.OrderBy(p => p.Key);
                foreach (var line in orderedlist.Select(item => string.Format(format, name, item.Key, item.Value)))
                {
                    sb.Append(line);
                }
                sb.Append("\r\n\r\n帮助\r\n\r\n");
                sb.Append("添加：" + string.Format(format, name, "新的键", "新的值"));
                sb.Append("修改：" + string.Format(format, name, "旧的键", "新的值"));
                sb.Append("删除：不对外开放");
                result = sb.ToString();
            }
            context.Response.Write(result);
        }

    }
}
