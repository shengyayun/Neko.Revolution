using System.Web;
using System.Web.Services;
using Library.Processor;

namespace Service.Neko
{
    /// <summary>
    /// main 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://langdaren.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class Main : WebService
    {

        [WebMethod]
        public byte[] PreLogin()
        {
            return Global.Core.PreLogin();
        }

        [WebMethod]
        public void Login(string yzm)
        {
            Global.Core.Login(yzm);
        }
    }
}
